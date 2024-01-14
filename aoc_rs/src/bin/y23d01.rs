use std::fs::read_to_string;

fn main() {
    let file_path = "/temp/input.txt";

    let part_one = calculate_part_one(file_path);
    println!("Part one: {}", part_one);
}

fn calculate_part_one(path: &str) -> u32 {
    let sum_calibration_values: u32 = read_to_string(path).unwrap()
        .lines().into_iter()
        .map(|line| get_calibration_value(line))
        .fold(0, |sum, val| sum + val);

    sum_calibration_values
}

fn get_calibration_value(input: &str) -> u32 {
    let chars: Vec<char> = input.chars()
        .into_iter()
        .filter(|c| c.is_numeric())
        .collect();

    let first_digit = chars.first().unwrap().to_digit(10).unwrap();
    let last_digit = chars.last().unwrap().to_digit(10).unwrap();

    first_digit * 10 + last_digit
}

macro_rules! calibration_value_tests {
    ($($name:ident: $value:expr,)*) => {
    $(
        #[test]
        fn $name() {
            let (input, expected) = $value;
            assert_eq!(expected, get_calibration_value(input));
        }
    )*
    }
}

calibration_value_tests! {
    digits_start_end: ("1abc2", 12),
    digits_middle: ("pqr3stu8vwx", 38),
    three_digits: ("a1b2c3d4e5f", 15),
    one_digit: ("treb7uchet", 77),
}
