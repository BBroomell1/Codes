//Brandon Broomell
//Binary Gap


#include <stdlib.h>
#include <iostream>
#include <bitset>

using namespace std;

#define maxLength 32

int solution(int N);

int main(){
  int num = 2000;
  int result = solution(num);
  cout << result << endl;
  return 0;
}

int solution(int N) {
    std::string binary = std::bitset<maxLength>(N).to_string();
    int result = 0, tempCounter = 0;;
    for(int i = 0; i < maxLength; i++)
    {
        if(binary[i] == '1')
        {
            i++;
            tempCounter = 0;
            while(binary[i] == '0')
            {
                i++;
                tempCounter++;
            }
            if(binary[i] == '1')
            {
                if(tempCounter > result)
                {
                    result = tempCounter;
                }
                i--;
            }
        }
    }
    return result;
}
