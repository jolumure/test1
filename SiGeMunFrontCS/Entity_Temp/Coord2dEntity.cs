namespace Entity_Temp
{
    public class Coord2dEntity
    {
        private double x;
        private double y;

        public Coord2dEntity(double x, double y)
        {
            this.x = x;
            this.y = y;
            XD = x;
            YD = y;
        }

        public double XD { get; set; }
        public double YD { get; set; }
    }
}