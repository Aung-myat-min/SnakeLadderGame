using Azure;
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

            if (numberOfPlayer < 2 || numberOfPlayer > 5)
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
            for (int i = 1; i <= numberOfPlayer; i++)
            {
                var player = new TblPlayer
                {
                    Color = GlobalData.playerColors[i - 1],
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
            response = Result<GameRoomResponseModel>.Success("Game Room Created!\nHere is the Game Room Code and Player Informations!", new GameRoomResponseModel { gameRoom = model });

        Result:
            return response;
        }

        public async Task<Result<PlayResponseModel>> Play(string roomCode)
        {
            var respsonse = new Result<PlayResponseModel>();
            var actions = new List<string>();

            #region Check Game Room Avaliable or Ended
            var gameRoom = await _db.TblGameRooms.Where(r => r.RoomCode == roomCode).FirstOrDefaultAsync();
            if (gameRoom is null)
            {
                respsonse = Result<PlayResponseModel>.NotFound("Game Room not found!");
                goto Result;
            }
            if (gameRoom.Winner != null)
            {
                respsonse = Result<PlayResponseModel>.NotFound("Game Room is ended!");
                goto Result;
            }
            #endregion

            #region Roll Dice
            var dice = new Random();
            var diceNumber = dice.Next(1, 7);
            #endregion

            #region Get Last Turn Player
            var lastTurn = gameRoom.LastTurn;
            var player = await _db.TblPlayers.Where(p => p.RoomCode == roomCode && p.Turn == lastTurn).FirstOrDefaultAsync();
            if (player is null)
            {
                respsonse = Result<PlayResponseModel>.NotFound("Player not found!");
                goto Result;
            }
            #endregion

            #region Calcualte New Position
            var board = _db.TblBoards.AsNoTracking().Where(b => b.BoardId == gameRoom.BoardId).FirstOrDefault();
            var winningPosition = board!.BoardDimension * board.BoardDimension;

            int? newPosition = player.Position + diceNumber;
            actions.Add($"{player.Color} + has reached to {newPosition}");

            if(newPosition > winningPosition)
            {
                newPosition = newPosition - (newPosition - winningPosition);
                actions.Add($"Since Player have reached beyond final point, Player has to move back to {newPosition}");
            }

            if (newPosition < winningPosition)
            {
                var route = await _db.TblBoardRoutes.Where(r => r.BoardId == gameRoom.BoardId && r.Place == player.Position).FirstOrDefaultAsync();
                if (route is null)
                {
                    respsonse = Result<PlayResponseModel>.NotFound("Route not found!");
                    goto Result;
                }

                switch (route.ActionType)
                {
                    case "Ladder":
                        newPosition = route.Destination;
                        actions.Add($"Lucky! Player has found a ladder and moved to {newPosition}");
                        break;
                    case "Snake":
                        newPosition = route.Destination;
                        actions.Add($"Oops! Player has found a snake and moved to {newPosition}");
                        break;
                    default:
                        break;
                }
            }

            if (newPosition == winningPosition)
            {
                gameRoom.Winner = player.PlayerId;
            }
        #endregion


        Result:
            return respsonse;
        }

        public async Task<Result<string>> GetGameInformation(string roomCode)
        {
            var response = new Result<string>();

            var game = await _db.TblGameRooms.AsNoTracking().Where(g => g.RoomCode == roomCode).FirstOrDefaultAsync();
            if(game is null)
            {
                response = Result<string>.NotFound("Game Room not found!");
                goto Result;
            }

            var players = await _db.TblPlayers.AsNoTracking().Where(p => p.RoomCode == roomCode).OrderByDescending(p => p.Position).ToListAsync();

            string gameInformation = $"In Room: {roomCode}\n";
            foreach (var player in players)
            {
                gameInformation += $"Player {player.Color} is at {player.Position}\n";
            }

            Result:
            return response;
        }
    }
}
