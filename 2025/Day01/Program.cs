var input = File.ReadAllLines ("input.txt")
    .Select (t => (t[0] == 'R' ? 1 : -1) * int.Parse (t.AsSpan ().Slice (1)))
    .ToArray ();

int zeroCount = 0;
int dialValue = 50;

foreach (var modification in input) {
    dialValue += modification;
    if (dialValue % 100 == 0)
        zeroCount++;
}

Console.WriteLine ($"Q1: {zeroCount}");

zeroCount = 0;
dialValue = 50;

foreach (var modification in input) {
    for (int i =0; i < Math.Abs (modification); i ++) {
        dialValue += Math.Sign (modification);
        if (dialValue == 0) {
            zeroCount++;
        } else if (dialValue == -1) {
            dialValue = 99;
        } else if (dialValue == 100) {
            dialValue = 0;
            zeroCount++;
        }
    }
}

Console.WriteLine ($"Q2: {zeroCount}");
