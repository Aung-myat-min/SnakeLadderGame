using SnakeLadderGame.Domain.Models;

namespace SnakeLadderGame.Domain.Interfaces
{
    public interface IGameRoomService
    {
        Task<Result<GameRoomResponseModel>> CreateGR(GameStartReqModel request);
        Task<Result<string>> GetGameInformation(string roomCode);
        Task<Result<PlayResponseModel>> Play(string roomCode);
    }
}