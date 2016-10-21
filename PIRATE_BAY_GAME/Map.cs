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
    
    static class MyResources
    {
        static public Label goldLbl;
        static public Label cornLbl;
        static public Label snacksLbl;
        static public Label coresLbl;
        static public Label armorLbl;

        static private BitmapImage resourcesPannel_IMG = new BitmapImage(new Uri("IMGs\\Resources\\resourcesPannel.png", UriKind.Relative));
        static private ImageBrush resourcesPannel_B = new ImageBrush(resourcesPannel_IMG);

        static private int gold { get; set; }

         static private int corn { get; set; }
         static  private int snacks { get; set; }
         static private int armor { get; set; }
         static private int cores { get; set; }

        #region Check Resources
         static public int CheckGold()
        {
            return gold;
        }
         static public int CheckCorn()
        {
            return corn;
        }
         static public int CheckSnacks()
        {
            return snacks;
        }
         static public int CheckArmor()
        {
            return armor;
        }
         static public int CheckCores()
        {
            return cores;
        }
        #endregion

        #region Chabge Resources
         static public void ChangeGoldValue(int n)
        {
            gold += n;
            goldLbl.Content = gold;
        }
         static public void ChangeCornValue(int n)
        {
            try
            {
                cornLbl.Dispatcher.Invoke(() =>
                    {
                        corn += n;
                        cornLbl.Content = corn;
                    });
            }
            catch (Exception) { }
        }
         static public void ChangeSnacksValue(int n)
        {
            snacks += n;
            snacksLbl.Content = snacks;
        }
         static public void ChangeArmorValue(int n)
        {
            armor += n;
            armorLbl.Content = armor;
        }
         static public void ChangeCoresValue(int n)
        {
            cores += n;
            coresLbl.Content = cores;
        }
        #endregion
         static public void SetBackground(Canvas canvas)
        {
            canvas.Background = resourcesPannel_B;
        }

    }
}
