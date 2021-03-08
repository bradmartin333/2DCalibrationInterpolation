with open('output.txt', 'r') as dataIn:
    with open('outputAddedCol.txt', 'w') as dataOut:
        for line in dataIn:
            line = line.rstrip('\n') + '\t0.000'
            print(line, file=dataOut)
            