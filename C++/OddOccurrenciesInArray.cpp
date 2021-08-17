//Brandon Broomell
#include <cstddef>
#include <stdlib.h>
#include <iostream>
#include <algorithm>



int solution(vector<int> &A) {

    int len = A.size();
    //Check if empty
    if(len == 0 || A.empty() == true)
    {
        return 0;
    }
    //Check if even sized array
    if(len % 2 == 0)
    {
        return 0;
    }

    //Sort array
    sort(A.begin(), A.end());

    int i= 0;
    //Iterate through array and check for pairs. If number has no pair return that number.
    do{
        if(A[i] != A[i+1])
        {
            return A[i];
        }

    i+= 2;
    }while(i < len);

}
