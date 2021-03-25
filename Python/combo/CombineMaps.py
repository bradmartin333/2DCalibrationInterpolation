from time import gmtime, strftime

targetFile = open("Python/combo/target.txt")
targetLines = targetFile.readlines()
targetFile.close()

sourceFile = open("Python/combo/source.txt")
sourceLines = sourceFile.readlines()
sourceFile.close()

cleanFile = open("Python/combo/clean.txt")
cleanLines = cleanFile.readlines()
cleanFile.close()

numLines = 0
if (len(targetLines) == len(sourceLines) == len(cleanLines)):
    numLines = len(targetLines)
else:
    print("INCONSISTENT FILES (0) - EXITING")
    exit(0)

with open('Python/combo/combined.txt', 'w') as dataOut:
    print('\' CHUCK 2D CALIBRATION', file=dataOut)
    print('\' CREATED ' + strftime("%Y-%m-%d %H:%M:%S", gmtime()), file=dataOut)
    print(':START2D 1 2 4 6 -5 -5 102', file=dataOut)
    print(':START2D POSUNIT=METRIC CORUNIT=METRIC\n', file=dataOut)
    
    for i in range(numLines):
        targetVals = targetLines[i].rstrip('\n').split('\t')
        sourceVals = sourceLines[i].rstrip('\n').split('\t')
        cleanVals = cleanLines[i].rstrip('\n').split('\t')

        numVals = 0
        if (len(targetVals) == len(sourceVals) == len(cleanVals)):
            numVals = len(targetVals)
        else:
            print("INCONSISTENT FILES (1) - EXITING " + str(i))
            exit(0)

        outputBuffer = ""
        for j in range(numVals):
            calValue = False

            theseVals = [float(targetVals[j]), float(sourceVals[j]), float(cleanVals[j])]
            for val in theseVals:
                if val != 0.0:
                    outputBuffer += str(val)
                    calValue = True

            if not calValue:
                outputBuffer += "0"
            
            if j < numVals - 1:
                outputBuffer += '\t'

        print(outputBuffer, file=dataOut)

    print('\n:END', file=dataOut)
