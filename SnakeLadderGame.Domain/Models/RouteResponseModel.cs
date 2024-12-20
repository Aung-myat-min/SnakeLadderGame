using SnakeLadderGame.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeLadderGame.Domain.Models
{
    public class RouteResponseModel
    {
        public TblBoardRoute? Route { get; set; }
        public List<TblBoardRoute>? LRoutes { get; set; }
    }
}
