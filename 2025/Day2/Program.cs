var ranges = File.ReadAllLines ("input.txt")
    .Where (t => !string.IsNullOrEmpty (t))
    .SelectMany (t => t.Split (","))
    .Select (t => (long.Parse (t.Split ("-", StringSplitOptions.None)[0]), long.Parse (t.Split ("-", StringSplitOptions.None)[1])))
    .ToArray ();

long invalidSum = 0;
foreach (var range in ranges) {
    for (long current = range.Item1; current <= range.Item2; current++) {
        var v = current.ToString ().AsSpan ();
        if (v.Length % 2 != 0)
            continue;

        if (v.Slice (0, v.Length / 2).SequenceEqual (v.Slice (v.Length / 2)))
            invalidSum += current;
    }
}

Console.WriteLine ($"Q1: {invalidSum}");

invalidSum = 0;
foreach (var range in ranges) {
    for (long current = range.Item1; current <= range.Item2; current++) {
        var v = current.ToString ().AsSpan ();

        for (int i = 1; i <= v.Length / 2; i++) {
            // If the string can't contain a multiple of this pattern, skip it.
            if (v.Length % i != 0)
                continue;

            var check = v;
            while (check.StartsWith (v.Slice (0, i)))
                check = check.Slice (i);

            if (check.Length == 0) {
                invalidSum += current;
                break;
            }
        }
    }
}

Console.WriteLine ($"Q2: {invalidSum}");
