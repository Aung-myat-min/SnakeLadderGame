using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeLadderGame.Domain.Models
{
    public class PlayResponseModel
    {
        public List<string> ActionsMade { get; set; } = null!;
        public string? Positions { get; set; }
        public string? NextTurn { get; set; }
    }
}
