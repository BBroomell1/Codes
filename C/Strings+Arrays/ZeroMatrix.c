//Brandon Broomell
//Checks a M X M matrix for zeroes. If found, will replace all other numbers in that column and row with zeroes.
//Page 91 Question 1.8

#include <stdio.h>
#include <stdlib.h>

#define SIZE 4

int** zeroMatrix(int matrix[][SIZE]);
void print(int** arr);
void destroy(int** arr);

int main(){
  int matrix[SIZE][SIZE] = {{0,1,2,3},{4,5,6,7},{8,9,0,1},{2,3,4,5}};

  int **newMatrix;
  newMatrix = zeroMatrix(matrix);
  print(newMatrix);
  destroy(newMatrix);
  return 0;
}

int** zeroMatrix(int matrix[SIZE][SIZE])
{
  int i, j;
  int **solution = (int **)malloc(sizeof(int *) * SIZE);
  if(NULL == solution)
  {
    printf("mem failed for solution.");
    return NULL;
  }
  for(i = 0; i < SIZE; i++)
  {
    solution[i] = (int *)malloc(sizeof(int) * SIZE);
    if(NULL == solution[i])
    {
      printf("mem failed for solution.");
      return NULL;
    }
  }

  int *rowsForZero = (int *)malloc(sizeof(int) * SIZE);
  int *colForZero = (int *)malloc(sizeof(int) * SIZE);
  if(NULL == rowsForZero || NULL == colForZero)
  {
    printf("mem failed for rows or cols for Zero.");
    return NULL;
  }
  for(i = 0; i < SIZE; i++)
  {
    rowsForZero[i] = 0;
    colForZero[i] = 0;
  }

  //Get rows and colunmns that are going to be zero.

  for(i = 0; i < SIZE; i++)
  {
    for(j = 0; j < SIZE; j++)
    {

      if(matrix[i][j] == 0)
      {
        rowsForZero[i] = 1;
        colForZero[j] = 1;
      }
    }
  }

  for(i = 0; i < SIZE; i++)
  {
    for(j = 0; j < SIZE; j++)
    {
      if(rowsForZero[i] == 1 || colForZero[j] == 1)
      {
        solution[i][j] = 0;
      }
      else
      {
        solution[i][j] = matrix[i][j];
      }
    }
  }

  free(rowsForZero);
  free(colForZero);
  return solution;
}

void print(int** arr)
{
  int i, j;
  for(i = 0; i < SIZE; i++)
  {
    printf("\n");
    for(j = 0; j < SIZE; j++)
    {
      printf("%5d", arr[i][j]);
    }
  }
}

void destroy(int** arr)
{
  int i;
  for(i = 0; i < SIZE; i++)
  {
    free(arr[i]);
  }
  free(arr);
}
