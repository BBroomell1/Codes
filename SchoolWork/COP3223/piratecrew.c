// Brandon Broomell
// COP 3223 Fall 0R04
// Date: 9/13/2017
// Assignment #2 piratecrew.c


#include <stdio.h>
#include <stdlib.h>
#include <ctype.h>


int main()
{
//Variables
    int swim = 0;
    char answer;
    char answer2;


// Ask if like digging
    printf(" Do you like digging for treasure??\n (Press 'Y' for yes or 'N' for no)\n");
    scanf(" %c", &answer);
// Ask if able to swim
    printf(" Are you able to swim??\n (Press 'Y' for yes or 'N' for no)\n");
    scanf(" %c", &answer2);




// Conditional statements
    if(toupper(answer) == 'Y')
    {
        if(toupper(answer2) == 'Y')
// If able to swim see how far
        {
            printf("How far can you swim in meters??\n (Answer must be 0 or more meters)\n");
            scanf("%d", &swim);

            if(swim >= 100)
            {
                printf(" You may join the crew! Arrr!\n");
            }
// If any condition fails "you may not join crew"
            else
            {
                printf("You may not join the crew.\n");
            }
        }
        else
        {
            printf("You may not join the crew.\n");
        }
    }
    else
    {
        printf("You may not join the crew.\n");
    }







    return 0;
}// END MAIN

