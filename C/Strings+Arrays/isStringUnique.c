//Brandon Broomell
//Checks to see if a string has all unique characters.
//Page 90 Question 1.1


#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <ctype.h>

#define MAX_CHARACTERS 100
#define ASCIITOTALNUMBER 255
int isUnique(char string[]);

int main(){
  char *str = (char *)malloc(MAX_CHARACTERS);
  if(NULL == str)
  {
    printf("No Memory allocated to str!");
    return 1;
  }
  printf("Enter String: ");
  fgets(str, MAX_CHARACTERS, stdin);


  if(isUnique(str) == 1)
  {
    printf("The string has all unique characters.");
  }
  else
  {
    printf("The string does not have all unique characters.");
  }
  free(str);
  return 0;
}

int isUnique(char string[])
{
  int array[ASCIITOTALNUMBER];
  int i;
  int len = strlen(string);
  printf("string length is: %d\n", len);
  int ascVal;
  for(i = 0; i < ASCIITOTALNUMBER; i++)
  {
    array[i] = 0;
  }

  for(i = 0; i < len; i++)
  {
    ascVal = (int)(string[i]);
    printf("%c : %d\n", string[i], ascVal);
    printf("array value: %d\n\n", array[ascVal]);
    if(array[ascVal] == 1)
    {
      return 0;
    }
    else
    {
      array[ascVal] = 1;
    }
  }

  return 1;
}
