using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static MineSweeperWPF.Enums;

namespace MineSweeperWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Grid mineField;
        private Enums.Level currentLevel;
        private Enums.GameStatus gameStatus;
        private static int cellSize = 30;
        private int[][] mineBoard;
        public MainWindow()
        {
            InitializeComponent();
            MineField mineField = new MineField(Enums.Level.Easy);
            this.AddChild(mineField);
            mineField.GameStatuChange += MineField_GameStatuChange;
        }

        private void MineField_GameStatuChange(GameStatus status)
        {
            throw new NotImplementedException();
        }

        

       
       
       
    }
}