module Utilities

open System
open System.IO
open System.Text.RegularExpressions

open PasswordPolicy

let GetLinesFromFile(path: string) =
    File.ReadLines(__SOURCE_DIRECTORY__ + @"../../" + path)

let GetLinesFromFileFSI2(path: string) =
    File.ReadAllLines(path)

let GetLinesFromFileFSI(path: string) =
    File.ReadLines(path)

let rec combination (num, list: 'a list) : 'a list list = 
    match num, list with
    | 0, _ -> [[]]
    | _, [] -> []
    | k, (x::xs) -> List.map ((@) [x]) (combination ((k-1), xs)) @ (combination (k, xs))

//let rec combination (num: int, list: List<'T>) : List<List<'T>> = 
//    match num, list with
//    | 0, _ -> [[]]
//    | _, [] -> []
//    | k, (x::xs) -> List.map ((@) [x]) (combination ((k-1), xs)) @ (combination (k, xs))

// XOR OPERATOR
let (^@) (a: bool) (b:bool) : bool =
    a <> b

let (|Regex|_|) pattern input =
    let m = Regex.Match(input, pattern)
    if m.Success then Some(List.tail [ for g in m.Groups -> g.Value ])
    else None

// DAY 03
let getCollisionsBasic (currentForest: list<int[]>) initX initY right down maxwidth maxheight =
    let positions = [initY..down..maxheight]
    seq {
        for pos in initY..down..maxheight do
            let currentPos = positions |> List.findIndex (fun x -> x = pos)
            let point = [|((initX + right) * (currentPos + 1)) % maxwidth; pos + down|]
            match currentForest |> List.exists (fun t -> t.[0] = point.[0] && t.[1] = point.[1]) with 
            | true -> yield point
            | _ -> ()
    } |> Seq.length

// DAY 04
let byrValid (elem:string) =
    elem.Length = 4 && (elem |> int) >= 1920 && (elem |> int) <= 2002

let iyrValid (elem:string) =
    elem.Length = 4 && (elem |> int) >= 2010 && (elem |> int) <= 2020

let eyrValid (elem:string)=
    elem.Length = 4 && (elem |> int) >= 2020 && (elem |> int) <= 2030

let hgtValid (elem:string)=
    let parts =
        match elem with
        | Regex @"(?<height>\d+)(?<unittype>\w+)" [m; M] -> Some { height= m |> int; unittype = M }
        | _ -> None
    match parts with
    | Some { HeightType.height = height; HeightType.unittype = unittype; } when unittype = "cm" -> height >= 150 && height <= 193
    | Some { HeightType.height = height; HeightType.unittype = unittype; } when unittype = "in" -> height >= 59 && height <= 76
    | _ -> false

let hclValid (elem:string)=
    match elem with
    | Regex @"#[0-9a-f]{6}" result -> true
    | _ -> false


let eclValid (elem:string)=
    ["amb"; "blu"; "brn"; "gry"; "grn"; "hzl"; "oth"] |> List.contains(elem)

let pidValid (elem:string)=
    elem.Length = 9 && elem |> Seq.forall Char.IsDigit