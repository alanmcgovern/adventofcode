
using System.Xml.Linq;

namespace Day23
{
    class Program
    {
        readonly record struct Connection(string First, string Second);

        static void Main (string[] args)
        {
            // intern some strings and use object reference equality later.
            // BADA BING BADA ZOOOOOM
            var connections = File.ReadAllLines ("input.txt")
                .Select (l => l.Split ('-'))
                .Select (l => new[] { string.Intern (l[0]), string.Intern (l[1]) })
                .ToArray ();

            var triplets = AllTriplets (connections);
            Console.WriteLine ($"Q1: {triplets.Count}");
        }

        static List<HashSet<string>> AllTriplets (string[][] array)
        {
            var validTriples = new List<HashSet<string>> ();
            var matches = new HashSet<string> (ReferenceEqualityComparer.Instance);
            HashSet<string> tmp = new HashSet<string> (ReferenceEqualityComparer.Instance);
            for (int i = 0; i < array.Length - 2; i++) {
                ref string[] a = ref array[i];
                bool aHasT = a[0][0] == 't' || a[1][0] == 't';
                for (int j = i + 1; j < array.Length - 1; j++) {
                    ref string[] b = ref array[j];
                    bool bHasT = b[0][0] == 't' || a[1][0] == 't';

                    tmp.Clear ();
                    tmp.Add (a[0]);
                    tmp.Add (a[1]);
                    tmp.Add (b[0]);
                    tmp.Add (b[1]);
                    if (tmp.Count < 4) {
                        for (int k = j + 1; k < array.Length; k++) {
                            ref string[] c = ref array[k];
                            if ((aHasT || bHasT || c[0][0] == 't' || c[1][0] == 't') &&
                                tmp.Contains (c[0]) && tmp.Contains (c[1]))
                                validTriples.Add (tmp.ToHashSet ());
                        }
                    }
                }
            }

            return validTriples;
        }
    }
}
