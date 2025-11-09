using AutoMapper;
using MediatR;
using MyApp.Infrastructure.Data.Repositories;
using MyApp.Shared.DTOs;

namespace MyApp.Application.Feature.UserManagement.Queries;

public class GetAllRolesQuery : IRequest<ApiResponse<List<RoleDto>>>
{
}
public class GetAllRolesQueryHandler : IRequestHandler<GetAllRolesQuery, ApiResponse<List<RoleDto>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public GetAllRolesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<ApiResponse<List<RoleDto>>> Handle(GetAllRolesQuery request, CancellationToken cancellationToken)
    {
        // TODO: Tambahkan caching
        var roles = await _unitOfWork.UserManager.GetAllRolesAsync();

        var rolesDto = _mapper.Map<List<RoleDto>>(roles);

        return ApiResponse<List<RoleDto>>.SuccessResult(rolesDto);
    }
}
