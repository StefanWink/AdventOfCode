namespace AdventOfCode.FSharp.Y23.D03

open System

module Day03 =

    type Position = { Row: int; Col: int }

    type Digit = { Digit: int; Position: Position }
    type Period = { Period: Position }
    type Symbol = { Symbol: char; Position: Position }
    type Star = Star of Symbol

    type Number = { Value: int; Positions: Position array }
    type PartNumber = PartNumber of Number

    type EngineSchematic =
        {
            Digits: Digit array
            Symbols: Symbol array
        }

    type Entry =
        | EntryNumber of Digit
        | EntrySymbol of Symbol
        | EntryPeriod of Period
    let parseDigit(c: char) : int =
        c - '0'
        |> int

    let parseEntry(c: char, position: Position) : Entry =
        match c with
        | d when System.Char.IsDigit d -> EntryNumber { Digit = parseDigit d; Position = position }
        | '.' -> EntryPeriod { Period = position }
        | _ -> EntrySymbol { Symbol = c; Position = position }

    let getDigit(entry: Entry) : option<Digit> =
        match entry with
        | EntryNumber d -> Some d
        | _ -> None

    let getSymbol(entry: Entry) : option<Symbol> =
        match entry with
        | EntrySymbol s -> Some s
        | _ -> None

    let getStar(symbol: Symbol) : option<Star> =
        match symbol with
        | s when s.Symbol = '*' -> Some (Star s)
        | _ -> None

    let parseLine(line: string, row: int) : EngineSchematic =
        let entries =
            line
            |> Seq.toArray
            |> Array.mapi (fun col c ->
                let position = { Row = row; Col = col }
                parseEntry(c, position)
            )

        let digits =
            entries
            |> Array.choose getDigit

        let symbols =
            entries
            |> Array.choose getSymbol

        let schematic =
            {
                Digits = digits
                Symbols = symbols
            }

        schematic

    let parseLines(lines: string array) : EngineSchematic array =
        lines
        |> Array.mapi (fun row line ->
            parseLine(line, row)
        )

    let precedes(number: Number, digit: Digit) : bool =
        let lastPosition = Array.last number.Positions
        lastPosition.Row = digit.Position.Row && lastPosition.Col = (digit.Position.Col - 1)

    let findNumbers(digits: Digit array) : Number array =
        digits
        |> Array.fold (fun numbers digit ->
            let number =
                {
                    Value = digit.Digit
                    Positions = [|digit.Position|]
                }

            if Array.isEmpty numbers then [| number |]
            else
                let lastNumber = Array.last numbers

                if precedes(lastNumber, digit) then
                    let numbersSub = Array.sub numbers 0 (numbers.Length - 1)

                    let updatedNumber =
                        {
                            Value = lastNumber.Value * 10 + digit.Digit
                            Positions = Array.append lastNumber.Positions [| digit.Position |]
                        }

                    Array.append numbersSub [| updatedNumber |]
                else
                    Array.append numbers [| number |]
        ) Array.empty<Number>

    let isAdjacent(pos1: Position, pos2: Position) : bool =
        Math.Abs (pos1.Row - pos2.Row) <= 1 &&
        Math.Abs (pos1.Col - pos2.Col) <= 1

    let findPartNumbers(puzzle: EngineSchematic) : PartNumber array =
        findNumbers puzzle.Digits
        |> Array.filter (fun number ->
            number.Positions
            |> Array.exists (fun pos ->
                puzzle.Symbols
                |> Array.exists (fun sym ->
                    isAdjacent(pos, sym.Position)
                )
            )
        )
        |> Array.map (fun number -> PartNumber number)

    let parsePuzzle(lines: string array) : EngineSchematic =
        let emptySchematic =
            {
                Digits = Array.empty
                Symbols = Array.empty
            }

        parseLines lines
        |> Array.fold (fun x y ->
        {
            Digits = Array.concat [x.Digits; y.Digits]
            Symbols = Array.concat [x.Symbols; y.Symbols]
        }) emptySchematic

    let getPartNumberValue(PartNumber partNumber) : int =
        partNumber.Value

    let calculatePartOne(lines: string array) : int =
        lines
        |> parsePuzzle
        |> findPartNumbers
        |> Array.sumBy getPartNumberValue

    type GearRatio = { PartNumber1: PartNumber; PartNumber2: PartNumber }

    let findAdjacentPartNumbers(star: Star, partNumbers: PartNumber array) : PartNumber array =
        let (Star starSymbol) = star
        let starPosition = starSymbol.Position

        partNumbers
        |> Array.filter (fun partNumber ->
            let (PartNumber number) = partNumber

            number.Positions
            |> Array.exists (fun position ->
                isAdjacent(position, starPosition)
            )
        )

    let findGearRatios(partNumbers: PartNumber array, stars: Star array) : GearRatio array =
        stars
        |> Array.choose (fun star ->
            match findAdjacentPartNumbers(star, partNumbers) with
            | adjacentPartNumbers when adjacentPartNumbers.Length = 2 ->
                Some
                    {
                        PartNumber1 = adjacentPartNumbers[0]
                        PartNumber2 = adjacentPartNumbers[1]
                    }
            | _ -> None
        )

    let getGearRatioValue(gearRatio: GearRatio) : int =
        let value1 = gearRatio.PartNumber1 |> getPartNumberValue
        let value2 = gearRatio.PartNumber2 |> getPartNumberValue
        value1 * value2

    let calculatePartTwo(lines: string array) : int =
        let puzzle =
            lines
            |> parsePuzzle

        let partNumbers =
            puzzle
            |> findPartNumbers

        let stars =
            puzzle.Symbols
            |> Array.choose getStar

        findGearRatios(partNumbers, stars)
        |> Array.sumBy getGearRatioValue
