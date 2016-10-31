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
    class MAIN
    {

        #region InitializeComponent..
        
        SailingWindow win;
        Random rand = new Random();
        Map MapClass = new Map();
        

        System.Timers.Timer shipSailTimer;
        System.Timers.Timer shipRepairTimer;
        System.Timers.Timer equipShipTimer;

        private bool defferedSail { get; set; }

        private int timeValue { get; set; }
        private int timeGetHome { get; set; }

        private Boolean isSailing { get; set; }
        private Boolean isRepairing { get; set; }

        private Canvas resorcesCanvas;
        private Canvas shipCanvas;
        private Canvas mapCanvas;
        private Canvas messageCanvas;

        MyShip ShipClass = new MyShip(ship_1_Brush, ship_2_Brush, null);

        private Button sailButton;
        private Button repairBtn;

        private Label newGoldL;
        private Label armorSpentL;
        private Label coresSpentL;
       
        private static BitmapImage ship_1_img = new BitmapImage(new Uri("IMGs\\ship_1.png", UriKind.Relative));
        private static BitmapImage ship_2_img = new BitmapImage(new Uri("IMGs\\ship_2.png", UriKind.Relative));
        private static ImageBrush ship_1_Brush = new ImageBrush(ship_1_img);
        private static ImageBrush ship_2_Brush = new ImageBrush(ship_2_img);
        static private BitmapImage messageBG_IMG = new BitmapImage(new Uri("IMGs\\Messages\\messageBackground.png", UriKind.Relative));
        private ImageBrush messageBG_B = new ImageBrush(messageBG_IMG);
        static private BitmapImage messageBG_Repair_IMG = new BitmapImage(new Uri("IMGs\\Messages\\messageBackground_repair.png", UriKind.Relative));
        private ImageBrush messageBG_Repair_B = new ImageBrush(messageBG_Repair_IMG);
        static private BitmapImage messageBG_Equip_IMG = new BitmapImage(new Uri("IMGs\\Messages\\messageBackground_equip.png", UriKind.Relative));
        private ImageBrush messageBG_Equip_B = new ImageBrush(messageBG_Equip_IMG);
        static private BitmapImage messageBG_Pepair100_IMG = new BitmapImage(new Uri("IMGs\\Messages\\messageBackground_repair_100.png", UriKind.Relative));
        private ImageBrush messageBG_Pepair100_B = new ImageBrush(messageBG_Pepair100_IMG);
        static private BitmapImage messageBG_shortOfGold_IMG = new BitmapImage(new Uri("IMGs\\Messages\\messageBackground_shortOfGold.png", UriKind.Relative));
        private ImageBrush messageBG_shortOfGold_B = new ImageBrush(messageBG_shortOfGold_IMG);
        static private BitmapImage messageBG_shortOfSnacks_IMG = new BitmapImage(new Uri("IMGs\\Messages\\messageBackground_shortOfSnacks.png", UriKind.Relative));
        private ImageBrush messageBG_shortOfSnacks_B = new ImageBrush(messageBG_shortOfSnacks_IMG);
        static private BitmapImage messageBG_howToPlay_IMG = new BitmapImage(new Uri("IMGs\\Messages\\messageBackground_HowToPlay.png", UriKind.Relative));
        private ImageBrush messageBG_howToPlay_B = new ImageBrush(messageBG_howToPlay_IMG);

        int startArmor { get; set; }
        int startCores { get; set; }
        
        #endregion

        public void StopRepairing()
        {
            if (isRepairing)
                shipRepairTimer.Stop();
        }

        public void ContinueRepairing()
        {
            if (isRepairing)
                shipRepairTimer.Start();
        }

        public void StopSailing()
        {
            if (isSailing)
                shipSailTimer.Stop();
        }

        public void ContinueSalling()
        {
            if (isSailing)
                shipSailTimer.Start();
        }

        public void TimeTick()
        {
            Animate();
        }

        public void StartPreparation(Canvas resorcesCanvas, Canvas shipCanvas, Canvas mapCanvas, Button sailButton, Canvas messageCanvas, Label newGoldL, Label armorSpentL, Label coresSpentL, Button repairBtn, Label goldLbl, Label cornLbl, Label snacksLbl, Label armorLbl, Label coresLbl)
        {
            this.resorcesCanvas = resorcesCanvas;
            this.shipCanvas = shipCanvas;
            this.mapCanvas = mapCanvas;
            this.sailButton = sailButton;
            this.messageCanvas = messageCanvas;
            this.newGoldL = newGoldL;
            this.armorSpentL = armorSpentL;
            this.coresSpentL = coresSpentL;
            this.repairBtn = repairBtn;

            equipShipTimer = new System.Timers.Timer(1);
            equipShipTimer.Elapsed += equipShipTimer_Elapsed;
            equipShipTimer.Start();

            MyResources.MyShipHp = 100;
            MyResources.MyShipArmor = 10;
            MyResources.MyShipCores = 10;

            MyResources.goldLbl = goldLbl;
            MyResources.cornLbl = cornLbl;
            MyResources.snacksLbl = snacksLbl;
            MyResources.armorLbl = armorLbl;
            MyResources.coresLbl = coresLbl;

            MyResources.SetBackground(resorcesCanvas);
            MyResources.ChangeGoldValue(100);
            MyResources.ChangeCornValue(10);
            MyResources.ChangeSnacksValue(10);
            MyResources.ChangeArmorValue(0);
            MyResources.ChangeCoresValue(0);

        }

        void equipShipTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (MyResources.MyShipArmor != 10)
            {
                if (10 - MyResources.MyShipArmor > MyResources.CheckArmor())
                {
                    MyResources.MyShipArmor += MyResources.CheckArmor();
                    MyResources.ChangeArmorValue(-MyResources.CheckArmor());
                }
                else
                {
                    MyResources.ChangeArmorValue(-(10 - MyResources.MyShipArmor));
                    MyResources.MyShipArmor = 10;
                }
            }
            if (MyResources.MyShipCores != 10)
            {
                if (10 - MyResources.MyShipCores > MyResources.CheckCores())
                {
                    MyResources.MyShipCores += MyResources.CheckCores();
                    MyResources.ChangeCoresValue(-MyResources.CheckCores());
                }
                else
                {
                    MyResources.ChangeCoresValue(-(10 - MyResources.MyShipCores));
                    MyResources.MyShipCores = 10;
                }

            }
        }

        public void DefferedStartSailShip()
        {
            defferedSail = true;
        }

        public void CheckDefferedSail()
        {
            if (!isSailing)

                if (defferedSail)
                    StartSailShip();
        }

        public void StartSailShip()
        {
            if (!isRepairing)
            {
                if (MyResources.MyShipHp < 10)
                {
                    messageCanvas.Background = messageBG_Repair_B;
                    messageCanvas.Visibility = System.Windows.Visibility.Visible;
                    newGoldL.Visibility = System.Windows.Visibility.Hidden;
                    coresSpentL.Visibility = System.Windows.Visibility.Hidden;
                    armorSpentL.Visibility = System.Windows.Visibility.Hidden;
                }
                else
                {
                    if ((MyResources.MyShipCores == 0) || (MyResources.MyShipArmor == 0))
                    {
                        messageCanvas.Background = messageBG_Equip_B;
                        messageCanvas.Visibility = System.Windows.Visibility.Visible;
                        newGoldL.Visibility = System.Windows.Visibility.Hidden;
                        coresSpentL.Visibility = System.Windows.Visibility.Hidden;
                        armorSpentL.Visibility = System.Windows.Visibility.Hidden;
                    }
                    else
                    {
                        if (MyResources.CheckSnacks() < 6)
                        {
                            messageCanvas.Background = messageBG_shortOfSnacks_B;
                            messageCanvas.Visibility = System.Windows.Visibility.Visible;
                            newGoldL.Visibility = System.Windows.Visibility.Hidden;
                            coresSpentL.Visibility = System.Windows.Visibility.Hidden;
                            armorSpentL.Visibility = System.Windows.Visibility.Hidden;
                        }
                        else
                        {
                            startArmor = MyResources.MyShipArmor;
                            startCores = MyResources.MyShipCores;

                            win = new SailingWindow();
                            win.Show();
                            isSailing = true;
                            MyResources.ChangeSnacksValue(-6);
                            repairBtn.IsEnabled = false;
                            sailButton.Visibility = System.Windows.Visibility.Hidden;
                            shipSailTimer = new System.Timers.Timer(30);
                            shipSailTimer.Elapsed += shipSailTimer_Elapsed;
                            timeValue = 0;
                            shipSailTimer.Start();
                            win.MyShipAppeare();
                        }
                    }
                }
            }

        }

        public void RepairBtnClick()
        {
            if ((!isSailing) && (!defferedSail))
            {
                isRepairing = true;
                sailButton.IsEnabled = false;
                shipRepairTimer = new System.Timers.Timer(5000);
                shipRepairTimer.Elapsed += shipRepairTimer_Elapsed;
                timeValue = 0;
                shipRepairTimer.Start();
            }
        }

        public void HowToPlay()
        {
            messageCanvas.Background = messageBG_howToPlay_B;
            messageCanvas.Visibility = System.Windows.Visibility.Visible;
            newGoldL.Visibility = System.Windows.Visibility.Hidden;
            coresSpentL.Visibility = System.Windows.Visibility.Hidden;
            armorSpentL.Visibility = System.Windows.Visibility.Hidden;
        }

        private void shipRepairTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                shipCanvas.Dispatcher.Invoke(() =>
                    {
                        if ((MyResources.CheckGold() > 20) && (MyResources.MyShipHp < 100))
                        {

                            MyResources.ChangeGoldValue(-20);
                            MyResources.MyShipHp = (MyResources.MyShipHp > 90) ? (100) : (MyResources.MyShipHp += 10);
                            ShipClass.checkShipCondition(shipCanvas);
                        }
                        else
                        {
                            win.Close();
                            shipRepairTimer.Stop();
                            isRepairing = false;
                            newGoldL.Visibility = System.Windows.Visibility.Hidden;
                            coresSpentL.Visibility = System.Windows.Visibility.Hidden;
                            armorSpentL.Visibility = System.Windows.Visibility.Hidden;

                            messageCanvas.Background = (MyResources.MyShipHp == 100) ? (messageBG_Pepair100_B) : (messageBG_shortOfGold_B);

                            messageCanvas.Visibility = System.Windows.Visibility.Visible;
                            sailButton.IsEnabled = true;
                        }
                    });
            }
            catch (Exception)
            { };
        }

        private void SailBack()
        {
            win.Close();
            shipCanvas.FlowDirection = FlowDirection.RightToLeft;
            shipCanvas.Margin = new Thickness(-133 + (timeValue - timeGetHome), 355, 0, 27);
        }

        private void SailResults()
        {
            int gold = rand.Next(50);
            
            newGoldL.Content = Convert.ToString(gold);

            armorSpentL.Content = startArmor-MyResources.MyShipArmor;
            coresSpentL.Content = startCores-MyResources.MyShipCores;

            MyResources.ChangeGoldValue(gold);

            ShipClass.checkShipCondition(shipCanvas);
            repairBtn.IsEnabled = true;

        }

        private void shipSailTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                shipCanvas.Dispatcher.Invoke(() =>
                {

                    timeValue++;

                    if (timeValue < 140)
                        shipCanvas.Margin = new Thickness(-timeValue, 355, 0, 27);

                    if (timeValue == 140)
                    {
                        timeGetHome = timeValue + (10 + rand.Next(30)) * 33+920;
                    }
                    if ((timeValue > 140) && (timeValue > timeGetHome))
                    {
                        SailBack();
                    }
                    if (shipCanvas.Margin.Left==8)
                    {
                        shipCanvas.FlowDirection = FlowDirection.LeftToRight;
                        shipSailTimer.Stop();
                        sailButton.Visibility = System.Windows.Visibility.Visible;
                        timeValue = 0;
                        isSailing = false;
                        SailResults();
                        messageCanvas.Background = messageBG_B;
                        newGoldL.Visibility = System.Windows.Visibility.Visible;
                        coresSpentL.Visibility = System.Windows.Visibility.Visible;
                        armorSpentL.Visibility = System.Windows.Visibility.Visible;
                        messageCanvas.Visibility = System.Windows.Visibility.Visible;
                    }
                });
            }
            catch (Exception)
            { }

        }

        private void Animate()
        {
            MapClass.AnimateMap(mapCanvas);
            ShipClass.animateShip(shipCanvas);
            ShipClass.checkShipCondition(shipCanvas);
        }

        public void CloseWin()
        {
            if (win != null)
                win.Close();
        }

    }
}
