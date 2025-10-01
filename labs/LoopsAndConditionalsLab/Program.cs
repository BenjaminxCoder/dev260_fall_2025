using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        // Task one: Sum of Even Numbers
        Console.WriteLine("Task one: Sum of Even Numbers");
        Console.WriteLine($"For loop sum: {SumEvenNumbersForLoop()}");
        Console.WriteLine($"While loop sum: {SumEvenNumbersWhileLoop()}");
        Console.WriteLine($"Foreach loop sum: {SumEvenNumbersForeachLoop()}");

        // Task two: Grading with Conditionals
        Console.WriteLine("\nTask 2: Grading with Conditionals");
        int[] testScores = { 95, 82, 76, 61, 50 };
        foreach (int score in testScores)
        {
            Console.WriteLine($"Score {score} → If/Else Grade: {GetLetterGradeIfElse(score)}");
            Console.WriteLine($"Score {score} → Switch Grade: {GetLetterGradeSwitch(score)}");
        }

        // Task three: Mini Challenge
        Console.WriteLine("\nTask 3: Mini Challenge");
        int sum = SumEvenNumbersForLoop();
        Console.WriteLine(CheckBigNumberIfElse(sum));
        Console.WriteLine(CheckBigNumberTernary(sum));
    }

    // Task one Functions
    static int SumEvenNumbersForLoop()
    {
        int sum = 0;
        for (int i = 2; i <= 100; i += 2)
            sum += i;
        return sum;
    }
    static int SumEvenNumbersWhileLoop()
    {
        int sum = 0, i = 2;
        while (i <= 100)
        {
            sum += i;
            i += 2;
        }
        return sum;
    }

    static int SumEvenNumbersForeachLoop()
    {
        int sum = 0;
        List<int> numbers = Enumerable.Range(1, 100).ToList();
        foreach (int num in numbers)
            if (num % 2 == 0) sum += num;
        return sum;
    }

    // Task two Functions
    static string GetLetterGradeIfElse(int score)
    {
        if (score >= 90) return "A";
        else if (score >= 80) return "B";
        else if (score >= 70) return "C";
        else if (score >= 60) return "D";
        else return "F";
    }

    static string GetLetterGradeSwitch(int score)
    {
        return score switch
        {
            >= 90 => "A",
            >= 80 => "B",
            >= 70 => "C",
            >= 60 => "D",
            _ => "F"
        };
    }

    // Task three Functions
    static string CheckBigNumberIfElse(int sum)
    {
        if (sum > 2000) return "That’s a big number!";
        else return $"Sum is {sum}";
    }

    static string CheckBigNumberTernary(int sum)
    {
        return (sum > 2000) ? "That’s a big number!" : $"Sum is {sum}";
    }
}