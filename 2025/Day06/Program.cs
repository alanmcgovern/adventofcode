var input = File.ReadAllLines ("input.txt")
    .Select (t => t.Split (' ', StringSplitOptions.RemoveEmptyEntries).ToArray ())
    .ToArray ();

var numbers = input.SkipLast (1)
    .Select (t => t.Select (int.Parse).ToArray ())
    .ToArray ();

var operators = input.Last ();

Console.WriteLine ($"Q1: {ComputeTopwise (numbers, operators)}");

long ComputeTopwise (int[][] numbers, string[] operators)
{
    long total = 0;
    for (int column = 0; column < numbers[0].Length; column++) {
        long? value = null;
        for (int row = 0; row < numbers.Length; row++) {
            if (value == null) {
                value = numbers[row][column];
            }else {
                if (operators[column] == "*")
                    value *= numbers[row][column];
                else if (operators[column] == "+")
                    value += numbers[row][column];
            }
        }

        total += value!.Value;
    }

    return total;
}
