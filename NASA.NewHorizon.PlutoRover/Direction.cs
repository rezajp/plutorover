namespace NASA.NewHorizon.PlutoRover
{
    public class Direction
    {
        private readonly int step;
        private readonly NavigationPoints navigationPoint;

        public Direction(int step, NavigationPoints navigationPoint)
        {
            this.step = step;
            this.navigationPoint = navigationPoint;
        }

        public enum NavigationPoints
        {
            North = 0,
            East = 90,
            South = 180,
            West = 270
        }

        private Movement GetMovement()
        {
            var movement = new Movement(0, 0);
            switch (navigationPoint)
            {
                case NavigationPoints.North:
                    movement.Y += step;
                    break;
                case NavigationPoints.East:
                    movement.X += step;
                    break;
                case NavigationPoints.West:
                    movement.X -= step;
                    break;
                case NavigationPoints.South:
                    movement.Y -= step;
                    break;
            }

            return movement;
        }

        public void Move(Position position)
        {
            var movement = GetMovement();
            
            position.X += movement.X;
            position.Y += movement.Y;
        }
    }
}