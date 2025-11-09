using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using MyApp.Base.Exceptions;
using MyApp.Infrastructure.Data.Repositories;
using MyApp.Shared.DTOs;

namespace MyApp.Application.Feature.MyClasses.Commands;

public class DeleteMyClassCommand : IRequest<ApiResponse<object>>
{
    public Guid RefId { get; set; }
}
public class DeleteMyClassCommandHandler : IRequestHandler<DeleteMyClassCommand, ApiResponse<object>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ILogger<DeleteMyClassCommandHandler> _logger;
    public DeleteMyClassCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<DeleteMyClassCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _logger = logger;
    }
    public async Task<ApiResponse<object>> Handle(DeleteMyClassCommand request, CancellationToken cancellationToken)
    {
        var myClass = await _unitOfWork.MyClasses
            .GetByRefIdAsync(request.RefId);
        if (myClass == null)
        {
            throw new NotFoundException("MyClass RefId", request.RefId);
        }

        await _unitOfWork.MyClasses.DeleteAsync(request.RefId);
        await _unitOfWork.SaveChangesAsync();

        _logger.LogInformation("MyClass deleted: {MyClassRefId}", request.RefId);

        return ApiResponse<object>.SuccessResult();
    }
}
