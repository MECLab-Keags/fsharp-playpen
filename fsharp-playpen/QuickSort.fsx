let rec quickSort list =
    match list with
    | [] -> []
    | firstElement::otherElements ->
        let smallerElements =
            otherElements 
            |> List.filter (fun e -> e < firstElement)
            |> quickSort
        let largerElements =
            otherElements
            |> List.filter (fun e -> e > firstElement)
            |> quickSort

        List.concat [smallerElements; [firstElement]; largerElements]
