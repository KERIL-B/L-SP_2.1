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
        Random rand = new Random();
        Map MapClass = new Map();
        Ship ShipClass = new Ship();
       // Resources ResourcesClass = new Resources();

        System.Timers.Timer shipSailTimer;
        System.Timers.Timer shipRepairTimer;



        private bool defferedSail { get; set; }

        private int timeValue { get; set; }
        private int timeGetHome { get; set; }

        private Boolean isSailing { get; set; }
        private Boolean isRepairing { get; set; }

        private Canvas resorcesCanvas;
        private Canvas shipCanvas;
        private Canvas mapCanvas;
        private Canvas messageCanvas;


        private Button sailButton;
        private Button repairBtn;

        private Label newGoldL;
        private Label armorSpentL;
        private Label coresSpentL;

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
            ShipClass.hp = 100;
            ShipClass.armor = 10;
            ShipClass.cores = 10;
            this.resorcesCanvas = resorcesCanvas;
            this.shipCanvas = shipCanvas;
            this.mapCanvas = mapCanvas;
            this.sailButton = sailButton;
            this.messageCanvas = messageCanvas;
            this.newGoldL = newGoldL;
            this.armorSpentL = armorSpentL;
            this.coresSpentL = coresSpentL;
            this.repairBtn = repairBtn;

            MyResources.goldLbl = goldLbl;
            MyResources.cornLbl = cornLbl;
            MyResources.snacksLbl = snacksLbl;
            MyResources.armorLbl = armorLbl;
            MyResources.coresLbl = coresLbl;

            MyResources.SetBackground(resorcesCanvas);
            MyResources.ChangeGoldValue(140);
            MyResources.ChangeCornValue(10);
            MyResources.ChangeSnacksValue(10);
            MyResources.ChangeArmorValue(0);
            MyResources.ChangeCoresValue(0);

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
                if (ShipClass.hp < 10)
                {
                    messageCanvas.Background = messageBG_Repair_B;
                    messageCanvas.Visibility = System.Windows.Visibility.Visible;
                    newGoldL.Visibility = System.Windows.Visibility.Hidden;
                    coresSpentL.Visibility = System.Windows.Visibility.Hidden;
                    armorSpentL.Visibility = System.Windows.Visibility.Hidden;
                }
                else
                {
                    if ((ShipClass.cores == 0) || (ShipClass.armor == 0))
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
                            isSailing = true;
                            MyResources.ChangeSnacksValue(-6);
                            repairBtn.IsEnabled = false;
                            sailButton.Visibility = System.Windows.Visibility.Hidden;
                            shipSailTimer = new System.Timers.Timer(200);
                            shipSailTimer.Elapsed += shipSailTimer_Elapsed;
                            timeValue = 0;
                            shipSailTimer.Start();
                        }
                    }
                }
            }

        }

        public void RepairBtnClick()
        {
            if ((!isSailing)&&(!defferedSail))
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
                        if ((MyResources.CheckGold() > 20) && (ShipClass.hp < 100))
                        {

                            MyResources.ChangeGoldValue(-20);
                            ShipClass.hp = (ShipClass.hp > 90) ? (100) : (ShipClass.hp += 10);
                            ShipClass.checkShipCondition(shipCanvas);
                        }
                        else
                        {
                            shipRepairTimer.Stop();
                            isRepairing = false;
                            newGoldL.Visibility = System.Windows.Visibility.Hidden;
                            coresSpentL.Visibility = System.Windows.Visibility.Hidden;
                            armorSpentL.Visibility = System.Windows.Visibility.Hidden;

                            messageCanvas.Background = (ShipClass.hp == 100) ? (messageBG_Pepair100_B) : (messageBG_shortOfGold_B);

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
            shipCanvas.FlowDirection = FlowDirection.RightToLeft;
            shipCanvas.Margin = new Thickness(-133 + (timeValue - timeGetHome) * 7, 0, 0, 0);
        }

        private void SailResults()
        {
            int gold = 0;
            int armorSpent = 0;
            int coresSpent = 0;
            int hpSpent = 0;

            int fightChance = rand.Next(10);
            if (fightChance != 0)
            {
                int winChance = rand.Next(10) + ShipClass.armor + ShipClass.cores;

                if (winChance > 25)
                {
                    gold = rand.Next(75);
                }
                else
                {
                    if (winChance > 15)
                    {
                        gold = rand.Next(75);
                        armorSpent = rand.Next(5);
                        coresSpent = rand.Next(5);
                        hpSpent = rand.Next(15);
                    }
                    else
                    {
                        if (winChance > 3)
                        {
                            gold = rand.Next(30);
                            armorSpent = rand.Next(9);
                            coresSpent = rand.Next(9);
                            hpSpent = rand.Next(50);
                        }
                        else
                        {
                            armorSpent = 10;
                            coresSpent = 10;
                            hpSpent = 100;
                        }
                    }
                }
            }

            newGoldL.Content = Convert.ToString(gold);
            if (armorSpent > ShipClass.armor)
                armorSpent = ShipClass.armor;
            if (coresSpent > ShipClass.cores)
                coresSpent = ShipClass.cores;

            armorSpentL.Content = Convert.ToString(armorSpent);
            coresSpentL.Content = Convert.ToString(coresSpent);

           MyResources.ChangeGoldValue(gold);

            ShipClass.armor = (ShipClass.armor - armorSpent < 0) ? (0) : (ShipClass.armor - armorSpent);
            ShipClass.cores = (ShipClass.cores - coresSpent < 0) ? (0) : (ShipClass.cores - coresSpent);
            ShipClass.hp = (ShipClass.hp - hpSpent < 0) ? (1) : (ShipClass.hp - hpSpent);

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

                    if (timeValue < 20)
                        shipCanvas.Margin = new Thickness(-timeValue * 7, 0, 0, 0);

                    if (timeValue == 20)
                    {
                        timeGetHome = timeValue + (10 + rand.Next(30)) * 5;
                    }
                    if ((timeValue > 49) && (timeValue > timeGetHome))
                    {
                        SailBack();
                    }
                    if (timeValue > timeGetHome + 19)
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



    }
}
