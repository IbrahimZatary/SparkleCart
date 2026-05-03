using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SparkeApp.DTOs.Order;
using SparkeApp.Services.Implementations;
using SparkeApp.Services.Interfaces;

namespace SparkeApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] 
    public class OrderController(IOrderService orderService) : ControllerBase
    {

        [HttpGet("all")]
        public async Task<IActionResult> GetOrdersAsync()
        {
            var result = await orderService.GetAllOrdersAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderByIdAsync(int id)
        {
            var result = await orderService.GetOrderByIdAsync(id);
            return Ok(result);

        }

        [HttpPut("status")]
        public async Task<IActionResult> UpdateOrderStatusAsync([FromBody] UpdateOrderStatusDto updateDto)
        {
            var result = await orderService.UpdateOrderStatusAsync(updateDto);
            return Ok(result);
        }
    }
}
