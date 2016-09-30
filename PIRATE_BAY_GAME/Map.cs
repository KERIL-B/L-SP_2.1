﻿using System;
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

namespace PIRATE_BAY_GAME
{
    class Map
    {
        private static BitmapImage map_1_img = new BitmapImage(new Uri("IMGs\\map\\map_1.png", UriKind.Relative));
        private static BitmapImage map_2_img = new BitmapImage(new Uri("IMGs\\map\\map_2.png", UriKind.Relative));
        private ImageBrush map_1_Brush = new ImageBrush(map_1_img);
        private ImageBrush map_2_Brush = new ImageBrush(map_2_img);

        private bool status = true;

        public void AnimateMap(Canvas canvas)
        {
            if (status)
                canvas.Background = map_1_Brush;
            else
                canvas.Background = map_2_Brush;
            status = !status;
        }
    }
    class Ship
    {
        private static BitmapImage ship_1_img = new BitmapImage(new Uri("IMGs\\ship_1.png", UriKind.Relative));
        private static BitmapImage ship_2_img = new BitmapImage(new Uri("IMGs\\ship_2.png", UriKind.Relative));
        private ImageBrush ship_1_Brush = new ImageBrush(ship_1_img);
        private ImageBrush ship_2_Brush = new ImageBrush(ship_2_img);

        public int armor { get; set; }
        public int cores { get; set; }
        public int hp { get; set; }

        private bool status = true;
        public void checkShipCondition(Canvas canvas)
        {
            DrawHp(canvas);
        }

        private void DrawHp(Canvas canvas)
        {
            canvas.Children.Clear();

            Rectangle healthBarCircuit = new Rectangle();
            healthBarCircuit.Width = 102;
            healthBarCircuit.Height = 21;
            healthBarCircuit.Stroke = new SolidColorBrush(Colors.Black);
            healthBarCircuit.StrokeThickness = 1;
            healthBarCircuit.Margin = new Thickness(10, 2, 0, 0);
            canvas.Children.Add(healthBarCircuit);

            Rectangle hp = new Rectangle();
            hp.Height = 13;
            hp.Width = this.hp;
            hp.StrokeThickness = 0;
            hp.Margin = new Thickness(11, 3, 0, 0);
            if (this.hp > 70)
                hp.Fill = new SolidColorBrush(Colors.LightGreen);
            else
                if (this.hp > 30)
                    hp.Fill = new SolidColorBrush(Colors.Yellow);
                else hp.Fill = new SolidColorBrush(Colors.Crimson);

            Rectangle armor = new Rectangle();
            armor.Height = 3;
            armor.Width = this.armor * 10;
            armor.StrokeThickness = 0;
            armor.Margin = new Thickness(11, 16, 0, 0);
            armor.Fill = Brushes.Silver;

            Rectangle cores = new Rectangle();
            cores.Height = 3;
            cores.Width = this.cores * 10;
            cores.StrokeThickness = 0;
            cores.Margin = new Thickness(11, 19, 0, 0);
            cores.Fill = Brushes.Firebrick;

            canvas.Children.Add(hp);
            canvas.Children.Add(cores);
            canvas.Children.Add(armor);
        }

        public void animateShip(Canvas canvas)
        {
            if (status)
                canvas.Background = ship_2_Brush;
            else
                canvas.Background = ship_1_Brush;
            status = !status;
        }
    }

    class Resources
    {
        private int gold { get; set; }

        public void SetGold(int n,Canvas canvas)
        {
            gold = n;
            DrawGold(canvas);
        }
        private void DrawGold(Canvas canvas)
        {
            canvas.Children.Clear();
            TextBlock tb = new TextBlock();
            tb.Text = gold + "";
            tb.FontSize = 20;
            tb.Margin = new Thickness(230, 1, 0, 0);
            canvas.Children.Add(tb);
        }
        public void AddGold(int n,Canvas canvas)
        {
            gold += n;
            DrawGold(canvas);
        }
    }
}
