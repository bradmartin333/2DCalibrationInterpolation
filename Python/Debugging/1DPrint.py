import pyperclip

output = ""

textfile = open("C:\\Repos\\2DCalibrationInterpolation\\Python\\Debugging\\1d.txt")
lines = textfile.readlines()
for line in lines:
    line = line.replace('\n','')
    vals = line.split('\t')
    for i in range(1, len(vals), 2):
        output += str(vals[i]) + '\n'
textfile.close()

pyperclip.copy(output)