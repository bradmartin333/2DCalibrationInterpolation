import matplotlib.pyplot as plt
import numpy as np
from scipy.interpolate import griddata

# data coordinates and values
xMin = 0
xMax = 800
yMin = 0
yMax = 500
inc = 5

xSteps = int((xMax - xMin) / inc)
ySteps = int((yMax - yMin) / inc)

x = [10,10,50,50]
y = [10,50,10,50]
z = [-1.001,-1.002,-1,-1.003]

vals = np.zeros((xSteps,ySteps))

# target grid to interpolate to
xi = np.arange(0,xMax,inc)
yi = np.arange(0,yMax,inc)
xi,yi = np.meshgrid(xi,yi)

# interpolate
zi = griddata((x,y),z,(xi,yi),method='cubic')
#zi = np.nan_to_num(zi)

# create XYZ interpolation table
for i in range(int(xMax/inc)):
    for j in range(int(yMax/inc)):
        if not np.isnan(zi[j][i]):
            vals[i,j] = zi[j][i]

vals = np.rot90(vals)
np.savetxt('output3.tsv', vals, delimiter='\t', fmt='%1.3f')