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
        MAIN aboutShip = new MAIN();
        MAIN_2 aboutBuildings = new MAIN_2();

        System.Timers.Timer timer;

        #region imgs
        private static BitmapImage cornFarmIcon_img = new BitmapImage(new Uri("IMGs\\Buildings\\Farm\\Description.png", UriKind.Relative));
        private ImageBrush cornFarmIcon_Brush = new ImageBrush(cornFarmIcon_img);
        private static BitmapImage kitchenIcon_img = new BitmapImage(new Uri("IMGs\\Buildings\\Kitchen\\Description.png", UriKind.Relative));
        private ImageBrush kitchenIcon_Brush = new ImageBrush(kitchenIcon_img);
        private static BitmapImage armoryIcon_img = new BitmapImage(new Uri("IMGs\\Buildings\\Armory\\Description.png", UriKind.Relative));
        private ImageBrush armoryIcon_Brush = new ImageBrush(armoryIcon_img);
        private static BitmapImage foundryIcon_img = new BitmapImage(new Uri("IMGs\\Buildings\\Foundry\\Description.png", UriKind.Relative));
        private ImageBrush foundryIcon_Brush = new ImageBrush(foundryIcon_img);

        private static BitmapImage messageBG_IMG = new BitmapImage(new Uri("IMGs\\Messages\\messageBackground_ noGold.png", UriKind.Relative));
        private ImageBrush messageBG_B = new ImageBrush(messageBG_IMG);
        private static BitmapImage messageBG_B_IMG = new BitmapImage(new Uri("IMGs\\Messages\\messageBackground_build.png", UriKind.Relative));
        private ImageBrush messageBG_B_B = new ImageBrush(messageBG_B_IMG);

        private static BitmapImage storyCnvs_IMG = new BitmapImage(new Uri("IMGs\\Story\\none.png", UriKind.Relative));
        private ImageBrush storyCnvs_B = new ImageBrush(storyCnvs_IMG);
        #endregion

        private bool isRightPannelActive { get; set; }
        private bool repairBtnClicked { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            StoryCanvas.Background = storyCnvs_B;
            cornFarmCnvs.Background = cornFarmIcon_Brush;
            kitchenCnvs.Background = kitchenIcon_Brush;
            armoryCnvs.Background = armoryIcon_Brush;
            foundryCnvs.Background = foundryIcon_Brush;

            BuildingCanvas.Background = messageBG_B_B;

            aboutShip.StartPreparation(resourcesCanvas, shipCanvas, mapCanvas, sailButton, MessageCanvas, newGoldL, armorSpentL, coresSpentL, repairBtn, goldLbl, cornLbl, snacksLbl, armorLbl, coresLbl);
            aboutBuildings.StartPreparation(cell_0_0, cell_0_1, cell_0_2, cell_1_0, cell_1_1, cell_1_2, cell_2_0, cell_2_1, cell_2_2);
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
                if (rightPannelCanvas.Visibility == System.Windows.Visibility.Hidden)
                {
                    #region Ship/map
                    aboutShip.TimeTick();

                    if (MessageCanvas.Visibility == System.Windows.Visibility.Visible)
                        timer.Stop();

                    aboutShip.CheckDefferedSail();

                    if (repairBtnClicked)
                    {
                        aboutShip.RepairBtnClick();
                        repairBtnClicked = false;
                    }
                    #endregion
                    #region Buildings

                    aboutBuildings.animateBuildings();

                    #endregion
                }
            });
            }
            catch (Exception)
            { }
        }

        private void showRightPannelButton_Click(object sender, RoutedEventArgs e)
        {
            aboutBuildings.TimeStop();
            rightPannelCanvas.Visibility = System.Windows.Visibility.Visible;
            showRightPannelButton.Visibility = System.Windows.Visibility.Hidden;
            hideRightPannelButton.Visibility = System.Windows.Visibility.Visible;
            timer.Stop();
            aboutShip.StopSailing();
            aboutShip.StopRepairing();
            cells.IsEnabled = false;
            isRightPannelActive = true;

        }

        private void hideRightPannelButton_Click(object sender, RoutedEventArgs e)
        {
            aboutBuildings.TimeGoOn();
            cells.IsEnabled = true;
            rightPannelCanvas.Visibility = System.Windows.Visibility.Hidden;
            showRightPannelButton.Visibility = System.Windows.Visibility.Visible;
            hideRightPannelButton.Visibility = System.Windows.Visibility.Hidden;
            timer.Start();
            aboutShip.ContinueSalling();
            aboutShip.ContinueRepairing();
            isRightPannelActive = false;
        }

        private void sailButton_Click(object sender, RoutedEventArgs e)
        {
            if (!repairBtnClicked)
            {
                if (!isRightPannelActive)
                    aboutShip.StartSailShip();
                else
                    aboutShip.DefferedStartSailShip();
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
            aboutShip.HowToPlay();
        }

        #region Cells allocation

        private void cell_0_0_MouseEnter(object sender, MouseEventArgs e)
        {
            aboutBuildings.ChangeActiveness(0, 0);
        }

        private void cell_0_0_MouseLeave(object sender, MouseEventArgs e)
        {
            aboutBuildings.ChangeActiveness(0, 0);
        }

        private void cell_0_1_MouseEnter(object sender, MouseEventArgs e)
        {
            aboutBuildings.ChangeActiveness(0, 1);
        }

        private void cell_0_1_MouseLeave(object sender, MouseEventArgs e)
        {
            aboutBuildings.ChangeActiveness(0, 1);
        }

        private void cell_0_2_MouseEnter(object sender, MouseEventArgs e)
        {
            aboutBuildings.ChangeActiveness(0, 2);
        }

        private void cell_0_2_MouseLeave(object sender, MouseEventArgs e)
        {
            aboutBuildings.ChangeActiveness(0, 2);
        }

        private void cell_1_0_MouseEnter(object sender, MouseEventArgs e)
        {
            aboutBuildings.ChangeActiveness(1, 0);
        }

        private void cell_1_0_MouseLeave(object sender, MouseEventArgs e)
        {
            aboutBuildings.ChangeActiveness(1, 0);
        }

        private void cell_1_1_MouseEnter(object sender, MouseEventArgs e)
        {
            aboutBuildings.ChangeActiveness(1, 1);
        }

        private void cell_1_1_MouseLeave(object sender, MouseEventArgs e)
        {
            aboutBuildings.ChangeActiveness(1, 1);
        }

        private void cell_1_2_MouseEnter(object sender, MouseEventArgs e)
        {
            aboutBuildings.ChangeActiveness(1, 2);
        }

        private void cell_1_2_MouseLeave(object sender, MouseEventArgs e)
        {
            aboutBuildings.ChangeActiveness(1, 2);
        }

        private void cell_2_0_MouseEnter(object sender, MouseEventArgs e)
        {
            aboutBuildings.ChangeActiveness(2, 0);
        }

        private void cell_2_0_MouseLeave(object sender, MouseEventArgs e)
        {
            aboutBuildings.ChangeActiveness(2, 0);
        }

        private void cell_2_1_MouseEnter(object sender, MouseEventArgs e)
        {
            aboutBuildings.ChangeActiveness(2, 1);
        }

        private void cell_2_1_MouseLeave(object sender, MouseEventArgs e)
        {
            aboutBuildings.ChangeActiveness(2, 1);
        }

        private void cell_2_2_MouseEnter(object sender, MouseEventArgs e)
        {
            aboutBuildings.ChangeActiveness(2, 2);
        }

        private void cell_2_2_MouseLeave(object sender, MouseEventArgs e)
        {
            aboutBuildings.ChangeActiveness(2, 2);
        }
        #endregion

        private void closeBuildingCanvas_Click(object sender, RoutedEventArgs e)
        {
            BuildingCanvas.Visibility = System.Windows.Visibility.Hidden;
        }

        #region Canvases mouse down

        private void cell_0_1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (aboutBuildings.isEmpty(0, 1))
            {
                BuildingCanvas.Visibility = System.Windows.Visibility.Visible;
                aboutBuildings.ActivateForBuildingCell(0, 1);
            }
        }

        private void cell_0_0_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (aboutBuildings.isEmpty(0, 0))
            {
                BuildingCanvas.Visibility = System.Windows.Visibility.Visible;
                aboutBuildings.ActivateForBuildingCell(0, 0);
            }

        }

        private void cell_0_2_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (aboutBuildings.isEmpty(0, 2))
            {
                BuildingCanvas.Visibility = System.Windows.Visibility.Visible;
                aboutBuildings.ActivateForBuildingCell(0, 2);
            }
        }

        private void cell_1_0_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (aboutBuildings.isEmpty(1, 0))
            {
                BuildingCanvas.Visibility = System.Windows.Visibility.Visible;
                aboutBuildings.ActivateForBuildingCell(1, 0);
            }
        }

        private void cell_1_1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (aboutBuildings.isEmpty(1, 1))
            {
                BuildingCanvas.Visibility = System.Windows.Visibility.Visible;
                aboutBuildings.ActivateForBuildingCell(1, 1);
            }
        }

        private void cell_1_2_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (aboutBuildings.isEmpty(1, 2))
            {
                BuildingCanvas.Visibility = System.Windows.Visibility.Visible;
                aboutBuildings.ActivateForBuildingCell(1, 2);
            }
        }

        private void cell_2_0_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (aboutBuildings.isEmpty(2, 0))
            {
                BuildingCanvas.Visibility = System.Windows.Visibility.Visible;
                aboutBuildings.ActivateForBuildingCell(2, 0);
            }
        }

        private void cell_2_1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (aboutBuildings.isEmpty(2, 1))
            {
                BuildingCanvas.Visibility = System.Windows.Visibility.Visible;
                aboutBuildings.ActivateForBuildingCell(2, 1);
            }
        }

        private void cell_2_2_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (aboutBuildings.isEmpty(2, 2))
            {
                BuildingCanvas.Visibility = System.Windows.Visibility.Visible;
                aboutBuildings.ActivateForBuildingCell(2, 2);
            }
        }
        #endregion

        private void kitchen_Click(object sender, RoutedEventArgs e)
        {
            if (MyResources.CheckGold() >= 30)
            {
                aboutBuildings.BuildKitchen();
            }
            else
            {
                MessageCanvas.Background = messageBG_B;
                MessageCanvas.Visibility = System.Windows.Visibility.Visible;
                newGoldL.Visibility = System.Windows.Visibility.Hidden;
                coresSpentL.Visibility = System.Windows.Visibility.Hidden;
                armorSpentL.Visibility = System.Windows.Visibility.Hidden;

            }
            BuildingCanvas.Visibility = System.Windows.Visibility.Hidden;
        }

        private void armory_Click(object sender, RoutedEventArgs e)
        {
            if (MyResources.CheckGold() >= 40)
            {
                aboutBuildings.BuildArmory();
            }
            else
            {
                MessageCanvas.Background = messageBG_B;
                MessageCanvas.Visibility = System.Windows.Visibility.Visible;
                newGoldL.Visibility = System.Windows.Visibility.Hidden;
                coresSpentL.Visibility = System.Windows.Visibility.Hidden;
                armorSpentL.Visibility = System.Windows.Visibility.Hidden;

            }
            BuildingCanvas.Visibility = System.Windows.Visibility.Hidden;

        }

        private void cornFarmBtn_Click(object sender, RoutedEventArgs e)
        {
            if (MyResources.CheckGold() >= 30)
            {
                aboutBuildings.BuildCornFarm();
            }
            else
            {
                MessageCanvas.Background = messageBG_B;
                MessageCanvas.Visibility = System.Windows.Visibility.Visible;
                newGoldL.Visibility = System.Windows.Visibility.Hidden;
                coresSpentL.Visibility = System.Windows.Visibility.Hidden;
                armorSpentL.Visibility = System.Windows.Visibility.Hidden;

            }
            BuildingCanvas.Visibility = System.Windows.Visibility.Hidden;
        }

        private void foundry_Click(object sender, RoutedEventArgs e)
        {
            if (MyResources.CheckGold() >= 40)
            {
                aboutBuildings.BuildFoundry();
            }
            else
            {
                MessageCanvas.Background = messageBG_B;
                MessageCanvas.Visibility = System.Windows.Visibility.Visible;
                newGoldL.Visibility = System.Windows.Visibility.Hidden;
                coresSpentL.Visibility = System.Windows.Visibility.Hidden;
                armorSpentL.Visibility = System.Windows.Visibility.Hidden;

            }
            BuildingCanvas.Visibility = System.Windows.Visibility.Hidden;
        }

        private void Window_Closed_1(object sender, EventArgs e)
        {
            aboutShip.CloseWin();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            StoryCanvas.Visibility = System.Windows.Visibility.Hidden;
        }

        private void showStoryBtn_Click(object sender, RoutedEventArgs e)
        {
            StoryCanvas.Visibility = System.Windows.Visibility.Visible;
            hideRightPannelButton_Click(sender, e);
        }

    }
}
