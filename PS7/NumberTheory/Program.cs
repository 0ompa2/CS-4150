using System;
using System.Text;
using System.Numerics;

namespace NumberTheory
{
    class Program
    {
        static void Main(string[] args)
        {
            string line = "";
            while ((line = Console.ReadLine()) != null && line != "")
            {
                string[] temp = line.Split();
                string value = temp[0];

                switch (value)
                {
                    // Print the greatest common divisor of a and b.
                    case "gcd":
                        Console.Out.WriteLine(gcd(long.Parse(temp[1]), long.Parse(temp[2])));
                        break;

                    // Print x^y mod N, which must be non-negative and less than N.
                    case "exp":
                        Console.Out.WriteLine(exponent(BigInteger.Parse(temp[1]), BigInteger.Parse(temp[2]), BigInteger.Parse(temp[3])));
                        break;

                    // Print a^−1 mod N, which must be positive and less than N. 
                    // If the inverse does not exist, print “none”
                    case "inverse":
                        long inv = inverse(long.Parse(temp[1]), long.Parse(temp[2]));
                        if (inv == 0)
                        {
                            Console.Out.WriteLine("none");
                        }
                        else
                        {
                            Console.Out.WriteLine(inv);
                        }
                        break;

                    // Print “yes” if p passes the Fermat test for a=2, a=3, and a=5 
                    // Print “no” otherwise.
                    case "isprime":
                        if (isprime(long.Parse(temp[1])))
                        {
                            Console.Out.WriteLine("yes");
                        }
                        else
                        {
                            Console.Out.WriteLine("no");
                        }
                        break;

                    // Print the modulus, public exponent, and private exponent of the RSA key pair derived from p and q. 
                    // The public exponent must be the smallest positive integer that works; q must be positive and less than N
                    case "key":
                        StringBuilder builder = new StringBuilder();
                        foreach (long l in RSAkey(long.Parse(temp[1]), long.Parse(temp[2])))
                        {
                            builder.Append(l).Append(" ");
                        }
                        Console.Out.WriteLine(builder);
                        break;

                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// Helper method that returns gcd(a,b) using interative Euclid's
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        private static long gcd(long a, long b)
        {
            if (b == 0)
            {
                return a;
            }
            else
            {
                return gcd(b, mod(a, b));
            }
        }

        /// <summary>
        /// Helper method that returns modulous of exponential
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="N"></param>
        /// <returns></returns>
        private static BigInteger exponent(BigInteger x, BigInteger y, BigInteger N)
        {

            if (y == 0)
            {
                return 1;
            }
            else
            {
                BigInteger z = exponent(x, y / 2, N);
                if (y % 2 == 0)
                {
                    return ((z % N) * (z % N)) % N;
                    //return mod((mod(z, N) * mod(z, N)), N);
                }
                else
                {
                    return ((x % N) * ((z % N) * (z % N))) % N;
                    //return mod((mod(x, N) * (mod(z, N) * mod(z, N))), N);
                }
            }
        }

        /// <summary>
        /// Helper method that calculates a numbers modular inverse
        /// or returns 0 if there is none
        /// </summary>
        /// <param name="a"></param>
        /// <param name="N"></param>
        /// <returns></returns>
        private static long inverse(long a, long N)
        {
            long[] temp = ee(a, N);
            if (temp[2] == 1)
            {
                return mod(temp[0], N);
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// Helper method for inverse helper method
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        private static long[] ee(long a, long b)
        {
            long[] temp = new long[3];
            if (b == 0)
            {
                return new long[] { 1, 0, a };
            }
            else
            {
                // [x’, y’, d] = ee(b, a mod b) 
                // return [y’,  x’ – (a/b)y’,  d]
                temp = ee(b, mod(a, b));
                return new long[] { temp[1], temp[0] - (a / b) * temp[1], temp[2] };
            }
        }

        /// <summary>
        /// Helper method that determines if p is prime
        /// using Fermat's Little Theorem
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        private static bool isprime(long p)
        {
            if (exponent(2, p - 1, p) != 1)
            {
                return false;
            }
            if (exponent(3, p - 1, p) != 1)
            {
                return false;
            }
            if (exponent(5, p - 1, p) != 1)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Helper method that generates RSA's modulus, public and private exponents
        /// </summary>
        /// <param name="p"></param>
        /// <param name="q"></param>
        /// <returns></returns>
        private static long[] RSAkey(long p, long q)
        {
            long[] returnValue = new long[3];
            returnValue[0] = p * q;
            long phi = (p - 1) * (q - 1);

            long publicExp = 0;
            for (int i = 2; i < phi; i++)
            {
                if (gcd(i, phi) == 1)
                {
                    publicExp = i;
                    break;
                }
            }

            returnValue[1] = publicExp;
            returnValue[2] = inverse(publicExp, phi);
            return returnValue;
        }

        /// <summary>
        /// Custom mod function, becuase C#'s '%' operator is a remainder operator
        /// and not a true modulus operator, in that it doesn't handle negative numbers
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        private static long mod(long a, long b)
        {
            return (a % b + b) % b;
        }
    }
}
