using System;
using System.Collections.Generic;

namespace SnakeLadderGame.Database.Models;

public partial class TblGameRoom
{
    public int GameId { get; set; }

    public string RoomCode { get; set; } = null!;

    public int BoardId { get; set; }

    public int LastTurn { get; set; }

    public int? Winner { get; set; }

    public int PlayerNumbers { get; set; }

    public virtual TblBoard Board { get; set; } = null!;

    public virtual ICollection<TblPlayer> TblPlayers { get; set; } = new List<TblPlayer>();
}
