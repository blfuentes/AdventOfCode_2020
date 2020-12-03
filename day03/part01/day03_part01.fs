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

let rec countCollisions (currentForest: list<int[]>) initX initY maxwidth maxheight right down =
    match initY <= maxheight with
    | true -> 
        let point = [|initX + right; initY + down|]
        let newWidth =
            match point.[0] >= maxwidth with
            | true -> maxwidth + width
            | false -> maxwidth
        let newForest =
            match point.[0] >= maxwidth with
            | true -> transportTrees 1 currentForest
            | false -> currentForest

        match newForest |> List.exists (fun t -> t.[0] = point.[0] && t.[1] = point.[1]) with 
        | true -> 1 + (countCollisions newForest (initX + right) (initY + down) newWidth maxheight right down)
        | _ -> countCollisions newForest (initX + right) (initY + down) newWidth maxheight right down
    | false -> 0

let execute =
    countCollisions trees 0 0 width height 3 1