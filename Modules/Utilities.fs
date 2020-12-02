module Utilities

open System.IO

let GetLinesFromFile(path: string) =
    File.ReadLines(__SOURCE_DIRECTORY__ + @"../../" + path)

let GetLinesFromFileFSI(path: string) =
    File.ReadLines(path)

let rec combination (num: int, list: List<'T>) : List<List<'T>> = 
    match num, list with
    | 0, _ -> [[]]
    | _, [] -> []
    | k, (x::xs) -> List.map ((@) [x]) (combination ((k-1), xs)) @ (combination (k, xs))

// XOR OPERATOR
let (^@) (a: bool) (b:bool) : bool =
    a <> b