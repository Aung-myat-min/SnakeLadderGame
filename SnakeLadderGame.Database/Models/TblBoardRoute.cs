using System;
using System.Collections.Generic;

namespace SnakeLadderGame.Database.Models;

public partial class TblBoardRoute
{
    public int RouteId { get; set; }

    public int Place { get; set; }

    public string ActionType { get; set; } = null!;

    public int? Destination { get; set; }

    public int BoardId { get; set; }

    public virtual TblBoard Board { get; set; } = null!;
}
