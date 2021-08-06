problem1 = map (*2) [1,5,-18,99]

problem2 = filter is_even (problemTwoList)
is_even n = n `mod` 2 == 0
problemTwoList = [ x | x <- [1..100] ]


problem3 = map (*3) problemThreeList
problemThreeList = [ x | x <- [1,3..77] ]


problem4 = sum (map (^2) problemFourFilter)
problemFourFilter = filter is_odd (problemFourList)
is_odd n = n `mod` 2 == 1
problemFourList = [1,2,3,4,5,7,8,1,43,25,65,22]
