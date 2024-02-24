using System;
using System.Collections.Generic;
using System.Configuration;
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
        private Enums.CellContent content;
        private Image btnImg;
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
                ChangeImgByState();
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
            btnImg = new Image();
            state = Enums.CellState.Covered;
            this.AddChild(btnImg);
            ChangeImgByState();
        }
        private void ChangeImgByState()
        {
           switch (state) 
            {
                case Enums.CellState.Covered: btnImg.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/covered.png")); break;
                case Enums.CellState.Flagged: btnImg.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/flag.png")); break;

                case Enums.CellState.Uncovered:
                    {
                        switch (content)
                        {
                            case Enums.CellContent.Num_0: btnImg.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/num_0.png")); break;
                            case Enums.CellContent.Num_1: btnImg.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/num_1.png")); break;
                            case Enums.CellContent.Num_2: btnImg.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/num_2.png")); break;
                            case Enums.CellContent.Num_3: btnImg.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/num_3.png")); break;
                            case Enums.CellContent.Num_4: btnImg.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/num_4.png")); break;
                            case Enums.CellContent.Num_5: btnImg.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/num_5.png")); break;
                            case Enums.CellContent.Num_6: btnImg.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/num_6.png")); break;
                            case Enums.CellContent.Num_7: btnImg.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/num_7.png")); break;
                            case Enums.CellContent.Num_8: btnImg.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/num_8.png")); break;
                            case Enums.CellContent.Mine: btnImg.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/mine.png")); break;
                        }
                        break;
                    }
                default : return;
            }
            
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
            
        }

        private  void Button_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as MineButton;
            if (button != null)
            {
                button.Content = "Clicked";
            }
        }

        private void Button_MouseUp(object sender, MouseButtonEventArgs e)
        {
           
        }

        private void Button_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var btn = sender as MineButton;
            if (e.RightButton == MouseButtonState.Pressed)
            {

            }
            else if (e.LeftButton == MouseButtonState.Pressed)
            {
                 v
            }
        }
    }
}
