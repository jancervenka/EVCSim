import numpy as np
import matplotlib.pyplot as plt
import matplotlib.patches as mptch


a = 3
T = 60
Vtrain = [a * i for i in range(0, T)]
Vtrain += [Vtrain[-1]] * 2
Vtrain += [a * i for i in range(T - 1, 0, -4)] + [0]
Vperm = [140 for i in range(len(Vtrain))]
VSBI = [160 for i in range(len(Vtrain))]
Mode = ['FS' for i in range(len(Vtrain))]
Vtarget = ['Null' for i in range(len(Vtrain))]
Vrel = ['Null' for i in range(len(Vtrain))]
Dtarget = ['Null' for i in range(len(Vtrain))]
Supervision1 = ['CSM' for i in range(len(Vtrain))]
Supervision2 = []

for i in range(len(Vtrain)):
	if Vtrain[i] < Vperm[i]:
		Supervision2.append('NoS')
	else:
		if Vtrain[i] < VSBI[i]:
			if Vtrain[i + 1] - Vtrain[i] > 0:
				Supervision2.append('WaS')
			else:
				Supervision2.append('IntS')
		else:
			Supervision2.append('IntS')

file = open("TestScenario1.txt", "w")
file.write('Vtrain Vperm VSBI Vtarget Vrel Dtarget Mode Supervision1 Supervision2 \n')

for i in range(len(Vtrain)):
	file.write(str(Vtrain[i]) + ' ' + 
		       str(Vperm[i]) + ' ' + 
		       str(VSBI[i]) + ' ' +
		       str(Vtarget[i]) + ' ' + 
		       str(Vrel[i]) + ' ' + 
		       str(Dtarget[i]) + ' ' +  
		       str(Mode[i]) + ' ' + 
		       Supervision1[i] + ' ' + 
		       Supervision2[i] + '\n')


VtrainPatch = mptch.Patch(color = 'blue', label = 'Train speed')
VOvSPatch = mptch.Patch(color = 'yellow', label = 'OvS limit')
VSBIPatch = mptch.Patch(color = 'red', label = 'SBI limit')

plt.plot(Vtrain)
plt.plot(Vperm, color = 'y')
plt.plot(VSBI, color = 'r')
plt.legend(handles = [VtrainPatch, VOvSPatch, VSBIPatch])
plt.xlabel('Time [s]')
plt.ylabel('Speed [km/h]')
#plt.title('ETCS DMI Test Scenario 1')
plt.xlim([0, len(Vtrain) - 1])
plt.ylim([0, 300])
plt.show()