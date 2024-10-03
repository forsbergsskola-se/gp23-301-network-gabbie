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
app.MapPost("/Tic-Tac-Toe-New-Game", (string player) =>
    {
    
    // Server decide first connection is player 1 (server) and 2 connection is player 2 (client)

    return "Let's Play!";
})
    .WithName("StartTicTacToe")
    .WithOpenApi();

// Player makes a move
app.MapPost("/Tic-Tac-Toe-Move", (int add, char playerSymbol) =>
{
    if (winner != null)
    {
        return $"{winner} won this turn. Start a new game!";
    }

    if (newGame % 2 == 0 && playerSymbol == 'x')
    {
        PlayMove(playerSymbol = playerX);
        return $"{playerSymbol} Turn to play";
    }

    if (newGame % 2 == 1 && playerSymbol == 'o')
    {
        PlayMove(playerSymbol = playerO);
        return $"{playerSymbol} Turn to play";
    }
        
void PlayMove(char symbol) 
{ 
    int choice = add-1; 
    if (board[choice] != 'X' && board[choice] != 'O') 
    { 
        board[choice] = symbol; 
        newGame++;
    }
}
    return "You can't do that!";
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


//Check if there's a winner
app.MapGet("/Tic-Tac-Toe-Winner", () =>
    {
        
        
        if (board[0] == board[1] && board[1] == board[2]) {
                if (board[0] == 'X')
                {
                    return $"Player 1 {winner} wins!";
                }
                return $"Player 2 {winner} wins!";
            
                //"winner first row horizontal";
        }

        if (board[3] == board[4] && board[4] == board[5])
        {
            if (board[3] == 'X')
            {
                return $"Player 1 {winner} wins!";
            }
        
            return $"Player 2 {winner} wins!";
            //"winner second row horizontal";
        }

        if (board[6] == board[7] && board[7] == board[8])
        {
            if (board[6] == 'X')
            {
                return $"Player 1 {winner} wins!";
            }
       
            return $"Player 2 {winner} wins!";
        }


        if (board[0] == board[3] && board[3] == board[6])
        { if (board[0] == 'X')
            {
            
                return $"Player 1 {winner} wins!";
            }
        
            return $"Player 2 {winner} wins!"; 
        }
        
        if (board[1] == board[4] && board[4] == board[7])
        { if (board[1] == 'X')
            {
            
                return $"Player 1 {winner} wins!";
            }
        
            return $"Player 2 {winner} wins!"; 
        }

        if (board[2] == board[5] && board[5] == board[8])
        {
            if (board[2] == 'X')
            {
            
                return $"Player 1 {winner} wins!";
            }
        
            return $"Player 2 {winner} wins!";
            
        }


        if (board[0] == board[4] && board[4] == board[8])
        { 
            if (board[0] == 'X')
            {
            
                return $"Player 1 {winner} wins!";
            }
        
            return $"Player 2 {winner} wins!";
            
        }

        if (board[0] == board[4] && board[4] == board[6])
        {
            if (board[0] == 'X')
            {
            
                return $"Player 1 {winner} wins!";
            }
        
            return $"Player 2 {winner} wins!";
            
        }
    
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


