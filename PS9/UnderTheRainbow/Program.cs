using System;

namespace UnderTheRainbow
{
    class Program
    {
        static void Main(string[] args)
        {
            string line;
            line = Console.ReadLine();

            int hotels = int.Parse(line);
            int count = 0;

            // List of hotels (index) and the distance to them from start
            // distance[0] is always 0
            // eg. [0, 350, 450, 825]
            int[] distance = new int[hotels + 1];

            // Cache to keep track of how much penalty has been accrued
            // to get to the hotel. Indices match distance[]
            int[] leastPenalty = new int[hotels + 1];

            while ((line = Console.ReadLine()) != null && line != "")
            {
                distance[count] = (int.Parse(line));
                count++;
            }

            // Penalty for driving is (400 - x)^2 per day, where x is the distance traveled
            // from the previous hotel to the current hotel
            for (int i = 0; i < distance.Length; i++)
            {
                leastPenalty[i] = (400 - distance[i]) * (400 - distance[i]);

                for (int j = 0; j < i; j++)
                {
                    // Calculate the daily penalty for each hotel remaining, where distance
                    // is the difference between previous hotel [i] and current [j]
                    int dailyPenalty = (int)Math.Pow(400 - (distance[i] - distance[j]), 2);

                    // If the current hotel plus the daily penalty is less than cached hotel
                    // and penalty, then update it. Else, keep checking for a better solution.
                    if ((leastPenalty[j] + dailyPenalty) < leastPenalty[i])
                    {
                        leastPenalty[i] = leastPenalty[j] + dailyPenalty;
                    }
                }
            }
            Console.Out.WriteLine(leastPenalty[hotels]);
        }
    }
}
