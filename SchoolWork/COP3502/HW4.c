#include <stdio.h>
#include <stdlib.h>
#include <string.h>

typedef struct DONUT{
  char donutName[25];
  struct DONUT * left;
  struct DONUT * right;
}DONUT;

typedef struct SHOP{
  char shopName[25];
  struct SHOP * left;
  struct SHOP * right;
  struct DONUT * rootDonut;
}SHOP;

struct DONUT * addDonut(struct DONUT * root);
struct SHOP * addShop(struct SHOP * root, struct DONUT * donutPtr);
void findShop(struct SHOP * rootShop, char shopName[25]);
void printDonut(struct DONUT * rootDonut);
void iterateShop(struct SHOP * rootShop, char donutName[25]);
int findDonut(struct DONUT * rootDonut, char donutName[25]);
int main()
{
  char tempShop[25];
  int numDo, numShop, i;
  char tempDo[25];
  SHOP * shopPtr;
  int choice;
  do {
    scanf("%d", &choice);
    switch(choice){
    case 1:

    scanf("%d", &numDo);
    scanf("%d", &numShop);

    DONUT * donutPtr;
    for(i = 0; i < numDo; i++)
    {
      donutPtr = addDonut(donutPtr);
    }

    for(i = 0; i < numShop; i++)
    {
      shopPtr = addShop(shopPtr, donutPtr);
    }
    break;

    case 2:

    scanf("%s", tempDo);


    break;

    case 3:

    scanf("%s", tempShop);
    findShop(shopPtr, tempShop);
    break;
  }//END SWITCH
  } while(choice != 0);



}//END MAIN

struct SHOP * addShop(struct SHOP * root, struct DONUT * donutPtr)
{
  DONUT * curDonut;
  SHOP * newRoot = root;
  SHOP * newShop;
  char tempName[25];
  scanf("%s", tempName);
  if(root == NULL)
  {
    strcpy(newShop->shopName, tempName);
    newShop->rootDonut = donutPtr;
    return newShop;
  }

  {
    int tempNum = 0;
    SHOP * curShopPtr;
    curShopPtr = newRoot;
    while(tempNum ==0)
    {
      if(strcmp(curShopPtr->shopName, tempName) == 0)
      {
        curDonut = curShopPtr->rootDonut;
        while(curDonut != NULL)
        {
          curDonut = curDonut->left;
        }
          curDonut = donutPtr;
          tempNum = 1;
      }
      else if(curShopPtr->shopName[0] <= tempName[0])
      {
        curShopPtr = curShopPtr->left;
        if(curShopPtr == NULL)
        {
          curShopPtr = newShop;
          tempNum = 1;
        }
      }
      else if(curShopPtr->shopName[0] > tempName[0])
      {
        curShopPtr = curShopPtr->right;
        if(curShopPtr == NULL)
        {
          curShopPtr = newShop;
          tempNum = 1;
        }
      }
    }
  }
}//END addShop

struct DONUT * addDonut(struct DONUT * root)
{
  char tempName[25];
  scanf("%s", tempName);
  DONUT * newRoot = root;
  DONUT * tempPtr = malloc(sizeof(DONUT) * 1);
  if(NULL == tempPtr)
  {
    return NULL;
  }
  strcpy(tempPtr->donutName, tempName);
  tempPtr->right = NULL;
  tempPtr->left = NULL;

  if(NULL == newRoot)
  {
    newRoot = tempPtr;
  }
  else
  {
    int tempNum = 0;
      DONUT * curPtr;
      curPtr = newRoot;
      while(tempNum == 0)
      {
        if(curPtr == NULL)
        {
          curPtr = tempPtr;
          tempNum = 1;
        }
        else if(curPtr->donutName[0] <= tempPtr->donutName[0])
        {
          curPtr = curPtr->left;
        }
        else if(curPtr->donutName[0] > tempPtr->donutName[0])
        {
          curPtr = curPtr->right;
        }
      }
  }
  return newRoot;
}//END addDonut

void findShop(struct SHOP * rootShop, char shopName[25])
{
  if(rootShop == NULL)
  {
    return;
  }
  if(strcmp(rootShop->shopName, shopName) == 0)
  {
    printDonut(rootShop->rootDonut);
    return;
  }
  findShop(rootShop->left, shopName);
  findShop(rootShop->right, shopName);
  return;
}//END printDonut

void printDonut(struct DONUT * rootDonut)
{
  if(rootDonut == NULL)
  {
    return;
  }
  printDonut(rootDonut->left);
  printDonut(rootDonut->right);
  printf("%s", rootDonut->donutName);
  return;
}//END printDonut

void iterateShop(struct SHOP * rootShop, char donutName[25])
{
  int tempNumber = 0;
  if(NULL == rootShop)
  {
    return;
  }
  iterateShop(rootShop->left, donutName);
  iterateShop(rootShop->right, donutName);
  tempNumber = findDonut(rootShop->rootDonut, donutName);
  if(tempNumber == 0)
  {
    return;
  }
  else
  {
    printf(rootShop->shopName);
    return;
  }
}//END iterateShop

int findDonut(struct DONUT * rootDonut, char donutName[25])
{
  if(NULL == rootDonut)
  {
    return 0;
  }
  if(strcmp(rootDonut->donutName, donutName) == 0)
  {
    return 1;
  }
  else
  {
    return findDonut(rootDonut->left, donutName) + findDonut(rootDonut->right, donutName);
  }
}//END findDonut
