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

namespace MineSweeperWPF
{
    /// <summary>
    /// MineField.xaml 的交互逻辑
    /// </summary>
    public partial class MineField : Grid
    {
        private static int cellSize = 30;
        private MineButton[][] mineFiledArray;
        private Enums.Level currentLevel;

        public event Action<Enums.GameStatus> GameStatuChange;
        public MineField(Enums.Level level)
        {
            this.currentLevel = level;
            InitializeComponent();
            initialMineFiledArray();
        }

        private void initialMineFiledArray() 
        {
            //inital mine filed array
            mineFiledArray = new MineButton[(int)currentLevel][];
            for (int i = 0; i < (int)currentLevel; i++) 
            {
                mineFiledArray[i] = new MineButton[(int)currentLevel];
            }

           
            this.Children.Clear();
            this.RowDefinitions.Clear();
            this.ColumnDefinitions.Clear();
            this.Height = cellSize * (int)currentLevel;
            this.Width = this.Height;
            for (int i = 0; i < (int)currentLevel; i++)
            {
                this.RowDefinitions.Add(new RowDefinition());
                this.ColumnDefinitions.Add(new ColumnDefinition());
            }
            for (int iRow = 0; iRow < (int)currentLevel; iRow++)
            {
                for (int jColumn = 0; jColumn < (int)currentLevel; jColumn++)
                {
                    mineFiledArray[iRow][jColumn] = new MineButton();
                    mineFiledArray[iRow][jColumn].Width = cellSize;
                    mineFiledArray[iRow][jColumn].Height = cellSize;
                    mineFiledArray[iRow][jColumn].posX = iRow;
                    mineFiledArray[iRow][jColumn].posY = jColumn;
                    this.Children.Add(mineFiledArray[iRow][jColumn]);
                    Grid.SetRow(mineFiledArray[iRow][jColumn], iRow);
                    Grid.SetColumn(mineFiledArray[iRow][jColumn], jColumn);
                    mineFiledArray[iRow][jColumn].MouseDown += MineButton_MouseDown;

                }
            }
        }

        private void MineButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var mineButton = (MineButton)sender;
            if (mineButton != null)
            {
                if (e.LeftButton == MouseButtonState.Pressed)
                {
                    GameStatuChange.Invoke(Enums.GameStatus.InProgress);
                        
                }
                
            }
        }
    }
}
