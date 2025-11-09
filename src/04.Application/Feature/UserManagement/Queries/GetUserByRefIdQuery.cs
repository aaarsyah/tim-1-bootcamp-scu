using AutoMapper;
using MediatR;
using MyApp.Base.Exceptions;
using MyApp.Infrastructure.Data.Repositories;
using MyApp.Shared.DTOs;

namespace MyApp.Application.Feature.UserManagement.Queries;

public class GetUserByRefIdQuery : IRequest<ApiResponse<UserDto>>
{
    public Guid RefId { get; set; }
}
public class GetUserByRefIdQueryHandler : IRequestHandler<GetUserByRefIdQuery, ApiResponse<UserDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public GetUserByRefIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<ApiResponse<UserDto>> Handle(GetUserByRefIdQuery request, CancellationToken cancellationToken)
    {
        // TODO: Tambahkan caching
        var user = await _unitOfWork.UserManager.GetUserByRefIdAsync(request.RefId);
        if (user == null)
        {
            throw new NotFoundException("Schedule RefId", request.RefId);
        }
        var userDto = _mapper.Map<UserDto>(user);

        return ApiResponse<UserDto>.SuccessResult(userDto);
    }
}
