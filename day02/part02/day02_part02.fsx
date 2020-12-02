﻿open System.IO

#load @"../../Modules/Utilities.fs"
#load @"../../Model/PasswordPolicy.fs"

open Utilities
open PasswordPolicy

//let file = "test_input.txt"
let file = "day02_input.txt"
let path = __SOURCE_DIRECTORY__ + @"../../" + file

let inputLines = List.ofSeq <| GetLinesFromFileFSI(path)
let inputCheck = inputLines 
                    |> List.map (fun line -> {
                        min = line.Split(' ').[0].Split('-').[0] |> int; 
                        max = (line.Split(' ').[0].Split('-').[1]) |> int; 
                        element = line.Split(' ').[1].TrimEnd(':');
                        code = line.Split(' ').[2]
                        })
 
let passwordIsValid(check: PasswordPolicy) : bool =
    let checkCode = check.code |> Array.ofSeq
    (
        (checkCode.[check.min-1] = (check.element |> char) && checkCode.[check.max-1] <> (check.element |> char)) || 
        (checkCode.[check.min-1] <> (check.element |> char) && checkCode.[check.max-1] = (check.element |> char))
    )

let numberOfValids = inputCheck |> List.filter (fun check -> passwordIsValid check) |> List.length