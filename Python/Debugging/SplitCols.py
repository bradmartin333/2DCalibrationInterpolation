import pyperclip

output = ""

textfile = open("C:\\Repos\\2DCalibrationInterpolation\\Python\\Debugging\\Book1.csv")
lines = textfile.readlines()
for line in lines:
    line = line.replace('\n','')
    vals = line.split(',')
    outputLine = ""
    for i in range(1, len(vals), 2):
        outputLine += str(vals[i]) + '\t'
    output += outputLine + '\n'
textfile.close()

pyperclip.copy(output)