module Utilities

open System.IO

let GetLinesFromFile(path: string) =
    File.ReadLines(__SOURCE_DIRECTORY__ + @"../../" + path)

let rec combination num list = 
    match num, list with
    | 0, _ -> [[]]
    | _, [] -> []
    | k, (x::xs) -> List.map ((@) [x]) (combination (k-1) xs) @ combination k xs