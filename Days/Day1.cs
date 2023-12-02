using System.Globalization;
using System.Net.NetworkInformation;
using static Helpers;

class Day1 {
    public static Dictionary<string, char> DigitStrings = new Dictionary<string, char>{
        {"one", '1'},
        {"two", '2'},
        {"three", '3'},
        {"four", '4'},
        {"five", '5'},
        {"six", '6'},
        {"seven", '7'},
        {"eight", '8'},
        {"nine", '9'}
    };

    public static void Part1(StreamReader sr) {
        int total = 0;
        for(string line = sr.ReadLine(); line != null; line = sr.ReadLine()) {
            string num = "";
            num += GetFirstDigit(line);
            line = ReverseString(line);
            num += GetFirstDigit(line);
            total += int.Parse(num);
        }
        
        Console.WriteLine(total);
    }

    public static char GetFirstDigit(string str){
        foreach(char ch in str) {
            if(char.IsDigit(ch)) {
                return ch;
            }
        }

        return 'x';
    }

    public static void Part2(StreamReader sr) {
        int total = 0;
        for(string line = sr.ReadLine(); line != null; line = sr.ReadLine()) {
            string num = "";
            num += SearchForDigit(line, 0, 1);
            num += SearchForDigit(line, line.Length - 1, -1);
            total += int.Parse(num);
        }
        Console.WriteLine(total);
    }

    public static char SearchForDigit(string str, int start, int inc) {
        string substr = "";
        int limit = (inc == 1) ? str.Length : -1;
        for(int i = start; i != limit; i += inc) {
            char ch = str[i];
            if(inc == 1) substr += ch;
            else substr = substr.Insert(0, char.ToString(ch));
            if(char.IsDigit(ch)) return ch;
            else {
                foreach(string digit in DigitStrings.Keys) {
                    if(substr.Contains(digit)) return DigitStrings[digit];
                }
            }
        }
        return 'x';
    }
}