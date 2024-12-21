using SnakeLadderGame.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeLadderGame.Domain.Models
{
    public class GameRoomResponseModel
    {
        public GameRoomPlayerModel? gameRoom { get; set; }
    }

    public class GameRoomPlayerModel
    {
        public TblGameRoom? gameRoom { get; set; }
        public List<TblPlayer>? players { get; set; }
    }
}
