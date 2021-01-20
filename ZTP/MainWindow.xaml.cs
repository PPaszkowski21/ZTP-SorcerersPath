﻿using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interop;
using ZTP.GameSingleton;

namespace ZTP
{
    public partial class MainWindow : Window
    {
        Game g;
        public MainWindow()
        {
            InitializeComponent();
            g = Game.GetInstance(myCanvas, mainGrid,1);
            g.Start();

            //Game g2;
            //g2 = Game.GetInstance(myCanvas, mainGrid, 2);
            //if (g == g2)
            //{
            //    throw new Exception();
            //}
        }
        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                g.ChangeStage(Game.ActualStage + 1);
                g.Start();
            };
            g.KeyIsDown(sender, e);
        }

        private void KeyIsUp(object sender, KeyEventArgs e)
        {
            g.KeyIsUp(sender, e);
        }
    }
}
