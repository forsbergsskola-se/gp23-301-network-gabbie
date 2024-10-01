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

int currentNumber = Random.Shared.Next(1, 101);
string winner = null;

app.MapGet("/randomnumber", () =>
    {
        return winner != null ? $"{winner} won the game. The number was {currentNumber}." : "The match is still going on!";
    })
    .WithName("GetRandomNumber")
    .WithOpenApi();


app.MapPut("/randomnumber", (int? number) =>
    {
        currentNumber = number ?? Random.Shared.Next(1, 101);
        winner = null;
        return (number == null ? "Random" : "Your") + " number successfully put. Tell your friends to try and guess it.";
    })
    .WithName("PutRandomNumber")
    .WithOpenApi();

app.MapPost("/randomnumber", (int guess, string name) =>
    {
        if (winner != null) return $"{winner} already won the game. Time to go home.";
        
        if (guess < currentNumber)
        {
            return "Guess higher.";
        } else if (guess > currentNumber)
        {
            return "Guess lower.";
        }
        else
        {
            winner = name;
            return "Correct! Great work!";
        }
    })
    .WithName("GuessRandomNumber")
    .WithOpenApi();

app.Run();