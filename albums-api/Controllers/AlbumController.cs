using albums_api.Models;
using Microsoft.AspNetCore.Mvc;

namespace albums_api.Controllers
{
    [Route("albums")]
    [ApiController]
    public class AlbumController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            var albums = Album.GetAll();
            return Ok(albums);
        }
    }
}
