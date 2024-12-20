using SnakeLadderGame.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeLadderGame.ConsoleAssociated.Model
{
    public class PrintResult
    {
        public static void Print<T>(Result<T?> model)
        {
            if (model.IsSuccess)
            {
                Console.WriteLine(model.Message);
                Console.WriteLine("Data Here:");

                if (model.Data!.GetType() == typeof(BoardResponseModel))
                {
                    var boardResponse = model.Data as BoardResponseModel;
                    if (boardResponse != null)
                    {
                        // Logging the board information
                        if (boardResponse.Board != null)
                        {
                            int? d = boardResponse.Board.board?.BoardDimension;
                            Console.WriteLine($"\nBoard Id: \t\t{boardResponse.Board.board?.BoardId}\n" +
                                $"Board Name: \t\t{boardResponse.Board.board?.BoardName}\n" +
                                $"Board Dimension: \t{d}x{d}\n");
                            if (boardResponse.Board.routes != null)
                            {
                                foreach (var route in boardResponse.Board.routes)
                                {
                                    Console.WriteLine($"RouteId: {route.RouteId}, Place: {route.Place}, Action: {route.ActionType}, Destination: {route.Destination}");
                                }
                            }
                        }

                        // Logging the list of boards
                        if (boardResponse.LBoards != null)
                        {
                            foreach (var boardRoute in boardResponse.LBoards)
                            {
                                int? d = boardRoute.board?.BoardDimension;
                                Console.WriteLine($"\nBoard Id: \t\t{boardRoute.board?.BoardId}\n" +
                                    $"Board Name: \t\t{boardRoute.board?.BoardName}\n" +
                                    $"Board Dimension: \t{d}x{d}\n");
                                if (boardRoute.routes != null)
                                {
                                    foreach (var route in boardRoute.routes)
                                    {
                                        Console.WriteLine($"RouteId: {route.RouteId}, Place: {route.Place}, Action: {route.ActionType}, Destination: {route.Destination}");
                                    }
                                }
                            }
                        }
                    }
                }
                else if (model.Data!.GetType() == typeof(RouteResponseModel))
                {
                    var routeResponse = model.Data as RouteResponseModel;
                    if (routeResponse != null)
                    {
                        // Logging the route information
                        if (routeResponse.Route != null)
                        {
                            Console.WriteLine($"RouteId: {routeResponse.Route.RouteId}, Place: {routeResponse.Route.Place}, Action: {routeResponse.Route.ActionType}, Destination: {routeResponse.Route.Destination}, BoardId: {routeResponse.Route.BoardId}");
                        }

                        // Logging the list of routes
                        if (routeResponse.LRoutes != null)
                        {
                            foreach (var route in routeResponse.LRoutes)
                            {
                                Console.WriteLine($"RouteId: {route.RouteId}, Place: {route.Place}, Action: {route.ActionType}, Destination: {route.Destination}, BoardId: {route.BoardId}");
                            }
                        }
                    }
                }
                else
                {
                    Console.WriteLine(model.Data);
                }
                return;
            }
            else
            {
                if (model.IsNormalError || model.IsValidationError)
                {
                    WarningException warningException = new WarningException(model.Message);
                    Console.Error.WriteLine(model.Message);
                }
                else
                {
                    Console.Error.WriteLine(model.Message);
                }

                WarningException myEx = new WarningException("There is no result pattern to catch!");
                Console.WriteLine(myEx.ToString());
            }
        }

    }
}
