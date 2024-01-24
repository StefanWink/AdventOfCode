mod my;

use std::fs::read_to_string;

fn main() {
    let input_path = "/temp/input.txt";

    let part_one = calculate_part_one(input_path);
    println!("Part one: {}", part_one);
}

fn calculate_part_one(path: &str) -> u32 {
    let sum_game_ids: u32 = read_to_string(path)
        .unwrap()
        .lines()
        .into_iter()
        .map(|line| get_game(line))
        .filter(|game| can_play_game(game))
        .map(|game| game.id)
        .sum();

    sum_game_ids
}

fn get_game(input: &str) -> my::Game {
    let parts: Vec<&str> = input.split(":").collect();

    my::Game {
        id: get_game_id(parts[0]),
        cube_sets: get_cube_sets(parts[1]),
    }
}

fn get_game_id(input: &str) -> u32 {
    let str = &input[5..];
    let number = str.parse().unwrap();
    number
}

fn get_cube_sets(input: &str) -> Vec<my::Cubeset> {
    let cube_sets: Vec<my::Cubeset> = input
        .trim()
        .split(";")
        .into_iter()
        .map(|set| get_cube_set(set))
        .collect();

    cube_sets
}

#[test]
fn get_one_cube_set() {
    let cube_set = get_cube_set("3 blue");
    assert_eq!(cube_set.amount_red, 0);
    assert_eq!(cube_set.amount_green, 0);
    assert_eq!(cube_set.amount_blue, 3);
}

#[test]
fn get_three_cube_sets() {
    let cube_set = get_cube_set("5 blue, 4 red, 13 green");
    assert_eq!(cube_set.amount_red, 4);
    assert_eq!(cube_set.amount_green, 13);
    assert_eq!(cube_set.amount_blue, 5);
}

fn get_cube_set(input: &str) -> my::Cubeset {
    let mut cube_set = my::Cubeset {
        amount_red: 0,
        amount_green: 0,
        amount_blue: 0,
    };

    let sets: Vec<&str> = input.trim().split(",").collect();

    for set in sets {
        let parts: Vec<&str> = set.trim().split(" ").collect();
        let amount = get_amount(parts[0].trim());
        let color = parts[1].trim();

        match color {
            "red" => cube_set.amount_red = amount,
            "green" => cube_set.amount_green = amount,
            "blue" => cube_set.amount_blue = amount,
            _ => panic!(),
        }
    }

    cube_set
}

fn get_amount(input: &str) -> u32 {
    let number = input.parse().unwrap();
    number
}

fn can_play_game(game: &my::Game) -> bool {
    let can_play_game = game
        .cube_sets
        .iter()
        .all(|cube_set| can_play_cube_set(&cube_set));

    can_play_game
}

fn can_play_cube_set(cube_set: &my::Cubeset) -> bool {
    cube_set.amount_red <= 12 && cube_set.amount_green <= 13 && cube_set.amount_blue <= 14
}
