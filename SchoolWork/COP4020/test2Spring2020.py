import pandas as pd
from graphics import *

def test2Problem1(str):
    reverseString = ""
    res = ""
    vowels = ['a', 'e', 'i', 'o', 'u', 'y']
    def reverse(str):
        if len(str) == 0:
            return str
        else:
            return reverse(str[1:]) + str[0]

    def isVowel(letter, vowels):
            for strv in vowels:
                if letter == strv:
                    return True
            return False


    reverseString = reverse(str)
    for i in range (0, len(reverseString)):
        if isVowel(reverseString[i].lower(), vowels) == False:
            res = res + reverseString[i]
        else:
            continue
    return res

def test2Problem2(n):
    count = 0
    maxCount = n * 2
    primeList = []
    i = 2
    def isPrime(n):
        for i in range(2, int(n/2) + 1):
            if n % i == 0:
                return False
        else:
            return True

    while count < maxCount:
        if isPrime(i) == True:
            primeList.append(i)
            count = count + 1
            i = i + 1
        else:
            i = i + 1
    return [x for x in primeList if primeList.index(x) % 2 == 0]





df = pd.read_csv("http://rcs.bu.edu/examples/python/data_analysis/Salaries.csv")

def test2Problem3a(df):
    df_maleSalary = df.query("sex=='Male'")['salary'].mean()
    return round(df_maleSalary,2)
def test2Problem3b(df):
    df_femaleAssistant = df.query("sex == 'Female'")
    df_femaleAssistant = df_femaleAssistant.query("rank == 'AsstProf'")['salary'].max()
    return round(df_femaleAssistant, 2)
def test2Problem3c(df):
    return round(df['salary'].std(), 2)

def test2Problem4():
    win = GraphWin()

    point1 = Point(50, 50)


    circle1 = Circle(point1, 25)
    circle1.setFill('orange')
    circle1.draw(win)
    point1.draw(win)

    point2 = Point(150, 50)


    circle2 = Circle(point2, 25)
    circle2.setFill('green')
    circle2.draw(win)
    point2.draw(win)

    triangle = Polygon(Point(100,75),Point(75,100),Point(125,100))
    triangle.setFill('red')
    triangle.draw(win)

    rectangle = Rectangle(Point(50,125),Point(150,150))
    rectangle.setFill('blue')
    rectangle.draw(win)

    label = Text(Point(100,175), 'It is another day in paradise')
    label.setSize(7)
    label.draw(win)

    message = Text(Point(win.getWidth()/2, 190), 'Click anywhere to quit.')
    message.draw(win)
    win.getMouse()
    win.close()


test2Problem4()
