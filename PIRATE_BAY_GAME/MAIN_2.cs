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
    class MAIN_2
    {
        private SolidColorBrush brActive = new SolidColorBrush(Colors.ForestGreen);
        private SolidColorBrush brPassive = new SolidColorBrush(Color.FromRgb(94, 179, 83));

        private Cell[,] cells = new Cell[3, 3];
        private int activeForBuildingCellX { get; set; }
        private int activeForBuildingCellY { get; set; }

        public void StartPreparation(Canvas cell_0_0, Canvas cell_0_1, Canvas cell_0_2,
                                     Canvas cell_1_0, Canvas cell_1_1, Canvas cell_1_2,
                                     Canvas cell_2_0, Canvas cell_2_1, Canvas cell_2_2)
        {
            cells[0, 0] = new Cell(cell_0_0);
            cells[0, 1] = new Cell(cell_0_1);
            cells[0, 2] = new Cell(cell_0_2);

            cells[1, 0] = new Cell(cell_1_0);
            cells[1, 1] = new Cell(cell_1_1);
            cells[1, 2] = new Cell(cell_1_2);

            cells[2, 0] = new Cell(cell_2_0);
            cells[2, 1] = new Cell(cell_2_1);
            cells[2, 2] = new Cell(cell_2_2);
        }

        public bool isEmpty(int i, int j)
        {
            return (cells[i, j].building != null) ? (false) : (true);
        }

        public void ChangeActiveness(int i, int j)
        {
            if (cells[i, j].building == null)
            {
                cells[i, j].isActive = !cells[i, j].isActive;
                cells[i, j].canvas.Background = (cells[i, j].isActive) ? (brActive) : (brPassive);
            }
        }

        public void ActivateForBuildingCell(int i, int j)
        {
            activeForBuildingCellX = i;
            activeForBuildingCellY = j;
        }
        public void BuildCornFarm()
        {
            MyResources.ChangeGoldValue(-30);
            cells[activeForBuildingCellX, activeForBuildingCellY].building = new CornFarm(cells[activeForBuildingCellX, activeForBuildingCellY].canvas);

        }

        public void BuildKitchen()
        {
            MyResources.ChangeGoldValue(-30);
            cells[activeForBuildingCellX, activeForBuildingCellY].building = new Kitchen(cells[activeForBuildingCellX, activeForBuildingCellY].canvas);
        }

        public void animateBuildings()
        {
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                {
                    if (cells[i, j].building != null)
                    {
                        if (!cells[i, j].building.isBuilding)
                            cells[i, j].building.Animate();
                    }
                }
        }

        public void TimeStop()
        {
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                {
                    if (cells[i, j].building != null)
                        cells[i, j].building.TimerPause();
                }
        }

        public void TimeGoOn()
        {
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                {
                    if (cells[i, j].building != null)
                        cells[i, j].building.TimerGoOn();
                }
        }
    }
}
