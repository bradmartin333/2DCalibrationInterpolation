namespace PolyInterp
{
    public class Interpolator
    {
        public double SW { get; set; }
        public double SE { get; set; }
        public double NW { get; set; }
        public double NE { get; set; }
        public double X1 { get; set; }
        public double X2 { get; set; }
        public double Y1 { get; set; }
        public double Y2 { get; set; }

        public Interpolator(double sw, double se, double nw, double ne, double x1, double x2, double y1, double y2)
        {
            SW = sw;
            SE = se;
            NW = nw;
            NE = ne;
            X1 = x1;
            X2 = x2;
            Y1 = y1;
            Y2 = y2;
        }

        public double Get(double x, double y)
        {
            double x2x1 = X2 - X1;
            double y2y1 = Y2 - Y1;
            double x2x = X2 - x;
            double y2y = Y2 - y;
            double yy1 = y - Y1;
            double xx1 = x - X1;
            return 1.0 / (x2x1 * y2y1) * (
                SW * x2x * y2y +
                SE * xx1 * y2y +
                NW * x2x * yy1 +
                NE * xx1 * yy1
            );
        }
    }
}
