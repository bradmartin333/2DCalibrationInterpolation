namespace Test
{
    public static class Interpolator
    {
        static public double linearInterpolate(double x, double x0, double x1, double y0, double y1)
        {
            if ((x1 - x0) == 0)
            {
                return (y0 + y1) / 2;
            }
            return y0 + (x - x0) * (y1 - y0) / (x1 - x0);
        }

        //https://www.paulinternet.nl/?page=bicubic

        public static double cubicInterpolate (double[] p, double x)
        {
            return p[1] + 0.5 * x * (p[2] - p[0] + x * (2.0 * p[0] - 5.0 * p[1] + 4.0 * p[2] - p[3] + x * (3.0 * (p[1] - p[2]) + p[3] - p[0])));
        }

        public static double bicubicInterpolate(double[][] p, double x, double y)
        {
            double[] arr = new double[4];
            arr[0] = cubicInterpolate(p[0], y);
            arr[1] = cubicInterpolate(p[1], y);
            arr[2] = cubicInterpolate(p[2], y);
            arr[3] = cubicInterpolate(p[3], y);
            return cubicInterpolate(arr, x);
        }
    }
}