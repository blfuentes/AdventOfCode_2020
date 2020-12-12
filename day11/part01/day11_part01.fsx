open System.Collections.Generic


#load @"../../Model/CustomDataTypes.fs"
#load @"../../Modules/Utilities.fs"

open Utilities
open CustomDataTypes

let file = "test_input.txt"
//let file = "day11_input.txt"
let path = __SOURCE_DIRECTORY__ + @"../../" + file
let inputLines = GetLinesFromFileFSI2(path)

let maxX = inputLines.[0].Length
let maxY = inputLines.Length

let placesDict = new Dictionary<int*int, SeatInfo>()

let plane =
    seq {
        for idx in [0..maxX - 1] do
            for jdx in [0..maxY - 1] do
                let seat =
                    match inputLines.[jdx].[idx] with
                    | '.' -> SeatStatus.FLOOR
                    | 'L' -> SeatStatus.EMPTY
                    | '#' -> SeatStatus.OCCUPIED
                    | _ -> failwith("wrong site type")
                let seatToAdd = { Seat = seat; Location = (idx, jdx); Content = inputLines.[jdx].[idx] }
                placesDict.Add((idx, jdx), seatToAdd)
                yield seatToAdd
    } |> List.ofSeq

let printPlane (planeToPrint: SeatInfo list) (dictPlane: Dictionary<(int*int), SeatInfo>)= 
    for idx in [0 .. maxX - 1] do
        for jdx in [0 .. maxY - 1] do
            //match placesDict((idx, jdx), )
            printf "%c" placesDict.[(jdx, idx)].Content
        printfn ""

let printPlane2 (planeToPrint: SeatInfo list)= 
    for seat in planeToPrint do
        if snd seat.Location = (maxY - 1) then
            printfn ""
        printf "%c" seat.Content

let emptySeats = plane |> List.filter(fun s -> s.Seat = SeatStatus.EMPTY)
let occupiedSeats = plane |> List.filter(fun s -> s.Seat = SeatStatus.OCCUPIED)

let getNewStatus (cheackPlane: SeatInfo list) (seat: SeatInfo) =
    let seatsToCheck =
        seq {
            for idx in [(fst seat.Location) - 1 .. (fst seat.Location) + 1] do
                for jdx in [(snd seat.Location) - 1 .. (snd seat.Location) + 1] do
                    if fst seat.Location <> idx && snd seat.Location <> jdx then
                        yield (idx, jdx)
        } |> List.ofSeq
    let adjEmpty = cheackPlane |> List.filter(fun s -> (seatsToCheck |> List.exists(fun c -> (fst c = fst s.Location) && (snd c = snd s.Location))) && s.Seat = SeatStatus.EMPTY)
    let adjOccupied = cheackPlane |> List.filter(fun s -> (seatsToCheck |> List.exists(fun c -> (fst c = fst s.Location) && (snd c = snd s.Location))) && s.Seat = SeatStatus.OCCUPIED)
    match seat.Seat with
    | SeatStatus.EMPTY -> 
        if adjEmpty.Length = 0 then
            { Seat= SeatStatus.OCCUPIED; Location= (seat.Location); Content= '#'}
        else
            { Seat= seat.Seat; Location= (seat.Location); Content= seat.Content } 
    | SeatStatus.OCCUPIED -> 
        if adjOccupied.Length > 3 then
            { Seat= SeatStatus.EMPTY; Location= (seat.Location); Content= 'L'}
        else
            { Seat= seat.Seat; Location= (seat.Location); Content= seat.Content } 
    | _ -> seat


//printPlane plane placesDict
printPlane2 plane
let newPlane = plane |> List.map (fun s -> getNewStatus plane s)
printPlane2 newPlane
//let rec StatusChanged initPlane eSeats oSeats=
//    le