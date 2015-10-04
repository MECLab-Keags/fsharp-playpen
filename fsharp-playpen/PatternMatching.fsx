(***  Pattern matching (with Option type) to avoid nulls.  ***)
let GetFile fileName =
    let file = System.IO.FileInfo(fileName)
    if (file.Exists) then 
        // Wrap the FileInfo object in the Option type 'Some' which is
        //  similar to Nullable<T> with a value in C# (except Option types allows for reference types)
        //  This ensures something is returned and no Null check is required.
        Some(file) 
    else 
        // Return the Option type 'None' which again is similar to Nullable<T> with null in C#.
        None

let ProcessFile filePath =
    // Execute our 'GetFile' func with the specified filePath.
    let fileOption = GetFile filePath
    // Inspect the result using pattern matching which forces us to handle all cases that maybe returned by 'GetFile'.
    //   if 'Some' is returned then we can be confident that the file exists and the FileInfo object is valid.
    //   otherwise 'None' is returned so handle the negative state.
    match fileOption with
        | Some file ->
                printfn "the file %s exists" file.Name
        | None ->
                printfn "the file does not exist"

ProcessFile "C:\\temp\\test.txt"
ProcessFile "C:\\temp\\doesnotexist.txt"


(***  Exhaustive pattern matching for edge cases  ***)
let rec MovingAverages list =
    match list with
        // Specified list is empty so return an empty list.
        | [] -> []
        
        // Get the first item and the second item, average them,
        // then recursively call itself with the rest of the items in the list.
        | x :: y :: rest ->
            let avg = (x+y)/2.0
            avg :: MovingAverages rest
        
        // Single item in the list therefore can't execute average. Return an empty list.
        | [_] -> []

MovingAverages []
MovingAverages [1.0]
MovingAverages [1.0; 2.0; 3.0; 3.5; 4.0; 4.8; 50.0; 25.0]


(***  Exhaustive pattern matching as an error handling technique  ***)

// define a "union" of two different alternatives (either Success or Failure), 
//  using generics to dynamically type the success/failure return value.
type Result<'success, 'error> =
    | Success of 'success
    | Failure of 'error

type FileErrorReason =
    | FileNotFound of string
    | UnauthorizedAccess of string * System.Exception

let execute action filePath =
    try
        use stream = new System.IO.StreamReader(filePath:string)
        let result = action stream
        stream.Close()
        Success(result)
    with
        | :? System.IO.FileNotFoundException as ex
            -> Failure(FileNotFound filePath)
        | :? System.Security.SecurityException as  ex
            -> Failure(UnauthorizedAccess (filePath, ex))

