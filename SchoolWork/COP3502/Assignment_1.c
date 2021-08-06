//BRANDON BROOMELL
//COP 3502 FALL 2018

#include <stdio.h>
#include <stdlib.h>
#include <string.h>

#define ERROR -1
typedef struct Convoy
{
  int shipID;
  int totalContainers;
  int *wordLength;
  char **contents;
}Convoy;

int main()
{
  //VARIABLES
  // n = number of containers, k = number of letters in word of container contents.
  // SHIPCOUNTER counts number of ships currently in convoy, counterID is used for shipID.
  // i, j, and h are used for loop counters
  int baseSize = 2;
  int eventVar, counterID = 0, shipCounter = 0, n, k, i, j, h;
  int position, container, tempID;

  //MALLOC Struct
  Convoy *conv = malloc(sizeof(struct Convoy) * baseSize);
  if(NULL == conv)
  {
    return EXIT_FAILURE;
  }

  do {
    scanf("%d", &eventVar);

    if(0 > eventVar || eventVar > 3)
    {
      printf("%d", ERROR);
    }

    else if(eventVar == 0)// SHIP LEAVES CONVOY
    {
      int testCase = 0;
      scanf("%d", &tempID);
      int i = 1;
      while(i <= shipCounter && testCase == 0)
      {
        if(conv[i].shipID == tempID)
        {
          testCase = 1;
        }
        else
        {
          testCase = 0;
          i++;
        }
      }// END WHILE LOOP THAT LOOKS FOR SHIP

      if(testCase == 1)
      {
        if(i == shipCounter) // if ship is last in convoy
        {
          for(j = 0; j < conv[i].totalContainers; j++)
          {
            free(conv[i].contents[j]);
          }
          free(conv[i].contents);
          free(conv[i].wordLength);
          conv[i].shipID = 0;
          conv[i].totalContainers = 0;
          shipCounter--;
        }// END IF STATEMENT SAYING SHIP IS LAST IN CONVOY
        else //realloc the ship at i with the same mem as the last ship, transfer all contents to ship at i, then free the last ship
        {
          conv[i].contents = (char **)realloc(conv[i].contents, sizeof(char *) * conv[shipCounter].totalContainers);
          if(conv[i].contents == NULL)
          {
            return EXIT_FAILURE;
          }
          else
          {
            int len;
            for(j = 1; j <= conv[shipCounter].totalContainers; j++)
            {
              len = conv[shipCounter].wordLength[j];
              free(conv[i].contents[j]);
              conv[i].contents[j] = malloc(sizeof(char) * len);
              if(conv[i].contents[j] == NULL)
              {
                return EXIT_FAILURE;
              }
              else
              {
                strcpy(conv[i].contents[j], conv[shipCounter].contents[j]);
              }
            }
            conv[i].shipID = conv[shipCounter].shipID;
            conv[i].totalContainers = conv[shipCounter].totalContainers;
          }//END ELSE FOR SHIP NOT LAST IN LINE
          shipCounter--;
        }//END ELSE
      }//END TEST CASE 1 (SHIP FOUND)
      else
      {
        printf("%d", ERROR);
      }//SHIP NOT FOUND
    }

    else if(eventVar == 1)// SHIP ENTERS CONVOY
    {
      counterID++;
      shipCounter++;
      if(shipCounter >= baseSize)// CHECK SIZE OF SHIP STRUCT ARRAY REALLOC IF NEEDED
      {
        baseSize *= 2;
        conv = (Convoy*)realloc(conv, baseSize * sizeof(Convoy));
        if(conv == NULL)
        {
          return EXIT_FAILURE;
        }
      }
      scanf("%d", &n);
      conv[shipCounter].totalContainers = n;
      conv[shipCounter].wordLength = malloc(sizeof(int) * n);
      if(NULL == conv[shipCounter].wordLength)
      {
        return EXIT_FAILURE;
      }
      conv[shipCounter].shipID = counterID;

      conv[shipCounter].contents = (char **)malloc(sizeof(char *) * n);
      if(NULL == conv[shipCounter].contents)
      {
        return EXIT_FAILURE;
      }
      for(i = 1; i <= n; i++)
      {
        scanf("%d", &k);
        conv[shipCounter].wordLength[i] = k;
        conv[shipCounter].contents[i] = malloc(sizeof(char) * (k + 1));
        if(NULL == conv[shipCounter].contents[i])
        {
          return EXIT_FAILURE;
        }
        else
        {
          scanf("%s", conv[shipCounter].contents[i]);
        }
      }
    }

    else if(eventVar == 2)// REQUEST ID OF SHIP
    {
      scanf("%d", &position);
      if(1 <= position && position <= shipCounter)
      {
        printf("%d\n", conv[position].shipID);
      }
      else
      {
        printf("%d\n", ERROR);
      }
    }

    else if(eventVar == 3)//REQUEST CONTENTS OF CONTAINER
    {
      scanf("%d", &position);
      if(1 > position || position > shipCounter)
      {
        printf("%d\n", ERROR);
      }
      else
      {
        scanf("%d", &container);
        if(1 > container || container > conv[position].totalContainers)
        {
          printf("%d\n", ERROR);
        }
        else
        {
          printf("%s\n", conv[position].contents[container]);
        }
      }
    }
  } while(eventVar != -1);

  //FREE MEMORY
  for(i = 1; i <= shipCounter; i++)
  {
    for(j = 1; j <= conv[i].totalContainers; j++)
    {
      free(conv[i].contents[j]);
    }
    free(conv[i].contents);
    free(conv[i].wordLength);
  }
  free(conv);

  printf("\n");
  return EXIT_SUCCESS;
}//END MAIN
