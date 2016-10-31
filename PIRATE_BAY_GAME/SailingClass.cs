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
using System.Windows.Shapes;

namespace PIRATE_BAY_GAME
{
    class SailingClass
    {


        Random rand = new Random();
        private static BitmapImage my_ship_1_img = new BitmapImage(new Uri("IMGs\\ship_1.png", UriKind.Relative));
        private static BitmapImage my_ship_2_img = new BitmapImage(new Uri("IMGs\\ship_2.png", UriKind.Relative));
        private static ImageBrush my_ship_1_Brush = new ImageBrush(my_ship_1_img);
        private static ImageBrush my_ship_2_Brush = new ImageBrush(my_ship_2_img);

        private static BitmapImage EnShip_1_img = new BitmapImage(new Uri("IMGs\\ship_1.png", UriKind.Relative));
        private static BitmapImage EnShip_2_img = new BitmapImage(new Uri("IMGs\\ship_2.png", UriKind.Relative));
        private static ImageBrush EnShip_1_Brush = new ImageBrush(EnShip_1_img);
        private static ImageBrush EnShip_2_Brush = new ImageBrush(EnShip_2_img);

        System.Timers.Timer bottleMooveTimer;
        System.Timers.Timer appeareTimer;
        System.Timers.Timer fightTimer;

        Canvas myShipCanvas;
        List<Canvas> bottomGunList = new List<Canvas>(3);
        Canvas enemyShipCanvas;
        List<Canvas> topGunList = new List<Canvas>(6);
        Canvas bottleCanvas;

        MyShip MyShip = new MyShip(my_ship_1_Brush, my_ship_2_Brush, null);
        EnemyShip Enemy;

        bool isMyShip = true;
        bool bottleAnim = true;
        int leftBottleMargin = -30;
        int topMargin;
        int timeValue = 0;
        int timeEndFight;
        int k = 0;

        public void AppeareShip()
        {
            isMyShip = true;
            appeareTimer = new System.Timers.Timer(30);
            appeareTimer.Elapsed += appeareTimer_Elapsed;
            appeareTimer.Start();
            myShipCanvas.Margin = new Thickness(228, 330, 0, 0);
        }

        void appeareTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            timeValue++;
            try
            {
                if (isMyShip)
                {
                    myShipCanvas.Dispatcher.Invoke(() =>
                 {
                     myShipCanvas.Margin = new Thickness(228 - timeValue, 350, 0, 0);
                     if (myShipCanvas.Margin.Left == 39)
                     {
                         appeareTimer.Stop();
                         GenerateEnemy();
                     }
                 });
                }
                else
                {
                    enemyShipCanvas.Dispatcher.Invoke(() =>
                        {
                            enemyShipCanvas.Margin = new Thickness(-190 + timeValue, 17, 0, 0);
                            if (enemyShipCanvas.Margin.Left == 5)
                            {
                                appeareTimer.Stop();
                                Fight();
                            }
                        });
                }
            }
            catch (Exception) { }
        }

        private void Fight()
        {
            fightTimer = new System.Timers.Timer(400);
            fightTimer.Elapsed += fightTimer_Elapsed;
            fightTimer.Start();
            timeValue = 0;

        }

        void fightTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            for (int i = 0; i < topGunList.Count; i++)
            {
                try
                {
                    if (i < 3)
                    {
                        bottomGunList[i].Dispatcher.Invoke(() =>
                            {
                                bottomGunList[i].Background = null;
                            });
                    }

                    topGunList[i].Dispatcher.Invoke(() =>
                    {
                        topGunList[i].Background = null;
                    });
                }
                catch (Exception) { }
            }
            timeValue++;
            int chance1 = rand.Next(5);
            int chance2 = rand.Next(10);
            if (Enemy.isAlive)
            {
                if (chance1 == 0)
                {
                    //My
                    Shoot(true);

                }
                if (chance2 == 0)
                {
                    //Enemy
                    Shoot(false);
                }
            }
            else
            {
                if (k == 0)
                    timeEndFight = timeValue + 5;
                k++;

            }
            if (timeEndFight == timeValue)
                EndFight();
        }

        private void Damage(bool down)
        {
            int missChance = rand.Next(5);
            if (missChance > 0)
            {
                if (down) //MyShip damage Enemy
                {
                    int damageRez = 0;
                    double damage = (MyResources.MyShipCores - Enemy.Armor());
                    damage = (damage > 0) ? (damage + rand.Next(10)) : (rand.Next(8));

                    if (Enemy.Armor() > 0)
                    {

                        damageRez = -(int)damage;
                        Enemy.Armor(-(rand.Next(3) + 1), enemyShipCanvas);
                    }
                    else
                    {
                        damage = damage * (1.5 + rand.Next(3) * 0.5);
                        damageRez = -(int)damage;
                    }
                    Enemy.Hp(damageRez, enemyShipCanvas);
                }
                else
                {
                    int damageRez = 0;
                    double damage;
                    damage = (Enemy.cores - MyResources.MyShipArmor);
                    damage = (damage > 0) ? (damage + rand.Next(10)) : (rand.Next(5));
                    if (MyResources.MyShipArmor > 0)
                    {
                        damageRez = -(int)damage;
                        int chance = rand.Next(3);
                        if (chance == 0)
                            MyResources.MyShipArmor -= rand.Next(1);
                        MyResources.MyShipArmor = (MyResources.MyShipArmor > 0) ? (MyResources.MyShipArmor) : (0);
                        MyShip.checkShipCondition(myShipCanvas);
                    }
                    else
                    {
                        damage = damage * 2;
                        damageRez = -(int)damage;
                    }
                    MyResources.MyShipHp += damageRez;
                    MyResources.MyShipHp = (MyResources.MyShipHp > 0) ? (MyResources.MyShipHp) : (1);
                    if (MyResources.MyShipHp == 1)
                        System.Windows.MessageBox.Show("БЕГСТВО");
                }
            }
        }

        private void Shoot(bool downShooting)
        {
            if (downShooting)
            {
                int chance = rand.Next(5);
                MyResources.MyShipCores -= (chance == 0) ? (1) : (0);
                MyResources.MyShipCores = (MyResources.MyShipCores >= 0) ? (MyResources.MyShipCores) : (0);
                MyShip.DrawHp(myShipCanvas);

                int x = rand.Next(3);
                try
                {
                    bottomGunList[x].Dispatcher.Invoke(() =>
                    {
                        bottomGunList[x].Background = new ImageBrush(new BitmapImage(new Uri("IMGs\\Fight\\smoke_down.png", UriKind.Relative)));
                    });
                }
                catch (Exception) { }
            }
            else
            {
                int x = rand.Next(7);
                try
                {
                    topGunList[x].Dispatcher.Invoke(() =>
                    {
                        topGunList[x].Background = new ImageBrush(new BitmapImage(new Uri("IMGs\\Fight\\smoke_up.png", UriKind.Relative)));
                    });
                }
                catch (Exception) { }
            }
            Damage(downShooting);
        }

        private void EndFight()
        {
            fightTimer.Stop();
            try
            {
                enemyShipCanvas.Dispatcher.Invoke(() =>
                {
                    enemyShipCanvas.Margin = new Thickness(-190, 17, 0, 0);
                });
            }
            catch (Exception) { }
        }

        private void GenerateEnemy()
        {
            int chance = rand.Next(10);

            if (chance > 0)
            {
                int chance2 = 0;

                string path0 = "IMGs\\Ship\\" + chance2 + "_0.png";
                string path1 = "IMGs\\Ship\\" + chance2 + "_1.png";
                string path2 = "IMGs\\Ship\\" + chance2 + "_2.png";

                ImageBrush enemy_ship_0_Brush = new ImageBrush(new BitmapImage(new Uri(path0, UriKind.Relative)));
                ImageBrush enemy_ship_1_Brush = new ImageBrush(new BitmapImage(new Uri(path1, UriKind.Relative)));
                ImageBrush enemy_ship_2_Brush = new ImageBrush(new BitmapImage(new Uri(path2, UriKind.Relative)));

                Enemy = new EnemyShip(enemy_ship_1_Brush, enemy_ship_2_Brush, enemy_ship_0_Brush);

                EnemyAppeare();
            }
        }

        private void EnemyAppeare()
        {
            isMyShip = false;
            timeValue = 0;
            appeareTimer.Start();
        }

        public void GetComponents(Canvas myShipCanvas, Canvas enemyShipCanvas, Canvas bottleCanvas, List<Canvas> topGunList, List<Canvas> bottomGunList)
        {
            this.myShipCanvas = myShipCanvas;
            this.bottomGunList = bottomGunList;
            this.enemyShipCanvas = enemyShipCanvas;
            this.topGunList = topGunList;
            this.bottleCanvas = bottleCanvas;


        }

        public void Animate()
        {
            MyShip.animateShip(myShipCanvas);
            BottleAnim();
            if (Enemy != null)
                Enemy.animateShip(enemyShipCanvas);
        }

        #region BOTTLE
        public void SendBottle()
        {
            int chance = rand.Next(3);
            if (chance == 0)
                BottleMoovment();
        }

        private void BottleMoovment()
        {
            bottleCanvas.Visibility = System.Windows.Visibility.Visible;
            const int top = 130;
            const int range = 290 - top;
            topMargin = rand.Next(range) + top;
            leftBottleMargin = -30;
            bottleMooveTimer = new System.Timers.Timer(40);
            bottleMooveTimer.Elapsed += bottleMooveTimer_Elapsed;
            bottleMooveTimer.Start();
        }

        public void StopBottleMoove()
        {
            try
            {
                bottleCanvas.Dispatcher.Invoke(() =>
                    {
                        if (bottleCanvas.Visibility != System.Windows.Visibility.Hidden)
                            bottleCanvas.Visibility = System.Windows.Visibility.Hidden;
                    });
            }
            catch (Exception) { }
            bottleMooveTimer.Stop();
            leftBottleMargin = -30;
        }

        void bottleMooveTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            leftBottleMargin++;
            try
            {
                bottleCanvas.Dispatcher.Invoke(() =>
                {
                    bottleCanvas.Margin = new Thickness(leftBottleMargin, topMargin, 0, 0);
                });
            }
            catch (Exception) { }
            if (leftBottleMargin == 200)
            {
                StopBottleMoove();
            }
        }

        private void BottleAnim()
        {
            BitmapImage bottle_1_img = new BitmapImage(new Uri("IMGs\\bottle\\b1.png", UriKind.Relative));
            BitmapImage bottle_2_img = new BitmapImage(new Uri("IMGs\\bottle\\b2.png", UriKind.Relative));
            ImageBrush bottle_1_Brush = new ImageBrush(bottle_1_img);
            ImageBrush bottle_2_Brush = new ImageBrush(bottle_2_img);
            bottleCanvas.Background = (bottleAnim) ? (bottle_1_Brush) : (bottle_2_Brush);
            bottleAnim = !bottleAnim;
        }
        #endregion
    }
}
