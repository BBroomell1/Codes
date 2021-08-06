//Brandon Broomell
//Question 3 for BIS
#include <stdio.h>
#include <stdlib.h>

#define NUM 2

int reducePolution(int A[], int N);


int main()
{
  int A[NUM] = {10,10};

  printf("%d", reducePolution(A, NUM));


  return 0;
}

int reducePolution(int A[], int N)
{
  float newA[N];
  float total, half, high,  tempHalf = 0;
  int i, tempPosition, counter = 0;
  for(i = 0; i < N; i++)
  {
    newA[i] = (float)A[i];
    total += A[i];
  }
  half = total / 2;

  do
  {
    tempPosition = 0;
    tempHalf = 0;
    high = 0;
    for(i = 0; i < N; i++)
    {
      if(high < newA[i])
      {
        high = newA[i];
        tempPosition = i;
      }
    }
    tempHalf = high / 2;
    newA[tempPosition] = tempHalf;
    total -= tempHalf;
    counter++;
  }while(total > half);
  return counter;
}
