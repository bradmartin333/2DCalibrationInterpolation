import matplotlib.pyplot as plt
import numpy as np
from scipy.interpolate import griddata

# data coordinates and values
x = [10,10,50,50]
y = [10,50,10,50]
z = [-1.001,-1.002,-1,-1.003]

# target grid to interpolate to
xi = np.arange(0,800,5)
yi = np.arange(0,500,5)
xi,yi = np.meshgrid(xi,yi)

# interpolate
zi = griddata((x,y),z,(xi,yi),method='cubic')
#zi = np.nan_to_num(zi)

f = open("output2.txt", "w+")
for i in range(160):
    for j in range(100):
        output = "{},{},{}\n"
        f.write(output.format(xi[0,i],yi[j,0],zi[j][i]))
f.close()