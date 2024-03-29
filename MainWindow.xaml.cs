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

        private static int cellSize = 30;
        private int[][] mineBoard;
        private Button btnStatus;
        private Image btnStatusImg;
        public MainWindow()
        {
            InitializeComponent();
            btnStatus = FindName("btnGameStatus") as Button;
            btnStatusImg = new Image();
            mineField.GameStatuChange += MineField_GameStatuChange;
            btnStatusImg.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/normal.png"));
            btnStatus.Content = btnStatusImg;
            mineField.resetMineFiledArray(Level.Easy);
            resizeWindow();
            this.AddHandler(MineField.RightBtnDownGlobalEvent, new RoutedEventHandler(MineButton_NumCellRightMouseBtnDown), true);
        }

        private void MineButton_NumCellRightMouseBtnDown(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void resizeWindow()
        {
            double width = mineField.Width  + 50;
            double height = mineField.Height + 120;
            this.Width = width;
            this.Height = height;
        }

        private void MineField_GameStatuChange(GameStatus status)
        {
            switch (status)
            {
                case GameStatus.NotStarted:
                case GameStatus.InProgress:
                    btnStatusImg.Source =new BitmapImage(new Uri("pack://application:,,,/Resources/normal.png")); break;
                case GameStatus.Won:
                    btnStatusImg.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/win.png")); break;
                case GameStatus.Lost:
                    btnStatusImg.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/lost.png")); break;
                    break;
                default:
                    break;
            }
        }

        private void btnGameStatus_Click(object sender, RoutedEventArgs e)
        {
            mineField.GameStatus= GameStatus.NotStarted;
            MineField_GameStatuChange(GameStatus.NotStarted);
            mineField.resetMineFiledArray(Level.Easy); 
        }

        internal void cbxLevel_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (mineField == null) return;
            switch(cbxLevel.SelectedIndex)
            {
                case 0:
                mineField.resetMineFiledArray(Level.Easy);
                break;
                case 1:
                mineField.resetMineFiledArray(Level.Medium);
                break;
                case 2:
                mineField.resetMineFiledArray(Level.Hard);
                break;  
            }
            resizeWindow();
        }
    }
}