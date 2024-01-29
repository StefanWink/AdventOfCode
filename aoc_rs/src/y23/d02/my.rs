use std::cmp::max;

pub struct Game {
    pub id: u32,
    pub cube_sets: Vec<Cubeset>,
}

impl Game {
    pub fn can_play_part_one(&self) -> bool {
        let can_play_part_one = self
            .cube_sets
            .iter()
            .all(|cube_set| cube_set.can_play_part_one());

        can_play_part_one
    }
}

pub struct Game2 {
    pub id: u32,
    pub min_cube_set: Cubeset,
}

impl Game2 {
    pub fn new(game: Game) -> Self {
        let min_cube_set = game
            .cube_sets
            .iter()
            .fold(Cubeset::new(0, 0, 0), |acc, num| {
                Cubeset::new(
                    max(acc.red, num.red),
                    max(acc.green, num.green),
                    max(acc.blue, num.blue),
                )
            });

        Self {
            id: game.id,
            min_cube_set,
        }
    }
}

#[derive(Debug, Eq, PartialEq)]
pub struct Cubeset {
    pub red: u32,
    pub green: u32,
    pub blue: u32,
}

impl Cubeset {
    pub fn new(red: u32, green: u32, blue: u32) -> Self {
        Self { red, green, blue }
    }

    pub fn can_play_part_one(&self) -> bool {
        self.red <= 12 && self.green <= 13 && self.blue <= 14
    }

    pub fn power(&self) -> u32 {
        self.red * self.green * self.blue
    }
}
