using SnakeLadderGame.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeLadderGame.Domain.Models
{
    public class BoardResponseModel
    {
        public TblBoard? Board { get; set; }
        public List<TblBoard>? LBoards { get; set; }
    }
}
