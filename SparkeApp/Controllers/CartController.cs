using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using SparkeApp.DTOs.Cart;
using SparkeApp.Services.Interfaces;

namespace SparkeApp.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CartController(ICartService cartService) : ControllerBase
{
    [HttpPost("Add")]
    // add to cart function 
    public async Task<IActionResult> AddToCartAsync([FromQuery] int userID, [FromBody] AddToCartRequestDto requestDto)
    {
        try
        {
            var result = await cartService.AddToCartAsync(userID, requestDto);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }

    }

    [HttpGet("{userId}")]
    public async Task<IActionResult> GetCartByUser(int userId)
    {
        try
        {
            var result = await cartService.GetCartByUser(userId);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }
    [HttpPut("quantity")]
    public async Task<IActionResult> UpdateQuantity([FromBody] UpdateQDto updateDto)
    {
    try
        {
            var result = await cartService.UpdateQuantityAsync(updateDto);
            return Ok(result);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }
    [HttpPost("checkout")]
    public async Task<IActionResult> CheckoutAsync([FromBody] CheckoutRequestDto request)
    {
        try
        {
            var result = await cartService.CheckoutAsync(request);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }

    }


}
