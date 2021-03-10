import matplotlib.pyplot as plt
import numpy as np
import os
from scipy.interpolate import griddata
from time import gmtime, strftime

# can create test data here
#data = np.random.rand(9,9) * (500)
#np.savetxt('data.txt', data, delimiter=',', fmt='%1.3f')

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
with open("Python/PositionMemory.txt") as data:
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

# reformat
textfile = open("output.txt")
lines = textfile.readlines()
numPos = 0
with open('2dCal.txt', 'w') as dataOut:
    print('\' CHUCK 2D CALIBRATION', file=dataOut)
    print('\' CREATED ' + strftime("%Y-%m-%d %H:%M:%S", gmtime()), file=dataOut)
    print(':START2D 2 1 6 4 -5 -5 102', file=dataOut)
    print(':START2D POSUNIT=METRIC CORUNIT=METRIC\n', file=dataOut)
    for line in lines:
        line = line.rstrip('\n')
        line = line.split('\t')
        numPos = len(line)
        linebuffer = ''
        for val in line:
            if val == '0.000':
                linebuffer += '0\t0\t'
            else:
                linebuffer += str(float(val) * -1) + '\t0\t'
        linebuffer += '0\t0\t0\t0'
        print(linebuffer, file=dataOut)
    for i in range(2):
        linebuffer = ''
        for j in range(numPos):
            linebuffer += '0\t0\t'
        linebuffer += '0\t0\t0\t0'
        print(linebuffer, file=dataOut)
    print('\n:END', file=dataOut)
textfile.close()

# cleanup
os.remove("output.txt")

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
