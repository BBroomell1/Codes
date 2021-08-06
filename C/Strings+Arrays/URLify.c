//Brandon Broomell
//Replaces all spaces in a string with %20
//Page 90 Question 1.3


#include <stdio.h>
#include <stdlib.h>
#include <string.h>

void URLify(char *string1, int num);

int main()
{
  int sizeOfString = 0;
  char *str = (char *)malloc(sizeof(char) * 250);
  if(NULL == str)
  {
    printf("No memory allocated\n");
    return 1;
  }
  printf("Enter the string: ");
  fgets(str, 250, stdin);

  printf("Enter size of string: ");
  scanf("%d", &sizeOfString);

  URLify(str, sizeOfString);
}

void URLify(char *string1, int num)
{
  char newString[250];
  int i = 0, j = 0;
  for(i = 0; i < num; i++)
  {
    if(string1[i] == ' ')
    {
      newString[j++] = '%';
      newString[j++] = '2';
      newString[j++] = '0';
    }
    else
    {
      newString[j++] = string1[i];
    }
  }
  int newLength = strlen(newString);
  for(i = 0; i < newLength; i++)
  {
    printf("%c", newString[i]);
  }
}
