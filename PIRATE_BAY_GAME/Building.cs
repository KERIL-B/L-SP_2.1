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
    class Building
    {
        protected bool canWork { get; set; }
        public bool animStatus { get; set; }
        public int hp { get; set; }
        protected Canvas canvas { get; set; }
        protected System.Timers.Timer timerBuilding;
        protected System.Timers.Timer timerWorking;
        protected int time { get; set; }
        private bool isTimerWork { get; set; }
        private bool isTimerStopped { get; set; }
        public bool isBuilding { get; set; }

        public Building(Canvas canvas)
        {
            timerBuilding = new System.Timers.Timer(250);
            hp = 100;
            isBuilding = true;
            //animStatus = true;
            this.canvas = canvas;
            // DrawProgressBar(0);
            timerBuilding.Elapsed += timer_Elapsed;
            time = 0;
            isTimerWork = true;
            timerBuilding.Start();

        }

        protected void StarWork()
        {
            timerWorking = new System.Timers.Timer(250);
            isBuilding = false;
            animStatus = true;
            DrawProgressBar(0);
            time = 0;
            timerWorking.Elapsed += timerWorking_Elapsed;
            isTimerWork = true;
            timerWorking.Start();
        }


        protected virtual void timerWorking_Elapsed(object sender, System.Timers.ElapsedEventArgs e) { }

        public void TimerPause()
        {
            if (isTimerWork)
            {
                if (!isBuilding)
                    timerWorking.Stop();
                else
                    timerBuilding.Stop();
                isTimerStopped = true;
            }
        }

        public void TimerGoOn()
        {
            if (isTimerWork)
                if (isTimerStopped)
                {
                    isTimerStopped = false;
                    if (!isBuilding)
                        timerWorking.Start();
                    else
                        timerBuilding.Start();
                }


        }

        protected void timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            
            time++;
            DrawBuildPB(time);
            if (time == 100)
            {
                time = 0;
                StarWork();
                timerBuilding.Stop();
            }
        }

        protected void DrawBuildPB(int value)
        {
            try
            {
                canvas.Dispatcher.Invoke(() =>
                {
                    canvas.Children.Clear();
                    Rectangle pbCircuit = new Rectangle();
                    pbCircuit.Width = 162;
                    pbCircuit.Height = 12;
                    pbCircuit.Stroke = Brushes.Black;
                    pbCircuit.StrokeThickness = 1;
                    pbCircuit.Margin = new Thickness(5, 5, 0, 0);
                    canvas.Children.Add(pbCircuit);

                    Rectangle pb = new Rectangle();
                    pb.Width = value * 1.6;
                    pb.Height = 10;
                    pb.Fill = Brushes.Silver;
                    pb.StrokeThickness = 0;
                    pb.Margin = new Thickness(6, 6, 0, 0);
                    canvas.Children.Add(pb);
                });
            }
            catch (Exception) { }
        }

        protected void DrawProgressBar(int value)
        {
            try
            {
                canvas.Dispatcher.Invoke(() =>
                    {
                        canvas.Children.Clear();
                        Rectangle pbCircuit = new Rectangle();
                        pbCircuit.Width = 162;
                        pbCircuit.Height = 12;
                        pbCircuit.Stroke = Brushes.Black;
                        pbCircuit.StrokeThickness = 1;
                        pbCircuit.Margin = new Thickness(5, 5, 0, 0);
                        canvas.Children.Add(pbCircuit);

                        Rectangle pb = new Rectangle();
                        pb.Width = value * 1.6;
                        pb.Height = 10;
                        pb.Fill = Brushes.Yellow;
                        pb.StrokeThickness = 0;
                        pb.Margin = new Thickness(6, 6, 0, 0);
                        canvas.Children.Add(pb);
                    });
            }
            catch (Exception) { }
        }

        public virtual void Animate()
        {
            animStatus = !animStatus;
        }
    }

    class CornFarm : Building
    {
        Random rand = new Random();
        private static BitmapImage cornFarm_0_img = new BitmapImage(new Uri("IMGs\\Buildings\\Farm\\0.png", UriKind.Relative));
        private static BitmapImage cornFarm_1_img = new BitmapImage(new Uri("IMGs\\Buildings\\Farm\\1.png", UriKind.Relative));
        private static BitmapImage cornFarm_2_img = new BitmapImage(new Uri("IMGs\\Buildings\\Farm\\2.png", UriKind.Relative));
        private ImageBrush cornFarm_0_Brush = new ImageBrush(cornFarm_0_img);
        private ImageBrush cornFarm_1_Brush = new ImageBrush(cornFarm_1_img);
        private ImageBrush cornFarm_2_Brush = new ImageBrush(cornFarm_2_img);

        public CornFarm(Canvas canvas)
            : base(canvas)
        {
            canvas.Background = cornFarm_0_Brush;
        }

        protected override void timerWorking_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            time++;
            base.DrawProgressBar(time);
            if (time == 100)
            {
                time = 0;
                MyResources.ChangeCornValue(rand.Next(3) + 1);
            }
        }

        public override void Animate()
        {
            base.Animate();
            canvas.Background = (animStatus) ? (cornFarm_1_Brush) : (cornFarm_2_Brush);
        }


    }

    class Kitchen : Building
    {
        Random rand = new Random();
        private static BitmapImage kitchen_0_img = new BitmapImage(new Uri("IMGs\\Buildings\\Kitchen\\0.png", UriKind.Relative));
        private static BitmapImage kitchen_1_img = new BitmapImage(new Uri("IMGs\\Buildings\\Kitchen\\1.png", UriKind.Relative));
        private static BitmapImage kitchen_2_img = new BitmapImage(new Uri("IMGs\\Buildings\\Kitchen\\2.png", UriKind.Relative));
        private ImageBrush kitchen_0_Brush = new ImageBrush(kitchen_0_img);
        private ImageBrush kitchen_1_Brush = new ImageBrush(kitchen_1_img);
        private ImageBrush kitchen_2_Brush = new ImageBrush(kitchen_2_img);

        public Kitchen(Canvas canvas)
            : base(canvas)
        {
            canvas.Background = kitchen_0_Brush;
        }

        protected override void timerWorking_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (time == 0)
            {
                canWork=(MyResources.CheckCorn()>=2)?(true):(false);
                if (canWork)
                    MyResources.ChangeCornValue(-2);
            }
            if (canWork)
            {
                time++;

                if (time == 100)
                {
                    time = 0;
                    MyResources.ChangeSnacksValue(1);
                }
            }
            base.DrawProgressBar(time);
        }


        public override void Animate()
        {
            base.Animate();
            canvas.Background = (animStatus) ? (kitchen_1_Brush) : (kitchen_2_Brush);
        }

    }
}
