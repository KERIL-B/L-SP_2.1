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
        public bool animStatus { get; set; }
        public int hp { get; set; }
        protected Canvas canvas { get; set; }
        protected System.Timers.Timer timer;
        protected int time { get; set; }
        private bool isTimerWork { get; set; }
        private bool isTimerStopped { get; set; } 

        public Building(Canvas canvas)
        {
            timer = new System.Timers.Timer(250);
            hp = 100;
            animStatus = true;
            this.canvas = canvas;
            DrawProgressBar(0);
            timer.Elapsed += timer_Elapsed;
            time = 0;
            isTimerWork = true;
            timer.Start();

        }
        public void TimerPause()
        {
            if (isTimerWork)
            {
                isTimerStopped = true;
                timer.Stop();
            }
        }

        public void TimerGoOn()
        {
            if (isTimerWork)
                if (isTimerStopped)
                {
                    isTimerStopped = false;
                    timer.Start();
                }
            

        }
        protected virtual void timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e) { }

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
                        pb.Width = value*1.6;
                        pb.Height = 10;
                        pb.Fill = Brushes.Yellow;
                        pb.StrokeThickness = 0;
                        pb.Margin = new Thickness(6, 6, 0, 0);
                        canvas.Children.Add(pb);
                    });
            }
            catch (Exception) { }
        }

        public virtual void Animate() { }
    }

    class CornFarm : Building
    {
        Random rand = new Random();
        private static BitmapImage cornFarm_1_img = new BitmapImage(new Uri("IMGs\\Buildings\\Farm\\1.png", UriKind.Relative));
        private static BitmapImage cornFarm_2_img = new BitmapImage(new Uri("IMGs\\Buildings\\Farm\\2.png", UriKind.Relative));
        private ImageBrush cornFarm_1_Brush = new ImageBrush(cornFarm_1_img);
        private ImageBrush cornFarm_2_Brush = new ImageBrush(cornFarm_2_img);

        public CornFarm(Canvas canvas) : base(canvas) { }

        protected override void timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            time++;
            base.DrawProgressBar(time);
            if (time == 100)
            {
                time = 0;
                MyResources.ChangeCornValue(rand.Next(3)+1);
            }
        }

        public override void Animate()
        {
            canvas.Background = (animStatus) ? (cornFarm_1_Brush) : (cornFarm_2_Brush);
            animStatus = !animStatus;
        }


    }
}
