using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SparkeApp.DTOs.Order;
using SparkeApp.Services.Implementations;
using SparkeApp.Services.Interfaces;

namespace SparkeApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController(IOrderService orderService) : ControllerBase
    {

        [HttpGet("all")]
        public async Task<IActionResult> GetOrdersAsync()
        {
            try
            {
                var result = await orderService.GetAllOrdersAsync();
                return Ok(result);

            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "An error occurred while fetching orders" });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderByIdAsync(int id)
        {

            try
            {
                var result = await orderService.GetOrderByIdAsync(id);
                return Ok(result);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "An error occurred while fetching the order" });


            }
        }

        [HttpPut("status")]
        public async Task<IActionResult> UpdateOrderStatusAsync([FromBody] UpdateOrderStatusDto updateDto)
        {
            try
            {
                var result = await orderService.UpdateOrderStatusAsync(updateDto);
                return Ok(result);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { error = ex.Message });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "An error occurred while updating order status" });
            }
        }
    }
}
