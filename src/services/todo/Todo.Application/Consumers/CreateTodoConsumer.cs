namespace Todo.Application.Consumers
{
    public class CreateTodoConsumer : IConsumer<ICreateTodo>
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public CreateTodoConsumer(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task Consume(ConsumeContext<ICreateTodo> context)
        {
            using var activity = ConsumerActivitySource.Source.StartActivity($"{nameof(CreateTodoConsumer)}");
            activity!.SetTag("CorrelationId", context.Message.CorrelationId);

            var command = context.Message;
            var model = _mapper.Map<CreateTodoCommand>(command);
            var response = await _mediator.Send(model);
        }
    }

    public class CreateTodoConsumerDefinition : ConsumerDefinition<CreateTodoConsumer>
    {
        public CreateTodoConsumerDefinition()
        {
            ConcurrentMessageLimit = SetConfigureConsumer.ConcurrentMessageLimit();
        }

        protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator, IConsumerConfigurator<CreateTodoConsumer> consumerConfigurator)
        {
            endpointConfigurator.SetConfigure();
        }
    }
}