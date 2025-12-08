var input = File.ReadAllLines ("input.txt")
    .Select (t => t.Split (' ', StringSplitOptions.RemoveEmptyEntries).ToArray ())
    .ToArray ();

var numbers = input.SkipLast (1)
    .Select (t => t.Select (int.Parse).ToArray ())
    .ToArray ();

var operators = input.Last ();

Console.WriteLine ($"Q1: {ComputeTopwise (numbers, operators)}");
Console.WriteLine ($"Q2: {ComputeTopwiseElite ()}");

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

static long ComputeTopwiseElite()
{
    var input = File.ReadAllLines ("input.txt")
        .ToArray ();

    // Add in a trailing separator
    var numbers = input.SkipLast (1)
        .Select (t => (t + " ").AsMemory ())
        .ToArray ();

    var operators = (input.Last () + " ").AsSpan ();

    long total = 0;
    while (operators.Length > 0) {
        var nextNumber = operators.Slice (1).IndexOfAnyExcept (' ') + 1;
        if (nextNumber == 0)
            nextNumber = operators.Length;
        var operatorSlice = operators.Slice (0, nextNumber);

        total += Encephalopod (numbers, operatorSlice);

        operators = operators.Slice (nextNumber);
        for (int i = 0; i < numbers.Length; i++)
            numbers[i] = numbers[i].Slice (nextNumber);
    }

    return total;
}

static long Encephalopod (ReadOnlyMemory<char>[] operandSlice, ReadOnlySpan<char> operatorSlice)
{
    // Subtract out the ' ' in between each group of numbers.
    var length = operatorSlice.Length - 1;
    var parts = operandSlice.Select (t => t.Slice (0, length)).ToArray ();

    var op = operatorSlice.Slice (operatorSlice.IndexOfAnyExcept (' '), 1)[0];

    long? total = null;
    for (int i = length - 1; i >= 0; i--) {
        long currentNumber = 0;
        foreach (var part in parts) {
            if (part.Span.Slice (i, 1)[0] != ' ')
                currentNumber = currentNumber * 10 + long.Parse (part.Span.Slice (i, 1));
        }
        if (total == null)
            total = currentNumber;
        else if (op == '*')
            total *= currentNumber;
        else if (op == '+')
            total += currentNumber;
    }

    Console.WriteLine ("  Was: {0}", total!.Value);
    return total!.Value;
}
