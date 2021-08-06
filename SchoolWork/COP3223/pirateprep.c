// Brandon Broomell
// Date 8/27/2017
// COP 3223C-17Fall 0R04
// Project 1: pirateprep.c

#include <stdio.h>
#include <stdlib.h>
#include <math.h>

//Constant variables
#define TOTAL_DISTANCE 7228
#define ORANGE_PER_MEMBER .5

// START MAIN
int main()
    {
// Variables
    float kiloPerDay = 0;
    float totalDays = 0;
    int crewMembers = 0;
    float totalOranges = 0;

// Get input variables
    printf(" Enter the number of kilometers you can travel per day: ");
    scanf("%f", &kiloPerDay);

    printf(" Enter the number of crew members there are: ");
    scanf("%d", &crewMembers);

// Calculations
    totalDays = TOTAL_DISTANCE / kiloPerDay;

    totalOranges = (ORANGE_PER_MEMBER * (float) crewMembers) * totalDays;

// Print to screen
    printf(" Your trip will take %.2f days to complete. \n You will need %.2f oranges to make the trip", totalDays, totalOranges);



return (0);
    }// END MAIN
