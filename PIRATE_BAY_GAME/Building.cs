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
        public  int hp { get; set; }
        protected Canvas canvas { get; set; }

        public Building(Canvas canvas)
        {
            hp = 100;
            animStatus = true;
            this.canvas = canvas;

        }
        
        public virtual void Animate(){}
    }

    class CornFarm : Building
    {
        private static BitmapImage cornFarm_1_img = new BitmapImage(new Uri("IMGs\\Buildings\\Farm\\1.png", UriKind.Relative));
        private static BitmapImage cornFarm_2_img = new BitmapImage(new Uri("IMGs\\Buildings\\Farm\\2.png", UriKind.Relative));
        private ImageBrush cornFarm_1_Brush = new ImageBrush(cornFarm_1_img);
        private ImageBrush cornFarm_2_Brush = new ImageBrush(cornFarm_2_img);

        public CornFarm(Canvas canvas) : base(canvas) { }

        public override void Animate()
        {
            canvas.Background=(animStatus)?(cornFarm_1_Brush):(cornFarm_2_Brush);
            animStatus = !animStatus;
        }
    }
}
