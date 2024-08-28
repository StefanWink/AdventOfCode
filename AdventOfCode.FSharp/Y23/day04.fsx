open System

type Card =
    { Id: int
      WinningNumbers: int array
      Numbers: int array }

let stringSplitOptions =
    StringSplitOptions.RemoveEmptyEntries
    ||| StringSplitOptions.TrimEntries

let parseCard (line: string) : Card =
    let colon = line.IndexOf(':')
    let pipe = line.IndexOf('|')

    let id = line.Substring(4, colon - 4).Trim() |> int

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
        { Id = id
          WinningNumbers = winningNumbers
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

let calculateMatchingNumbersCount (card: Card) : int =
    let matchingNumbers =
        card.Numbers
        |> Array.filter (fun number -> Array.contains number card.WinningNumbers)

    matchingNumbers.Length

let calculatePartTwo (lines: string array) : int =
    // Index = card ID
    let mutable copies = Array.zeroCreate (lines.Length + 1)

    let cards = lines |> Array.map parseCard

    for card in cards do
        let count = calculateMatchingNumbersCount card

        printfn $"Card {card.Id} has {count} matching numbers"

        let winsCopies = 1 + copies[card.Id]

        for i in 1..count do
            let index = card.Id + i

            if index <= lines.Length then
                printfn $"  and wins {winsCopies} copies of Card {index}"
                copies[index] <- copies[index] + winsCopies

    cards.Length + (copies |> Array.sum)

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
