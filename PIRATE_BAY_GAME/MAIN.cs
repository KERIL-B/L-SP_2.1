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
        Random rand = new Random();
        Map MapClass = new Map();
        Ship ShipClass = new Ship();
        Resources ResourcesClass = new Resources(); 

        System.Timers.Timer shipSailTimer;
        private int timeValue { get; set; }
        private int timeGetHome { get; set; }
        private Canvas shipCanvas = new Canvas();
        private Canvas mapCanvas = new Canvas();
        private Canvas messageCanvas = new Canvas();
        private Button sailButton = new Button();
        private Label newGoldL = new Label();
        private Label armorSpentL = new Label();
        private Label coresSpentL = new Label();
        private Boolean isSailing { get; set; }

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

        public void StartPreparation(Canvas shipCanvas, Canvas mapCanvas, Button sailButton, Canvas messageCanvas, Label newGoldL, Label armorSpentL, Label coresSpentL)
        {
            ShipClass.hp=100;
            ShipClass.armor=10;
            ShipClass.cores = 10;
            this.shipCanvas = shipCanvas;
            this.mapCanvas = mapCanvas;
            this.sailButton = sailButton;
            this.messageCanvas = messageCanvas;
            this.newGoldL = newGoldL;
            this.armorSpentL = armorSpentL;
            this.coresSpentL = coresSpentL;

            BitmapImage messageBG_IMG = new BitmapImage(new Uri("IMGs\\messageBackground.png", UriKind.Relative));
            ImageBrush messageBG_B = new ImageBrush(messageBG_IMG);
            messageCanvas.Background = messageBG_B;
            ResourcesClass.SetGold(100,mapCanvas);

        }

        public void StartSailShip()
        {
            isSailing = true;
            sailButton.Visibility = System.Windows.Visibility.Hidden;
            shipSailTimer = new System.Timers.Timer(200);
            shipSailTimer.Elapsed += shipSailTimer_Elapsed;
            timeValue = 0;
            shipSailTimer.Start();

        }

        private void SailBack()
        {
            shipCanvas.FlowDirection = FlowDirection.RightToLeft;
            shipCanvas.Margin = new Thickness(-133 + (timeValue - timeGetHome) * 7, 0, 0, 0);
        }

        private void SailResults()
        {
            int gold=0;
            int armorSpent=0;
            int coresSpent=0;
            int hpSpent=0;

            int fightChance = rand.Next(10);
            if (fightChance != 0)
            {
                int winChance = rand.Next(10)+ShipClass.armor+ShipClass.cores;

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
            
            newGoldL.Content = gold + "";
            if (armorSpent > ShipClass.armor)
                armorSpent = ShipClass.armor;
            if (coresSpent > ShipClass.cores)
                coresSpent = ShipClass.cores;

            armorSpentL.Content = armorSpent + "";
            coresSpentL.Content = coresSpentL + "";

            ResourcesClass.AddGold(gold,mapCanvas);
            if (ShipClass.armor - armorSpent < 0)
                ShipClass.armor = 0;
            else ShipClass.armor -= armorSpent;

            if (ShipClass.cores - coresSpent < 0)
                ShipClass.cores = 0;
            else ShipClass.cores -= coresSpent;

            if (ShipClass.hp - hpSpent < 0)
                ShipClass.cores = 1;
            else ShipClass.hp -= hpSpent;

            ShipClass.checkShipCondition(shipCanvas);

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
