//Brandon Broomell
//8/17/2021
//Codility Challenge

vector<int> solution(vector<int> &A, int K) {
    if(A.empty() || K == 0)
    {
        return A;
    }
    int len = A.size();
    //cout << len << endl;
    //Return A if moving K spaces will result in same array, if K is 0, or if A has < 2 numbers.
    if(K % len == 0 || len < 2)
    {
        //cout << "returning second cond" << endl;
        return A;
    }

    int mov = K % len;

    vector<int> res(len);

    int i = 0;

    for(i = 0; i < len; i++)
    {
        //cout << "i: " << i << endl;
        //cout << "res[]: " << (i + mov) % len << endl;
        //cout << "A[i]: " << A[i] << endl;
        res[(i + mov) % len] = A[i];
    }

    return res;

}
