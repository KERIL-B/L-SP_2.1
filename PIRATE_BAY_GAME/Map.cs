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
        public Ship(ImageBrush ship_1_Brush, ImageBrush ship_2_Brush, ImageBrush ship_0_Brush)
        {
            this.ship_0_Brush = ship_0_Brush;
            this.ship_1_Brush = ship_1_Brush;
            this.ship_2_Brush = ship_2_Brush;
        }

        protected ImageBrush ship_0_Brush;
        protected ImageBrush ship_1_Brush;
        protected ImageBrush ship_2_Brush;

        private bool status = true;

        public void checkShipCondition(Canvas canvas)
        {
            DrawHp(canvas);
        }

        public virtual void DrawHp(Canvas canvas)
        {
            try
            {
                canvas.Dispatcher.Invoke(() =>
                    {
                        canvas.Children.Clear();
                        double leftMargin = canvas.Width / 2 - 50;

                        Rectangle healthBarCircuit = new Rectangle();
                        healthBarCircuit.Width = 102;
                        healthBarCircuit.Height = 21;
                        healthBarCircuit.Stroke = new SolidColorBrush(Colors.Black);
                        healthBarCircuit.StrokeThickness = 1;
                        healthBarCircuit.Margin = new Thickness(leftMargin, 2, 0, 0);
                        canvas.Children.Add(healthBarCircuit);
                    });
            }
            catch (Exception)
            { }
        }

        public void animateShip(Canvas canvas)
        {
            if (status)
                canvas.Background = ship_2_Brush;
            else
                canvas.Background = ship_1_Brush;
            status = !status;
            DrawHp(canvas);
        }
    }

    class MyShip : Ship
    {
        public MyShip(ImageBrush ship_1_Brush, ImageBrush ship_2_Brush, ImageBrush ship_0_Brush)
            : base(ship_1_Brush, ship_2_Brush, ship_0_Brush)
        {
        }

        public override void DrawHp(Canvas canvas)
        {
            base.DrawHp(canvas);
            try
            {
                canvas.Dispatcher.Invoke(() =>
                    {
                        double leftMargin = canvas.Width / 2 - 50;
                        Rectangle hp = new Rectangle();
                        hp.Height = 13;
                        hp.Width = MyResources.MyShipHp;
                        hp.StrokeThickness = 0;
                        hp.Margin = new Thickness(leftMargin + 1, 3, 0, 0);
                        if (MyResources.MyShipHp > 70)
                            hp.Fill = new SolidColorBrush(Colors.LightGreen);
                        else
                            if (MyResources.MyShipHp > 30)
                                hp.Fill = new SolidColorBrush(Colors.Yellow);
                            else hp.Fill = new SolidColorBrush(Colors.Crimson);

                        Rectangle armor = new Rectangle();
                        armor.Height = 3;
                        armor.Width = MyResources.MyShipArmor * 10;
                        armor.StrokeThickness = 0;
                        armor.Margin = new Thickness(leftMargin + 1, 16, 0, 0);
                        armor.Fill = Brushes.Silver;

                        Rectangle cores = new Rectangle();
                        cores.Height = 3;
                        cores.Width = MyResources.MyShipCores * 10;
                        cores.StrokeThickness = 0;
                        cores.Margin = new Thickness(leftMargin + 1, 19, 0, 0);
                        cores.Fill = Brushes.Firebrick;

                        canvas.Children.Add(hp);
                        canvas.Children.Add(cores);
                        canvas.Children.Add(armor);
                    });
            }
            catch (Exception) { }
        }
    }

    class EnemyShip : Ship
    {
        Random rand = new Random();

        public bool isAlive { get; set; }
        private int hp { get; set; }
        private int armor { get; set; }
        public int cores { get; set; }


        public void Hp(int hp, Canvas canvas)
        {
            this.hp += hp;
            if (this.hp <= 0)
            {
                this.hp = 0;
                base.ship_1_Brush = base.ship_0_Brush;
                base.ship_2_Brush = base.ship_0_Brush;
                isAlive = false;
            }
            base.checkShipCondition(canvas);
        }
      
        public int Hp()
        {
            return this.hp;
        }

        public void Armor(int armor, Canvas canvas)
        {
            this.armor += armor;
            this.armor=(this.armor<0)?(0):(this.armor);
            base.checkShipCondition(canvas);
        }
        public int Armor()
        {
            return this.armor;
        }

        public EnemyShip(ImageBrush ship_1_Brush, ImageBrush ship_2_Brush, ImageBrush ship_0_Brush)
            : base(ship_1_Brush, ship_2_Brush, ship_0_Brush)
        {
            isAlive = true;
            this.armor = rand.Next(10) + 1;
            this.cores = rand.Next(10) + 1;
            this.hp = rand.Next(100) + 1;
        }

        public override void DrawHp(Canvas canvas)
        {
            base.DrawHp(canvas);
            try
            {
                canvas.Dispatcher.Invoke(() =>
                    {
                        double leftMargin = canvas.Width / 2 - 50;

                        Rectangle hp = new Rectangle();
                        hp.Height = 13;
                        hp.Width = this.hp;
                        hp.StrokeThickness = 0;
                        hp.Margin = new Thickness(leftMargin + 1, 3, 0, 0);
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
                        armor.Margin = new Thickness(leftMargin + 1, 16, 0, 0);
                        armor.Fill = Brushes.Silver;

                        Rectangle cores = new Rectangle();
                        cores.Height = 3;
                        cores.Width = this.cores * 10;
                        cores.StrokeThickness = 0;
                        cores.Margin = new Thickness(leftMargin + 1, 19, 0, 0);
                        cores.Fill = Brushes.Firebrick;

                        canvas.Children.Add(hp);
                        canvas.Children.Add(cores);
                        canvas.Children.Add(armor);
                    });
            }
            catch (Exception) { }
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

        static public int MyShipHp { get; set; }
        static public int MyShipArmor { get; set; }
        static public int MyShipCores { get; set; }

        static private int gold { get; set; }
        static private int corn { get; set; }
        static private int snacks { get; set; }
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

        #region Change Resources
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
            try
            {
                snacksLbl.Dispatcher.Invoke(() =>
                    {
                        snacks += n;
                        snacksLbl.Content = snacks;
                    });
            }
            catch (Exception) { }
        }
        static public void ChangeArmorValue(int n)
        {
            try
            {
                armorLbl.Dispatcher.Invoke(() =>
                    {
                        armor += n;
                        armorLbl.Content = armor;
                    });
            }
            catch (Exception) { }
        }
        static public void ChangeCoresValue(int n)
        {
            try
            {
                coresLbl.Dispatcher.Invoke(() =>
                    {
                        cores += n;
                        coresLbl.Content = cores;
                    });
            }
            catch (Exception) { }
        }
        #endregion

        static public void SetBackground(Canvas canvas)
        {
            canvas.Background = resourcesPannel_B;
        }

    }
}
