using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TicTacToeGUI
{
    /// <summary>
    /// Interaction logic for TicTacToeMainMenu.xaml
    /// </summary>
    public partial class TicTacToeMainMenu : UserControl
    {
        public TicTacToeMainMenu()
        {
            InitializeComponent();
        }

        public void LoadBoard(object sender, RoutedEventArgs e)
        {
            Window window = Window.GetWindow(this);
            window.Content = new TicTacToeBoard();
        }

        public void Gameinfo(object sender, RoutedEventArgs e)
        {
            string Message = "Παιχνίδι Τρίλιζας Player VS Computer";
            Message += "\n";
            Message += "Όνομα Προγραμματιστή : Ψαλτάκης Νικόλαος";
            Message += "\n";
            Message += "Το Παιχνίδι Αυτο δημιουργήθηκε με την γλώσσα προγραμματισμού c# χρησιμοποιώντας τον αλγόριθμο MiniMax";
            MessageBox.Show(Message, "Πληροφορίες");
        }
    }
}
