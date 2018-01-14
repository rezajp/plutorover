namespace NASA.NewHorizon.PlutoRover
{
    internal class Compass
    {
        public Direction.NavigationPoints GetNavigationPoint(Direction.NavigationPoints currentNavigationPoint,
            int degree)
        {
            var currentDirectionDegree = (int) currentNavigationPoint;
            currentDirectionDegree += degree;
            if (currentDirectionDegree < 0)
            {
                currentDirectionDegree += 360;
            }
            else if (currentDirectionDegree >= 360)
            {
                currentDirectionDegree -= 360;
            }

            return (Direction.NavigationPoints) currentDirectionDegree;
        }
    }
}