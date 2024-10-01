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
bool isGamePlaying = false;
// Start the Game
app.MapPost("/Tic-Tac-Toe-New-Game", (char player) =>
{
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
                if (board[choice] != player)
                {
                    board[choice] = player;
                    return "Next players turn";
                }
                
                else
                {
                    return "Field is taken, choose another field";
                }
                    
                    
            }
        }
        return $"Make a move {player}";
    })
    .WithName("PlayerMove")
    .WithOpenApi();


// Check whose turn it is
app.MapGet("/Tic-Tac-Toe", () =>
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

/*
//Check if there's a winner
app.MapGet("/Tic-Tac-Toe", () =>
    {
        
    })
    .WithName("GetWinner")
    .WithOpenApi();

// Start a new Game
app.MapPut("/Tic-Tac-Toe", (string name) =>
    {
        
    })
    .WithName("NewGame")
    .WithOpenApi();*/


app.Run();


