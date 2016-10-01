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
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MAIN go = new MAIN();
        System.Timers.Timer timer;
        
        public MainWindow()
        {
            InitializeComponent();
            go.StartPreparation(resourcesCanvas, shipCanvas, mapCanvas, sailButton, MessageCanvas, newGoldL, armorSpentL, coresSpentL, repairBtn,goldLbl,cornLbl,snacksLbl,armorLbl,coresLbl);
            timer = new System.Timers.Timer(500);
            timer.Elapsed += timer_Elapsed;
            timer.Start();
            
            
        }

        private void timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                this.Dispatcher.Invoke(() =>
            {
                go.TimeTick();
                if (MessageCanvas.Visibility == System.Windows.Visibility.Visible)
                    timer.Stop();
                
            });
            }
            catch (Exception)
            { }
        }

        private void showRightPannelButton_Click(object sender, RoutedEventArgs e)
        {
            rightPannelCanvas.Visibility = System.Windows.Visibility.Visible;
            showRightPannelButton.Visibility = System.Windows.Visibility.Hidden;
            hideRightPannelButton.Visibility = System.Windows.Visibility.Visible;
            timer.Stop();
            go.StopSailing();
            go.StopRepairing();

        }

        private void hideRightPannelButton_Click(object sender, RoutedEventArgs e)
        {
            rightPannelCanvas.Visibility = System.Windows.Visibility.Hidden;
            showRightPannelButton.Visibility = System.Windows.Visibility.Visible;
            hideRightPannelButton.Visibility = System.Windows.Visibility.Hidden;
            timer.Start();
            go.ContinueSalling();
            go.ContinueRepairing();
        }

        private void sailButton_Click(object sender, RoutedEventArgs e)
        {
            go.StartSailShip();
        }

        private void closeMessageCanvas_Click(object sender, RoutedEventArgs e)
        {
            MessageCanvas.Visibility = System.Windows.Visibility.Hidden;
            timer.Start();
        }

        private void repairBtn_Click(object sender, RoutedEventArgs e)
        {
            go.RepairBtnClick();
        }
    }
}
