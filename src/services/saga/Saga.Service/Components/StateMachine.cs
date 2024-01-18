using Core.Events.Parameters;
using Core.MessageBrokers;
using Core.MessageBrokers.Enums;
using MassTransit;
using Saga.Application.Parameters;
using Saga.Persistence;

namespace Saga.Service;

public class StateMachine : MassTransitStateMachine<StateInstance>
{
    #region State

    public State Start { get; set; }
    public State AddParameter { get; set; }
    public State Completed { get; set; }

    #endregion

    #region Event

    public Event<IStart> StartEvent { get; private set; }
    public Event<IAddParameter> AddParameterEvent { get; private set; }
    public Event<ICompleted> CompletedEvent { get; private set; }

    #endregion

    [Obsolete]
    protected StateMachine()
    {
        QueueConfigurationExtensions.AddQueueConfiguration(null, out IQueueConfiguration queueConfiguration);
        InstanceState(instance => instance.CurrentState);

        SetCorrelationId();
        Initially(StartActivity(queueConfiguration));
        During(Start, AddParameterActivity(queueConfiguration));

        During(AddParameter, CompletedActivity(queueConfiguration));
    }
    [Obsolete]
    private EventActivities<StateInstance> CompletedActivity(IQueueConfiguration queueConfiguration)
    {
        return When(CompletedEvent)
                 .TransitionTo(Completed)
                 .Send(new Uri($"queue:{queueConfiguration.Names[QueueName.Completed]}"), context => new CompletedCommand(context.Data.CorrelationId)
                 {
                     CurrentState = context.Instance.CurrentState
                 }).Finalize();
    }

    [Obsolete]
    private EventActivities<StateInstance> AddParameterActivity(IQueueConfiguration queueConfiguration)
    {
        return When(AddParameterEvent)
                .Then(context =>
                {
                    context.Instance.SessionId = context.Data.SessionId;
                    context.Instance.CreatedOn = DateTime.Now;
                })
                .TransitionTo(AddParameter)
                .Send(new Uri($"queue:{queueConfiguration.Names[QueueName.AddParameter]}"), context => new AddParameterCommand(context.Instance.CorrelationId)
                {
                    SessionId = context.Data.SessionId,
                });
    }

    [Obsolete]
    private EventActivities<StateInstance> StartActivity(IQueueConfiguration queueConfiguration)
    {
        return When(StartEvent)
                .Then(context =>
                {
                    context.Instance.SessionId = context.Data.SessionId;
                    context.Instance.CreatedOn = DateTime.Now;
                })
                .TransitionTo(Start)
                .Send(new Uri($"queue:{queueConfiguration.Names[QueueName.Start]}"), context => new StartCommand(context.Instance.CorrelationId)
                {
                    SessionId = context.Data.SessionId,
                    CurrentState = context.Instance.CurrentState,
                    CreatedOn = DateTime.Now
                });
    }

    private void SetCorrelationId()
    {
        Event(() => StartEvent, instance => instance.CorrelateBy<Guid>(state => state.SessionId, context => context.Message.SessionId).SelectId(s => s.Message.SessionId));
        Event(() => AddParameterEvent, instance => instance.CorrelateById(selector => selector.Message.CorrelationId));
        Event(() => CompletedEvent, instance => instance.CorrelateById(selector => selector.Message.CorrelationId));
    }
}
