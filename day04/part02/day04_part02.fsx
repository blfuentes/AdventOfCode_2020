open System.IO
open System.Collections.Generic

#load @"../../Modules/Utilities.fs"

open Utilities

let file = "test_input.txt"
//let file = "day04_input.txt"
let path = __SOURCE_DIRECTORY__ + @"../../" + file

let values = GetLinesFromFileFSI(path)