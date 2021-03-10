#include <iostream> 
#include <stdio.h>
#include <stdlib.h>
#include <gsl/gsl_math.h>
#include <gsl/gsl_interp2d.h>
#include <gsl/gsl_spline2d.h>

/*
	./interpx.exe {1} {2} {3} {4} {5} {6} {7}
	{0} = xRange of measurements
	{1} = yRange of measurements
	{2} = X coordinate of interest
	{3} = Y coordinate of interest
	{4] = SW z measurement 
	{5] = NW z measurement
	{6] = NE z measurement
	{7] = SE z measurement
*/

int main(int argc, char** argv) 
{
	const gsl_interp2d_type* T = gsl_interp2d_bilinear;
	const double xRange = atof(argv[1]);
	const double yRange = atof(argv[2]);
	const size_t Nx = (size_t)(xRange); /* number of points to interpolate */
	const size_t Ny = (size_t)(yRange);
	const double xa[] = { 0.0, 1.0 }; /* define unit square */
	const double ya[] = { 0.0, 1.0 };
	const size_t nx = sizeof(xa) / sizeof(double); /* x grid points */
	const size_t ny = sizeof(ya) / sizeof(double); /* y grid points */
	double* za = (double*)malloc(nx * ny * (int)sizeof(double));
	gsl_spline2d* spline = gsl_spline2d_alloc(T, nx, ny);
	gsl_interp_accel *xacc = gsl_interp_accel_alloc();
	gsl_interp_accel* yacc = gsl_interp_accel_alloc();

	/* set z grid values */
	gsl_spline2d_set(spline, za, 0, 0, atof(argv[5]));
	gsl_spline2d_set(spline, za, 0, 1, atof(argv[6]));
	gsl_spline2d_set(spline, za, 1, 1, atof(argv[7]));
	gsl_spline2d_set(spline, za, 1, 0, atof(argv[8]));

	/* initialize interpolation */
	gsl_spline2d_init(spline, xa, ya, za, nx, ny);

	/* interpolate and print */
	double xi = atof(argv[3]) / (Nx - 1.0);
	double yj = atof(argv[4]) / (Ny - 1.0);
	double zij = gsl_spline2d_eval(spline, xi, yj, xacc, yacc);
	printf("\n%f\n", zij);
	gsl_spline2d_free(spline);
	gsl_interp_accel_free(xacc);
	gsl_interp_accel_free(yacc);
	free(za);

	return 0;
}