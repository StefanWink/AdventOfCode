open System

type Card =
    { WinningNumbers: int array
      Numbers: int array }

let stringSplitOptions =
    StringSplitOptions.RemoveEmptyEntries
    ||| StringSplitOptions.TrimEntries

let parseCard (line: string) : Card =
    let colon = line.IndexOf(':')
    let pipe = line.IndexOf('|')

    let winningNumbers =
        line
            .Substring(colon + 2, pipe - colon - 2)
            .Split(' ', stringSplitOptions)
        |> Array.map Int32.Parse

    let numbers =
        line
            .Substring(pipe + 2, line.Length - pipe - 2)
            .Split(' ', stringSplitOptions)
        |> Array.map Int32.Parse

    let card =
        { WinningNumbers = winningNumbers
          Numbers = numbers }

    card

let calculatePoints (card: Card) : int =
    let initialPoints = 0

    let points =
        card.Numbers
        |> Array.filter (fun number -> Array.contains number card.WinningNumbers)
        |> Array.fold
            (fun totalPoints _ ->
                match totalPoints with
                | 0 -> 1
                | _ -> totalPoints * 2)
            initialPoints

    points

let calculatePartOne (lines: string array) : int =
    lines
    |> Array.map parseCard
    |> Array.sumBy calculatePoints

let calculatePartTwo (lines: string array) : int64 = failwith "todo"

let userFolder =
    System.Environment.SpecialFolder.UserProfile
    |> System.Environment.GetFolderPath

let lines =
    [| userFolder
       "Documents"
       "AdventOfCode"
       "input.txt" |]
    |> System.IO.Path.Combine
    |> System.IO.File.ReadAllLines

let partOne = lines |> calculatePartOne
printfn $"Part one: {partOne}"

let partTwo = lines |> calculatePartTwo
printfn $"Part two: {partTwo}"
