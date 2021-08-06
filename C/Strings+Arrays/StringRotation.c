//Brandon Broomell
//Check if one string is a rotation of another
//Page 91 Question 1.9

#include <stdio.h>
#include <stdlib.h>
#include <string.h>

#define SIZE 500
int isSubstring(char *string1, char *string2);

int main()
{
  int result;
  char *string1 = (char *)malloc(sizeof(char) * SIZE);
  char *string2 = (char *)malloc(sizeof(char) * SIZE);
  if(NULL == string1 || NULL == string2)
  {
    printf("mem allocation failed for string1 or string2");
    return 1;
  }

  printf("Enter first string: ");
  fgets(string1, SIZE, stdin);

  printf("Enter second string: ");
  fgets(string2, SIZE, stdin);

  result = isSubstring(string1, string2);
  if(result == 1)
  {
    printf("Yes!");
  }
  else if(result == 0)
  {
    printf("No!");
  }

free(string1);
free(string2);
return 0;
}

int isSubstring(char *string1, char *string2)
{
  int mainCounter, i, j;
  int lenString1 = strlen(string1) - 1;
  int lenString2 = strlen(string2) - 1;
  if(lenString1 == lenString2)
  {
    for(mainCounter = 0; mainCounter < lenString1; mainCounter++)
    {
      if(string1[mainCounter] == string2[0])
      {
        i = mainCounter;
        j = 0;
        while(string1[i] == string2[j])
        {

          j++;
          i++;
          if(i == lenString1)
          {
            i = 0;
          }
          printf("I: %5d  J: %d\n", i, j);
          if(lenString2 == j && mainCounter == i)
          {
            return 1;
          }
        }
      }
    }
  }
  return 0;
}
