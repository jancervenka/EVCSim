a = 3 # acceleration [m / s^2]
T = 60 # 60 seconds

Vtrain = [a * i for i in range(0, T)] # acceleration phase
Vtrain += [Vtrain[-1]] * 2
Vtrain += [a * i for i in range(T - 1, 0, -4)] + [0] # intervention and decceleration

file = open("TestScenario1_B.txt", "w")
file.write('Vtrain 0 Vperm 140 VSBI 160 Vtarget null Vrel null ' + 
	       'Mode FS Supervision1 CSM Supervision2 NoS \n')

for i in range(len(Vtrain)):
    file.write('Vtrain' + ' ' + str(Vtrain[i]) + ' ')
    if i > 0 and Vtrain[i] == 141 and Vtrain[i - 1] == 138:
        file.write('Supervision2' + ' ' + 'WaS')

    elif i > 0 and Vtrain[i] == 162 and Vtrain[i - 1] == 159:
        file.write('Supervision2' + ' ' + 'IntS')

    elif i > 0 and Vtrain[i] == 129 and Vtrain[i - 1] == 141:
        file.write('Supervision2' + ' ' + 'NoS')

    file.write('\n')