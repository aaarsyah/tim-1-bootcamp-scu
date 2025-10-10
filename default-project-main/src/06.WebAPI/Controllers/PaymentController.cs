using Microsoft.AspNetCore.Mvc;
using MyApp.WebAPI.Models.DTOs;
using MyApp.WebAPI.Services;
using MyApp.WebAPI.Models;


namespace MyApp.WebAPI.Controllers
{
 
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;
        private readonly ILogger<PaymentController> _logger;

        /// <summary>
        /// Constructor
        /// </summary>
        public PaymentController(IPaymentService paymentService, ILogger<PaymentController> logger)
        {
            _paymentService = paymentService;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<PaymentDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<PaymentDto>>> GetPayment()
        {
            try
            {
                var payment = await _paymentService.GetAllPaymentAsync();
                return Ok(payment);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving payment");
                return StatusCode(500, "Internal server error");
            }
        }

      
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(PaymentDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PaymentDto>> GetPayment(int id)
        {
            try
            {
                var payment = await _paymentService.GetPaymentByIdAsync(id);
                if (payment == null)
                {
                    return NotFound($"Payment with ID {id} not found");
                }
                return Ok(payment);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving payment {PaymentId}", id);
                return StatusCode(500, "Internal server error");
            }
        }

      
        [HttpPost]
        [ProducesResponseType(typeof(PaymentDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<PaymentDto>> CreatePayment(CreatePaymentDto createPaymentDto)
        {
            try
            {
                var payment = await _paymentService.CreatePaymentAsync(createPaymentDto);
                return CreatedAtAction(nameof(GetPayment), new { id = payment.Id }, payment);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating payment");
                return StatusCode(500, "Internal server error");
            }
        }

      
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(PaymentDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PaymentDto>> UpdatePayment(int id, UpdatePaymentDto updatePaymentDto)
        {
            try
            {
                var payment = await _paymentService.UpdatePaymentAsync(id, updatePaymentDto);
                if (payment == null)
                {
                    return NotFound($"Payment with ID {id} not found");
                }
                return Ok(payment);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating payment {PaymentId}", id);
                return StatusCode(500, "Internal server error");
            }
        }


        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeletePayment(int id)
        {
            try
            {
                var result = await _paymentService.DeletePaymentAsync(id);
                if (!result)
                {
                    return NotFound($"Payment with ID {id} not found");
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting payment {PaymentId}", id);
                return StatusCode(500, "Internal server error");
            }
        }
    }
}