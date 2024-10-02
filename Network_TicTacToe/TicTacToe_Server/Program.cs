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
char playerX = 'X';
char playerO = 'O';
string name1 = null; // unnecessary extra
string name2 = null; // unnecessary extra
// Start the Game
app.MapPost("/Tic-Tac-Toe-New-Game", (string player1, string player2) =>
{
    // this whole endpoint is extra and unnecessary
    name1 = player1;
    name2 = player2;

    return "Let's Play!";
})
    .WithName("StartTicTacToe")
    .WithOpenApi();

// Player makes a move
app.MapPost("/Tic-Tac-Toe-Move", (int add, char playerMark) =>
    {
        if (winner != null)
            return $"{winner} won this turn. Start a new game!";
        if (name1 == null || name2 == null)
            return "Go to Start Tic-Tac-Toe-New-Game! To start playing";
        
        if (newGame % 2 == 0)
            playerMark = playerX;
        if (newGame % 2 == 1) 
            playerMark = playerO;
        int choice = add-1; 
        if (board[choice] != 'X' && board[choice] != 'O') 
        { 
            board[choice] = playerMark; 
            newGame++; 
            return "Next players turn"; // Make this the next {player} 
        } 
        return "Field is taken, choose another field";
    })
    .WithName("PlayerMove")
    .WithOpenApi();


// Check whose turn it is
app.MapGet("/Tic-Tac-Toe-Turn", () =>
    {
        if (newGame % 2 == 0)
            return $"{name1} turn to play\n" + 
                   " ___________ \n" +
                   $"| {board[0]} | {board[1]} | {board[2]} |\n" +
                   "|___|___|___|\n"+ 
                   $"| {board[3]} | {board[4]} | {board[5]} |\n"+ 
                   "|___|___|___|\n"+ 
                   $"| {board[6]} | {board[7]} | {board[8]} |\n"+ 
                   "|___|___|___|";
        
        return $"{name2} turn to play\n" + 
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


//Check if there's a winner
app.MapGet("/Tic-Tac-Toe-Winner", () =>
    {
        if (board[0] == board[1] && board[1] == board[2])
        {
            winner = "winner";
            if (board[0] == 'X')
            {
                return $"Player 1 {name1} wins!";
            }
            return $"Player 2 {name2} wins!";
            
            //"winner first row horizontal";
        }

        if (board[3] == board[4] && board[4] == board[5])
        {
            if (board[3] == 'X')
            {
                
                return $"Player 1 {name1} wins!";
            }
            
            return $"Player 2 {name2} wins!";
            //"winner second row horizontal";
        }

        if (board[6] == board[7] && board[7] == board[8])
        {
            if (board[6] == 'X')
            {
                return $"Player 1 {name1} wins!";
            }
           
            return $"Player 2 {name2} wins!";
        }


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
            name1 = null;
            name2 = null;
            board[0] = '1'; board[1] = '2'; board[2] = '3';
            board[3] = '4'; board[4] = '5'; board[5] = '6'; 
            board[6] = '7'; board[7] = '8'; board[8] = '9';
            return "New Game" +
                   " ___________ \n" +
                   $"| {board[0]} | {board[1]} | {board[2]} |\n" +
                   "|___|___|___|\n"+ 
                   $"| {board[3]} | {board[4]} | {board[5]} |\n"+ 
                   "|___|___|___|\n"+ 
                   $"| {board[6]} | {board[7]} | {board[8]} |\n"+ 
                   "|___|___|___|";
        }
        return "Game Ongoing";

    })
    .WithName("NewGame")
    .WithOpenApi();


app.Run();


