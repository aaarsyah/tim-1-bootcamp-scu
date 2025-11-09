using AutoMapper;
using MediatR;
using MyApp.Infrastructure.Data.Repositories;
using MyApp.Shared.DTOs;

namespace MyApp.Application.Feature.UserManagement.Queries;

public class GetAllUsersQuery : IRequest<ApiResponse<List<UserDto>>>
{
}
public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, ApiResponse<List<UserDto>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public GetAllUsersQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<ApiResponse<List<UserDto>>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        // TODO: Tambahkan caching
        var users = await _unitOfWork.UserManager.GetAllUsersAsync();

        var SchedulesDto = _mapper.Map<List<UserDto>>(users);

        return ApiResponse<List<UserDto>>.SuccessResult(SchedulesDto);
    }
}
