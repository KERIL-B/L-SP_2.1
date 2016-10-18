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
        private SolidColorBrush brActive = new SolidColorBrush(Colors.ForestGreen);
        private SolidColorBrush brPassive = new SolidColorBrush(Color.FromRgb(94, 179, 83));
        private bool isRightPannelActive { get; set; }
        private bool repairBtnClicked { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            go.StartPreparation(resourcesCanvas, shipCanvas, mapCanvas, sailButton, MessageCanvas, newGoldL, armorSpentL, coresSpentL, repairBtn, goldLbl, cornLbl, snacksLbl, armorLbl, coresLbl);
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

                go.CheckDefferedSail();

                if (repairBtnClicked)
                {
                    go.RepairBtnClick();
                    repairBtnClicked = false;
                }
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
            cells.IsEnabled = false;
            isRightPannelActive = true;

        }

        private void hideRightPannelButton_Click(object sender, RoutedEventArgs e)
        {
            cells.IsEnabled = true;
            rightPannelCanvas.Visibility = System.Windows.Visibility.Hidden;
            showRightPannelButton.Visibility = System.Windows.Visibility.Visible;
            hideRightPannelButton.Visibility = System.Windows.Visibility.Hidden;
            timer.Start();
            go.ContinueSalling();
            go.ContinueRepairing();
            isRightPannelActive = false;
        }

        private void sailButton_Click(object sender, RoutedEventArgs e)
        {
            if (!repairBtnClicked)
            {
                if (!isRightPannelActive)
                    go.StartSailShip();
                else
                    go.DefferedStartSailShip();
            }

        }

        private void closeMessageCanvas_Click(object sender, RoutedEventArgs e)
        {
            MessageCanvas.Visibility = System.Windows.Visibility.Hidden;
            timer.Start();
        }

        private void repairBtn_Click(object sender, RoutedEventArgs e)
        {
            repairBtnClicked = true;

        }

        private void howToPlay_Btn_Click(object sender, RoutedEventArgs e)
        {
            go.HowToPlay();
        }

        #region

        private void cell_0_0_MouseEnter(object sender, MouseEventArgs e)
        {
            cell_0_0.Background = brActive;
        }

        private void cell_0_0_MouseLeave(object sender, MouseEventArgs e)
        {
            cell_0_0.Background = brPassive;
        }

        private void cell_0_1_MouseEnter(object sender, MouseEventArgs e)
        {
            cell_0_1.Background = brActive;
        }

        private void cell_0_1_MouseLeave(object sender, MouseEventArgs e)
        {
            cell_0_1.Background = brPassive;
        }

        private void cell_0_2_MouseEnter(object sender, MouseEventArgs e)
        {
            cell_0_2.Background = brActive;
        }

        private void cell_0_2_MouseLeave(object sender, MouseEventArgs e)
        {
            cell_0_2.Background = brPassive;
        }

        private void cell_1_0_MouseEnter(object sender, MouseEventArgs e)
        {
            cell_1_0.Background = brActive;
        }

        private void cell_1_0_MouseLeave(object sender, MouseEventArgs e)
        {
            cell_1_0.Background = brPassive;
        }

        private void cell_1_1_MouseEnter(object sender, MouseEventArgs e)
        {
            cell_1_1.Background = brActive;
        }

        private void cell_1_1_MouseLeave(object sender, MouseEventArgs e)
        {
            cell_1_1.Background = brPassive;
        }

        private void cell_1_2_MouseEnter(object sender, MouseEventArgs e)
        {
            cell_1_2.Background = brActive;
        }

        private void cell_1_2_MouseLeave(object sender, MouseEventArgs e)
        {
            cell_1_2.Background = brPassive;
        }

        private void cell_2_0_MouseEnter(object sender, MouseEventArgs e)
        {
            cell_2_0.Background = brActive;
        }

        private void cell_2_0_MouseLeave(object sender, MouseEventArgs e)
        {
            cell_2_0.Background = brPassive;
        }

        private void cell_2_1_MouseEnter(object sender, MouseEventArgs e)
        {
            cell_2_1.Background = brActive;
        }

        private void cell_2_1_MouseLeave(object sender, MouseEventArgs e)
        {
            cell_2_1.Background = brPassive;
        }

        private void cell_2_2_MouseEnter(object sender, MouseEventArgs e)
        {
            cell_2_2.Background = brActive;
        }

        private void cell_2_2_MouseLeave(object sender, MouseEventArgs e)
        {
            cell_2_2.Background = brPassive;
        }
        #endregion
    }
}
