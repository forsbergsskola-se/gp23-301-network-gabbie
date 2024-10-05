var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
char[] board = {'1', '2', '3', '4', '5', '6', '7', '8', '9'};
string winner = null;
int newGame = 0;
const char playerX = 'X';
const char playerO = 'O';
char[] playerSymbols = [playerX, playerO];
int players = 0;
// Start the Game
app.MapPost("/Tic-Tac-Toe-New-Game", () =>
    {
        if (players >= 2)
            return "game is full";
        // Server decide first connection is player 1 (server) and 2 connection is player 2 (client)

    return $"You are {players++}";
    })
    .WithName("StartTicTacToe")
    .WithOpenApi();

// Player move
app.MapPost("/Tic-Tac-Toe-Move", (int add, int playerNr) =>
    {
        if (winner != null)
        {
            return $"{winner} won this turn. Start a new game!";
        }

        if (newGame % 2 != playerNr)
        {
            return $"not your Turn!";
        }

        if(!PlayMove(playerSymbols[playerNr])) return "Invalid Move!";

        if (GetWinner() != default)
        {
            winner = GetWinner().ToString();
        }
        
        return $"successful turn";

        
        bool PlayMove(char symbol) 
        { 
            int choice = add-1;
            if (board[choice] == playerX || board[choice] == playerO) return false;
            
            board[choice] = symbol; 
            newGame++;
            return true;
        }
    })
    .WithName("PlayerMove")
    .WithOpenApi();


// Check whose turn it is
app.MapGet("/Tic-Tac-Toe-Turn", () =>
    {
        if (newGame % 2 == 0)
            return $"{playerX} turn to play\n" + 
                   " ___________ \n" +
                   $"| {board[0]} | {board[1]} | {board[2]} |\n" +
                   "|___|___|___|\n"+ 
                   $"| {board[3]} | {board[4]} | {board[5]} |\n"+ 
                   "|___|___|___|\n"+ 
                   $"| {board[6]} | {board[7]} | {board[8]} |\n"+ 
                   "|___|___|___|";
        
        return $"{playerO} turn to play\n" + 
               " ___________ \n" +
               $"| {board[0]} | {board[1]} | {board[2]} |\n" + 
               "|___|___|___|\n"+ 
               $"| {board[3]} | {board[4]} | {board[5]} |\n"+ 
               "|___|___|___|\n"+ 
               $"| {board[6]} | {board[7]} | {board[8]} |\n"+ 
               "|___|___|___|";
    })
    .WithName("CheckTurn")
    .WithOpenApi();

char GetWinner()
{
    for (int i = 0; i < 7; i+=3)
    {
        if (board[i] == board[i+1] && board[i+1] == board[i+2])
        {
            return board[i];
        }
    }
    
    for (int i = 0; i < 3; i++)
    {
        if (board[i] == board[i+3] && board[i+3] == board[i+6])
        { 
            return board[i];
        }
    }



    if (board[0] == board[4] && board[4] == board[8]) 
    { 
        return board[0];
    }
    
    if (board[2] == board[4] && board[4] == board[6]) 
    { 
        return board[0];
    }
    
    return default;
}

//Check if there's a winner
app.MapGet("/Tic-Tac-Toe-Winner", () =>
    {
        // draw is missing
    })
    .WithName("GetWinner")
    .WithOpenApi();

// Start a new Game
app.MapPut("/Tic-Tac-Toe-Restart", (int restartGame) =>
    {
        newGame = restartGame;
        if (newGame == 0)
        {
            winner = null;
            board[0] = '1'; board[1] = '2'; board[2] = '3';
            board[3] = '4'; board[4] = '5'; board[5] = '6'; 
            board[6] = '7'; board[7] = '8'; board[8] = '9';
            return "New Game";
        }
        return "Game Ongoing";

    })
    .WithName("NewGame")
    .WithOpenApi();


app.Run();


