from fnmatch import translate
import copy

states = input().split(',')
states[0] = states[0].replace("{", "")
states[-1] = states[-1].replace("}", "")

alphabet = input().split(',')
alphabet[0] = alphabet[0].replace("{", "")
alphabet[-1] = alphabet[-1].replace("}", "")

FinalStates = input().split(',')
FinalStates[0] = FinalStates[0].replace("{", "")
FinalStates[-1] = FinalStates[-1].replace("}", "")

num = int(input())

transitions = dict()
for j in range(0 , len(states)):
    transitions[states[j]] = dict()
for i in range(0 , num):
    line = input().split(',')
    transitions[line[0]][line[1]] = line[2] 

myinput = input()
def func2(a):
    alaki = list()
    # if('$' in transitions[a]):
    while('$' in transitions[a]):
        alaki.append(a)
        a = transitions[a]['$']
    return alaki
def func1(a, b):
    if (b in transitions[a]):
        return True
first = list()
first.append(states[0])


result = 0 

def func(a):
    global first
    global result
    size = len(a)
    for p in range(0 , size):
        sec = list()
        for n in range(0 , len(first)):
            if func1(first[n], a[p]):
                sec.append(transitions[first[n]][a[p]])
                for k in range(0 , len(func2(first[n]))):
                    sec.append(func2(first[n])[k])
            else: 
                if(len(first) == 0):
                    result = 0
                    n = len(first)
                    p = size + 1
                    break
                    # print("Rejected") 
                n -= 1
        first = copy.deepcopy(sec)
    
    for u in range (0, len(first)):
        
        if(first[u] in FinalStates):
            result = 1
            break
    if(result == 1):
        print("Accepted") 
    else: 
        print("Rejected")
func(myinput)