using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyApp.WebAPI.Data;
using MyApp.WebAPI.Models.Entities;
using AutoMapper;
using MyApp.WebAPI.Models.Dtos;


namespace MyApp.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceDetailController : ControllerBase
    {
        private readonly AppleMusicDbContext _context;
        private readonly IMapper _mapper;

        public InvoiceDetailController(AppleMusicDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/InvoiceDetails
        [HttpGet]
        public async Task<ActionResult<IEnumerable<InvoiceDetailsResponseDto>>> GetInvoiceDetails()
        {
            var invoiceDetail = await _context.InvoiceDetails
                .Include(d => d.Schedule)
                .Include(d => d.Invoice)
                .ToListAsync();

            var invoiceDetailDtos = _mapper.Map<List<InvoiceDetailsResponseDto>>(invoiceDetails);
            return Ok(invoiceDetailsDtos);
        }

        // GET: api/InvoiceDetails/5
        [HttpGet("{id}")]
        public async Task<ActionResult<InvoiceDetailsResponseDto>> GetInvoiceDetailsById(int id)
        {
            var invoiceDetails = await _context.InvoiceDetails
                .Include(d => d.Schedule)
                .Include(d => d.Invoice)
                .FirstOrDefaultAsync(d => d.Id == id);

            if (invoiceDetail == null)
            {
                return NotFound();
            }

            var invoiceDetailsDto = _mapper.Map<InvoiceDetailsResponseDto>(invoiceDetails);
            return Ok(invoiceDetailsDto);
        }

        // Optional: Add POST, PUT, DELETE methods if needed
    }
}
