using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace TicTacToeGUI
{
    /// <summary>
    /// Interaction logic for TicTacToeBoard.xaml
    /// </summary>
    public partial class TicTacToeBoard : UserControl
    {
        private static Player Player1 = new Player("Player1", 'X');
        private static Player Computer = new Player("Player2", 'O');
        private static Player CurrentPlayer { get; set; } = Player1;
        private List<char> ButtonItems { get; set; } = new List<char>();
        private char[,] Board { get; set; } = new char[3, 3]
            {
                {'1','2','3'},
                {'4', '5', '6'},
                {'7', '8', '9'}
            };

        public TicTacToeBoard()
        {
            InitializeComponent();
            DataContext = this;
            whoStartsFirst();
            CurrentPlayerLabel();
            ScoreLabel();
            InitializeGame();
        }

        private void whoStartsFirst()
        {
            Random random = new Random();
            int rand = random.Next(0, 100);
            if (rand % 2 == 0)
            {
                CurrentPlayer = Player1;
            }
            else
            {
                CurrentPlayer = Computer;
            }
        }

        private void CurrentPlayerLabel()
        {
            PlayerTurn.Content = "Παίζει ο " + CurrentPlayer.PlayerName + " Με το Σύμβολο " + CurrentPlayer.PlayerSymbol;
        }

        private void ScoreLabel()
        {
            PlayerScore.Content = "Player - Computer : " + Player1.PlayerScore + " - " + Computer.PlayerScore;
        }

        private void InitializeGame()
        {
            Board = new char[3, 3]{
            {'1','2','3'},
            {'4', '5', '6'},
            {'7', '8', '9'}
            };
            ButtonItems = new List<char> { '1', '2', '3', '4', '5', '6', '7', '8', '9' };
            int count = 0;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    string ButtonName = $"Place{i}{j}";
                    Button MyButton = (Button)FindName(ButtonName);
                    MyButton.Content = ButtonItems[count++];
                    MyButton.IsEnabled = true;
                }
            }
            if (CurrentPlayer == Computer)
            {
                CurrentPlayer = Computer;
                GetComputerMove(Board, Player1, Computer);
                CurrentPlayer = Player1;
                CurrentPlayerLabel();
            }
        }

        private void CellClickedCommand(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            char ButtonContent = (char)button.Content;
            button.Content = CurrentPlayer.PlayerSymbol;
            for (int row = 0; row < Board.GetLength(0); row++)
            {
                for (int col = 0; col < Board.GetLength(1); col++)
                {
                    if (Board[row, col] == ButtonContent)
                    {
                        Board[row,col] = CurrentPlayer.PlayerSymbol;
                    }
                }
            }
            button.IsEnabled = false;
            if (isGameFinished())
            {
                InitializeGame();
            }else
            {
                CurrentPlayer = Computer;
                GetComputerMove(Board, Player1, Computer);
                if (isGameFinished())
                {
                    whoStartsFirst();
                    InitializeGame();
                }
            }
            CurrentPlayer = Player1;
        }

        private bool isGameFinished()
        {
            if (check_winning())
            {
                MessageBox.Show($"Νίκησε ο {CurrentPlayer.PlayerName}", "Ενημερωση");
                CurrentPlayer.PlayerScore++;
                ScoreLabel();
                CurrentPlayer = Player1;
                return true;
            }
            if (isBoardFull())
            {
                MessageBox.Show($"Ισσοπαλία Δεν Νίκησε κανείς", "Ενημερωση");
                CurrentPlayerLabel();
                return true;
            }
            return false;
        }

        private bool check_winning()
        {
            //checking if someone wins
            // checking Horizontal
            for (int i = 0; i < 3; i++)
            {
                string ButtonName1 = $"Place{i}{0}";
                string ButtonName2 = $"Place{i}{1}";
                string ButtonName3 = $"Place{i}{2}";
                Button MyButton1 = (Button)FindName(ButtonName1);
                Button MyButton2 = (Button)FindName(ButtonName2);
                Button MyButton3 = (Button)FindName(ButtonName3);
                if (MyButton1.Content.Equals(MyButton2.Content) && MyButton1.Content.Equals(MyButton3.Content))
                {
                    return true;
                }
            }
            //checking vertical
            for (int i = 0; i < 3; i++)
            {
                string ButtonName1 = $"Place{0}{i}";
                string ButtonName2 = $"Place{1}{i}";
                string ButtonName3 = $"Place{2}{i}";
                Button MyButton1 = (Button)FindName(ButtonName1);
                Button MyButton2 = (Button)FindName(ButtonName2);
                Button MyButton3 = (Button)FindName(ButtonName3);
                if (MyButton1.Content.Equals(MyButton2.Content) && MyButton1.Content.Equals(MyButton3.Content))
                {
                    return true;
                }
            }
            //cheching diagonally
            if (Place00.Content.Equals(Place11.Content) && Place00.Content.Equals(Place22.Content))
            {
                return true;
            }
            if (Place02.Content.Equals(Place11.Content) && Place02.Content.Equals(Place20.Content))
            {
                return true;
            }
            return false;
        }

        private bool isBoardFull()
        {
            int count = 0;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    string ButtonName = $"Place{i}{j}";
                    Button MyButton = (Button)FindName(ButtonName);
                    if (MyButton.Content.Equals(ButtonItems[count++]))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public  void GetComputerMove(char[,]Board, Player Player1, Player Computer)
        {
            int bestScore = int.MinValue;
            Tuple<int, int> bestMove = Tuple.Create(-1, -1);
            for (int row = 0; row < 3 ; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    if (Board[row,col] != 'X' && Board[row, col] != 'O')
                    {
                        char temp = Board[row, col];
                        Board[row, col] = Computer.PlayerSymbol; // Simulate the computer's move
                        int score = MiniMax.Minimax(Board, 0, int.MinValue, int.MaxValue, false, Player1.PlayerSymbol, Computer.PlayerSymbol);
                        Board[row, col] = temp; // Undo the computer's move
                        if (score > bestScore){ 
                            bestScore = score;
                            bestMove = Tuple.Create(row, col);
                        }
                    }
                }
            }
            int rowNumber = bestMove.Item1;
            int colNumber = bestMove.Item2;
            int Content = Board[rowNumber, colNumber];
            Board[rowNumber, colNumber] = CurrentPlayer.PlayerSymbol;
            string ButtonName = $"Place{rowNumber}{colNumber}";
            Button MyButton = (Button)FindName(ButtonName);
            MyButton.Content = Computer.PlayerSymbol;
            MyButton.IsEnabled = false;
        }
    }
}