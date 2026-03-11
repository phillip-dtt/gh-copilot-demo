using albums_api.Services;
using Microsoft.AspNetCore.Mvc;

namespace albums_api.Controllers
{
    [Route("cart")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly CartService _cartService;

        public CartController(CartService cartService)
        {
            _cartService = cartService;
        }

        [HttpGet("{cartId}")]
        public IActionResult GetCart(string cartId)
        {
            var cart = _cartService.GetCart(cartId);
            return Ok(cart);
        }

        [HttpPost("{cartId}/items")]
        public IActionResult AddItem(string cartId, [FromBody] AddItemRequest request)
        {
            if (request.AlbumId <= 0)
                return BadRequest("AlbumId must be a positive integer.");

            var cart = _cartService.AddItem(cartId, request.AlbumId);
            if (cart == null)
                return NotFound($"Album {request.AlbumId} not found.");

            return Ok(cart);
        }

        [HttpDelete("{cartId}/items/{albumId}")]
        public IActionResult RemoveItem(string cartId, int albumId)
        {
            var cart = _cartService.RemoveItem(cartId, albumId);
            return Ok(cart);
        }
    }

    public record AddItemRequest(int AlbumId);
}
