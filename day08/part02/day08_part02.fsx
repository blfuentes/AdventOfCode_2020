open System.IO
open System.Collections.Generic
open System

#load @"../../Model/CustomDataTypes.fs"
#load @"../../Modules/Utilities.fs"

open Utilities
open CustomDataTypes

//let file = "test_input.txt"
let file = "day08_input.txt"
let path = __SOURCE_DIRECTORY__ + @"../../" + file
let inputLines = GetLinesFromFileFSI2(path)

let operations = 
    seq {
        for line in inputLines do
            let parts = line.Split(' ')
            let operation =
                match parts.[0] with
                | "acc" -> HandheldOpType.ACC
                | "jmp" -> HandheldOpType.JMP
                | "nop" -> HandheldOpType.NOP
                | _ -> HandheldOpType.MISSING
            yield {
                Op = operation;
                Offset = parts.[1] |> int
            }
    } |> Array.ofSeq

let rec calculateAccumulator (currentValue: int) (consumedOps: int list) (newOpIdx: int) (consumed: bool) (checkOpIdx: int list) (program: HandledOperation[]) =
    if newOpIdx = program.Length then
        (true, currentValue)
    else
        match consumedOps |> List.contains(newOpIdx) with
        | true -> calculateAccumulator 0 [] 0 false checkOpIdx.Tail operations
        | false -> 
            let newOp = program.[newOpIdx]
            match newOp.Op with
            | HandheldOpType.ACC -> calculateAccumulator (currentValue + newOp.Offset) (consumedOps @ [newOpIdx]) (newOpIdx + 1) consumed checkOpIdx program
            | HandheldOpType.JMP when checkOpIdx.Length > 0 && newOpIdx = checkOpIdx.Head && not consumed -> calculateAccumulator currentValue (consumedOps @ [newOpIdx]) (newOpIdx + 1) true checkOpIdx.Tail program
            | HandheldOpType.JMP -> calculateAccumulator currentValue (consumedOps @ [newOpIdx]) (newOpIdx + newOp.Offset) false checkOpIdx program
            | HandheldOpType.NOP when checkOpIdx.Length > 0 && newOpIdx = checkOpIdx.Head && not consumed -> calculateAccumulator currentValue (consumedOps @ [newOpIdx]) (newOpIdx + newOp.Offset) true checkOpIdx program
            | HandheldOpType.NOP -> calculateAccumulator currentValue (consumedOps @ [newOpIdx]) (newOpIdx + 1) false checkOpIdx program
            | _ -> (false, currentValue)

let checkOpIdx = operations |> Array.filter(fun o -> o.Op = HandheldOpType.JMP ||o.Op = HandheldOpType.NOP) |> Array.map(fun o -> Array.IndexOf(operations, o))
let result = calculateAccumulator 0 [] 0 false (checkOpIdx |> List.ofArray) operations
    