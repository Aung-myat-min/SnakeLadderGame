// See https://aka.ms/new-console-template for more information
using Microsoft.IdentityModel.Tokens;
using SnakeLadderGame.ConsoleAssociated.Model;
using SnakeLadderGame.Database.Models;
using SnakeLadderGame.Domain.Features;
using System.Threading.Tasks;

Console.WriteLine("This console app is to insert and view boards and routes information for Snake Ladder Game.\n");

int choice = 0;
AppDBContext _db = new AppDBContext();
BoardService boardService = new BoardService(_db);
RouteService routeService = new RouteService(_db);

while (choice != 99)
{
    Console.Write("\n1. Insert Board\n2. View Boards\n3. Insert Routes\n99. Exit Program\nChoose Action: ");

    while (true)
    {
        try
        {
            choice = int.Parse(Console.ReadLine()!);
            break;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            continue;
        }
    }

    switch (choice)
    {
        case 1:
            await InsertBoard();
            break;
        case 2:
            await ViewBoards();
            break;
        case 3:
            await InsertRoute();
            break;
        case 99:
            Console.WriteLine("Exiting....");
            break;
        default:
            Console.WriteLine("Invalid Input");
            break;
    }
}

async Task InsertBoard()
{
    Console.Write("Enter Board Name: ");
    string boardName = Console.ReadLine()!;
    Console.Write("Enter Dimension: ");
    int dimension;
    while (true)
    {
        try
        {
            dimension = int.Parse(Console.ReadLine()!);
            break;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Invalid Input!");
            continue;
        }
    }

    if (boardName.IsNullOrEmpty())
    {
        Console.WriteLine("Invalid Board Name.");
        return;
    }

    TblBoard board = new TblBoard()
    {
        BoardName = boardName,
        BoardDimension = dimension
    };

    var response = await boardService.CreateBoard(board);

    if (response is null)
    {
        Console.WriteLine("Error Creating new board!");
    }
    else
    {
        PrintResult.Print(response);
        Console.WriteLine();
    }
}

async Task ViewBoards()
{
    var response = await boardService.GetBoards();

    if (response is null)
    {
        Console.WriteLine("Error Getting boards!");
    }
    else
    {
        PrintResult.Print(response);
        Console.WriteLine();
    }
}

async Task InsertRoute()
{
    Console.Write("Enter Board Id: ");
    int boardId;
    while (true)
    {
        try
        {
            boardId = int.Parse(Console.ReadLine()!);
            break;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Invalid Input!" + ex.Message);
            continue;
        }
    }

    var response = await boardService.GetBoard(boardId);

    if (response.IsError)
    {
        Console.WriteLine(response.Message);
        return;
    }

    PrintResult.Print(response);

    List<TblBoardRoute> routes = new List<TblBoardRoute>();

    int place = 0;

    int boardChoice = 0;
    while (boardChoice != 99)
    {
        EnumAcitonType actionType;
        int destination;
        Console.WriteLine($"\nYou are now inserting value for cell {place + 1}\n");
        Console.Write("\n1. Normal Step\n2. Ladder Step\n3. Snake Step\n99. Exit\nEnter Corresponding Action: ");
        while (true)
        {
            try
            {
                boardChoice = int.Parse(Console.ReadLine()!);
                break;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Invalid Input!" + ex.Message);
                continue;
            }
        }

        switch (boardChoice)
        {
            case 1:
                place++;
                actionType = EnumAcitonType.Normal;

                var normalRoute = new TblBoardRoute
                {
                    Place = place,
                    ActionType = actionType.ToString(),
                    BoardId = boardId
                };
                routes.Add(normalRoute);
                break;
            case 2:
                place++;
                actionType = EnumAcitonType.Ladder;

                Console.Write("Enter Destination: ");
                while (true)
                {
                    try
                    {
                        destination = int.Parse(Console.ReadLine()!);
                        break;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Invalid Input!" + ex.Message);
                        continue;
                    }
                }

                var ladderRoute = new TblBoardRoute
                {
                    Place = place,
                    ActionType = actionType.ToString(),
                    Destination = destination,
                    BoardId = boardId
                };
                routes.Add(ladderRoute);
                break;
            case 3:
                place++;
                actionType = EnumAcitonType.Snake;

                Console.Write("Enter Destination: ");
                while (true)
                {
                    try
                    {
                        destination = int.Parse(Console.ReadLine()!);
                        break;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Invalid Input!" + ex.Message);
                        continue;
                    }
                }

                var snakeRoute = new TblBoardRoute
                {
                    Place = place,
                    ActionType = actionType.ToString(),
                    Destination = destination,
                    BoardId = boardId
                };
                routes.Add(snakeRoute);
                break;
            case 99:
                Console.WriteLine("Exiting....");
                break;
            default:
                Console.WriteLine("Invalid Input");
                continue;
        }
    }

    foreach (var route in routes)
    {
        var routeResponse = await routeService.CreateRoute(route);
        PrintResult.Print(routeResponse);
        Console.WriteLine();
    }
}
