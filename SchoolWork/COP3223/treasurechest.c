//Brandon Broomell
//Date: 10/29/2017
//treasurechest.c
//Project 6
//COP 3223 Fall 2017


#include <stdio.h>
#include <stdlib.h>

#define MAX_KEYS 7
#define CLRS system("cls")


int main(){

//VARIABLES
    int i, j, correctKeys = 0;
    char *fileName;
    int key[7];
    int userInput[7];

//Allocate memory for file pointer and check if malloc worked. If not return 1 and then
fileName = malloc(sizeof(char) * 26);
if(NULL == fileName)
{
  fprintf(stderr, "malloc for fileName failed\n");
  return 1;
}



//PROMPT USER FOR NAME OF FILE
    printf("Enter the name of the file.\n");
    scanf("%s", fileName);


// OPEN FILE AND SCAN INTO ARRAY KEY
    FILE *ifp;
    ifp = fopen(fileName, "r");
    if(NULL == ifp)
    {
        fprintf(stderr, "Could not find file %c\n", fileName);
        return 1;
    }

//USE FOR LOOP TO SCAN EACH KEY INTO EACH INDEX OF ARRAY
    for(i = 0; i < MAX_KEYS; i++)
    {
        fscanf(ifp, "%d", &key[i]);
    }


    printf("\nThere are 7 keys used to unlock the burried treasure.\nEnter 7 numbers ranging 0 - 100 to guess the key combinations.\n\n");
// PROMPT USER FOR THEIR INPUTS IN FIRST LOOP THEN CHECK FOR DUPLICATES IN SECOND LOOP
    for(i = 0; i < MAX_KEYS; i++)
    {
          int temp;
          printf("Enter key %d: ", i + 1);
          scanf("%d", &userInput[i]);
          for(j = 0; j < i; j++)
          {
            if(userInput[i] == userInput[j])
            {
              printf("Can not use the same value.\n");
              printf("%d already exists.\n", userInput[i]);
              --i;
            }
          }
     }

//COMPARE KEY TO USER INPUT
for(i = 0; i < MAX_KEYS; i++)
{
  if(key[i] == userInput[i])
  {
    correctKeys++;
  }
}


//OUTPUT TO THE USER IF THEY OPENED THE CHEST OR NOT, IF NOT SHOW HOW MANY KEYS THEY GOT RIGHT
if(correctKeys == MAX_KEYS)
{
  printf("==================================================\n");
  printf("Congratulations you guessed the correct key code!!\n");
  printf("YOU OPENED THE TREASURE CHEST\n");
  printf("==================================================\n");
}
else
{
  printf("\n\n=====================\n");
  printf("You did not open the treasure chest. \nYou got %d keys correct.\n\n\n", correctKeys);
}

return 0;
}//END MAIN
