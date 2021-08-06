//Brandon Broomell
//COP 3502 FALL2018

#include <stdio.h>
#include <stdlib.h>

int binSearch(int start, int end, int numParasols, int numChairs, int * array);
int checkFunct(int length, int numParasols,int numChairs, int * array);

#define TRUE 1
#define FALSE 0
int main()
{
    int numChairs, numPar, i, maxLength, length;
    int *array;

    scanf("%d", &numChairs);
    scanf("%d", &numPar);

    array = malloc(sizeof(int) * numChairs);
    if(NULL == array)
    {
      return EXIT_FAILURE;
    }

    for(i = 0; i < numChairs; i++)
    {
      scanf("%d", &array[i]);
    }

    maxLength = ((array[numChairs - 1]) + (array[numChairs - 1] % numPar)) / numPar;

    length = binSearch(0, maxLength, numPar,numChairs - 1, array);
    printf("\n%d\n", length);

    free(array);
    array = NULL;
    return EXIT_SUCCESS;
}//END MAIN


int binSearch(int start, int end, int numParasols, int numChairs, int * array)
{
  if(start == end)
  {
    return start;
  }
  int mid, check = FALSE;
  mid = (start + end) / 2;


  check = checkFunct(mid, numParasols, numChairs, array);

  if(check == TRUE)
  {
    binSearch(start, mid, numParasols, numChairs, array);
  }
  else if(check == FALSE)
  {
    binSearch(mid + 1, end, numParasols, numChairs, array);
  }

}// END BINSEARCH

int checkFunct(int length, int numParasols, int numChairs, int * array)
{
  int * tempArr, b;
  int maxChairs = array[numChairs];
  int check = FALSE;

  tempArr = malloc(sizeof(int) * maxChairs + 1);
  if(NULL == tempArr)
  {
    return EXIT_FAILURE;
  }

  for(b = 0; b < maxChairs + 1; b++)
  {
    tempArr[b] = 0;
  }
int i, j = 0, k, placeHolder;
k = 0;
  for(placeHolder = 0; placeHolder <= maxChairs && j < numParasols;)
  {
    if(placeHolder == array[k])
    {
      k++;
      for(i = 0; i < length; i++)
      {
        tempArr[placeHolder] = TRUE;
        if(placeHolder == array[k])
        {
          k++;
        }
        placeHolder++;
      }
      j++;
    }
    else
    {
      placeHolder++;
    }
  }

  for(k = 0; k <= numChairs; k++)
  {
    if(tempArr[array[k]] == TRUE)
    {
      check = TRUE;
    }
    else
    {
      check = FALSE;
    }
  }
  free(tempArr);
  tempArr = NULL;
return check;
}// END CHECKFUNCT
