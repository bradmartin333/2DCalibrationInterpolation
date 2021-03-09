textfile = open("output.txt")
lines = textfile.readlines()
with open('formattedOutput.txt', 'w') as dataOut:
    for line in lines:
        line = line.rstrip('\n')
        line = line.split('\t')
        linebuffer = ''
        for val in line:
            if val == '0.000':
                linebuffer += '0\t0\t'
            else:
                linebuffer += val + '\t0\t'
        linebuffer += '0\t0\t0\t0'
        print(linebuffer, file=dataOut)
            