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

        // GET /cart/{cartId}
        [HttpGet("{cartId}")]
        public IActionResult GetCart(string cartId)
        {
            var cart = _cartService.GetCart(cartId);
            return Ok(cart);
        }

        // POST /cart/{cartId}/items  body: { "albumId": 1 }
        [HttpPost("{cartId}/items")]
        public IActionResult AddItem(string cartId, [FromBody] AddItemRequest request)
        {
            var cart = _cartService.AddItem(cartId, request.AlbumId);
            return Ok(cart);
        }

        // DELETE /cart/{cartId}/items/{albumId}
        [HttpDelete("{cartId}/items/{albumId}")]
        public IActionResult RemoveItem(string cartId, int albumId)
        {
            var cart = _cartService.RemoveItem(cartId, albumId);
            return Ok(cart);
        }
    }

    public record AddItemRequest(int AlbumId);
}
