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
        if (winner != null)
            return $"{winner} won this turn. Start a new game!";
        
        
        if (newGame % 2 == 0)
            player = playerX;
        if (newGame % 2 == 1) 
            player = playerO;
        int choice = add-1; 
        if (board[choice] != 'X' && board[choice] != 'O') 
        { 
            board[choice] = player; 
            newGame++; 
            return "Next players turn"; // Make this the next {player} 
        } 
        return "Field is taken, choose another field";
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
app.MapGet("/Tic-Tac-Toe-Winner", () =>
    {
        string xWinner = playerX.ToString();
        string oWinner = playerO.ToString();
        if (board[0] == board[1] && board[1] == board[2])
        {
            if (board[0] == 'X')
            {
                winner = xWinner;
                return $"Player 1 {winner} wins!";
            }
            winner = oWinner; 
            return $"Player 2 {winner} wins!";
            
            return "winner first row horizontal";
        }

        if (board[3] == board[4] && board[4] == board[5])
        {
            if (board[3] == 'X')
            {
                winner = xWinner;
                return $"Player 1 {winner} wins!";
            }
            
            return $"Player 2 {winner} wins!";
            return "winner second row horizontal";
        }

        if (board[6] == board[7] && board[7] == board[8])
        {
            if (board[6] == 'X')
            {
                winner = xWinner;
                return $"Player 1 {winner} wins!";
            }
            winner = oWinner; 
            return $"Player 2 {winner} wins!";
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
    })
    .WithName("GetWinner")
    .WithOpenApi();

// Start a new Game
app.MapPut("/Tic-Tac-Toe", (int restartGame) =>
    {
        newGame = restartGame;
        winner = null;
        if (newGame == 0)
        {
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


