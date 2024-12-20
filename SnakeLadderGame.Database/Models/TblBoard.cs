using System;
using System.Collections.Generic;

namespace SnakeLadderGame.Database.Models;

public partial class TblBoard
{
    public int BoardId { get; set; }

    public string BoardName { get; set; } = null!;

    public int BoardDimension { get; set; }

    public virtual ICollection<TblBoardRoute> TblBoardRoutes { get; set; } = new List<TblBoardRoute>();

    public virtual ICollection<TblGameRoom> TblGameRooms { get; set; } = new List<TblGameRoom>();
}
