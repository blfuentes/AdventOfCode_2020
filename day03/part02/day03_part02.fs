module day03_part02

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

let getCollisions currentForest right down= 
    let mutable idx = 0
    let mutable forests = 0
    let mutable maxWidth = width 
    let points =
        seq {
            for jdx in [|0..down..height - 1|] do
                let point = [|idx + right; jdx + down|]
                if point.[0] >= maxWidth then 
                    forests <- forests + 1
                    maxWidth <- maxWidth + width
                else
                    forests <- forests
                let checkForest = transportTrees forests currentForest
                match checkForest |> List.exists (fun t -> t.[0] = point.[0] && t.[1] = point.[1]) with 
                | true -> yield point
                | _ -> ()
                idx <- idx + right
        } |>List.ofSeq
    points.Length

let slopesToCheck = [[|1; 1|]; [|3; 1|]; [|5; 1|]; [|7; 1|]; [|1; 2|]]

let execute =
    slopesToCheck |> List.map (fun s -> getCollisions trees s.[0] s.[1] ) |> List.fold (*) 1