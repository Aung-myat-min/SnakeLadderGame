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
        Console.WriteLine();  // Clear separation after printing result
    }
}

async Task ViewBoards()
{

    var response = await boardService.GetBoards();

    if (response is null)
    {
        Console.WriteLine("Error Getting new board!");
    }
    else
    {
        PrintResult.Print(response);
        Console.WriteLine();  // Clear separation after printing result
    }
}
