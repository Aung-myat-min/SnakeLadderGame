using Microsoft.EntityFrameworkCore;
using SnakeLadderGame.Database.Models;
using SnakeLadderGame.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeLadderGame.Domain.Features
{
    public class GameRoomService
    {
        private readonly AppDBContext _db;
        private readonly BoardService _boardService;

        public GameRoomService(AppDBContext db)
        {
            _db = db;
            _boardService = new BoardService(db);
        }

        public async Task<Result<GameRoomResponseModel>> CreateGR(int numberOfPlayer, int boardId)
        {
            var response = new Result<GameRoomResponseModel>();

            if(numberOfPlayer < 2 || numberOfPlayer > 5)
            {
                response = Result<GameRoomResponseModel>.ValidationErr("Number of player must be between 2 and 5!");
                goto Result;
            }
            var board = await _boardService.GetBoard(boardId);
            if (!board.IsSuccess)
            {
                response = Result<GameRoomResponseModel>.NotFound("Board not found!");
                goto Result;
            }

            var lastId = await _db.TblGameRooms.AsNoTracking().Select(r => r.GameId).LastOrDefaultAsync();
            var roomCode = "R" + (lastId + 1).ToString("D4");

            var newRoom = new TblGameRoom
            {
                RoomCode = roomCode,
                BoardId = boardId,
                LastTurn = 1,
                PlayerNumbers = numberOfPlayer
            };
            _db.Add(newRoom);
            int result = await _db.SaveChangesAsync();
            if (result == 0)
            {
                response = Result<GameRoomResponseModel>.Error("Game Room is not created!");
                goto Result;
            }

            var players = new List<TblPlayer>();
            for(int i = 1; i <= numberOfPlayer; i++)
            {
                var player = new TblPlayer
                {
                    Color = GlobalData.playerColors[i -1],
                    Position = 1,
                    Turn = i,
                    RoomCode = roomCode
                };
                players.Add(player);
            }
            await _db.TblPlayers.AddRangeAsync(players);
            int result2 = await _db.SaveChangesAsync();

            if (result2 == 0)
            {
                response = Result<GameRoomResponseModel>.Error("Players are not created!");
                goto Result;
            }

            var model = new GameRoomPlayerModel
            {
                gameRoom = newRoom,
                players = players
            };
            response = Result<GameRoomResponseModel>.Success( "Game Room Created!\nHere is the Game Room Code and Player Informations!", new GameRoomResponseModel { gameRoom = model });

        Result:
            return response;
        }
    }
}
