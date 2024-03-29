using System;
using System.Collections.Generic;
using System.DirectoryServices;
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
    public class TriggerBtnDownArgs : RoutedEventArgs
    {
         public TriggerBtnDownArgs(RoutedEvent routedEvent, object source) : base(routedEvent, source)
        {
            
         }
 
         public MineButton btn { get; set; }
     }
    /// <summary>
    /// MineField.xaml 的交互逻辑
    /// </summary>
    public partial class MineField : Grid
    {
        private static int cellSize = 30;
        private MineButton[][] mineFiledArray;
        private Enums.Level currentLevel;
        private Enums.GameStatus gameStatus;
        private int[] levelMineFileSize = { 10, 15, 20 };
        private int[] levelMineCount = { 10, 40, 99 };
        public Enums.GameStatus GameStatus { get => gameStatus; set => gameStatus = value; }

        public event Action<Enums.GameStatus> GameStatuChange;

        public static readonly RoutedEvent RightBtnDownGlobalEvent = EventManager.RegisterRoutedEvent
            ("RightBtnDownGlobal", RoutingStrategy.Bubble, typeof(EventHandler<TriggerBtnDownArgs>), typeof(MineField));
        public event RoutedEventHandler RightBtnDownGlobalHandler
        {
            add { this.AddHandler(RightBtnDownGlobalEvent, value); }
            remove
            {
                this.RemoveHandler(RightBtnDownGlobalEvent, value);
            }
        }

        public MineField()
        {
            InitializeComponent();
            currentLevel = Enums.Level.Easy;

            this.Height = cellSize *levelMineFileSize[(int)currentLevel];
            this.Width = cellSize * levelMineFileSize[(int)currentLevel] ;
            this.AddHandler(MineButton.RightBtnDownEvent, new RoutedEventHandler(btnRightClick));
        }
        public void resetMineFiledArray(Enums.Level level)
        {
            this.currentLevel = level;
            initialMineFiledArray();
            GameStatus = Enums.GameStatus.NotStarted;
        }
        private void initialMineFiledArray() 
        {
            //inital mine filed array
            mineFiledArray = new MineButton[levelMineFileSize[(int)currentLevel]][];
            for (int i = 0; i < levelMineFileSize[(int)currentLevel]; i++) 
            {
                mineFiledArray[i] = new MineButton[levelMineFileSize[(int)currentLevel]];
            }

           
            this.Children.Clear();
            this.RowDefinitions.Clear();
            this.ColumnDefinitions.Clear();
            this.Height = cellSize * levelMineFileSize[(int)currentLevel];
            this.Width = this.Height;
            for (int i = 0; i < levelMineFileSize[(int)currentLevel]; i++)
            {
                this.RowDefinitions.Add(new RowDefinition());
                this.ColumnDefinitions.Add(new ColumnDefinition());
            }
            for (int iRow = 0; iRow < levelMineFileSize[(int)currentLevel]; iRow++)
            {
                for (int jColumn = 0; jColumn < levelMineFileSize[(int)currentLevel]; jColumn++)
                {
                    mineFiledArray[iRow][jColumn] = new MineButton();
                    mineFiledArray[iRow][jColumn].Width = cellSize;
                    mineFiledArray[iRow][jColumn].Height = cellSize;
                    mineFiledArray[iRow][jColumn].posX = iRow;
                    mineFiledArray[iRow][jColumn].posY = jColumn;
                    this.Children.Add(mineFiledArray[iRow][jColumn]);
                    Grid.SetRow(mineFiledArray[iRow][jColumn], iRow);
                    Grid.SetColumn(mineFiledArray[iRow][jColumn], jColumn);
                    mineFiledArray[iRow][jColumn].CellClicked += MineField_CellClicked;
                }
            }
        }

        private void MineField_CellClicked(object? sender, EventArgs e)
        {
            var mineButton = (MineButton)sender;
            if (GameStatus == Enums.GameStatus.NotStarted)
            {
                GenerateMines(mineButton.posX, mineButton.posY);
                GameStatus = Enums.GameStatus.InProgress;
                UncoverEmptyCells(mineButton.posX, mineButton.posY);
                GameStatuChange?.Invoke(GameStatus);
            }
            else if (GameStatus == Enums.GameStatus.InProgress)
            {
                if(mineButton.state == Enums.CellState.Covered)
                {
                    if (mineButton.content == Enums.CellContent.Mine)
                    {
                        mineButton.State = Enums.CellState.Uncovered;
                        GameStatus = Enums.GameStatus.Lost;
                        uncoverAllCells(mineButton.posX, mineButton.posY);
                        GameStatuChange?.Invoke(GameStatus);
                    }
                    else if (isGameWon())
                    {
                        GameStatus = Enums.GameStatus.Won;
                        GameStatuChange?.Invoke(GameStatus);
                    }
                    else if (mineButton.content < Enums.CellContent.Mine)
                    {
                        UncoverEmptyCells(mineButton.posX, mineButton.posY);
                    }
                }
                else if (mineButton.state == Enums.CellState.Uncovered)
                {
                    if(mineButton.content>Enums.CellContent.Num_0 && mineButton.content <= Enums.CellContent.Num_8)
                    {
                        uncoverAroundCells(mineButton);
                    }    
                }


            }
        }
        //if the cell is a number cell, uncover all the cells around it
        private void uncoverAroundCells(MineButton mineButton)
        {
            int posX = mineButton.posX;
            int posY = mineButton.posY;
            int count = 0;
            if (posX - 1 >= 0 && posY - 1 >= 0 && mineFiledArray[posX - 1][posY - 1].state == Enums.CellState.Flagged)
            {
                count++;
            }
            if (posX - 1 >= 0 && mineFiledArray[posX - 1][posY].state == Enums.CellState.Flagged)
            {
                count++;
            }
            if (posX - 1 >= 0 && posY + 1 < levelMineFileSize[(int)currentLevel] && mineFiledArray[posX - 1][posY + 1].state == Enums.CellState.Flagged)
            {
                count++;
            }
            if (posY - 1 >= 0 && mineFiledArray[posX][posY - 1].state == Enums.CellState.Flagged)
            {
                count++;
            }
            if (posY + 1 < levelMineFileSize[(int)currentLevel] && mineFiledArray[posX][posY + 1].state == Enums.CellState.Flagged)
            {
                count++;
            }
            if (posX + 1 < levelMineFileSize[(int)currentLevel] && posY - 1 >= 0 && mineFiledArray[posX + 1][posY - 1].state == Enums.CellState.Flagged)
            {
                count++;
            }
            if (posX + 1 < levelMineFileSize[(int)currentLevel] && mineFiledArray[posX + 1][posY].state == Enums.CellState.Flagged)
            {
                count++;
            }
            if (posX + 1 < levelMineFileSize[(int)currentLevel] && posY + 1 < levelMineFileSize[(int)currentLevel] && mineFiledArray[posX + 1][posY + 1].state == Enums.CellState.Flagged)
            {
                count++;
            }
            if (count == (int)mineButton.content)
            {
                if (posX - 1 >= 0 && posY - 1 >= 0 && mineFiledArray[posX - 1][posY - 1].state == Enums.CellState.Covered)
                {
                    MineField_CellClicked(mineFiledArray[posX - 1][posY - 1], new EventArgs());
                }
                if (posX - 1 >= 0 && mineFiledArray[posX - 1][posY].state == Enums.CellState.Covered)
                {
                    MineField_CellClicked(mineFiledArray[posX - 1][posY], new EventArgs());
                }
                if (posX - 1 >= 0 && posY + 1 < levelMineFileSize[(int)currentLevel] && mineFiledArray[posX - 1][posY + 1].state == Enums.CellState.Covered)
                {
                    MineField_CellClicked(mineFiledArray[posX - 1][posY + 1], new EventArgs());
                }
                if (posY - 1 >= 0 && mineFiledArray[posX][posY - 1].state == Enums.CellState.Covered)
                {
                    MineField_CellClicked(mineFiledArray[posX][posY - 1], new EventArgs());
                }
                if (posY + 1 < levelMineFileSize[(int)currentLevel] && mineFiledArray[posX][posY + 1].state == Enums.CellState.Covered)
                {
                    MineField_CellClicked(mineFiledArray[posX][posY + 1], new EventArgs());
                }
                if (posX + 1 < levelMineFileSize[(int)currentLevel] && posY - 1 >= 0 && mineFiledArray[posX + 1][posY - 1].state == Enums.CellState.Covered)
                {
                    MineField_CellClicked(mineFiledArray[posX + 1][posY - 1], new EventArgs());
                }
                if (posX + 1 < levelMineFileSize[(int)currentLevel] && mineFiledArray[posX + 1][posY].state == Enums.CellState.Covered)
                {
                    MineField_CellClicked(mineFiledArray[posX + 1][posY], new EventArgs());
                }
                if (posX + 1 < levelMineFileSize[(int)currentLevel] && posY + 1 < levelMineFileSize[(int)currentLevel] && mineFiledArray[posX + 1][posY + 1].state == Enums.CellState.Covered)
                {
                    MineField_CellClicked(mineFiledArray[posX + 1][posY + 1], new EventArgs());
                }

            }
        }

        private void uncoverAllCells(int posX, int posY)
        { 
            for (int i = 0; i < levelMineFileSize[(int)currentLevel]; i++)
            {
                for (int j = 0; j < levelMineFileSize[(int)currentLevel]; j++)
                {
                    if (mineFiledArray[i][j].content == Enums.CellContent.Mine)
                    {
                        mineFiledArray[i][j].State = Enums.CellState.Uncovered;
                        mineFiledArray[i][j].ChangeImgByState();
                    }
                }
            }
        }

        private void UncoverEmptyCells(int posX, int posY)
        {
            if (posX < 0 || posY < 0 || posX >= levelMineFileSize[(int)currentLevel] || posY >= levelMineFileSize[(int)currentLevel])
            {
                return;
            }
            if (mineFiledArray[posX][posY].State == Enums.CellState.Covered)
            {
                mineFiledArray[posX][posY].State = Enums.CellState.Uncovered;
                mineFiledArray[posX][posY].ChangeImgByState();
                if (mineFiledArray[posX][posY].content ==Enums.CellContent.Num_0)
                {
                    UncoverEmptyCells(posX - 1, posY);
                    UncoverEmptyCells(posX + 1, posY);
                    UncoverEmptyCells(posX, posY - 1);
                    UncoverEmptyCells(posX, posY + 1);
                    UncoverEmptyCells(posX+1, posY + 1);
                    UncoverEmptyCells(posX-1, posY - 1);
                    UncoverEmptyCells(posX-1, posY + 1);
                    UncoverEmptyCells(posX+1, posY - 1);
                }
            }
            if (isGameWon())
            {
                GameStatus = Enums.GameStatus.Won;
                GameStatuChange?.Invoke(GameStatus);
            }
        }

        private bool isGameWon()
        {
            for (int i = 0; i < levelMineFileSize[(int)currentLevel]; i++)
            {
                for (int j = 0; j < levelMineFileSize[(int)currentLevel]; j++)
                {
                    if (mineFiledArray[i][j].State == Enums.CellState.Covered && mineFiledArray[i][j].content != Enums.CellContent.Mine)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        //Generate Mines
        private void GenerateMines(int posX, int posY)
        {
            Random random = new Random();
            int mineCount = 0;
            while (mineCount < levelMineCount[(int)currentLevel])
            {
                int x = random.Next(0, levelMineFileSize[(int)currentLevel]);
                int y = random.Next(0, levelMineFileSize[(int)currentLevel]);
                if (mineFiledArray[x][y].State == Enums.CellState.Covered 
                    && (x != posX && y != posY)
                    && (x != posX && y != posY-1)
                    && (x != posX && y != posY + 1)
                    && (x != posX - 1 && y != posY)
                    && (x != posX - 1 && y != posY - 1)
                    && (x != posX - 1 && y != posY + 1)
                    && (x != posX + 1 && y != posY)
                    && (x != posX + 1 && y != posY - 1)
                    && (x != posX + 1 && y != posY + 1)
                    )
                {
                    mineFiledArray[x][y].content = Enums.CellContent.Mine;
                    mineCount++;
                }
            }
            GenerateNumbers();
        }
        private void GenerateNumbers()
        {
            for (int i = 0; i < levelMineFileSize[(int)currentLevel]; i++)
            {
                for (int j = 0; j < levelMineFileSize[(int)currentLevel]; j++)
                {
                    if (mineFiledArray[i][j].content != Enums.CellContent.Mine)
                    {
                        mineFiledArray[i][j].content = GetNumber(i, j);
                    }
                }
            }
        }

        private Enums.CellContent GetNumber(int i, int j)
        {
            int count = 0;
            if (i - 1 >= 0 && j - 1 >= 0 && mineFiledArray[i - 1][j - 1].content == Enums.CellContent.Mine)
            {
                count++;
            }
            if (i - 1 >= 0 && mineFiledArray[i - 1][j].content == Enums.CellContent.Mine)
            {
                count++;
            }
            if (i - 1 >= 0 && j + 1 < levelMineFileSize[(int)currentLevel] && mineFiledArray[i - 1][j + 1].content == Enums.CellContent.Mine)
            {
                count++;
            }
            if (j - 1 >= 0 && mineFiledArray[i][j - 1].content == Enums.CellContent.Mine)
            {
                count++;
            }
            if (j + 1 < levelMineFileSize[(int)currentLevel] && mineFiledArray[i][j + 1].content == Enums.CellContent.Mine)
            {
                count++;
            }
            if (i + 1 < levelMineFileSize[(int)currentLevel] && j - 1 >= 0 && mineFiledArray[i + 1][j - 1].content == Enums.CellContent.Mine)
            {
                count++;
            }
            if (i + 1 < levelMineFileSize[(int)currentLevel] && mineFiledArray[i + 1][j].content == Enums.CellContent.Mine)
            {
                count++;
            }
            if (i + 1 < levelMineFileSize[(int)currentLevel] && j + 1 < levelMineFileSize[(int)currentLevel] && mineFiledArray[i + 1][j + 1].content == Enums.CellContent.Mine)
            {
                count++;
            }
            switch (count)
            {
                case 0: return Enums.CellContent.Num_0;
                case 1: return Enums.CellContent.Num_1;
                case 2: return Enums.CellContent.Num_2;
                case 3: return Enums.CellContent.Num_3;
                case 4: return Enums.CellContent.Num_4;
                case 5: return Enums.CellContent.Num_5;
                case 6: return Enums.CellContent.Num_6;
                case 7: return Enums.CellContent.Num_7;
                case 8: return Enums.CellContent.Num_8;
                default: return Enums.CellContent.Num_0;
            }
        }

        internal void btnRightClick(object sender, RoutedEventArgs e)
        {
            var mBtn = e.OriginalSource as MineButton;
            List<MineButton> mineButtons = FindNeighborButtons(mBtn);
            foreach (var item in mineButtons)
            {
                item.PlayButtonDownAnimation();
            }
            //if count of flagged cell equals to the number of the cell, uncover all the cells around it
            if (mBtn.content > Enums.CellContent.Num_0 && mBtn.content <= Enums.CellContent.Num_8)
            {
                uncoverAroundCells(mBtn);
            }

        }



        private List<MineButton> FindNeighborButtons(MineButton? mBtn)
        {
            List<MineButton> mineButtons = new List<MineButton>();
            if (mBtn == null)
            {
                return mineButtons;
            }
            int posX = mBtn.posX;
            int posY = mBtn.posY;
            if (posX - 1 >= 0 && posY - 1 >= 0)
            {
                mineButtons.Add(mineFiledArray[posX - 1][posY - 1]);
            }
            if (posX - 1 >= 0)
            {
                mineButtons.Add(mineFiledArray[posX - 1][posY]);
            }
            if (posX - 1 >= 0 && posY + 1 < levelMineFileSize[(int)currentLevel])
            {
                mineButtons.Add(mineFiledArray[posX - 1][posY + 1]);
            }
            if (posY - 1 >= 0)
            {
                mineButtons.Add(mineFiledArray[posX][posY - 1]);
            }
            if (posY + 1 < levelMineFileSize[(int)currentLevel])
            {
                mineButtons.Add(mineFiledArray[posX][posY + 1]);
            }
            if (posX + 1 < levelMineFileSize[(int)currentLevel] && posY - 1 >= 0)
            {
                mineButtons.Add(mineFiledArray[posX + 1][posY - 1]);
            }
            if (posX + 1 < levelMineFileSize[(int)currentLevel])
            {
                mineButtons.Add(mineFiledArray[posX + 1][posY]);
            }
            if (posX + 1 < levelMineFileSize[(int)currentLevel] && posY + 1 < levelMineFileSize[(int)currentLevel])
            {
                mineButtons.Add(mineFiledArray[posX + 1][posY + 1]);
            }
            return mineButtons;
        }
    }
}
