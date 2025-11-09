using AutoMapper;
using MediatR;
using MyApp.Base.Exceptions;
using MyApp.Infrastructure.Data.Repositories;
using MyApp.Shared.DTOs;

namespace MyApp.Application.Feature.MyClasses.Queries;

public class GetAllMyClassesByUserRefIdQuery : IRequest<ApiResponse<List<MyClassDto>>>
{
    public Guid UserRefId { get; set; }
}
public class GetAllMyClassesByUserRefIdQueryHandler : IRequestHandler<GetAllMyClassesByUserRefIdQuery, ApiResponse<List<MyClassDto>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public GetAllMyClassesByUserRefIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<ApiResponse<List<MyClassDto>>> Handle(GetAllMyClassesByUserRefIdQuery request, CancellationToken cancellationToken)
    {
        var myClasses = await _unitOfWork.MyClasses.GetAllByUserRefIdAsync(request.UserRefId);
        if (myClasses == null)
        {
            throw new NotFoundException("User RefId", request.UserRefId);
        }

        var myClassesDto = _mapper.Map<List<MyClassDto>>(myClasses);

        return ApiResponse<List<MyClassDto>>.SuccessResult(myClassesDto);
    }
}
