import matplotlib.pyplot as plt
import numpy as np
import os
from scipy.interpolate import griddata
from time import gmtime, strftime

# machine stage info
xMin = 0
xMax = 800
yMin = 0
yMax = 500
inc = 5
xSteps = int((xMax - xMin) / inc)
ySteps = int((yMax - yMin) / inc)

# collect data
optics = False
x = []
y = []
z = []
with open("Python/19MAR2021/PositionMemorySpartanTarget_MetroCal_Shifted.txt") as data:
    for line in data:
        positions = line.split(',')
        x.append(float(positions[0]))
        y.append(float(positions[1]))
        if optics:
            z.append(float(positions[5]))
        else:
            z.append(float(positions[2]))

# # remove min
# minZ = min(z)
# for i in range(len(z)):
#     z[i] -= minZ

# remove avg
avgZ = sum(z) / len(z)
for i in range(len(z)):
    z[i] -= avgZ

# empty table
vals = np.zeros((xSteps,ySteps))

# target grid to interpolate to
xi = np.arange(0,xMax,inc)
yi = np.arange(0,yMax,inc)
xi,yi = np.meshgrid(yi,xi)

# interpolate
zi = griddata((x,y),z,(yi,xi),method='linear')

# insert valid calibration values into table
for i in range(int(xMax/inc)):
    for j in range(int(yMax/inc)):
        if not np.isnan(zi[i][j]):
            vals[i,j] = zi[i][j]

# output
np.savetxt('output.txt', vals, delimiter='\t', fmt='%1.3f')

# reformat
textfile = open("output.txt")
lines = textfile.readlines()

numPos = len(lines[0].rstrip('\n').split('\t'))
blankLine = '0\t0\t'
for j in range(numPos):
    blankLine += '0\t0\t'
blankLine += '0\t0'

with open('Python/2dCal.txt', 'w') as dataOut:
    print('\' CHUCK 2D CALIBRATION', file=dataOut)
    print('\' CREATED ' + strftime("%Y-%m-%d %H:%M:%S", gmtime()), file=dataOut)
    print(':START2D 1 2 4 6 -5 -5 102', file=dataOut)
    print(':START2D POSUNIT=METRIC CORUNIT=METRIC\n', file=dataOut)
    print(blankLine, file=dataOut)
    for line in lines:
        line = line.rstrip('\n').split('\t')
        linebuffer = '0\t0\t'
        for val in line:
            if val == '0.000':
                linebuffer += '0\t0\t'
            else:
                linebuffer += str(float(val) * -1) + '\t' + str(float(val) * -1) + '\t'
        linebuffer += '0\t0'
        print(linebuffer, file=dataOut)
    print(blankLine, file=dataOut)
    print('\n:END', file=dataOut)

textfile.close()

# cleanup
os.remove("output.txt")

# show graph
fig = plt.figure()
ax = fig.add_subplot(111)
ax.xaxis.set_ticks_position('top')
ax.xaxis.set_label_position('top') 
plt.contourf(yi,xi,vals)
plt.plot(x,y,'k.')
plt.xlabel('X',fontsize=16)
plt.ylabel('Y',fontsize=16)
plt.gca().invert_xaxis()
plt.gca().invert_yaxis()
plt.show()
