﻿open System.IO
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