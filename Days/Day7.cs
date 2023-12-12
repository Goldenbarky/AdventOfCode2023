using System.Text.RegularExpressions;
using static Helpers;

class Day7 {

    public static void Part1(StreamReader sr) {
        int total = 0;
        List<string> hands = new();
        Dictionary<string, int> bids = new();
        for(string line = sr.ReadLine(); line != null; line = sr.ReadLine()) {
            Regex r = new Regex(@"(\w{5}) (\d+)");
            Match m = r.Match(line);

            string hand = m.Groups[1].ToString();
            bids.Add(hand, int.Parse(m.Groups[2].ToString()));
            hands.Add(hand);
        }

        hands.Sort(new HandComparer1());
        for(int i = 0; i < hands.Count; i++) {
            total += bids[hands[i]] * (i + 1);
        }
        Console.WriteLine(total);
    }

    public static void Part2(StreamReader sr) {
        int total = 0;
        List<string> hands = new();
        Dictionary<string, int> bids = new();
        for(string line = sr.ReadLine(); line != null; line = sr.ReadLine()) {
            Regex r = new Regex(@"(\w{5}) (\d+)");
            Match m = r.Match(line);

            string hand = m.Groups[1].ToString();
            bids.Add(hand, int.Parse(m.Groups[2].ToString()));
            hands.Add(hand);
        }

        hands.Sort(new HandComparer2());
        for(int i = 0; i < hands.Count; i++) {
            total += bids[hands[i]] * (i + 1);
        }
        Console.WriteLine(total);
    }
}

class HandComparer1 : IComparer<string> {
    public int Compare(string x, string y)
    {
        int typeX = GetHandType(x);
        int typeY = GetHandType(y);

        if(typeX > typeY) return 1;
        else if(typeY > typeX) return -1;
        else {
            for(int i = 0; i < 5; i++) {
                int comparison = CompareCards(x[i], y[i]);
                if(comparison == 0) continue;
                else return comparison;
            }
            return 0;
        }

        throw new NotImplementedException();
    }

    public static Dictionary<char, int> GetValuePairs(string hand) {
        Dictionary<char, int> valuePairs = new();
        foreach(char ch in hand) {
            if(valuePairs.ContainsKey(ch)) valuePairs[ch]++;
            else valuePairs.Add(ch, 1);
        }

        return valuePairs;
    }

    /// <summary>
    /// Returns the type of the hand.
    /// 0 for high card
    /// 1 for one pair
    /// 2 for two pair
    /// 3 for three of a kind
    /// 4 for full house
    /// 5 for four of a kind
    /// 6 for 5 of a kind
    /// </summary>
    /// <param name="hand"></param>
    /// <returns></returns>
    public static int GetHandType(string hand) {
        Dictionary<char, int> valuePairs = GetValuePairs(hand);

        int highest = valuePairs.Values.Max();
        
        //Check for up to 3 of a kind (as well as full house)
        if(highest > 3 || highest == 3 && valuePairs.Values.Contains(2)) {
            return highest + 1;
        } else if (highest == 3) return 3;

        //Calculate num of pairs
        int numPairs = 0;
        foreach(int value in valuePairs.Values) {
            if(value == 2) numPairs++;
        }

        return numPairs;
    }

    /// <summary>
    /// Returns a value greater than 0 if x is greater than y
    /// Returns 0 if x is equal to y
    /// Returns a value less than 0 if x is less than y
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    public static int CompareCards(char x, char y) {
        int card1 = ConvertCardToInt(x);
        int card2 = ConvertCardToInt(y);

        return card1 - card2;
    }

    public static int ConvertCardToInt(char ch) {
        if(ch == 'A') return 14;
        else if(ch == 'K') return 13;
        else if(ch == 'Q') return 12;
        else if(ch == 'J') return 11;
        else if(ch == 'T') return 10;
        else return int.Parse(ch.ToString());
    }
}

class HandComparer2 : IComparer<string> {
    public int Compare(string x, string y)
    {
        int typeX = GetHandType(x);
        int typeY = GetHandType(y);

        if(typeX > typeY) return 1;
        else if(typeY > typeX) return -1;
        else {
            for(int i = 0; i < 5; i++) {
                int comparison = CompareCards(x[i], y[i]);
                if(comparison == 0) continue;
                else return comparison;
            }
            return 0;
        }

        throw new NotImplementedException();
    }

    public static Dictionary<char, int> GetValuePairs(string hand) {
        Dictionary<char, int> valuePairs = new();
        foreach(char ch in hand) {
            if(valuePairs.ContainsKey(ch)) valuePairs[ch]++;
            else valuePairs.Add(ch, 1);
        }

        return valuePairs;
    }

    /// <summary>
    /// Returns the type of the hand.
    /// 0 for high card
    /// 1 for one pair
    /// 2 for two pair
    /// 3 for three of a kind
    /// 4 for full house
    /// 5 for four of a kind
    /// 6 for 5 of a kind
    /// </summary>
    /// <param name="hand"></param>
    /// <returns></returns>
    public static int GetHandType(string hand) {
        Dictionary<char, int> valuePairs = GetValuePairs(hand);

        int buffer = 0;
        if(valuePairs.ContainsKey('J')) {
            buffer = valuePairs['J'];
            valuePairs.Remove('J');
        }

        if(valuePairs.Count == 0) {
            return 6;
        }

        int highest = valuePairs.Values.Max();
        for(int i = 0; i < valuePairs.Count; i++) {
            if(valuePairs.Values.ElementAt(i) == highest) {
                valuePairs[valuePairs.Keys.ElementAt(i)] += buffer;
                break;
            }
        }
        highest += buffer;
        
        //Check for up to 3 of a kind (as well as full house)
        if(highest > 3 || highest == 3 && valuePairs.Values.Contains(2)) {
            return highest + 1;
        } else if (highest == 3) return 3;

        //Calculate num of pairs
        int numPairs = 0;
        foreach(int value in valuePairs.Values) {
            if(value == 2) numPairs++;
        }

        return numPairs;
    }

    /// <summary>
    /// Returns a value greater than 0 if x is greater than y
    /// Returns 0 if x is equal to y
    /// Returns a value less than 0 if x is less than y
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    public static int CompareCards(char x, char y) {
        int card1 = ConvertCardToInt(x);
        int card2 = ConvertCardToInt(y);

        return card1 - card2;
    }

    public static int ConvertCardToInt(char ch) {
        if(ch == 'A') return 14;
        else if(ch == 'K') return 13;
        else if(ch == 'Q') return 12;
        else if(ch == 'J') return 0;
        else if(ch == 'T') return 10;
        else return int.Parse(ch.ToString());
    }
}