open System
open System.IO
open System.Net

let get callback url =
    let request = WebRequest.Create(Uri(url))
    use response = request.GetResponse()
    use stream = response.GetResponseStream()
    use reader = new StreamReader(stream)
    callback reader url

let callbackWithThousand (reader:IO.StreamReader) url =
    let html = reader.ReadToEnd()
    let html1000 = html.Substring(0, 1000);
    printfn "Downloaded %s. First 1000 characters are %s" url html1000

// test it...
let google = get callbackWithThousand "http://google.com"

// Bind a function with the same callback.
let fetch = get callbackWithThousand
let google2 = fetch "http://google.com"
let microsoft = fetch "http://microsoft.com"

// Fetch each site in list
let sites = ["http://yahoo.com"; "http://bing.com"; "http://apple.com"]
sites |> List.map fetch

