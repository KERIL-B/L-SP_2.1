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
    class Story
    {
        private Canvas canvas;

        public Story(Canvas canvas)
        {
            this.canvas = canvas;
            parts.Add(new Passage(0));
            parts.Add(new Passage(1));
            parts.Add(new Passage(2));/*
            parts[3] = new Passage(3);
            parts[4] = new Passage(4);
            parts[5] = new Passage(5);
            parts[6] = new Passage(6);
            parts[7] = new Passage(7);
            parts[8] = new Passage(8);
            parts[9] = new Passage(9);*/

        }
        public List<Passage> parts = new List<Passage>(10);
    }



    class Passage
    {
        public Passage(int i)
        {
            path = "IMGs\\Story\\" + i + ".png";
            this.numberInStory = i;
            isRead = false;
            img = new ImageBrush(new BitmapImage(new Uri(path, UriKind.Relative)));
        }
        string path;
        public int numberInStory;
        public ImageBrush img;
        public bool isRead;
    }
}
