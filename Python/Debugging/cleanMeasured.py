x = []
y = []
z = []
with open("Python/Debugging/MEASURED.txt") as data:
  for line in data:
    positions = line.split(',')
    x.append(float(positions[0]))
    y.append(float(positions[1]))
    z.append(float(positions[2]))

avgZ = sum(z) / len(z)

with open('Python/Debugging/CLEANMEASURED.txt', 'w') as dataOut:
  for i in range(len(x)):
    print(str(x[i]) + ',' + str(y[i]) + ',' + str(round((z[i] - avgZ), 3)), file=dataOut)