using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

using System.Windows.Threading;
using ZTP.Actions;
using ZTP.EnemyAttacks;
using ZTP.GameSingleton;
using ZTP.Images;
using ZTP.Monsters;
using ZTP.PlayerClassess;
using ZTP.Spells;

namespace ZTP
{
    public partial class MainWindow : Window
    {
        Game g;
        public MainWindow()
        {
            InitializeComponent();
            g = Game.GetInstance(myCanvas);
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
