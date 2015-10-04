
let (|Int|_|) value =
    match System.Int32.TryParse(value) with
    | (true,int) -> Some(int)
    | _ -> None

let (|Bool|_|) value =
    match System.Boolean.TryParse(value) with
    | (true, bool) -> Some(bool)
    | _ -> None


// Create a generic function to execute parsing
let Parse value =
    match value with
    | Int i -> printfn "The value is an int '%i'" i
    | Bool b -> printfn "The value is a bool '%b'" b
    | _ -> printfn "The value '%s' is something else" value