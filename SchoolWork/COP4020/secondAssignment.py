def word_smith(str1, str2):
    secondStringAsSet = set()
    firstStringList = []
    finalList = []
    counter = 0
    vowels = ['a', 'e', 'i', 'o', 'u', 'y']

    def beginWithVowel(letter):
        for strv in vowels:
            if letter == strv:
                return True
        return False

    def makeSecondStringSet(str):
        counter = 0
        while(counter < len(str)):
            if ((str[counter].isalpha()) and (str[counter] != " ")):
                secondStringAsSet.add(str[counter])
                counter += 1
            else:
                counter += 1
        counter = 0

    def makeFirstStringList(string):
        counter = 0
        word = ""
        length = len(string)
        while(counter < length):
            while((counter < length) and (string[counter] != " ") and (string[counter].isalpha)):
                word += string[counter]
                counter += 1
            firstStringList.append(word)
            word = ""
            counter += 1
        counter = 0

    def firstContainsSecond(string):
        counter = 0
        length = len(string)
        for character in string:
            if character in secondStringAsSet:
                return True
        return False

    makeFirstStringList(str1)
    makeSecondStringSet(str2)

    for string in firstStringList:
        if(len(string) < 4):
            continue
        elif(beginWithVowel(string[0].lower())):
            finalList.append(string)
        elif(firstContainsSecond(string)):
            finalList.append(string)
        else:
            continue

    return len(finalList)


def base_builder(n):
    sumValue = 0
    quatValue = ""
    def baseConversion(n):
        if n <= 0:
            return ""
        if (n / 4) <= 0:
            return str(n % 4)
        else:
            return baseConversion(int(n / 4)) + str(int(n % 4))

    quatValue = baseConversion(n)
    for char in quatValue:
        sumValue += int(char)

    answer = sumValue, int(quatValue)
    return answer
