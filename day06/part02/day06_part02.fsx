open System.IO
open System.Collections.Generic
open System

#load @"../../Model/PasswordPolicy.fs"
#load @"../../Modules/Utilities.fs"

open Utilities
open PasswordPolicy

//let file = "test_input.txt"
let file = "day06_input.txt"
let path = __SOURCE_DIRECTORY__ + @"../../" + file
let inputLines = GetLinesFromFileFSI2(path)
