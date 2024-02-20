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
    /// MineButton.xaml 的交互逻辑
    /// </summary>
    public partial class MineButton : Button
    {
        private Enums.CellState state;
        private Enums.CellType type;
        private Image btnImg;
        private Label btnLabel;
        public  int DisplayValue { get; set; }
        public int posX { get; set; }
        public int posY { get; set; }
        public Enums.CellState State
        {
            get
            {
                return state;
            }
            set
            {
                state = value;
                if (CellStateChanged != null)
                {
                    CellStateChanged(this, state);
                }
            }
        }
        public event EventHandler<Enums.CellState> CellStateChanged;
        public MineButton()
        {
            InitializeComponent();
            state = Enums.CellState.Covered;
            btnLabel = new Label();
            btnLabel.Foreground = Brushes.Red;
            btnLabel.FontSize = 20;
            btnLabel.Padding = new Thickness(0);
        }

        protected override void OnMouseRightButtonUp(MouseButtonEventArgs e)
        {
            base.OnMouseRightButtonUp(e);
            if (State == Enums.CellState.Covered)
            {
                State = Enums.CellState.Flagged;
            }
            else if (State == Enums.CellState.Flagged)
            {
                State = Enums.CellState.Covered;
            }
            
            //redraw the button
            DrawButton();
        }

        private void DrawButton()
        {
            if (State == Enums.CellState.Flagged)
            {
                btnLabel.Content = "?";
                
            }
            else if (State == Enums.CellState.Uncovered)
            {
                if (type == Enums.CellType.Mine)
                {
                    btnLabel.Content = "!";
                }
                else
                {
                    btnLabel.Content = DisplayValue.ToString();
                }
            }
            this.Content = btnLabel;
            this.Margin = new Thickness(0);
         }
    }
}
