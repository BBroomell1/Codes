// Brandon Broomell
// Lab 1 Problem
// Date: 11/19/2017
// COP 3502C - 17FALL 0003


#include <stdio.h>


char ToLower(char myLetter);
void printBinary(int someNum);
int main()
{
  char letter = 'A';
  char lower;
  int number = 0;


  printf(" \t\t UPPER\t\t\t\t\t\t\tlower\n");
  printf(" \t\t =====\t\t\t\t\t\t\t=====\n");
  printf("\tbinary\t\thex\tDec\t--\t\t\tbinary\t\thex\tdec\t--\n");
  while(letter >= 'A' && letter <= 'Z')
  {
    printf("\t");
    printBinary(letter);
    printf("\t%x", letter);
    printf("\t%d", letter);
    printf("\t%c", letter);
    printf("\t\t\t");
    lower = ToLower(letter);
    printBinary(lower);
    printf("\t%x", lower);
    printf("\t%d", lower);
    printf("\t%c", lower);
    printf("\n");
    letter++;
  }

}//END MAIN


char ToLower(char myLetter)
{
   int num = 32;
  char letter;
  letter = myLetter | num;
  return letter;
}

void printBinary(int someNum)
{
  int i;
  for(i = 7; i >= 0; i--)
  {
    putchar((someNum &(1 << i)) ? '1' : '0');

    if(i == 4)
    printf(" ");
  }
}
