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
    class Cell
    {



        public bool isActive { get; set; }
        public Building building { get; set; }
        public Canvas canvas;


        public Cell(Canvas canvas)
        {
            this.isActive = false;
            this.building = null;
            this.canvas = canvas;
        }

    }

}
