
using System;
using System.Diagnostics.Metrics;

static char [][] GetInput ()
    => File.ReadAllLines ("input.txt")
        .Select (t => t.ToArray ())
        .ToArray ();

Console.WriteLine ($"Q1: {Part1 ()}");
Console.WriteLine ($"Q2: {Part2 ()}");

static int Part1 ()
{
    var input = GetInput ();
    var startIndex = input[0].IndexOf ('S');
    input[0][startIndex] = '|';

    int splits = 0;
    for (int row = 1; row < input.Length; row++) {
        for (int col = 0; col < input[row].Length; col++) {
            if (input[row - 1][col] == '|') {
                if (input[row][col] == '.') {
                    input[row][col] = '|';
                } else if (input[row][col] == '^') {
                    input[row][col - 1] = '|';
                    input[row][col + 1] = '|';
                    splits++;
                }
            }
        }
    }
    return splits;
}

static long Part2 ()
{
    var grid = GetInput ();
    var startIndex = grid[0].IndexOf ('S');
    grid[0][startIndex] = '|';

    var journeys = new Dictionary<(int, int), long> {
        {(0, startIndex), 1 }
    };
    for (int row = 1; row < grid.Length; row++) {
        for (int i = 0; i < grid[row].Length; i++) {
            if (grid[row - 1][i] == '|') {
                if (grid[row][i] == '^') {
                    grid[row][i - 1] = '|';
                    grid[row][i + 1] = '|';
                    journeys[(row, i - 1)] = journeys.GetValueOrDefault ((row, i - 1)) + journeys[(row - 1, i)];
                    journeys[(row, i + 1)] = journeys.GetValueOrDefault ((row, i + 1)) + journeys[(row - 1, i)];
                } else {
                    grid[row][i] = '|';
                    journeys[(row, i)] = journeys.GetValueOrDefault ((row, i)) + journeys[(row - 1, i)];
                }
                journeys.Remove ((row - 1, i));

            }
        }
    }
    return journeys.Values.Sum ();
}
