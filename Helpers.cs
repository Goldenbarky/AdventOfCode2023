using System.Collections;

class Helpers {
    public static string ReverseString(string str) {
        var charArray = str.ToCharArray();
        Array.Reverse(charArray);

        return new string(charArray);
    }

    /// <summary>
    /// Gets the Manhattan distance between two points
    /// </summary>
    /// <param name="coord1"></param>
    /// <param name="coord2"></param>
    /// <returns></returns>
    public static int GetManDistance((int, int) coord1, (int, int) coord2) {
        int distance = 0;
        distance += Math.Abs(coord1.Item1 - coord2.Item1);
        distance += Math.Abs(coord1.Item2 - coord2.Item2);

        return distance;
    }

    /// <summary>
    /// Returns A mod B
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    public static int Modulo(int a, int b) {
        return ((a % b) + b) % b;
    }

    /// <summary>
    /// Moves the stream reader back to the beginning of the stream
    /// </summary>
    /// <param name="sr"></param>
    /// <returns></returns>
    public static StreamReader RestartStream(StreamReader sr) {
        sr.DiscardBufferedData();
        sr.BaseStream.Seek(0, SeekOrigin.Begin);

        return sr;
    }

    /// <summary>
    /// Computes the A Star algorithm to determine the cost of the cheapest path between two points on a map
    /// </summary>
    /// <param name="start">Tuple of the start indexes</param>
    /// <param name="end">Tuple of the end indexes</param>
    /// <param name="map">Map of path costs</param>
    /// <returns></returns>
    public static int AStar((int, int) start, (int, int) end, char[,] map) {
        int[,] costs = new int[map.GetLength(0), map.GetLength(1)];

        for(int i = 0; i < costs.GetLength(0); i++) {
            for(int j = 0; j < costs.GetLength(1); j++) {
                costs[i,j] = int.MaxValue;
            }
        }

        costs[start.Item1, start.Item2] = 0;

        Queue<(int, int)> moves = new Queue<(int, int)>();
        moves.Enqueue(start);

        while(moves.Count > 0) {
            (int, int) move = moves.Dequeue();

            if(move == end) {
                return costs[move.Item1, move.Item2];
            } 

            int x = move.Item1;
            int y = move.Item2;
            char currPos = map[x, y];
            int currCost = costs[x, y];

            //left
            if(x - 1 >= 0 && map[x-1, y] - currPos <= 1 && costs[x-1, y] == int.MaxValue) {
                costs[x-1, y] = currCost + 1;
                moves.Enqueue((x-1, y));
            }
            //right
            if(x + 1 < map.GetLength(0) && map[x+1, y] - currPos <= 1 && costs[x+1, y] == int.MaxValue) {
                costs[x+1, y] = currCost + 1;
                moves.Enqueue((x+1, y));
            }
            //down
            if(y - 1 >= 0 && map[x, y-1] - currPos <= 1 && costs[x, y-1] == int.MaxValue) {
                costs[x, y-1] = currCost + 1;
                moves.Enqueue((x, y-1));
            }
            //up
            if(y + 1 < map.GetLength(0) && map[x, y+1] - currPos <= 1 && costs[x, y+1] == int.MaxValue) {
                costs[x, y+1] = currCost + 1;
                moves.Enqueue((x, y+1));
            }
        }

        return int.MaxValue;
    }

    public static int[] StringArrayToInt(string[] input) {
        int[] array = new int[input.Count()];
        for(int i = 0; i < input.Count(); i++) {
            array[i] = int.Parse(input[i]);
        }

        return array;
    }

    public static void Print(string str) {
        Console.WriteLine(str);
    }

    public static void Print(List<object> list) {
        list.ForEach(x => Print(x + ", "));
    }
}