﻿using SnakeLadderGame.Database.Models;
using SnakeLadderGame.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeLadderGame.Domain.Features
{
    public class BoardService
    {
        private AppDBContext _db;

        public BoardService(AppDBContext db)
        {
            _db = db;
        }

        public async Task<Result<BoardResponseModel>> CreateBoard(TblBoard newBoard)
        {
            var response = new Result<BoardResponseModel>();

            await _db.TblBoards.AddAsync(newBoard);
            int result = await _db.SaveChangesAsync();

            if (result == 0)
            {
                response = Result<BoardResponseModel>.Error("Failed to create board");
                goto Result;
            }

            response = Result<BoardResponseModel>.Success("Board created successfully", new BoardResponseModel { Board = newBoard });

        Result:
            return response;
        }
    }
}