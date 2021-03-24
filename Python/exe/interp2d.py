import numpy as np
from scipy.interpolate import griddata

while True:
    args = input().split(' ')
    thisX = float(args[0])
    thisY = float(args[1])

    # collect data
    xStr = args[2].split(',')
    x = np.asarray(xStr, dtype=np.float64, order='C')
    yStr = args[3].split(',')
    y = np.asarray(yStr, dtype=np.float64, order='C')
    zStr = args[4].split(',')
    z = np.asarray(zStr, dtype=np.float64, order='C')

    # remove avg
    avgZ = sum(z) / len(z)
    for i in range(len(z)):
        z[i] -= avgZ

    # target grid to interpolate to
    xi = np.arange(0,int(max(x)))
    yi = np.arange(0,int(max(y)))
    xi,yi = np.meshgrid(xi,yi)

    # interpolate
    zi = griddata((x,y),z,(xi,yi),method='linear')

    # make 1mm x 1mm data
    XF = int(np.floor(thisX))
    XC = int(np.ceil(thisX))
    YF = int(np.floor(thisY))
    YC = int(np.ceil(thisY))
    xData = [XF, XC, XF, XC]
    yData = [YF, YF, YC, YC]
    x = [0, 1000, 0, 1000]
    y = [0, 0, 1000, 1000]
    z = [0, 0, 0, 0]
    for i in range(4):
        z[i] = zi[yData[i],xData[i]]
    
    # target grid to interpolate to
    xi = np.arange(0,1000)
    yi = np.arange(0,1000)
    xi,yi = np.meshgrid(xi,yi)

    # interpolate
    zi = griddata((x,y),z,(xi,yi),method='linear')

    # output
    print(zi[int((thisY-YF)*1000),int((thisX-XF)*1000)])
