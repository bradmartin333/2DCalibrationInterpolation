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
#zi = np.rot90(zi)
zi = np.nan_to_num(zi)
np.savetxt('output.tsv', zi, delimiter='\t', fmt='%1.3f')

# # plot
# fig = plt.figure()
# ax = fig.add_subplot(111)
# plt.contourf(xi,yi,zi,np.arange(min(z),max(z),0.0001))
# plt.plot(x,y,'k.')
# plt.xlabel('x',fontsize=16)
# plt.ylabel('y',fontsize=16)
# plt.show()