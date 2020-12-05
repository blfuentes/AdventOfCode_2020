module Utilities

open System
open System.IO
open System.Text.RegularExpressions

open PasswordPolicy

let GetLinesFromFile(path: string) =
    File.ReadAllLines(__SOURCE_DIRECTORY__ + @"../../" + path)

let GetLinesFromFileFSI2(path: string) =
    File.ReadAllLines(path)

let GetLinesFromFileFSI(path: string) =
    File.ReadLines(path)

let rec combination (num, list: 'a list) : 'a list list = 
    match num, list with
    | 0, _ -> [[]]
    | _, [] -> []
    | k, (x::xs) -> List.map ((@) [x]) (combination ((k-1), xs)) @ (combination (k, xs))

let getLinesGroupBySeparator (inputLines: string list) (separator: string) =
    let complete = 
        seq {
            for line in inputLines do
                yield! line.Split(' ')
        } |> List.ofSeq
    let folder (a) (cur, acc) = 
        match a with
        | _ when a <> separator -> a::cur, acc
        | _ -> [], cur::acc
    
    let result = List.foldBack folder (complete) ([List.last complete], []) 
    (fst result)::(snd result)


let getLinesGroupBySeparator2 (inputLines: string list) (separator: string) =
    let complete = 
        seq {
            for line in inputLines do
                yield! line.Split(' ')
        } |> List.ofSeq
    let folder (a) (cur, acc) = 
        match a with
        | _ when a <> separator -> a::cur, acc
        | _ -> [], cur::acc
        
    let result = List.foldBack folder (complete) ([], [])
    (fst result)::(snd result)

let folder (a) (cur, acc) = 
    match a with
    | _ when a <> 0 -> a::cur, acc
    | _ -> [], cur::acc

let split lst =
    let result = List.foldBack folder (lst) ([], [])
    (fst result)::(snd result)

//let split lst =
//    let folder (a, b) (cur, acc) = 
//        match a with
//        | _ when a < b -> a::cur, acc
//        | _ -> [a], cur::acc

//    let result = List.foldBack folder (List.pairwise lst) ([List.last lst], []) 
//    (fst result)::(snd result)

//printfn "%A" (split [1; 2; 3; 2; 2; 4; 1; 5;])

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

// DAY 05
let rec calculateSeat minRowCur maxRowCur minColCur maxColCur (index:int) (seatdefinition:string)=
    match index < seatdefinition.Length with
    | true -> 
        match seatdefinition.[index] with
        | 'F' -> calculateSeat minRowCur (minRowCur + (maxRowCur - minRowCur) / 2)  minColCur maxColCur (index + 1) seatdefinition
        | 'B' -> calculateSeat (minRowCur + (maxRowCur + 1 - minRowCur) / 2) maxRowCur minColCur maxColCur (index + 1) seatdefinition
        | 'L' -> calculateSeat minRowCur maxRowCur minColCur (minColCur + (maxColCur - minColCur) / 2) (index + 1) seatdefinition
        | 'R' -> calculateSeat minRowCur maxRowCur (minColCur + (maxColCur + 1 - minColCur) / 2) maxColCur  (index + 1) seatdefinition
        | _ -> 0
    | false -> minRowCur * 8 + minColCur