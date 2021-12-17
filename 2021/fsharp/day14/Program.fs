﻿open System.IO

let parseInput(filePath:string) =
    let lines = File.ReadAllLines(filePath)
    let polymer = lines[0].ToCharArray ();
    let rules = lines
                |> Array.skip 2
                |> Array.map(fun line -> line.Split(" -> "))
                |> Array.map(fun seq -> (seq[0], seq[1]))
                |> Map

    polymer, rules

let growPolymerPairs(pairs:Map<string, int64>, transformations:Map<string,string>, iterations:int) = 
    let rec transform iteration (pairs:Map<string, int64>) =
        if iteration > 0 then
            pairs
            |> Map.toArray
            |> Array.collect (fun (key, count) -> [| ($"{key[0]}{transformations[key]}", count) ; ($"{transformations[key]}{key[1]}", count) |])
            |> Array.groupBy(fun (key, _count) -> key)
            |> Array.map(fun (key, counts) -> (key, counts |> Array.map snd |> Array.sum))
            |> Map.ofArray
            |> transform(iteration - 1)
        else
            pairs

    transform iterations pairs
    
let calculate_delta(filePath:string, iterations:int) =
    let (polymer, transformations) = parseInput(filePath)
    
    let growPolymer (pairs) =
        growPolymerPairs(pairs, transformations, iterations)

    polymer
        |> Array.windowed(2)
        |> Array.map (fun pair -> $"{pair[0]}{pair[1]}")
        |> Array.groupBy id
        |> Array.map(fun (key, value) -> key,(int64)value.Length)
        |> Map
        |> growPolymer
        |> Map.toArray
        |> Array.append([| ($"{polymer |> Array.last}", 1) |])
        |> Array.groupBy(fun (key, _) -> key[0])
        |> Array.map(fun (key, counts) -> (key, counts |> Array.map snd |> Array.sum))
        |> Array.sortByDescending(fun (_key, value) -> value)
        |> fun final -> (final |> Array.head |> snd) - (final |> Array.last |> snd)

let execute =
    ("input.txt", 10)
    |> calculate_delta
    |> printfn "Q1 - delta is: %d"

    ("input.txt", 40)
    |> calculate_delta
    |> printfn "Q2 - delta is: %d"
execute