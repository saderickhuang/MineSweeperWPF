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
            ResetMineFiled();
        }


        private void ResetMineFiled()
        {
            gameStatus = GameStatus.NotStarted;
            mineField = this.FindName("MineField") as Grid ;
            if (mineField == null)
            {
                return;
            }
            mineField.Children.Clear();
            mineField.RowDefinitions.Clear();
            mineField.ColumnDefinitions.Clear();
            currentLevel = Enums.Level.Hard;
            mineField.Height = cellSize * (int)currentLevel;
            mineField.Width = mineField.Height;
            for (int i = 0; i < (int)currentLevel; i++)
            {
                mineField.RowDefinitions.Add(new RowDefinition());
                mineField.ColumnDefinitions.Add(new ColumnDefinition());
            }

            for (int i = 0; i < (int)currentLevel; i++)
            {
                for (int j = 0; j < (int)currentLevel; j++)
                {
                   MineButton mineButton = new MineButton();
                    mineButton.posX = i;
                    mineButton.posY = j;
                    mineButton.Width = cellSize;
                    mineButton.Height = cellSize;
                    mineButton.CellStateChanged += MineButton_CellStateChanged;
                    mineField.Children.Add(mineButton);
                    Grid.SetRow(mineButton, i);
                    Grid.SetColumn(mineButton, j);
                    
                }
            }
        }
        private int[][] generateMineFieldMap(int mineCount, int initialX, int initialY)
        {
            int[][] mineFieldMap = new int[(int)currentLevel][];
            for (int i = 0; i < (int)currentLevel; i++)
            {
                mineFieldMap[i] = new int[(int)currentLevel];
                for (int j = 0; j < (int)currentLevel; j++)
                {
                    mineFieldMap[i][j] = 0;
                }
            }

            for (int i = 0; i < mineCount; i++)
            {
                // Generate mine field map logic here
                mineFieldMap[0][i] = 1;
            }

            return mineFieldMap;
        }
        private void MineButton_CellStateChanged(object? sender, Enums.CellState e)
        {
            MineButton mineButton = sender as MineButton;
            if(gameStatus == GameStatus.NotStarted)
            {
                gameStatus = GameStatus.InProgress;
                mineBoard = generateMineFieldMap((int)currentLevel, 0, 0);

            }   
        }
    }
}