module day05_part01

open System.IO
open System.Collections.Generic
open Utilities


let path = "day05/day05_input.txt"
let inputLines = GetLinesFromFile(path)

let execute =
    inputLines |> List.ofArray |> List.map (fun s -> calculateSeat 0 127 0 7 0 s) |> List.max