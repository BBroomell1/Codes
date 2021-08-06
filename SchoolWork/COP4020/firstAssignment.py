def mult2(x,y):
    return x * y


def mult3(x,y,z):
    return x * y * z


def first_a(n):
    return [x for x in range(1,n+1) if (x % 6 == 0 or x % 11 == 0)]


def first_b(n):
    return [x for x in range(1,n+1) if isMult6Or11(x) == True]
def isMult6Or11(x):
    return True if x % 6 == 0 or x % 11 == 0 else False


def second_a(n):
    return [x for x in range(1,n+1) if str(x) == str(x)[::-1] and str(x)[0] == '3']


def second_b(n):
    return [x for x in range(1,n+1) if isPalindromeThatStartsWithDigit3(x) == True]
def  isPalindromeThatStartsWithDigit3(x):
    return True if str(x) == str(x)[::-1] and str(x)[0] == '3' else False
