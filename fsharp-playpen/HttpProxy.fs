module HttpProxyHelper

open Microsoft.FSharp.Control.CommonExtensions
open System
open System.IO
open System.Net

let GetAsync (url:string) = 
    async {
        let request = WebRequest.CreateHttp(url)
        use! response = request.AsyncGetResponse()
        use stream = response.GetResponseStream()
        use reader = new IO.StreamReader(stream)
        return reader.ReadToEnd()
    }

let WithRetry (url:string) func =
        let result = func url
        result |> Async.StartAsTask


type HttpProxy() = 
    member x.GetAsyncLazy = GetAsync
    member x.GetAsync(url) = GetAsync url |> Async.StartAsTask

