module day02_part02
open System.IO
open Utilities

let path = "day02/day02_input.txt"
let inputLines = GetLinesFromFile(path) |> Seq.map int |> Seq.toList

let execute =
    0