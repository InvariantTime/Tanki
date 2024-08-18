using Microsoft.AspNetCore.Mvc;
using Tanki.Domain.Models;
using Tanki.Requests;
using Tanki.Services;

namespace Tanki.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoomController : ControllerBase
    {
        private readonly IRoomService _service;

        public RoomController(IRoomService service)
        {
            _service = service;
        }

        [HttpPost()]
        public async Task<IActionResult> CreateRoom(RoomCreateRequest request)
        {
            var room = new Room
            {
                Name = request.Name,
            };

            await _service.CreateRoom(room);
            return Ok();
        }

        [HttpGet]
        public IActionResult GetRooms()
        {
            var rooms = _service.GetAll();
            return Ok(rooms);
        }
    }
}