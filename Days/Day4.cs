using System.Text.RegularExpressions;
using static Helpers;

class Day4 {

    public static void Part1(StreamReader sr) {
        int total = 0;
        for(string line = sr.ReadLine(); line != null; line = sr.ReadLine()) {
            line = line.Split(':')[1];
            string[] card = line.Split('|');

            int[] winners = ReadNumbers(card[0]);
            int[] cardNums = ReadNumbers(card[1]);

            int numWins = 0;
            foreach(int num in cardNums) {
                if(winners.Contains(num)) numWins++;
            }

            if(numWins > 0) total += (int)Math.Pow(2, numWins-1);
        }
        Console.WriteLine(total);
    }

    public static int[] ReadNumbers(string line) {
        Regex r = new Regex(@"(\d+)");
        Match m = r.Match(line);

        List<int> nums = new();
        while(m.Success) {
            nums.Add(int.Parse(m.ToString()));
            m = m.NextMatch();
        }

        return nums.ToArray();
    }

    public static void Part2(StreamReader sr) {
        int total = 0;
        LinkedList<int> cardCopies = new();
        for(string line = sr.ReadLine(); line != null; line = sr.ReadLine()) {
            line = line.Split(':')[1];
            string[] card = line.Split('|');

            int[] winners = ReadNumbers(card[0]);
            int[] cardNums = ReadNumbers(card[1]);

            int numWins = 0;
            foreach(int num in cardNums) {
                if(winners.Contains(num)) numWins++;
            }

            LinkedListNode<int> curr = cardCopies.First;
            if(curr == null) curr = cardCopies.AddLast(1);
            else curr.Value += 1;

            int multiplier = curr.Value;
            total += multiplier;
            while(curr.Next != null && numWins > 0) {
                curr = curr.Next;
                curr.Value += multiplier;
                numWins--;
            }
            while(numWins > 0) {
                cardCopies.AddLast(multiplier);
                numWins--;
            }
            cardCopies.RemoveFirst();
        }
        Console.WriteLine(total);
    }
}