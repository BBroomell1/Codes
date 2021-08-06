//Brandon Broomell
//Removes duplicates from an unsorted linked list.
//Page 94 Question 2.1

#include <stdio.h>
#include <stdlib.h>

typedef struct Node{
    int data;
    struct Node* next;
}Node;

struct Node* createList();
void printList(Node* head);

int main(){

  Node* newList = createList();
  printList(newList);

  return 0;
}


struct Node* createList()
{
  struct Node* head = (Node*)malloc(sizeof(struct Node));
  if(NULL == head)
  {
    printf("Mem Alloc failed for head.");
    return NULL;
  }

  struct Node* newNode;
  struct Node* current;
  int i, temp;

  printf("Enter number: ");
  scanf("%d", &temp);
  head->data = temp;
  head->next = NULL;

  current = head;

  for(i = 0; i < 9; i++)
  {
    newNode = (Node*)malloc(sizeof(struct Node));
    if(NULL == newNode)
    {
      printf("Mem Allocation failed for current");
      return NULL;
    }
    printf("Enter number: ");
    scanf("%d", &temp);

    newNode->data = temp;
    newNode->next = NULL;

    current->next = newNode;
    current = current->next;

  }

  return head;
}

void printList(Node* head)
{
  if(NULL == head)
  {
    printf("list is empty");
  }
  Node* current = head;
  printf("List: ");
  while(NULL != current)
  {
    printf("%4d, ", current->data);
    current = current->next;
  }
  printf("\n");
}
