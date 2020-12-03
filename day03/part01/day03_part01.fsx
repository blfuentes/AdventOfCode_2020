open System.IO
open System.Collections.Generic

#load @"../../Modules/Utilities.fs"

open Utilities

//let file = "test_input.txt"
let file = "day03_input.txt"
let path = __SOURCE_DIRECTORY__ + @"../../" + file

let values = GetLinesFromFileFSI(path) |> Array.ofSeq |> Array.map (fun line -> line.ToCharArray())

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
let trees2 = transportTrees 1 trees

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
            | true -> yield point (*printfn "Element found in (%i, %i)" point.[0] point.[1]*)
            | _ -> ()
            idx <- idx + 3
    } |>List.ofSeq
points.Length