using System;
using System.Windows;
using System.Windows.Input;
using ZTP.GameSingleton;

namespace ZTP
{
    public partial class MainWindow : Window
    {
        Game g;
        public MainWindow()
        {
            InitializeComponent();
            Application.Current.MainWindow.WindowState = WindowState.Maximized;
            g = Game.GetInstance(myCanvas, mainGrid);
            Game g2;
            g2 = Game.GetInstance(myCanvas, mainGrid);
            if (g != g2)
            {
                throw new Exception();
            }
        }
        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            g.KeyIsDown(sender, e);
        }

        private void KeyIsUp(object sender, KeyEventArgs e)
        {
            g.KeyIsUp(sender, e);
        }
    }
}
