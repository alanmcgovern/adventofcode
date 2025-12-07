using System.Collections.Generic;

var banks = File.ReadAllLines ("input.txt")
    .Where (t => !string.IsNullOrEmpty (t))
    .Select (t => t.Select (l => long.Parse(l.ToString())).ToArray ().AsMemory ())
    .ToArray ();

long Joltinator3000(int totalEnabled)
{
    long totalJoltage = 0;

    foreach (var originalBank in banks) {
        long bankJoltage = 0;
        var bank = originalBank;
        var remainingToEnable = totalEnabled;
        while (remainingToEnable > 0) {
            (var value, var index) = bank.Span.Slice (0, bank.Length - remainingToEnable + 1).MaxValueAndIndex ();
            bankJoltage = bankJoltage * 10 + value;
            remainingToEnable--;
            bank = bank.Slice (index + 1);
        }

        totalJoltage += bankJoltage;
    }

    return totalJoltage;
}

Console.WriteLine ($"{Joltinator3000 (2)}");
Console.WriteLine ($"{Joltinator3000 (12)}");


static class Extensions
{
    public static (long value, int index) MaxValueAndIndex(this Span<long> span)
    {
        long maxValue = span[0];
        int maxValueIndex = 0;
        for (int i = 1; i < span.Length; i ++) {
            if (span[i] > maxValue) {
                maxValue = span[i];
                maxValueIndex = i;
            }
        }

        return (maxValue, maxValueIndex);
    }
}
