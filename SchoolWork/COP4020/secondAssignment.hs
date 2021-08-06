problem1 n = [ x | x <- shortenList (n * 2) ]

isPrime n = ip n [2..(n `div` 2)]
  where
  ip _ [] = True
  ip n (x:xs)
    | n `mod` x == 0 = False
    | otherwise = ip n xs

createPrimeList n = go n 1 2
  where
  go n count p
    | count > n = []
    | isPrime p = p : go n (count + 1) (p + 1)
    | otherwise = go n count (p + 1)

shortenList n = go n 0 (createPrimeList n)
  where
  go n count (x:xs)
    | count `mod` 2 == 0 = x : go n (count + 1) (xs)
    | null xs = []
    | otherwise = go n (count + 1) (xs)




problem2 n = go n 1 1
  where
  go n f s
    | (f + s) > n = []
    | isThreeRightmostDigit s == True = s : go n s (f + s)
    | otherwise = go n s (f + s)

isThreeRightmostDigit n = if n `mod` 10 == 3 then True else False



problem3 n = go n 1
  where
  go n count
    | count > n = []
    | isMultipleOfFive count == True = count : go n (count + 1)
    | hasThreeFactors count == True = count : go n (count + 1)
    | otherwise = go n (count + 1)

isMultipleOfFive n = if n `mod` 5 == 0 then True else False

hasThreeFactors n = if length (factorList n) == 2 then True else False
factorList n = go n (n `div` 2) 1
  where
  go n x count
    | count > x = []
    | isFactor n count = count : go n x (count + 1)
    | otherwise = go n x (count + 1)

isFactor n c = if n `mod` c == 0 then True else False
