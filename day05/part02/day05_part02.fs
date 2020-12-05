module day05_part02

open System.IO
open System.Collections.Generic
open Utilities


let path = "day05/day05_input.txt"
let inputLines = GetLinesFromFile(path)

let execute =
    let sortedSeats = inputLines |> List.ofArray |> List.map (fun s -> calculateSeat 0 127 0 7 0 s) |> List.sort
    let isMissing (a, b) = b - a <> 1
    1 + (sortedSeats |> List.pairwise |> List.find (fun x -> isMissing x) |> fst)