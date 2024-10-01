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

// Start the Game
app.MapPost("/Tic-Tac-Toe", (string name) =>
    {
        return "X or O";
    })
    .WithName("StartTicTacToe")
    .WithOpenApi();

// Check who's turn it is
app.MapGet("/Tic-Tac-Toe", () =>
    { 
        return "Who's Turn is it";
    })
    .WithName("CheckTurn")
    .WithOpenApi();

// Player makes a move
app.MapPost("/Tic-Tac-Toe", (int numberPlacment) =>
    {
        return "Make a move";
    })
    .WithName("MakeMove")
    .WithOpenApi();

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
    .WithOpenApi();


app.Run();


