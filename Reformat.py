from time import gmtime, strftime

textfile = open("output.txt")
lines = textfile.readlines()
numPos = 0
with open('formattedOutput.txt', 'w') as dataOut:
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