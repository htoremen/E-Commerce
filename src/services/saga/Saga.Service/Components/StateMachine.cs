using Core.Events.TodoItems;
using Core.Events.TodoLists;
using Core.MessageBrokers;
using Core.MessageBrokers.Enums;
using MassTransit;
using Saga.Application.Parameters;
using Saga.Application.TodoLists;
using Saga.Persistence;

namespace Saga.Service;

public class StateMachine : MassTransitStateMachine<StateInstance>
{
    #region State

    public State CreateTodo { get; set; }
    public State AddTodoItem { get; set; }
    public State DeleteTodo { get; set; }
    public State CompleteTodo { get; set; }

    #endregion

    #region Event

    public Event<ICreateTodo> CreateTodoEvent { get; private set; }
    public Event<IAddTodoItem> AddTodoItemEvent { get; private set; }
    public Event<IDeleteTodo> DeleteTodoEvent { get; private set; }
    public Event<ICompleteTodo> CompleteTodoEvent { get; private set; }

    #endregion

    [Obsolete]
    public StateMachine()
    {
        QueueConfigurationExtensions.AddQueueConfiguration(null, out IQueueConfiguration queueConfiguration);
        InstanceState(instance => instance.CurrentState);

        SetCorrelationId();
        Initially(CreateTodoActivity(queueConfiguration));
        During(CreateTodo, AddTodoItemActivity(queueConfiguration));
        During(AddTodoItem, DeleteTodoActivity(queueConfiguration), CompleteTodoActivity(queueConfiguration));
    }

    [Obsolete]
    private EventActivities<StateInstance> CompleteTodoActivity(IQueueConfiguration queueConfiguration)
    {
        return When(CompleteTodoEvent)
                 .TransitionTo(CompleteTodo)
                 .Send(new Uri($"queue:{queueConfiguration.Names[QueueName.CompleteTodo]}"), context => new CompleteTodoCommand(context.Data.CorrelationId)
                 {
                     CurrentState = context.Instance.CurrentState
                 }).Finalize();
    }

    [Obsolete]
    private EventActivities<StateInstance> DeleteTodoActivity(IQueueConfiguration queueConfiguration)
    {
        return When(DeleteTodoEvent)
                 .TransitionTo(DeleteTodo)
                 .Send(new Uri($"queue:{queueConfiguration.Names[QueueName.DeleteTodo]}"), context => new DeleteTodoCommand(context.Data.CorrelationId)
                 {
                     CurrentState = context.Instance.CurrentState
                 }).Finalize();
    }

    [Obsolete]
    private EventActivities<StateInstance> AddTodoItemActivity(IQueueConfiguration queueConfiguration)
    {
        return When(AddTodoItemEvent)
                .Then(context =>
                {
                    context.Instance.SessionId = context.Data.SessionId;
                    context.Instance.CreatedOn = DateTime.Now;
                })
                .TransitionTo(AddTodoItem)
                .Send(new Uri($"queue:{queueConfiguration.Names[QueueName.AddTodoItem]}"), context => new AddTodoItemCommand(context.Instance.CorrelationId)
                {
                    SessionId = context.Data.SessionId,
                });
    }

    [Obsolete]
    private EventActivities<StateInstance> CreateTodoActivity(IQueueConfiguration queueConfiguration)
    {
        return When(CreateTodoEvent)
                .Then(context =>
                {
                    context.Instance.SessionId = context.Data.SessionId;
                    context.Instance.CreatedOn = DateTime.Now;
                })
                .TransitionTo(CreateTodo)
                .Send(new Uri($"queue:{queueConfiguration.Names[QueueName.CreateTodo]}"), context => new CreateTodoCommand(context.Instance.CorrelationId)
                {
                    SessionId = context.Data.SessionId,
                    CurrentState = context.Instance.CurrentState,
                    CreatedOn = DateTime.Now
                });
    }

    private void SetCorrelationId()
    {
        Event(() => CreateTodoEvent, instance => instance.CorrelateBy<Guid>(state => state.SessionId, context => context.Message.SessionId).SelectId(s => s.Message.SessionId));
        Event(() => AddTodoItemEvent, instance => instance.CorrelateById(selector => selector.Message.CorrelationId));
        Event(() => DeleteTodoEvent, instance => instance.CorrelateById(selector => selector.Message.CorrelationId));
        Event(() => CompleteTodoEvent, instance => instance.CorrelateById(selector => selector.Message.CorrelationId));
    }
}
