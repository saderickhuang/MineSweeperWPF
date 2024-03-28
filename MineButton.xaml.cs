﻿using System;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
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

        public Enums.CellState state;
        public Enums.CellType type;
        public Enums.CellContent content;
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


        public static readonly RoutedEvent NumCellRightMouseBtnDownEvent = EventManager.RegisterRoutedEvent(
        name: "NumCellRightMouseBtnDownEvent",
        routingStrategy: RoutingStrategy.Bubble,
        handlerType: typeof(RoutedEventHandler),
        ownerType: typeof(MineButton));

        public event EventHandler<Enums.CellState> CellStateChanged;

        public event EventHandler CellClicked;

        public MineButton()
        {
            InitializeComponent();
            btnImg = new Image();
            state = Enums.CellState.Covered;
            this.AddChild(btnImg);
            ChangeImgByState();
            
            
        }
        private bool isTwoButtonNeighbor(MineButton btn1, MineButton btn2)
        {
            if (btn1 == btn2)
            {
                return false;
            }
            if (Math.Abs(btn1.posX - btn2.posX) <= 1 && Math.Abs(btn1.posY - btn2.posY) <= 1)
            {
                return true;
            }
            return false;
        }
        private void MineButton_NumCellRightMouseBtnDown(object? sender, EventArgs e)
        {
            var mineButton = (MineButton)sender;
            if (isTwoButtonNeighbor(this, mineButton))
            {
                //Trigger a animation
                DoubleAnimation doubleAnimation = new DoubleAnimation();
                //
            }
        }

        public void ChangeImgByState()
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
                            case Enums.CellContent.Mine:  btnImg.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/mine.png")); break;
                        }
                        break;
                    }
                default : return;
            }
            
        }
        
        protected override void OnMouseRightButtonDown(MouseButtonEventArgs e)
        {
            if (State == Enums.CellState.Uncovered && content >= Enums.CellContent.Num_0 && content <= Enums.CellContent.Num_8)
            {
                RaiseEvent(new RoutedEventArgs(NumCellRightMouseBtnDownEvent));
                
            }



        }
        protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            if (State == Enums.CellState.Covered)
            {
                State = Enums.CellState.Flagged;
            }
            else if (State == Enums.CellState.Flagged)
            {
                State = Enums.CellState.Covered;
            }
            else if (State == Enums.CellState.Uncovered)
            {
                CellClicked.Invoke(this, new EventArgs());
            }
        }
        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonUp(e);
            CellClicked.Invoke(this, new EventArgs());
        }

        private void Button_MouseEnter(object sender, MouseEventArgs e)
        {
            if(state == Enums.CellState.Covered)
            {
                DropShadowEffect dropShadowEffect = new DropShadowEffect();
                dropShadowEffect.Color = Colors.Black;
                dropShadowEffect.Direction = 320;
                dropShadowEffect.ShadowDepth = 5;
                dropShadowEffect.BlurRadius = 30;
                btnImg.Effect  = dropShadowEffect;
                
            }
        }

        private void Button_MouseLeave(object sender, MouseEventArgs e)
        {
            //if (state == Enums.CellState.Covered)
            {
                btnImg.Effect = null;

            }
        }
    }
}
