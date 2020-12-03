module day03_part01

open System.IO
open Utilities


let path = "day03/day03_input.txt"
let values = GetLinesFromFile(path) |> Array.ofSeq |> Array.map (fun line -> line.ToCharArray())

let width = values.[0].Length
let height = values.Length

let trees =
    seq {
        for idx in [|0..height - 1|] do
            for jdx in [|0 .. width - 1|] do
                match values.[idx].[jdx] with
                    | '#' -> yield [|jdx; idx|]
                    | _  -> ()
    } |> List.ofSeq

let transportTrees (numPos: int) (input: list<int[]>) : list<int[]> =
    input |> List.map (fun t -> [|t.[0] + numPos * width; t.[1]|])

let execute =
    let mutable idx = 0
    let mutable forests = 0
    let mutable maxWidth = width 
    let points =
        seq {
            for jdx in [|0..height - 1|] do
                let point = [|idx + 3; jdx + 1|]
                if point.[0] >= maxWidth then 
                    forests <- forests + 1
                    maxWidth <- maxWidth + width
                else
                    forests <- forests
                let checkForest = transportTrees forests trees
                match checkForest |> List.exists (fun t -> t.[0] = point.[0] && t.[1] = point.[1]) with 
                | true -> yield point
                | _ -> ()
                idx <- idx + 3
        } |>List.ofSeq
    points.Length