type Person = {GivenName:string; FamilyName:string;}

// Immutable type has a cloning function built in which allows for custom transforms.
let john = {GivenName = "John"; FamilyName = "Toogood"}
let alice = {john with GivenName = "Alice"}

