// Brandon Broomell
// COP 3223 FALL 0R04
// Assignment 3 pirategear.c
// 9/24/2017

#include <stdio.h>
#include <stdlib.h>

#define NEW 15
#define USED 5
int main()
{

// Variables
    int newGear = 0, usedGear = 0, totalCost = 0, choice = 0, pieces = 0, totalPieces = 0;
    float average = 0;

// START LOOP
    do
    {
        printf("What would you like to do??\n");
        printf(" 1) Buy New Gear\n");
        printf(" 2) Buy Used Gear\n");
        printf(" 3) Quit\n");

        scanf("%d", &choice);

        //Calculate for new gear
        if(choice == 1)
        {
            printf("How many pieces of new gear would you like to buy?\n");
            scanf("%d", &newGear);
            totalPieces += newGear;
            totalCost += (newGear * NEW);
        }
        //Calculate for used gear
        else if(choice == 2)
        {
            printf("How many pieces of used gear would you like to buy?\n");
            scanf("%d", &usedGear);
            totalPieces += usedGear;
            totalCost += (usedGear * USED);
        }
        //Account for any input other than 1 2 or 3.
        else if((choice != 1) && (choice != 2) && (choice != 3))
        {
            printf("\nSorry %d is an invalid choice. Please choose again.\n\n", choice);
        }

    }
    while(choice != 3); //END LOOP BY PRESSING 3

// Calculate average by type casting totalCost and totalPieces into floats.
    if((totalCost == 0) || (totalPieces == 0))
        average = 0;
    else
        average = (float)totalCost / (float)totalPieces;


// Print out summary
    printf("Your total cost is %d gold pieces\n", totalCost);
    printf("You obtained %d pieces of new gear and %d pieces of used gear\n", newGear, usedGear);
    printf("The average cost per piece of gear is %.2f\n", average);

    return 0;
}// END MAIN
