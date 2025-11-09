using AutoMapper;
using MediatR;
using MyApp.Infrastructure.Data.Repositories;
using MyApp.Shared.DTOs;

namespace MyApp.Application.Feature.MyClasses.Queries;

public class GetAllMyClassesQuery : IRequest<ApiResponse<List<MyClassDto>>>
{
}
public class GetAllMyClassesQueryHandler : IRequestHandler<GetAllMyClassesQuery, ApiResponse<List<MyClassDto>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public GetAllMyClassesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<ApiResponse<List<MyClassDto>>> Handle(GetAllMyClassesQuery request, CancellationToken cancellationToken)
    {
        // TODO: Tambahkan caching
        var myClasses = await _unitOfWork.MyClasses.GetAllAsync();

        var myClassesDto = _mapper.Map<List<MyClassDto>>(myClasses);

        return ApiResponse<List<MyClassDto>>.SuccessResult(myClassesDto);
    }
}
