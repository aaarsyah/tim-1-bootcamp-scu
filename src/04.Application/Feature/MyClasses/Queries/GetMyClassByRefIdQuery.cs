using AutoMapper;
using MediatR;
using MyApp.Base.Exceptions;
using MyApp.Infrastructure.Data.Repositories;
using MyApp.Shared.DTOs;

namespace MyApp.Application.Feature.MyClasses.Queries;

public class GetMyClassByRefIdQuery : IRequest<ApiResponse<MyClassDto>>
{
    public Guid RefId { get; set; }
}
public class GetMyClassByRefIdQueryHandler : IRequestHandler<GetMyClassByRefIdQuery, ApiResponse<MyClassDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public GetMyClassByRefIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<ApiResponse<MyClassDto>> Handle(GetMyClassByRefIdQuery request, CancellationToken cancellationToken)
    {
        // TODO: Tambahkan caching
        var myClass = await _unitOfWork.MyClasses.GetByRefIdAsync(request.RefId);
        if (myClass == null)
        {
            throw new NotFoundException("Course RefId", request.RefId);
        }

        var myClassDto = _mapper.Map<MyClassDto>(myClass);

        return ApiResponse<MyClassDto>.SuccessResult(myClassDto);
    }
}
