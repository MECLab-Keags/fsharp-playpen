open System
open System.Threading

// Traditional asyc with an event callback.
let tranditionalWithCallback =
    let event = new AutoResetEvent(false)

    let timer = new System.Timers.Timer(2000.0)
    timer.Elapsed.Add(fun _ -> event.Set() |> ignore)

    printfn "Waiting for timer at %O" DateTime.Now.TimeOfDay
    timer.Start()

    printfn "Do something while waiting for the timer to execute."
    event.WaitOne() |> ignore
    
    printfn "Timer elapsed at %O" DateTime.Now.TimeOfDay

// F# style of handling events using Async workflow.
let asyncWorkflows =
    let timer = new System.Timers.Timer(2000.0)
    // Use async workflow to handle the timer.Elapsed event and pipe the result into an Ignore.
    let timerEvent = Async.AwaitEvent(timer.Elapsed) |> Async.Ignore

    printfn "Waiting for timer at %O" DateTime.Now.TimeOfDay
    timer.Start()

    printfn "Do something while waiting for the timer to execute."

    // Block until the 'timerEvent' async workflow to complete.
    Async.RunSynchronously timerEvent
    printfn "Timer elapsed at %O" DateTime.Now.TimeOfDay
