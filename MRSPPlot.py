import matplotlib.pyplot as plt

MRSP = [120 for i in range(0, 2999)]
MRSP += [60 for i in range(0, 2000)]

plt.plot(MRSP, color = 'b')
plt.xlabel('distance [m]')
plt.ylabel('MRSP value [km/h]')
plt.title('MRSP Test')
plt.xlim([0, len(MRSP) - 1])
plt.ylim([0, 150])
plt.show()
