using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace GameOfLife
{
    class Grid
    {
        private static readonly Random rnd = new Random();
        private readonly int SizeX;
        private readonly int SizeY;
        private Cell[,] cells;
        private Cell[,] nextGenerationCells;
        private readonly Canvas drawCanvas;
        private readonly Ellipse[,] cellsVisuals;

        
        public Grid(Canvas c)
        {
            drawCanvas = c;
            SizeX = (int) (c.Width / 5);
            SizeY = (int)(c.Height / 5);
            cells = new Cell[SizeX, SizeY];
            nextGenerationCells = new Cell[SizeX, SizeY];
            cellsVisuals = new Ellipse[SizeX, SizeY];

            ActionForEachCell((i, j) =>
            {
                cells[i, j] = new Cell(i, j, 0, GetRandomBoolean());
                nextGenerationCells[i, j] = new Cell(i, j, 0, false);
                InitCellsVisuals(i, j);
            });

            UpdateGraphics();
            
        }


        public void Clear()
        {
            ActionForEachCell((i, j) =>
            {
                cells[i, j] = new Cell(i, j, 0, false);
                cellsVisuals[i, j].Fill = Brushes.Gray;
            });
        }


        void MouseMove(object sender, MouseEventArgs e)
        {
            var cellVisual = sender as Ellipse;
            
            int i = (int) cellVisual.Margin.Left / 5;
            int j = (int) cellVisual.Margin.Top / 5;

            if (e.LeftButton == MouseButtonState.Pressed && !cells[i, j].IsAlive)
            {
                cells[i, j].IsAlive = true;
                cells[i, j].Age = 0;
                cellVisual.Fill = Brushes.White;
            }
        }

        public void UpdateGraphics()
        {
            ActionForEachCell((i, j) =>
            {
                var cell = cells[i, j];
                cellsVisuals[i, j].Fill = cell.IsAlive
                    ? (cell.Age < 2 ? Brushes.White : Brushes.DarkGray)
                    : Brushes.Gray;
            });
        }

        public void InitCellsVisuals(int i, int j)
        {
            cellsVisuals[i, j] = new Ellipse();
            cellsVisuals[i, j].Width = cellsVisuals[i, j].Height = 5;
            double left = cells[i, j].PositionX;
            double top = cells[i, j].PositionY;
            cellsVisuals[i, j].Margin = new Thickness(left, top, 0, 0);
            cellsVisuals[i, j].Fill = Brushes.Gray;
            drawCanvas.Children.Add(cellsVisuals[i, j]);

            cellsVisuals[i, j].MouseMove += MouseMove;
            cellsVisuals[i, j].MouseLeftButtonDown += MouseMove;
        }
        
        public void UpdateToNextGeneration()
        {
            (this.cells, this.nextGenerationCells) = (this.nextGenerationCells, this.cells);
            UpdateGraphics();
        }
        

        public void Update()
        {
            bool alive;
            int age;
            ActionForEachCell((i, j) =>
            {
                (alive, age) = CalculateNextGeneration(i, j);   // OPTIMIZED
                nextGenerationCells[i, j].IsAlive = alive;  // OPTIMIZED
                nextGenerationCells[i, j].Age = age;  // OPTIMIZED
            });
            UpdateToNextGeneration();
        }

        private (bool isAlive, int age) CalculateNextGeneration(int row, int column)     // OPTIMIZED
        {
            bool isAlive = cells[row, column].IsAlive;
            int count = CountNeighbors(row, column);
            if (!isAlive)
            {
                if (count == 3)
                {
                    return (true, 0);
                }

                return (isAlive, cells[row, column].Age);
            }

            if (count == 2 || count == 3)
            {
                return (true, cells[row, column].Age + 1);
            }

            return (false, 0);
        }

        private int CountNeighbors(int i, int j)
        {
            int count = 0;

            if (i != SizeX - 1 && cells[i + 1, j].IsAlive) count++;
            if (i != SizeX - 1 && j != SizeY - 1 && cells[i + 1, j + 1].IsAlive) count++;
            if (j != SizeY - 1 && cells[i, j + 1].IsAlive) count++;
            if (i != 0 && j != SizeY - 1 && cells[i - 1, j + 1].IsAlive) count++;
            if (i != 0 && cells[i - 1, j].IsAlive) count++;
            if (i != 0 && j != 0 && cells[i - 1, j - 1].IsAlive) count++;
            if (j != 0 && cells[i, j - 1].IsAlive) count++;
            if (i != SizeX - 1 && j != 0 && cells[i + 1, j - 1].IsAlive) count++;

            return count;
        }

        private void ActionForEachCell(Action<int, int> action)
        {
            for (int i = 0; i < SizeX; i++)
            {
                for (int j = 0; j < SizeY; j++)
                {
                    action(i, j);
                }
            }
        }

        private static bool GetRandomBoolean()
        {
            return rnd.NextDouble() > 0.8;
        }
    }
}