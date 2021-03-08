import numpy as np
data = np.random.rand(9,9) * (500)
np.savetxt('data.txt', data, delimiter=',', fmt='%1.3f')
