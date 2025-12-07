using System.Runtime.ExceptionServices;

var input = File.ReadAllLines ("input.txt");
var ranges = input.TakeWhile (t => t.Length > 0).Select (t => { var parts = t.Split ('-'); return (long.Parse (parts[0]), long.Parse (parts[1])); }).ToArray ();
var ids = input.SkipWhile (t => t.Length > 0).Skip (1).Select (t => long.Parse (t)).ToArray ();

var freshIds = new HashSet<long> ();
foreach (var v in ids) {
    foreach (var range in ranges)
        if (v >= range.Item1 && v <= range.Item2)
            freshIds.Add (v);
}

Console.WriteLine ($"Q1: {freshIds.Count}");
Console.WriteLine ($"Q2: {CountRanges(MergeRanges(ranges.ToList ()))}");

static List<(long, long)> MergeRanges(List<(long, long)> ranges)
{
    bool changed;
    do {
        changed = false;
        for (int i = 0; i < ranges.Count; i++) {
            for (int j = i + 1; j < ranges.Count; j++) {
                if (Intersects (ranges[i], ranges[j])) {
                    ranges.Add ((Math.Min (ranges[i].Item1, ranges[j].Item1), Math.Max (ranges[i].Item2, ranges[j].Item2)));
                    ranges.RemoveAt (j);
                    ranges.RemoveAt (i);
                    changed = true;
                }
            }
        }
    } while (changed);
    ranges = ranges.OrderBy (t => t.Item1).ToList ();
    
    return ranges;
}

static bool Intersects ((long low, long high) first, (long low, long high) second)
    => (first.low >= second.low && first.low <= second.high)
    || (first.high >= second.low && first.high <= second.high)
    || (second.low >= first.low && second.low <= first.high)
    || (second.high >= first.low && second.high <= first.high);

static long CountRanges (List<(long, long)> ranges)
    => ranges.Sum (t => t.Item2 - t.Item1 + 1);
