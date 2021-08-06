// Brandon Broomell
// Date: 10/14/2017
// COP 3223 Fall 2017
// Project 5: settingsail.c

#include <stdio.h>
#include <stdlib.h>


int main ()
{
    FILE *ifp;
    int i, curMonth = 1, withinBounds = 0, header = 0, greatestMonth = 1;
    float lBound = 0, uBound = 0, curVal = 0, prevPercentage = 0, percentage = 0;
    char fileName[30 + 1];

// GET THE COLDEST TEMPERATURE CREW CAN SAIL IN(HAS TO BE BETWEEN 0 AND 80)
do{
    printf("What is the coldest temperature your crew can sail in?\n (enter a value between 0 and 80): \n");
    scanf("%f", &lBound);
    if((lBound < 0) || (lBound > 80))
    {
        printf("Please enter a value between 0 and 80 degrees.\n");
    }
}while((lBound < 0) || (lBound > 80));

//GET THE HOTTEST TEMPERATURE THE CREW CAN SAIL IN(HAS TO BE BETWEEN THE COLDEST AND 120)
do{
    printf("What is the hottest temperature your crew can sail in?\n(Enter a value between %.0f and 120): \n", lBound);
    scanf("%f", &uBound);
    if((uBound < lBound) || (uBound > 120))
    {
        printf("Please enter a value higher than %f and less than 120 degrees.\n");
    }
}while((uBound < lBound) || (uBound > 120));


//GET FILE NAME
    printf("What is the name of the file: \n");
    scanf("%s", &fileName);

//OPEN FILE AND CHECK IF VALID FILENAME
    ifp = fopen(fileName, "r");
    if(NULL == ifp)
    {
        fprintf(stderr, "Filename: %s could not be found\n", fileName);
    }



//LOOP UNTIL END OF FILE, READ EACH SET OF DATA INTO THE CURRENT VALUE, CALCULATE THE PERCENTAGE,
//PRINT TO THE SCREEN THE CURRENT MONTH THEN EVALUATE IF THE MONTH PRIOR IS GREATER THAN THE PREVIOUS AND STORE THE J VALUE
    while(fscanf(ifp, "%d", &header) != EOF)
    {
        //RESET WITHINBOUNDS VALUE (USED TO KEEP COUNT OF HOW MANY TEMPERATURES ARE IN THE RANGE FOR EACH MONTH
        withinBounds = 0;


        for(i = 0; i < header; i++)
        {
            fscanf(ifp, "%f", &curVal);
            if((curVal >= lBound) && (curVal <= uBound))
            {
                withinBounds++;
            }
        }
        percentage = (float)withinBounds / (float)header;
        percentage = percentage * 100;

        printf("Month %d: %.1f\n", curMonth, percentage);
        if(percentage > prevPercentage)
        {
            prevPercentage = percentage;
            greatestMonth = curMonth;
        }
        curMonth++;
    }

    printf("\nYou should leave for the caribean in month %d!!!!!!!!!!!\n", greatestMonth);
    fclose(ifp);
    return 0;
}
