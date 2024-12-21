using SnakeLadderGame.Database.Models;
using SnakeLadderGame.Domain.Models;

namespace SnakeLadderGame.Domain.Interfaces
{
    public interface IBoardService
    {
        Task<Result<BoardResponseModel>> CreateBoard(TblBoard newBoard);
        Task<Result<BoardResponseModel>> GetBoard(int boardId);
        Task<Result<BoardResponseModel>> GetBoards();
    }
}