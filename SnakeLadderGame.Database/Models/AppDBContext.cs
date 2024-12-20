using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SnakeLadderGame.Database.Models;

public partial class AppDBContext : DbContext
{
    public AppDBContext()
    {
    }

    public AppDBContext(DbContextOptions<AppDBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TblBoard> TblBoards { get; set; }

    public virtual DbSet<TblBoardRoute> TblBoardRoutes { get; set; }

    public virtual DbSet<TblGameRoom> TblGameRooms { get; set; }

    public virtual DbSet<TblPlayer> TblPlayers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=Aung-Myat-Min\\SQL;Database=SnakeLadderGame;User Id=sa;Password=sasa@123;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TblBoard>(entity =>
        {
            entity.HasKey(e => e.BoardId).HasName("PK__Tbl_Boar__F9646BF2C48E3F99");

            entity.ToTable("Tbl_Board");

            entity.Property(e => e.BoardName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TblBoardRoute>(entity =>
        {
            entity.HasKey(e => e.RouteId).HasName("PK__Tbl_Boar__80979B4DD3758F26");

            entity.ToTable("Tbl_BoardRoute");

            entity.Property(e => e.ActionType)
                .HasMaxLength(30)
                .IsUnicode(false);

            entity.HasOne(d => d.Board).WithMany(p => p.TblBoardRoutes)
                .HasForeignKey(d => d.BoardId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Board_Route");
        });

        modelBuilder.Entity<TblGameRoom>(entity =>
        {
            entity.HasKey(e => e.GameId).HasName("PK__Tbl_Game__2AB897FDEB2EBB29");

            entity.ToTable("Tbl_GameRoom");

            entity.HasIndex(e => e.RoomCode, "UQ__Tbl_Game__4F9D5231C78C8684").IsUnique();

            entity.Property(e => e.LastTurn).HasDefaultValueSql("((1))");
            entity.Property(e => e.RoomCode)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasOne(d => d.Board).WithMany(p => p.TblGameRooms)
                .HasForeignKey(d => d.BoardId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_GameRoom_Board");
        });

        modelBuilder.Entity<TblPlayer>(entity =>
        {
            entity.HasKey(e => e.PlayerId).HasName("PK__Tbl_Play__4A4E74C8FB67A7B3");

            entity.ToTable("Tbl_Player");

            entity.Property(e => e.Color)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Position).HasDefaultValueSql("((1))");
            entity.Property(e => e.RoomCode)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasOne(d => d.RoomCodeNavigation).WithMany(p => p.TblPlayers)
                .HasPrincipalKey(p => p.RoomCode)
                .HasForeignKey(d => d.RoomCode)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Player_GameRoom");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
