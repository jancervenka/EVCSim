import matplotlib.pyplot as plt


Gradient = [5 for i in range(0, 999)]
Gradient += [-5 for i in range(0, 4000)]

plt.plot(Gradient, color = 'r')
plt.xlabel('distance [m]')
plt.ylabel('gradient [‰]')
plt.title('Gradient Profile Test')
plt.xlim([0, len(Gradient) - 1])
plt.ylim([-7, 7])
plt.show()