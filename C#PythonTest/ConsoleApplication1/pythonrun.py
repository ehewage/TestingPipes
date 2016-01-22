import time
import struct

f = open(r'\\.\pipe\NPtest', 'r+b', 0)
#f = open(r'\\.\pipe\Pipeline', 'r+b', 0)
i = 1

# while True:
    # s = 'Message[{0}]'.format(i)
    # i += 1
        
    # f.write(struct.pack('I', len(s)) + s)   # Write str length and str
    # f.seek(0)                               # EDIT: This is also necessary
    # print 'Wrote:', s

    # n = struct.unpack('I', f.read(4))[0]    # Read str length
    # s = f.read(n)                           # Read str
    # f.seek(0)                               # Important!!!
    # print 'Read:', s

    # time.sleep(2)

while True:	
	s = 'Message[{0}]'.format(i)
	i += 1
	f.write(struct.pack('I', len(s)) + s)
	f.seek(0)
	print 'Wrote;', s	