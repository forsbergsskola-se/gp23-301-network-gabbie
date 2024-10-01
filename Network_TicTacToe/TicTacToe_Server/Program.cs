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
char playerX;
char playerO;
// Start the Game
app.MapPost("/Tic-Tac-Toe-New-Game", (char player1, char player2) =>
{
    playerX = player1;
    playerO = player2;
    return  " ___________ \n" + 
            "|___|___|___|\n" + 
            "|___|___|___|\n" + 
            "|___|___|___|";
})
    .WithName("StartTicTacToe")
    .WithOpenApi();

// Player makes a move
app.MapPost("/Tic-Tac-Toe-Move", (int add, char player) =>
    {
        int choice = add-1;
        if (winner == null)
        {
            for (int i = 0; i < choice; i++)
            {
                if (board[choice] != 'x'|| board[choice] != 'o')
                {
                    board[choice] = player;
                    return "Next players turn";
                }
                return "Field is taken, choose another field";
            }
        }
        return $"Make a move {player}";
    })
    .WithName("PlayerMove")
    .WithOpenApi();


// Check whose turn it is
app.MapGet("/Tic-Tac-Toe-Turn", () =>
    {
        return $" ___________ \n" +
               $"| {board[0]} | {board[1]} | {board[2]} |\n" +
               "|___|___|___|\n"+ 
               $"| {board[3]} | {board[4]} | {board[5]} |\n"+ 
               "|___|___|___|\n"+ 
               $"| {board[6]} | {board[7]} | {board[8]} |\n"+ 
               "|___|___|___|";
    })
    .WithName("CheckTurn")
    .WithOpenApi();


//Check if there's a winner
app.MapGet("/Tic-Tac-Toe-Winner", (char name) =>
    {
        if (board[0] == board[1] && board[1] == board[2])
        {
            if (board[0] =='x')
                return "Player 1 wins!";
            return "Player 2 wins!";
            return "winner first row horizontal";
        }

        if (board[3] == board[4] && board[4] == board[5])
        {
            if (board[3] =='x')
                return "Player 1 wins!";
            return "Player 2 wins!";
            return "winner second row horizontal";
        }
        if (board[6] == board[7] && board[7] == board[8])
        { return "winner third row horizontal"; }


        if (board[0] == board[3] && board[3] == board[6])
        { return "winner first row vertical"; }
        if (board[1] == board[4] && board[4] == board[7])
        { return "winner secon row vertical"; }
        if (board[2] == board[5] && board[5] == board[8])
        { return "winner third row vertical"; }


        if (board[0] == board[4] && board[4] == board[8])
        { return "winner top left cross";}
        if (board[0] == board[4] && board[4] == board[6])
        { return "winner top right cross"; }
        
        return "no winner yet";
    })
    .WithName("GetWinner")
    .WithOpenApi();

// Start a new Game
/*app.MapPut("/Tic-Tac-Toe", (string name) =>
    {
        
    })
    .WithName("NewGame")
    .WithOpenApi();*/


app.Run();


