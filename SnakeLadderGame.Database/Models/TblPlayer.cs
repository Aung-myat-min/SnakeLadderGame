using System;
using System.Collections.Generic;

namespace SnakeLadderGame.Database.Models;

public partial class TblPlayer
{
    public int PlayerId { get; set; }

    public string Color { get; set; } = null!;

    public int? Position { get; set; }

    public int Turn { get; set; }

    public string RoomCode { get; set; } = null!;

    public virtual TblGameRoom RoomCodeNavigation { get; set; } = null!;
}
