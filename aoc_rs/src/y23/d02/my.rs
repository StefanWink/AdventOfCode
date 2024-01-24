pub struct Game {
    pub id: u32,
    pub cube_sets: Vec<Cubeset>,
}

pub struct Cubeset {
    pub amount_red: u32,
    pub amount_green: u32,
    pub amount_blue: u32,
}
