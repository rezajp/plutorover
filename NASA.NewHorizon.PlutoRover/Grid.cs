using System;

namespace NASA.NewHorizon.PlutoRover
{
    internal class Grid
    {
        private const int GridSize = 100;
        public int CalculateVerticalPosition(int position, Action callback)
        {
            if (position == -1)
            {
                position = 1;
                callback();
            }
            else if (position == GridSize+1)
            {
                position = 0;
                callback();
            }

            return position;
        }

        public int CalculateHorizontalPosition(int position)
        {
            if (position == GridSize+1)
            {
                return 0;
            }

            if (position == -1)
            {
                return GridSize;
            }

            return position;
        }
    }
}