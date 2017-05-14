import matplotlib.pyplot as plt

a = 3 # acceleration [m / s^2]

Vperm = [100 for i in range(0, 15)]
Vperm += [a * i for i in range(33, 18, -1)]
Vperm += [55 for i in range(30, 0, -1)]

Vtrain = [70 for i in range(0, 15)]
Vtrain += [a * i for i in range(23, 17, -1)]
Vtrain += [52 for i in range(39, 0, -1)]

file = open("TestScenario2.txt", "w")
file.write('Vtrain 70 Vperm 100 VSBI 160 Vtarget 55 Vrel null ' + 
	       'Mode FS Supervision1 PIM Supervision2 NoS \n')

for i in range(len(Vtrain)):
    file.write('Vtrain' + ' ' + str(Vtrain[i]) + ' ')
    file.write('Vperm' + ' ' + str(Vperm[i]) + ' ')
    if Vperm[i] == 99:
        file.write('Supervision1' + ' ' + 'TSM')

    elif i > 0 and Vperm[i] == 55 and Vperm[i - 1] == 57:
        file.write('Vtarget' + ' ' + 'null' + ' ')
        file.write('Supervision1' + ' ' + 'CSM')
    
    file.write('\n')

plt.plot(Vtrain, color = 'b')
plt.plot(Vperm, color = 'y')
plt.xlabel('time [s]')
plt.ylabel('speed [km/h]')
plt.title('ETCS DMI Test Scenario 2')
plt.xlim([0, len(Vperm) - 1])
plt.ylim([0, 200])
plt.show()
