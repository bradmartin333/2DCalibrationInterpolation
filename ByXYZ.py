import matplotlib.pyplot as plt
import numpy as np
from scipy.interpolate import griddata

# machine stage info
xMin = 0
xMax = 500
yMin = 0
yMax = 500
inc = 5
xSteps = int((xMax - xMin) / inc)
ySteps = int((yMax - yMin) / inc)

# collect data
optics = True
x = []
y = []
z = []
with open('PosDataTestA.txt') as data:
    for line in data:
        positions = line.split(',')
        x.append(float(positions[0]))
        y.append(float(positions[1]))
        if optics:
            z.append(float(positions[5]))
        else:
            z.append(float(positions[2]))

# remove min
minZ = min(z)
for i in range(len(z)):
    z[i] -= minZ

# empty table
vals = np.zeros((ySteps,xSteps))

# target grid to interpolate to
xi = np.arange(0,xMax,inc)
yi = np.arange(0,yMax,inc)
xi,yi = np.meshgrid(xi,yi)

# interpolate
zi = griddata((x,y),z,(xi,yi),method='linear')

# insert valid calibration values into table
for j in range(int(yMax/inc)):
    for i in range(int(xMax/inc)):
        if not np.isnan(zi[j][i]):
            vals[j,i] = zi[j][i]

# output
np.savetxt('output.txt', vals, delimiter='\t', fmt='%1.3f')

# show graph
fig = plt.figure()
ax = fig.add_subplot(111)
ax.xaxis.set_ticks_position('top')
ax.xaxis.set_label_position('top') 
plt.contourf(xi,yi,vals)
plt.plot(x,y,'k.')
plt.xlabel('X',fontsize=16)
plt.ylabel('Y',fontsize=16)
plt.gca().invert_xaxis()
plt.gca().invert_yaxis()
plt.show()
