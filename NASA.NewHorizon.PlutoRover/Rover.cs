using System;
using System.Linq.Expressions;

namespace NASA.NewHorizon.PlutoRover
{
    public class Rover
    {
        private readonly IObstacleDetector obstacleDetector;
        public Direction.NavigationPoints NavigationPoint { get; private set; }
        public Position Position { get; }
        private bool positiveStep = true;

        public Rover(IObstacleDetector obstacleDetector) : this(0, 0, Direction.NavigationPoints.North,
            obstacleDetector)
        {
        }

        public Rover(int x, int y, Direction.NavigationPoints navigationPoint, IObstacleDetector obstacleDetector)
        {
            this.obstacleDetector = obstacleDetector;
            NavigationPoint = navigationPoint;
            this.Position = new Position(x, y);
        }

        public void Move(string commands)
        {
            var moves = commands.ToCharArray();
            foreach (var move in moves)
            {
                ParseCommand(move);
            }
        }

        private void ParseCommand(char moveCommand)
        {
            switch (moveCommand)
            {
                case 'F':
                    Move(1);
                    break;
                case 'B':
                    Move(-1);
                    break;
                case 'R':
                    Turn(90);
                    break;
                case 'L':
                    Turn(-90);
                    break;
                default:
                    throw new Exception($"Unrecognisable move {moveCommand}");
            }
        }

        private void Turn(int degree)
        {
            var newNavigationPoint = new Compass().GetNavigationPoint(NavigationPoint, degree);

            if ((NavigationPoint == Direction.NavigationPoints.East ||
                 NavigationPoint == Direction.NavigationPoints.West) && degree == 180)
                positiveStep = !positiveStep;

            NavigationPoint = newNavigationPoint;
        }

        private void Move(int step)
        {
            var currectX = Position.X;
            var currentY = Position.Y;
            if (!positiveStep)
            {
                step = -1 * step;
            }

            var direction = new Direction(step, NavigationPoint);
            direction.Move(Position);

            FixWrapping();
            CheckObstacles(currectX, currentY);
        }

        private void CheckObstacles(int currectX, int currentY)
        {
            if (obstacleDetector.AnyObstacle(Position))
            {
                this.Position.X = currectX;
                this.Position.Y = currentY;
                throw new ObstacleException();
            }
        }

        private void FixWrapping()
        {
            var grid = new Grid();
            if (NavigationPoint == Direction.NavigationPoints.North ||
                NavigationPoint == Direction.NavigationPoints.South)
            {
                this.Position.Y = grid.CalculateVerticalPosition(this.Position.Y, () =>
                {
                    NavigationPoint =new Compass().GetNavigationPoint(NavigationPoint, 180);
                });
            }
            else
            {
                this.Position.X = grid.CalculateHorizontalPosition(this.Position.X);
            }
        }
    }
}