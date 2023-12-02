using System.Text.RegularExpressions;
using static Helpers;

class Day2 {

    public static void Part1(StreamReader sr) {
        string indexRegex = @"Game (?<gamenum>\d+):";
        string cubesRegex = @"(?:(\d+) (red|green|blue))";
        int total = 0;
        for(string line = sr.ReadLine(); line != null; line = sr.ReadLine()) {
            Regex r1 = new Regex(indexRegex);
            Match m = r1.Match(line);
            int gamenum = int.Parse(m.Groups[1].ToString());

            //Remove the game num
            line = line.Split(':')[1].Trim();

            //Split into rounds
            string[] games = line.Split(';');
            bool possible = true;
            foreach(string game in games) {
                Regex r2 = new Regex(cubesRegex);
                Match cubes = r2.Match(game);

                while(cubes.Success) {
                    string color = cubes.Groups[2].ToString();
                    int num = int.Parse(cubes.Groups[1].ToString());

                    if((color == "green" && num > 13) ||
                        (color == "red" && num > 12) ||
                        (color == "blue" && num > 14)) possible = false;
                    
                    cubes = cubes.NextMatch();
                }
            }
            if(possible) total += gamenum;
        }
        Console.WriteLine(total);
    }

    public static void Part2(StreamReader sr) {
        string indexRegex = @"Game (?<gamenum>\d+):";
        string cubesRegex = @"(?:(\d+) (red|green|blue))";
        int total = 0;
        for(string line = sr.ReadLine(); line != null; line = sr.ReadLine()) {
            Regex r1 = new Regex(indexRegex);
            Match m = r1.Match(line);
            int gamenum = int.Parse(m.Groups[1].ToString());

            //Remove the game num
            line = line.Split(':')[1].Trim();

            //Split into rounds
            string[] rounds = line.Split(';');
            (int,int,int) minimums = new();
            foreach(string round in rounds) {
                Regex r2 = new Regex(cubesRegex);
                Match cubes = r2.Match(round);

                while(cubes.Success) {
                    string color = cubes.Groups[2].ToString();
                    int num = int.Parse(cubes.Groups[1].ToString());

                    if(color == "red" && num > minimums.Item1) minimums.Item1 = num;
                    else if(color == "blue" && num > minimums.Item2) minimums.Item2 = num;
                    else if(color == "green" && num > minimums.Item3) minimums.Item3 = num;
                    
                    cubes = cubes.NextMatch();
                }
            }
            total += minimums.Item1 * minimums.Item2 * minimums.Item3;
        }
        Console.WriteLine(total);
    }
}