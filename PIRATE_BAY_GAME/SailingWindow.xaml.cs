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
    /// <summary>
    /// Логика взаимодействия для SailingWindow.xaml
    /// </summary>
    public partial class SailingWindow : Window
    {
        System.Timers.Timer animTimer;

        SailingClass aboutSailing = new SailingClass();
        public SailingWindow()
        {
            InitializeComponent();

            List<Canvas> gunListTop = new List<Canvas>(6);
            gunListTop.Add(gun0);
            gunListTop.Add(gun1);
            gunListTop.Add(gun2);
            gunListTop.Add(gun3);
            gunListTop.Add(gun4);
            gunListTop.Add(gun5);


            List<Canvas> gunListBottom = new List<Canvas>(3);
            gunListBottom.Add(gun00);
            gunListBottom.Add(gun01);
            gunListBottom.Add(gun02);

            aboutSailing.GetComponents(myShipCanvas, enemyShipCanvas, bottleCanvas, gunListTop,gunListBottom);

            bottleCanvas.Margin = new Thickness(-30, 0, 0, 0);
            animTimer = new System.Timers.Timer(500);
            animTimer.Elapsed += animTimer_Elapsed;
            animTimer.Start();

            aboutSailing.SendBottle();
        }


        void animTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                this.Dispatcher.Invoke(() =>
                    {
                        aboutSailing.Animate();

                    });
            }
            catch (Exception) { }
        }

        public void MyShipAppeare()
        {
            aboutSailing.AppeareShip();
        }

        private void bottleCanvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            aboutSailing.StopBottleMoove();
        }
    }
}
