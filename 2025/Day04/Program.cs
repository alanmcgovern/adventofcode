var input = File.ReadAllLines ("input.txt")
    .Select (t => t.ToArray ())
    .ToArray ();

static void PrintGrid(char[][] grid)
{
    return;
    Console.WriteLine ();
    foreach (var row in grid)
        Console.WriteLine (new String (row));
    Console.WriteLine ();
}

static char[][] GrowGrid (char[][] grid, char pad)
{
    var results = new List<char[]> ();
    var headerFooter = new string (pad, grid[0].Length + 2);
    results.Add (headerFooter.ToArray ());
    foreach (var row in grid) {
        var l = new List<char> ();
        l.Add (pad);
        l.AddRange (row);
        l.Add (pad);
        results.Add (l.ToArray ());
    }
    results.Add (headerFooter.ToArray ());

    return results.ToArray ();
}

static int CountAccessible (char[][] grid)
{
    PrintGrid (grid);

    var total = 0;
    for (int y = 1; y < grid.Length - 1; y++) {
        for (int x = 1; x < grid[y].Length - 1; x++) {
            if (grid[y][x] != '@')
                continue;

            int neighbours = 0;
            for (int i = x - 1; i <= x + 1; i ++) {
                for (int j = y - 1; j <= y + 1; j ++) {
                    neighbours += (grid[j][i] == '@' || grid[j][i] == 'A') ? 1 : 0;
                }
            }

            // We count the node itself as well as it's neighbours.
            if (neighbours <= 4) {
                grid[y][x] = 'A';
                total++;
            }
        }
    }

    PrintGrid (grid);
    return total;
}


Console.WriteLine ($"Q1: {CountAccessible (GrowGrid (input, '?'))}");

var grid = GrowGrid (input, '?');
int accessible = 0;
var totalAccessible = 0;
do {
    accessible = CountAccessible (grid);
    foreach(var row in grid) {
        for (int i = 0; i < row.Length; i++)
            if (row[i] == 'A')
                row[i] = '.';
    }
    totalAccessible += accessible;
} while (accessible > 0);

Console.WriteLine ($"Q2: {totalAccessible}");
