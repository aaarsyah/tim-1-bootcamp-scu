using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using MyApp.Domain.Models;
using MyApp.Infrastructure.Data.Repositories;
using MyApp.Shared.DTOs;

namespace MyApp.Application.Feature.MyClasses.Commands;

public class CreateMyClassCommand : IRequest<ApiResponse<MyClassDto>>
{
    public CreateMyClassRequestDto createMyClassDto { get; set; } = null!;
}
public class CreateMyClassCommandHandler : IRequestHandler<CreateMyClassCommand, ApiResponse<MyClassDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ILogger<CreateMyClassCommandHandler> _logger;
    public CreateMyClassCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<CreateMyClassCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _logger = logger;
    }
    public async Task<ApiResponse<MyClassDto>> Handle(CreateMyClassCommand request, CancellationToken cancellationToken)
    {
        var myClass = _mapper.Map<MyClass>(request.createMyClassDto);

        await _unitOfWork.MyClasses.CreateAsync(myClass);
        await _unitOfWork.SaveChangesAsync();

        _logger.LogInformation("MyClass created with RefID: {MyClassRefId}", myClass.RefId);

        var myClassDto = _mapper.Map<MyClassDto>(myClass);

        return ApiResponse<MyClassDto>.SuccessResult(myClassDto);
    }
}
