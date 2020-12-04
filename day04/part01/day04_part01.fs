module day04_part01

open System.IO
open System.Collections.Generic
open Utilities


let path = "day04/day04_input.txt"
let inputLines = GetLinesFromFile(path) |> Array.ofSeq |> List.ofArray

let allFields = [|"byr"; "iyr"; "eyr"; "hgt"; "hcl"; "ecl"; "pid"; "cid"|]
let requiredFields = [|"byr"; "iyr"; "eyr"; "hgt"; "hcl"; "ecl"; "pid"|]

let passportList = new List<List<string>>()
passportList.Add(new List<string>())

let values = 
    let mutable currentIndex = 0
    for line in inputLines do
        match line with 
        | "" -> 
            passportList.Add(new List<string>())
            currentIndex <- currentIndex + 1
        | _ -> passportList.Item(currentIndex).AddRange(line.Split(' '))
    passportList

let passPortIsValid (credentials: List<string>) =
    requiredFields |> Array.forall (fun field -> credentials.Exists(fun cred -> cred.StartsWith(field)))

let execute =
    passportList |> List.ofSeq |> List.filter(fun p -> passPortIsValid p) |> List.length