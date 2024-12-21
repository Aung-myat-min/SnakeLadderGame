using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SnakeLadderGame.Domain.Interfaces;
using SnakeLadderGame.Domain.Models;

namespace SnakeLadderGame.API.Controllers.EndPoints
{
    [Route("api/[controller]")]
    [ApiController]
    public class SnakeLadderGameController : BaseController
    {
        private readonly IGameRoomService _roomService;

        public SnakeLadderGameController(IGameRoomService roomService)
        {
            _roomService = roomService;
        }

        [HttpPost("")]
        public async Task<IActionResult> StartANewGame(GameStartReqModel players)
        {
            try
            {
                var response = await _roomService.CreateGR(players);
                return Execute(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{roomCode}")]
        public async Task<IActionResult> PlayGame(string roomCode)
        {
            try
            {
                var response = await _roomService.Play(roomCode);
                return Execute(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
