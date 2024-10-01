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
int[] square = {0, 1, 2, 3, 4, 5, 6, 7, 8, 9};
string winner = null;
bool isGamePlaying = false;
// Start the Game
app.MapPost("/Tic-Tac-Toe", (char name) =>
{
    return winner != null ? $"{winner} won the game. Create a new one" : " ___________ \n" + 
                                                                         "|___|___|___|\n" + 
                                                                         "|___|___|___|\n" + 
                                                                         "|___|___|___|";
})
    .WithName("StartTicTacToe")
    .WithOpenApi();

/*
// Check whose turn it is
app.MapGet("/Tic-Tac-Toe", () =>
    { 
        Console.WriteLine(" ___________ ");
        Console.WriteLine($"|{0}|,{1}|,{2}|");
        Console.WriteLine("|___|___|___|");
        Console.WriteLine($"|{3}|,{4}|,{5}|");
        Console.WriteLine("|___|___|___|");
        Console.WriteLine($"|{6}|,{7}|,{8}|");
        Console.WriteLine("|___|___|___|");
        
        return "Who's Turn is it";
    })
    .WithName("CheckTurn")
    .WithOpenApi();

// Player makes a move
app.MapPost("/Tic-Tac-Toe", (int numberPlacement) =>
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
    .WithOpenApi();*/


app.Run();


