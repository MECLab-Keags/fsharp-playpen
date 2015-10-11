
type CartItem = string

type EmptyState = NoItems

type ActiveState = {UnpaidItems : CartItem list}

type PaidForState = {PaidItems : CartItem list; Payment : decimal}

type Cart =
    | Empty of EmptyState
    | Active of ActiveState
    | PaidFor of PaidForState

// ***************************
// Empty cart operations
// ***************************
let addToEmptyCart item =
    // returns a new 'Active' cart with an UnpaidItems list which only contain the single item.
    Cart.Active {UnpaidItems=[item]}

// ***************************
// Active cart operations
// ***************************
let addToActiveCart cart itemToAdd =
    // create a new list with the existing cart's UnpaidItems and the 'itemToAdd' 
    let newItems = itemToAdd :: cart.UnpaidItems
    // create a new Active cart using the existing cart and the new list of UnpaidItems.
    Cart.Active {cart with UnpaidItems=newItems}

let removeFromActiveCart cart itemToRemove =
    // Create a new list from the existing cart's list that doesn't include the item that is to be removed.
    let newList = cart.UnpaidItems |> List.filter(fun item -> item <> itemToRemove)
    match newList with
        | [] -> Cart.Empty NoItems
        | _ -> Cart.Active {cart with UnpaidItems=newList}

let payForActiveCart cart payment =
    Cart.PaidFor {PaidItems=cart.UnpaidItems; Payment=payment}

// ***************************
// Add nice helper functions as methods on the CartState types.
// ***************************
type EmptyState with
    member this.Add = addToEmptyCart
type ActiveState with
    member this.Add = addToActiveCart this
    member this.Remove = removeFromActiveCart this
    member this.Checkout = payForActiveCart this

let addItemToCart cart item =
    match cart with
        | Empty state -> state.Add item
        | Active state -> state.Add item
        | PaidFor state -> 
            printfn "ERROR: The cart has been paid for."
            cart

let removeItemFromCart cart item =
    match cart with
        | Empty state -> 
            printfn "ERROR - removeItemFromCart: Cart is empty"
            cart
        | Active state -> state.Remove item
        | PaidFor state -> 
            printfn "ERROR - removeItemFromCart: Cart has been paid for"
            cart

let displayCart cart =
    match cart with
        | Empty state -> printfn "Cart is empty"
        | Active state -> printfn "Cart contains %A of unpaid items" state.UnpaidItems
        | PaidFor state -> printfn "Cart contains %A of paid items, to amount of %f" state.PaidItems state.Payment


// ***************************
// Add nice helper methods to the Cart.
// ***************************
type Cart with
    static member NewCart = Cart.Empty NoItems
    member this.Add = addItemToCart this
    member this.Remove = removeItemFromCart this
    member this.Display = displayCart this

// ***************************
// Test the design
// ***************************

let empty = Cart.NewCart
empty.Display

let cartA = empty.Add "PC"
printf "cartA="; cartA.Display

let cartB = cartA.Add "Monitor"
printf "cartB="; cartB.Display

let cartC = cartB.Remove "PC"
printf "cartC="; cartC.Display

let emptyCart1 = cartC.Remove "Monitor"
printf "cartD="; emptyCart1.Display

let emptyCart2 = emptyCart1.Remove "Something"
printf "emptyCart2"; emptyCart2.Display


