﻿using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using ZTP.Actions;

namespace ZTP.Monsters
{
    public class Monster : ICustomObserver
    {
        public Monster(int hp, Image image, string tag, int heigh, int width, int speed, int damage, List<string> images)
        {
            this.Speed = speed;
            this.Damage = damage;
            this.HitPoints = hp;
            this.Images = images;
            this.Instance = new Rectangle
            {
                Name = "enemy",
                Tag = tag,
                Height = heigh,
                Width = width,
                Fill = new VisualBrush(image)
            };
        }
        public int HitPoints { get; set; }
        public Rectangle Instance { get; set; }
        public int Speed { get; set; }
        public int Damage { get; set; }
        public List<string> Images { get; }
        public int Direction { get; set; }

        public void Update()
        {

            this.Speed = 0;
        }
    }
}
