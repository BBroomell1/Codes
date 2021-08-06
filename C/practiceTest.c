#include <stdio.h>
#include <stdlib.h>
#include <math.h>

int solution(int N);

int main(){
 printf("%d", solution(499));
}

int solution(int N)
{
  int firstSum = 0;
  int result = 0;
  int temp[5] = {0,0,0,0,0};
  int temp2[5] = {0,0,0,0,0};
  int i = 0, j = 0;

  i = 4;
  while(N > 0)
  {
    temp[i] = N % 10;
    N = N / 10;
    i--;
  }
  for(i = 0; i < 5; i++)
  {
    firstSum += temp[i];
    temp2[i] = temp[i];
  }

  i = 4;
  j = 4;

  while(firstSum > 0)
  {
      if(firstSum - (9-temp[j]) > 0)
      {
        temp2[i] = 9;
        firstSum -= (9-temp[j]);
      }
      else
      {
        temp2[i] = firstSum + temp[j];
        firstSum -= firstSum;
      }
    j--;
    i--;
  }

  int counter = 4;
  for(i = 0; i < 5; i++)
  {
    result += temp2[i] * pow(10, counter);
    counter --;
  }
 return result;
}
