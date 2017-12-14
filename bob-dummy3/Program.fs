// Laziness, memoization, expressions, function calls, and function values

// Everything in F# is an expression->it's a piece of pipe
// Example: This is not a control flow. It's a y-shaped evaluator
//if x then y else z
let myFunction x y z = if x then y else z 
let myFunctionShortcut = myFunction

// FSharp by default is an "eager evaluator"
// That means that it computes things as soon as possible
// This is a function value
let doubleANumber x = 2*x
// if I added this code later
let whatsDoubleFour = doubleANumber 4
// Then f# evaluates doubleANumber immediately and replaces it with 4
// Without the parenthesis, f# evaluates it immediately, 
// only once, and never again (for this assignment)
// This usually works fine because if I call it again, I'll use a new variable
let whatsDoubleSix = doubleANumber 6

// You run into trouble when the function can change around
// From SO:
//let read_rest_of_csv() =
//    csv_data.Add(csv_fileH.ReadFields()) |> ignore
//    not csv_fileH.EndOfData

    //while read_rest_of_csv() do ignore None
// Without the parens, read_rest_of_csv would eval once, be true
// and the loop would never terminate. So you have to use parens to
// make F# evaluate it each time. I called this "memoization" but it's 
// really just "function values" as opposed to "function calls"

// Memoization is where fsharp keeps a dictionary of inputs and outputs to a function each time it runs
// If it runs again with the same input, it returns the initial output from the dictionary,
// saving the cycles of having to run the code again
let memoize squareANumber x = x*x 
// This is useful is the computation is expensive and there's no need to re-compute if the inputs are the same


// if the function is really expensive, and/or there's a bunch, and you're not sure you'll ever need the result,
// you can explicitly label it as "lazy". FSharp won't evaluate it until you call "Force"

let tripleSquareANumber x = 3*x*x
let tripleSquareSeven = lazy (tripleSquareANumber 7)
// tripleSquareSeven has no value. It's just hanging out ready to be evaluated
// if you ever need it. (Technically, it's of a type Lazy<int>)
// Here's how we get the answer
let theRealTripleSquareSeven = tripleSquareSeven.Force

// In practice I've only gotten hung up on whether to use parenthesis or not, and that
// was when I was processing streams/files. I haven't used memoization or lazy functions

// if you want a bunch of stuff that only gets generated when you need it,
// use a sequence
let aBillionNumbers=seq{1..1000000000}
// It only generates/gives you values as you request them (as opposed to eagerly creating the entire thing up front)
let firstTenNumbersOfABillion = aBillionNumbers |> Seq.take 10

[<EntryPoint>]
let main argv = 
    printfn "%A" argv
    0 // return an integer exit code
