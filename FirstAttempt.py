import matplotlib.pyplot as plt
import numpy as np
from scipy.interpolate import griddata

# data coordinates and values
x = [10,10,250,250,490,490,10,250,490]
y = [10,490,10,490,490,10,250,250,250]
z = [-1.001,-1.002,-1,-1,-1.003,-1.001,-0.995,-1.00,-1.001]

# target grid to interpolate to
xi = yi = np.arange(0,500,5)
xi,yi = np.meshgrid(xi,yi)

# interpolate
zi = griddata((x,y),z,(xi,yi),method='cubic')

# plot
fig = plt.figure()
ax = fig.add_subplot(111)
plt.contourf(xi,yi,zi,np.arange(min(z),max(z),0.00001))
plt.plot(x,y,'k.')
plt.xlabel('x',fontsize=16)
plt.ylabel('y',fontsize=16)
plt.show()