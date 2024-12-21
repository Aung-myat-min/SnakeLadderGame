using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeLadderGame.Domain.Models
{
    public class GameStartReqModel
    {
        public int NumberOfPlayers { get; set; }
        public int BoardId { get; set; }
    }
}
