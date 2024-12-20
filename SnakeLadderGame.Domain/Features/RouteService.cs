using SnakeLadderGame.Database.Models;
using SnakeLadderGame.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeLadderGame.Domain.Features
{
    public class RouteService
    {
        private readonly AppDBContext _db;

        public RouteService(AppDBContext db)
        {
            _db = db;
        }

        public async Task<Result<RouteResponseModel>> CreateRoute(TblBoardRoute newRoute)
        {
            var response = new Result<RouteResponseModel>();

            await _db.TblBoardRoutes.AddAsync(newRoute);
            int result = await _db.SaveChangesAsync();

            if (result == 0)
            {
                response = Result<RouteResponseModel>.Error("Failed to create route");
                goto Result;
            }

            response = Result<RouteResponseModel>.Success("Route created successfully", new RouteResponseModel { Route = newRoute });

        Result:
            return response;
        }
    }
}
