//Brandon Broomell
//COP 3502C
//Assignment3

#include <stdio.h>
#include <stdlib.h>
#include <string.h>

typedef struct Node
{
  char  description[20];
  struct Node * next;
}Node;

typedef struct Bags
{
  int cost;
  int weight;
  char bagContents[20];
  struct Bags * nextBag;
} Bags;

typedef struct Location
{
  struct Bags * topBag;
} Location;

struct Bags * push(struct Bags *topBag);
struct Bags * pop(struct Bags *topBag);
struct Node * pushPtr(struct Node *headPtr, char contents[20]);
struct Node * freePtr(struct Node *headPtr);
void printPtr(struct Node *headPtr);



int main()
{
  int choice, i;
  int inputLocation, money, capacity, numBags, totalCost;
  char tempContents[20];

  Node * headPtr;
  //Allocate a ptr to location struct
  Location *location_ptr = malloc(sizeof(Location) * 1000);
  if(NULL == location_ptr)
  {
    return EXIT_FAILURE;
  }

  do {
    scanf("%d", &choice);

    switch(choice){
      case -1 :
        totalCost = 0;
        scanf("%d", &inputLocation);
        scanf("%d", &money);
        scanf("%d", &capacity);
        if(NULL == location_ptr[inputLocation].topBag)
        {
          printf("%d\n", 0);
        }
        else
        {
          while((money >= location_ptr[inputLocation].topBag->cost) && (capacity >= location_ptr[inputLocation].topBag->weight) && (location_ptr[inputLocation].topBag != NULL))
          {
            money -= location_ptr[inputLocation].topBag->cost;
            capacity -= location_ptr[inputLocation].topBag->weight;
            totalCost += location_ptr[inputLocation].topBag->cost;
            strcpy(tempContents, location_ptr[inputLocation].topBag->bagContents);
            headPtr = pushPtr(headPtr, tempContents);
            location_ptr[inputLocation].topBag = pop(location_ptr[inputLocation].topBag);
          }
          printf("%d ", totalCost);
          printPtr(headPtr);
          printf("\n");
          headPtr = freePtr(headPtr);
        }//END ELSE
        break;//END case -1

      case 1 :
        scanf("%d", &inputLocation);
        scanf("%d", &numBags);
        for(i = 0; i < numBags; i++)
        {
            location_ptr[inputLocation].topBag = push(location_ptr[inputLocation].topBag);
        }//END FOR LOOP
        break;//END case 1
    }//END switch
  } while(choice != 0);
}//END MAIN

int isEmpty(struct Bags *topBag)
{
  if(NULL == topBag)
  {
    return 1;
  }
  else
  {
    return 0;
  }
}//END isEmpty

struct Bags * push(struct Bags *topBag)
{
  Bags *curBag = malloc(sizeof(Bags) * 1);
    if(NULL == curBag)
    {
      return NULL;
    }
    curBag->nextBag = NULL;
    scanf("%d", &curBag->cost);
    scanf("%d", &curBag->weight);
    scanf("%s", curBag->bagContents);
    if(NULL == topBag)
    {
      return curBag;
    }
    else
    {
      curBag->nextBag = topBag;
      return curBag;
    }
}//END push

struct Bags * pop(struct Bags *topBag)
{
  if(NULL == topBag)
  {
    return NULL;
  }
  else
  {
    Bags * tempBag = topBag->nextBag;
    free(topBag->nextBag);
    free(topBag);
    return tempBag;
  }
}//END pop

struct Node * pushPtr(struct Node *headPtr, char contents[20])
{
  Node * curPtr = malloc(sizeof(Node) * 1);
  if(NULL == curPtr)
  {
    printf("curPtr malloc failed");
    return NULL;
  }
  curPtr->next = NULL;

  strcpy(curPtr->description, contents);
  if(NULL ==  headPtr)
  {
    return curPtr;
  }
  else
  {
    curPtr->next = headPtr;
    return curPtr;
  }
}//END pushPtr

void printPtr(struct Node *headPtr)
{
  if(NULL == headPtr)
  {
    return;
  }
  printPtr(headPtr->next);
  printf("%s ", headPtr->description);
}//END printPtr

struct Node *freePtr(struct Node *headPtr)
{
  if(NULL == headPtr)
  {
    return NULL;
  }
  else
  {
    Node *tempPtr = headPtr->next;
    free(headPtr);
    return freePtr(tempPtr);
  }
}//END freePtr
