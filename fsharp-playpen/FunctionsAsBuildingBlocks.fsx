
// Building blocks
let add2 x = x + 2
let multiplyBy3 x = x * 3
let square x = x * x

let add2ThenMultiplyBy3 = add2 >> multiplyBy3

let multiplyBy3ThenSquare = multiplyBy3 >> square

// helper logging functions
let logMessage message x = printf "%s%i" message x; x
let logMessageN message x = printfn "%s%i" message x; x

let multiplyBy3ThenSquareLogged = 
    logMessage "before="
    >> multiplyBy3
    >> logMessage " after multiplyBy3="
    >> square
    >> logMessageN " result="




