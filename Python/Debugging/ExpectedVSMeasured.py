import numpy as np
import matplotlib.pyplot as plt
from scipy.interpolate import griddata

# Load data from CSV
dat = np.genfromtxt('Python/Debugging/CLEANSOURCE.txt', delimiter=',',skip_header=0)
X_dat = dat[:,0]
Y_dat = dat[:,1]
Z_dat = dat[:,2]

# Convert from pandas dataframes to numpy arrays
X, Y, Z, = np.array([]), np.array([]), np.array([])
for i in range(len(X_dat)):
  X = np.append(X, X_dat[i])
  Y = np.append(Y, Y_dat[i])
  Z = np.append(Z, Z_dat[i])

# dat = np.genfromtxt('Python/Debugging/CLEANMEASURED.txt', delimiter=',',skip_header=0)
# Z_dat = dat[:,2]

# for i in range(len(Z_dat)):
#   Z[i] -= Z_dat[i]

# create x-y points to be used in heatmap
xi = np.linspace(X.min(), X.max(), 1000)
yi = np.linspace(Y.min(), Y.max(), 1000)

# Interpolate for plotting
zi = griddata((X, Y), Z, (xi[None,:], yi[:,None]), method='cubic')

# Create the contour plot
CS = plt.contourf(xi, yi, zi)
plt.gca().invert_xaxis()
plt.gca().invert_yaxis()
plt.xlabel('X',fontsize=16)
plt.ylabel('Y',fontsize=16)
plt.colorbar(label = "Micrometers of Error")  
plt.title("Spartan Source Chuck Flatness")
plt.show()