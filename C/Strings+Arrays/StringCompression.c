//Brandon Broomell
//Takes a string and outputs the letter and count of that letter.
//If the newly created string is longer it will output the original.
//Page 91 Question 1.6



#include <stdio.h>
#include <stdlib.h>
#include <string.h>

char *returnString(char *string1);

int main()
{
  int lenStr, lenNewStr;
  char *str = (char *)malloc(sizeof(char) * 250);
  if(NULL == str)
  {
    printf("Memory Allocation Failed.");
    return 1;
  }

  char *newStr = (char *)malloc(sizeof(char) * 500);
  if(NULL == newStr)
  {
    printf("Memory Allocation Failed for newStr.");
    return 1;
  }

  printf("Enter string: ");
  fgets(str, 250, stdin);
  lenStr = strlen(str);

  newStr = returnString(str);
  lenNewStr = strlen(newStr);

  if(lenNewStr > lenStr)
  {
    printf("%s", str);
  }
  else
  {
    printf("%s", newStr);
  }
}

char *returnString(char *string1)
{
  char *newString = (char *)malloc(sizeof(char) * 500);
  char current;
  int i, j = 0, count, tempNum = 0;
  int len = strlen(string1);
  for(i = 0; i < len; i++)
  {
    current = string1[i];
    count = 0;
    newString[j++] = current;
    while(string1[i] == current)
    {
      count++;
      i++;
    }

    while(1)
    {
      if(count <= 0)
      {
        break;
      }
      else
      {
        tempNum = count % 10;
        count = count / 10;
        newString[j++] = tempNum + '0';
      }
    }
    i--;
  }
newString[--j] = '\0';
return newString;
}
