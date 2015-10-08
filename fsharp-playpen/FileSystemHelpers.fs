module FileSystemHelpers

open Microsoft.FSharp.Control.CommonExtensions

(***  Exhaustive pattern matching as an error handling technique  ***)
// define a "union" of two different alternatives (either Success or Failure), 
//  using generics to dynamically type the success/failure return value.
type Result<'success, 'error> =
    | Success of 'success
    | Failure of 'error

// define a "union" of two different error alternatives that represents a file error:
//  - FileNotFound of a string value
//  - UnauthorizedAccess of a string and System.Exception
type FileErrorReason =
    | FileNotFound of string
    | UnauthorizedAccess of string * System.Exception

type FileHelper =
    // Open's the specified file and executes the specifield action.
    member this.ExecuteOnFile action filePath =
        try
            use stream = new System.IO.StreamReader(filePath:string)
            let result = action stream
            stream.Close()
            Success(result)
        with
            // Handle a FileNotFoundException and return a Failure result.
            | :? System.IO.FileNotFoundException as ex
                -> Failure(FileNotFound filePath)
            // Handle a SecurityException and return an UnauthorizaedAccess Failure result (with the exception).
            | :? System.Security.SecurityException as  ex
                -> Failure(UnauthorizedAccess (filePath, ex))