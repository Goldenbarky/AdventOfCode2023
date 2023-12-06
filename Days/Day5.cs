using System.Text.RegularExpressions;
using static Helpers;

class Day5 {

    public static void Part1(StreamReader sr) {
        string seedString = sr.ReadLine().Split(':')[1];
        Regex r = new Regex(@"(\d+)");
        Match m = r.Match(seedString);

        List<long> startingSeeds = new List<long>();
        while(m.Success) {
            startingSeeds.Add(long.Parse(m.ToString()));
            m = m.NextMatch();
        }
        long[] seeds = startingSeeds.ToArray();
        long[] newSeeds = startingSeeds.ToArray();

        for(string line = sr.ReadLine(); line != null; line = sr.ReadLine()) {
            if(line == "") {
                sr.ReadLine();
                seeds = newSeeds.ToArray();
                continue;
            }

            Regex r2 = new Regex(@"(\d+) (\d+) (\d+)");
            Match m2 = r2.Match(line);

            long dest = long.Parse(m2.Groups[1].ToString());
            long source = long.Parse(m2.Groups[2].ToString());
            long range = long.Parse(m2.Groups[3].ToString());
            long difference = source - dest;

            for(int i = 0; i < seeds.Length; i++) {
                if(seeds[i] >= source && seeds[i] < source + range) newSeeds[i] -= difference;
            }
        }

        long smallest = seeds.Min();
        Console.WriteLine(smallest);
    }

    public static void Part2(StreamReader sr) {
        for(string line = sr.ReadLine(); line != null; line = sr.ReadLine()) {
            
        }
    }
}