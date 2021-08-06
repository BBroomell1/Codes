// Brandon Broomell
// COP 3223 Fall 2017
// 9/28/2017
// Program 4 landingcrew.c


#include <stdio.h>
#include <stdlib.h>

int main(){

int days;
int trips;
int i, j;
float tripTime, averageTime, totalTime;

//GET NUMBER OF DAYS
printf(" How many days will you observe the landing crew?\n");
scanf("%d", &days);

for(i = 0; i < days; i++)
    {
//RESET VALUES FOR TRIP TIMES
    tripTime = 0;
    averageTime = 0;
    totalTime = 0;

//GET NUMBER OF TRIPS
    printf(" How many trips were completed in day %d\n", i + 1);
    scanf("%d", &trips);

    for(j = 0; j < trips; j++)
    {
        printf(" How long was trip %d?\n", j + 1);
        scanf("%f", &tripTime);
        totalTime += tripTime;
    }
//CALCULATE AVERAGE AND PRINT TO SCREEN
    averageTime = totalTime / (float)trips;
printf("Day #%d: The average time was %.3f\n", i + 1,  averageTime);

}


return 0;
}//END MAIN
