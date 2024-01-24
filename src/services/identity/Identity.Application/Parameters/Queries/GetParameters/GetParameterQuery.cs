namespace Identity.Application.Parameters
{
    public class GetParameterQuery : IRequest<GenericResponse<List<GetParameterResponse>>>
    {
    }

    public class GetParameterQueryHandler : IRequestHandler<GetParameterQuery, GenericResponse<List<GetParameterResponse>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetParameterQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<GenericResponse<List<GetParameterResponse>>> Handle(GetParameterQuery request, CancellationToken cancellationToken)
        {
            var response = _unitOfWork.Parameter.Find(x => x.IsActive).Select(x => new GetParameterResponse { ParameterName = x.ParameterName, ParameterTypeId = x.ParameterTypeId }).ToList();
            return GenericResponse<List<GetParameterResponse>>.Success(response, 200);
        }
    }
}
