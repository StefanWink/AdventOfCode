mod my;

use my::{Cubeset, Game, Game2};
use std::fs::read_to_string;

fn main() {
    let input_path = "/temp/input.txt";

    let part_one = calculate_part_one(input_path);
    println!("Part one: {}", part_one);

    let part_two = calculate_part_two(input_path);
    println!("Part two: {}", part_two);
}

fn calculate_part_one(path: &str) -> u32 {
    let sum_game_ids: u32 = read_to_string(path)
        .unwrap()
        .lines()
        .into_iter()
        .map(|line| get_game(line))
        .filter(|game| game.can_play_part_one())
        .map(|game| game.id)
        .sum();

    sum_game_ids
}

fn calculate_part_two(path: &str) -> u32 {
    let sum_cube_set_powers: u32 = read_to_string(path)
        .unwrap()
        .lines()
        .into_iter()
        .map(|line| get_game(line))
        .map(|game| Game2::new(game))
        .map(|game| game.min_cube_set.power())
        .sum();

    sum_cube_set_powers
}

fn get_game(input: &str) -> Game {
    let parts: Vec<&str> = input.split(":").collect();

    Game {
        id: get_game_id(parts[0]),
        cube_sets: get_cube_sets(parts[1]),
    }
}

fn get_game_id(input: &str) -> u32 {
    let str = &input[5..];
    let number = str.parse().unwrap();
    number
}

fn get_cube_sets(input: &str) -> Vec<Cubeset> {
    let cube_sets: Vec<Cubeset> = input
        .trim()
        .split(";")
        .into_iter()
        .map(|set| get_cube_set(set))
        .collect();

    cube_sets
}

#[cfg(test)]
mod tests {
    use super::*;

    #[test]
    fn get_one_cube_set() {
        let cube_set = get_cube_set("3 blue");
        let expected = Cubeset::new(0, 0, 3);

        assert_eq!(cube_set, expected);
    }

    #[test]
    fn get_three_cube_sets() {
        let cube_set = get_cube_set("5 blue, 4 red, 13 green");
        let expected = Cubeset::new(4, 13, 5);

        assert_eq!(cube_set, expected);
    }
}

fn get_cube_set(input: &str) -> Cubeset {
    let mut red: u32 = 0;
    let mut green: u32 = 0;
    let mut blue: u32 = 0;

    let sets: Vec<&str> = input.trim().split(",").collect();

    for set in sets {
        let parts: Vec<&str> = set.trim().split(" ").collect();
        let amount = get_amount(parts[0].trim());
        let color = parts[1].trim();

        match color {
            "red" => red = amount,
            "green" => green = amount,
            "blue" => blue = amount,
            _ => panic!(),
        }
    }

    let cube_set = Cubeset::new(red, green, blue);

    cube_set
}

fn get_amount(input: &str) -> u32 {
    let number = input.parse().unwrap();
    number
}
