using static Helpers;

class Day3 {

    public static void Part1(StreamReader sr) {
        char[,] map = ReadIntoMap(sr);
        int total = 0;

        bool readingNum = false;
        string num = "";
        bool isPartNumber = false;
        for(int y = 0; y < map.GetLength(0); y++) {
            for(int x = 0; x < map.GetLength(1); x++) {
                char ch = map[y,x];

                if(char.IsDigit(ch) && !readingNum) {
                    readingNum = true;
                    isPartNumber = false;
                    num += ch;
                } else if(char.IsDigit(ch) && readingNum) {
                    num += ch;
                } else if(!char.IsDigit(ch) && readingNum) {
                    if(isPartNumber) total += int.Parse(num);
                    readingNum = false;
                    num = "";
                }

                if(!isPartNumber && readingNum) {
                    for(int row = -1; row <= 1 && !isPartNumber; row++) {
                        for(int col = -1; col <= 1 && !isPartNumber; col++) {
                            if(x + row < 0 || x + row >= map.GetLength(0)) break;
                            if(y + col < 0 || y + col >= map.GetLength(1)) continue;
                            if(row == 0 && col == 0) continue;
                            char character = map[y + col, x + row];
                            if(!char.IsDigit(character) && character != '.') isPartNumber = true;
                        }
                    }
                }
            }
        }
        Console.WriteLine(total);
    }

    public static void Part2(StreamReader sr) {
        char[,] map = ReadIntoMap(sr);

        List<(int,int)> gearLocations = new(); 
        for(int x = 0; x < map.GetLength(1); x++) {
            for(int y = 0; y < map.GetLength(0); y++) {
                if(map[x,y] == '*') gearLocations.Add((x, y));
            }
        }

        int total = 0;
        foreach((int, int) gear in gearLocations) {
            int x = gear.Item1;
            int y = gear.Item2;

            bool readingNum = false;
            List<(int,int)> nums = new();
            for(int row = -1; row <= 1; row++) {
                readingNum = false;
                for(int col = -1; col <= 1; col++) {
                    if(x + row < 0 || x + row >= map.GetLength(0)) break;
                    if(y + col < 0 || y + col >= map.GetLength(1)) continue;
                    char ch = map[x + row, y + col];

                
                    if(char.IsDigit(ch) && !readingNum) {
                        readingNum = true;
                        nums.Add((x + row, y + col));
                    } else if(char.IsDigit(ch) && readingNum) {
                        continue;
                    } else if(!char.IsDigit(ch) && readingNum) {
                        readingNum = false;
                    }
                }
            }

            if(nums.Count == 2) {
                int gearRatio = 1;
                foreach((int, int) coord in nums) {
                    int index = coord.Item2;
                    string number = "";

                    //Find the start to the number
                    while(index-1 >= 0 && char.IsDigit(map[coord.Item1, index-1])) index--;
                    //Record until the end
                    while(index < map.GetLength(1) && char.IsDigit(map[coord.Item1, index])) number += map[coord.Item1, index++];

                    gearRatio *= int.Parse(number);
                }

                total += gearRatio;
            }
        }
        Console.WriteLine(total);
    }

    public static char[,] ReadIntoMap(StreamReader sr) {
        string line = sr.ReadLine();
        char[,] map = new char[line.Length, line.Length];

        for(int i = 0; i < line.Length; i++) {
            for(int j = 0; j < line.Length; j++) {
                map[i, j] = line[j];
            }

            line = sr.ReadLine();
            if(line == null) break;
        }

        return map;
    }
}