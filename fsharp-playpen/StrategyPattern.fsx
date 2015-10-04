
type Animal(noiseMakingStrategy) =
    member this.MakeNoise = noiseMakingStrategy() |> printfn "Make noise %s"

// Create a cat that has 'meowing' as the noiseMakingStrategy
let meowing() = "Meow"
let cat = Animal(meowing)
cat.MakeNoise

let woofOrBark() = if (System.DateTime.Now.Second % 2 = 0)
                   then "Woof" else "Bark"
let dog = Animal(woofOrBark)
dog.MakeNoise
dog.MakeNoise
