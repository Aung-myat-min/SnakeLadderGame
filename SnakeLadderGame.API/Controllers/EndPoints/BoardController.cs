using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SnakeLadderGame.Domain.Interfaces;

namespace SnakeLadderGame.API.Controllers.EndPoints
{
    [Route("api/[controller]")]
    [ApiController]
    public class BoardController : BaseController
    {
        private readonly IBoardService _boardService;

        public BoardController(IBoardService boardService)
        {
            _boardService = boardService;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetBoards()
        {
            try
            {
                var response = await _boardService.GetBoards();
                return Execute(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
