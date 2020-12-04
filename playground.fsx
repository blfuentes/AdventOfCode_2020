let thelist = [1; 2; 3; 0; 2; 4; 0; 5; 6; 0; 7]

let folder (a) (cur, acc) = 
    match a with
    | _ when a <> 0 -> a::cur, acc
    | _ -> [], cur::acc

let split lst =
    let result = List.foldBack folder (lst) ([], [])
    (fst result)::(snd result)

printfn "%A" (split thelist)

List.fold (fun acc x -> x :: acc) [] thelist
List.foldBack (fun x acc -> x :: acc) thelist []


let split2 lst =
    let folder (a, b) (cur, acc) = 
        match a with
        | _ when a < b -> a::cur, acc
        | _ -> [a], cur::acc

    let result = List.foldBack folder (List.pairwise lst) ([List.last lst], []) 
    (fst result)::(snd result)

printfn "%A" (split2 thelist)