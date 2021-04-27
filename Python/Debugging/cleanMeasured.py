import math

x = []
y = []
z = []
with open("Python/Debugging/SOURCEMAP.txt") as data:
  for line in data:
    positions = line.split(',')
    x.append(float(positions[0]))
    y.append(float(positions[1]))
    z.append(float(positions[2]))

center = [242.032, 292.368]
radius = 100

avgZ = sum(z) / len(z)

with open('Python/Debugging/CLEANSOURCE.txt', 'w') as dataOut:
  for i in range(len(x)):
    if (math.dist(center, [x[i], y[i]]) < radius):
      print(str(x[i]) + ',' + str(y[i]) + ',' + str(round((z[i] - avgZ), 3)), file=dataOut)