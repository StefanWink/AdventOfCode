namespace AdventOfCode.FSharp.Y23.D02

open System

module Day02 =

    type Cubeset =
        {
            red: int
            green: int
            blue: int
        }

    type Game =
        {
            Id: int
            Cubesets: Cubeset[]
        }

    let stringSplitOptions =
        StringSplitOptions.RemoveEmptyEntries |||
        StringSplitOptions.TrimEntries

    let parseCubeset(cubeset: string) =
        let colors = cubeset.Split(',', stringSplitOptions)

        let mutable red = 0
        let mutable green = 0
        let mutable blue = 0

        for color in colors do
            let c = color.Split(' ', stringSplitOptions)

            match c[1] with
            | "red" -> red <- c[0] |> int
            | "green" -> green <- c[0] |> int
            | "blue" -> blue <- c[0] |> int
            | _ -> ()

        let cubeset =
            {
                red = red
                green = green
                blue = blue
            }

        cubeset

    let parseCubesets(line: string) =
        let parts = line.Split(';', stringSplitOptions)

        let cubesets =
            parts
            |> Array.map parseCubeset

        cubesets

    let parseGameId(line: string) =
        line.Substring(5)
        |> int

    let parseGame(line: string) =
        let parts = line.Split(':', stringSplitOptions)

        let game =
            {
                Id = parseGameId parts[0]
                Cubesets = parseCubesets parts[1]
            }

        game

    let canPlayCubeset(cubeset: Cubeset) =
        cubeset.red <= 12 && cubeset.green <= 13 && cubeset.blue <= 14

    let canPlayGame(game: Game) =
        game.Cubesets
        |> Array.forall canPlayCubeset

    let calculatePartOne lines =
        lines
        |> Array.map parseGame
        |> Array.filter canPlayGame
        |> Array.sumBy (fun game -> game.Id)
