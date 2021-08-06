
test1Problem1 n = go n 1 0
  where
  go n c f
    | c > n = show (f `mod` 10000000000)
    | otherwise = go n (c + 1) (f + (c^c))

test1Problem2 n = go n 0 0 (createPrimeList(n*3))
  where
  go n c sc (x:xs)
    | c == n = []
    | sc `mod` 3 == 0 = x*c : go n (c + 1) (sc + 1) xs
    | otherwise = go n c (sc + 1) xs





isPrime n = ip n [2..(isqrt (n-1)+1)]
  where
  ip _ [] = True
  ip n (x:xs)
    | n `mod` x == 0 = False
    | otherwise = ip n xs

isqrt = floor . sqrt . fromIntegral

createPrimeList n = go n 1 1
  where
  go n count p
    | count > n = []
    | isPrime p = p : go n (count + 1) (p + 1)
    | otherwise = go n count (p + 1)



numberOfPrimeFactors n = [ x | x <- [1..n], isFactor n x && isPrime x]

isFactor n c = if n `mod` c == 0 then True else False
