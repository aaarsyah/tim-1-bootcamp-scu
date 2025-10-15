using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyApp.WebAPI.Data;
using MyApp.WebAPI.Models.Entities;
using AutoMapper;


namespace MyApp.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoicesController : ControllerBase
    {
        private readonly AppleMusicDbContext _context;

        private readonly IMapper _mapper;

        public InvoicesController(AppleMusicDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper; 
        }


        // GET: api/Invoices
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Invoice>>> GetInvoices()
        {
            return await _context.Invoices
                .Include(i => i.User)
                .Include(i => i.PaymentMethod)
                .Include(i => i.InvoiceDetails)
                    .ThenInclude(d => d.Schedule)
                .ToListAsync();
        }

        // GET: api/Invoices/5
        [HttpGet("{id}")]
        public async Task<ActionResult<InvoiceResponseDto>> GetInvoiceById(int id)
        {
            var invoice = await _context.Invoices
                .Include(i => i.User)
                .Include(i => i.PaymentMethod)
                .Include(i => i.InvoiceDetails)
                    .ThenInclude(d => d.Schedule)
                .FirstOrDefaultAsync(i => i.Id == id);

            if (invoice == null)
            {
                return NotFound();
            }

            var invoiceDto = _mapper.Map<InvoiceResponseDto>(invoice);
            return Ok(invoiceDto);
        }

    }
}
