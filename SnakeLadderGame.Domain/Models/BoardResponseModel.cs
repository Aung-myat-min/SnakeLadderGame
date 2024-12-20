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
        public BoardRouteResponseModel? Board { get; set; }
        public List<BoardRouteResponseModel>? LBoards { get; set; }
    }

    public class BoardRouteResponseModel
    {
        public TblBoard? board { get; set; }
        public List<TblBoardRoute>? routes { get; set; }
    }
}
