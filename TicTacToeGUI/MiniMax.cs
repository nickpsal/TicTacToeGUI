using System;

namespace TicTacToeGUI
{
    internal class MiniMax
    {
        public static int Minimax(char[,] Board,int depth, int alpha, int beta, bool isMaximizingPlayer, char Player1Symbol, char ComputerSymbol)
        {
            if (CheckIfWin(Board))
            {
                if (isMaximizingPlayer)
                {
                    return -1; // The computer loses
                }
                else
                {
                    return 1; // The computer wins
                }
            }
            if (checkIfBoardisFull(Board))
            {
                return 0; // It's a tie
            }
            if (isMaximizingPlayer)
            {
                int minScore = int.MinValue;
                for (int row = 0; row < Board.GetLength(0); row++)
                {
                    for (int col = 0; col < Board.GetLength(1); col++)
                    {
                        if (Board[row, col] != Player1Symbol && Board[row, col] != ComputerSymbol)
                        {
                            char temp = Board[row, col];
                            Board[row, col] = ComputerSymbol; // Simulate the computer's move
                            int score = Minimax(Board, depth + 1, alpha, beta, false, Player1Symbol, ComputerSymbol);
                            Board[row, col] = temp; // Undo the computer's move
                            minScore = Math.Max(score, minScore);
                            alpha = Math.Max(alpha, score);
                            if (beta <= alpha)
                            {
                                break; // Beta cut-off
                            }
                        }
                    }
                }
                return minScore;
            }
            else
            {
                int bestScore = int.MaxValue;
                for (int row = 0; row < Board.GetLength(0); row++)
                {
                    for (int col = 0; col < Board.GetLength(1); col++)
                    {
                        if (Board[row, col] != Player1Symbol && Board[row, col] != ComputerSymbol)
                        {
                            char temp = Board[row, col];
                            Board[row, col] = Player1Symbol; // Simulate the player's move
                            int score = Minimax(Board, depth + 1, alpha, beta, true, Player1Symbol, ComputerSymbol);
                            Board[row, col] = temp; // Undo the player's move
                            bestScore = Math.Min(score, bestScore);
                            beta = Math.Min(beta, score);
                            if (beta <= alpha)
                            {
                                break; // Alpha cut-off
                            }
                        }
                    }
                }
                return bestScore;
            }
        }

        private static bool checkIfBoardisFull(char[,] Board)
        {
            for (int row = 0; row < Board.GetLength(0); row++)
            {
                for (int col = 0; col < Board.GetLength(1); col++)
                {

                    if (!Board[row, col].Equals('X') && !Board[row, col].Equals('O'))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private static bool CheckIfWin(char[,] Board)
        {
            //check Horizontal
            for (int row = 0; row < Board.GetLength(0); row++)
            {
                if (Board[row, 0] == Board[row, 1] && Board[row, 0] == Board[row, 2])
                {
                    return true;
                }
            }
            //check vertical
            for (int col = 0; col < Board.GetLength(1); col++)
            {
                if (Board[0, col] == Board[1, col] && Board[0, col] == Board[2, col])
                {
                    return true;
                }
            }
            //check diagonal
            if (Board[0, 0] == Board[1, 1] && Board[0, 0] == Board[2, 2])
            {
                return true;
            }
            if (Board[0, 2] == Board[1, 1] && Board[0, 2] == Board[2, 0])
            {
                return true;
            }
            return false;
        }
    }
}
