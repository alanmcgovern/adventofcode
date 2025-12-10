var points = File.ReadAllLines ("input.txt")
    .Select (t => t.Split (','))
    .Select (t => (long.Parse (t[0]), long.Parse (t[1])))
    .ToArray ();

var coverage = new Dictionary<((long, long), (long, long)), long> ();
for (int i = 0; i < points.Length; i ++) {
    for (int j = i; j < points.Length; j++) {
        var a = points[i];
        var b = points[j];
        coverage[(a, b)] = (1 + Math.Abs (b.Item1 - a.Item1)) * (1 + Math.Abs (b.Item2 - a.Item2));
    }
}

Console.WriteLine ($"Q1: {coverage.Values.Max ()}");
