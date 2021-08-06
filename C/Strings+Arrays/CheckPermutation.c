//Brandon Broomell
//Checks if one string input is a permutation of another string input.
//Page 90 Question 1.2


#include <stdio.h>
#include <stdlib.h>
#include <string.h>

#define ASCII_NUMBER 255
#define MAX_STRING_LENGTH 250

int isPermutation(char *string1, char *string2);
int main()
{
  int result = -1;
  char *string1 = (char *)malloc(MAX_STRING_LENGTH);
  char *string2 = (char *)malloc(MAX_STRING_LENGTH);
  if(NULL == string1 || NULL == string2)
  {
    printf("No Memory Allocation\n");
    return 1;
  }

  printf("Enter string1: ");
  fgets(string1, MAX_STRING_LENGTH, stdin);

  printf("Enter string2: ");
  fgets(string2, MAX_STRING_LENGTH, stdin);

  int len1 = strlen(string1);
  int len2 = strlen(string2);

  if(len1 >= len2)
  {
    result = isPermutation(string1, string2);
  }
  if(len2 > len1)
  {
    result = isPermutation(string2, string1);
  }

  if(result == 1)
  {
    printf("Yes one is a permutation of the other!");
    return 0;
  }
  else if(result == 0)
  {
    printf("No one is not a permutataion of the other!");
    return 0;
  }

  printf("Something went wrong!!!!");
  return 0;
}

int isPermutation(char *longer, char *shorter)
{
  printf("longer: %s", longer);
  printf("shorter: %s", shorter);
  int counter = 0, i = 0, j = 0;
  int lenLong, lenShort;
  lenLong = strlen(longer);
  lenShort = strlen(shorter);

  while(counter < lenLong)
  {
    j = counter;
    if(longer[j] == shorter[0])
    {
      for(i = 0; i < lenShort; i++)
      {
        printf("longer char : %c\n", longer[j]);
        printf("shorter char : %c\n", shorter[i]);
        printf("i: %d\n", i);
        printf("lenShort: %d\n", lenShort);
        if(lenShort-1 == i)
        {
          return 1;
        }
        else if(j > lenLong)
        {
          printf("break for len\n");
          break;
        }
        else if(shorter[i] != longer[j])
        {
          printf("break else if\n");
          break;
        }
        j++;
      }
    }
    counter++;
  }
  return 0;
}
