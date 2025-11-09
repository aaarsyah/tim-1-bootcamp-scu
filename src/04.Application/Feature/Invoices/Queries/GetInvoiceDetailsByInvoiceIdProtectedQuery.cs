using AutoMapper;
using MediatR;
using MyApp.Base.Exceptions;
using MyApp.Infrastructure.Data.Repositories;
using MyApp.Shared.DTOs;

namespace MyApp.Application.Feature.Invoices.Queries;

public class GetInvoiceDetailsByInvoiceIdProtectedQuery : IRequest<ApiResponse<IEnumerable<InvoiceDetailDto>>>
{
    public Guid RefId { get; set; }
    public bool IsPrivileged { get; set; }
    public Guid UserRefId { get; set; }
}
public class GetInvoiceDetailsByInvoiceIdProtectedQueryHandler : IRequestHandler<GetInvoiceDetailsByInvoiceIdProtectedQuery, ApiResponse<IEnumerable<InvoiceDetailDto>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public GetInvoiceDetailsByInvoiceIdProtectedQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<ApiResponse<IEnumerable<InvoiceDetailDto>>> Handle(GetInvoiceDetailsByInvoiceIdProtectedQuery request, CancellationToken cancellationToken)
    {
        // TODO: Tambahkan caching
        var invoice = await _unitOfWork.InvoiceManager.GetByRefIdAsync(request.RefId);
        if (invoice == null)
        {
            throw new NotFoundException("Invoice RefId", request.RefId);
        }
        if (!request.IsPrivileged && (invoice.User?.RefId != request.UserRefId))
        {
            // Demi keamanan: tetap return error yang sama untuk mencegah bocornya invoice pengguna
            throw new NotFoundException("Invoice RefId", request.RefId);
        }
        var invoiceDetails = await _unitOfWork.InvoiceManager.GetDetailsByRefIdAsync(request.RefId);
        var invoiceDetailsDto = _mapper.Map<IEnumerable<InvoiceDetailDto>>(invoiceDetails);
        return ApiResponse<IEnumerable<InvoiceDetailDto>>.SuccessResult(invoiceDetailsDto);
    }
}
