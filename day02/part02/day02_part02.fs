module day02_part02

open System.IO
open Utilities
open PasswordPolicy


let path = "day02/day02_input.txt"
let inputLines = List.ofSeq <| GetLinesFromFile(path)

let inputCheck = inputLines 
                |> List.map (fun line -> {
                    min = line.Split(' ').[0].Split('-').[0] |> int; 
                    max = (line.Split(' ').[0].Split('-').[1]) |> int; 
                    element = line.Split(' ').[1].TrimEnd(':');
                    code = line.Split(' ').[2]
                    })
 
let passwordIsValid(check: PasswordPolicy) : bool =
    let checkCode = check.code |> Array.ofSeq
    (checkCode.[check.min-1] = (check.element |> char)) <> (checkCode.[check.max-1] = (check.element |> char))

let execute =
    inputCheck |> List.filter (fun check -> passwordIsValid check) |> List.length