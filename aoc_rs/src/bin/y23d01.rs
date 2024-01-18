use std::fs::read_to_string;

fn main() {
    let input_path = "/temp/input.txt";

    let part_one = calculate_part_one(input_path);
    println!("Part one: {}", part_one);

    let part_two = calculate_part_two(input_path);
    println!("Part two: {}", part_two);
}

fn calculate_part_one(path: &str) -> u32 {
    let sum_calibration_values: u32 = read_to_string(path).unwrap()
        .lines().into_iter()
        .map(|line| get_calibration_value(line))
        .sum();

    sum_calibration_values
}

fn calculate_part_two(path: &str) -> u32 {
    let numbers = get_numbers();

    let sum_calibration_values: u32 = read_to_string(path).unwrap()
        .lines().into_iter()
        .map(|line| replace_numbers(line, &numbers))
        .map(|line| get_calibration_value(&line))
        .sum();

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

struct Number {
    text_length: usize,
    number_text: String,
    number_value: char
}

fn get_numbers() -> Vec<Number> {
    let one = Number {
        number_text: String::from("one"),
        text_length: 3,
        number_value: '1'
    };

    let two = Number {
        number_text: String::from("two"),
        text_length: 3,
        number_value: '2'
    };

    let three = Number {
        number_text: String::from("three"),
        text_length: 5,
        number_value: '3'
    };

    let four = Number {
        number_text: String::from("four"),
        text_length: 4,
        number_value: '4'
    };

    let five = Number {
        number_text: String::from("five"),
        text_length: 4,
        number_value: '5'
    };

    let six = Number {
        number_text: String::from("six"),
        text_length: 3,
        number_value: '6'
    };

    let seven = Number {
        number_text: String::from("seven"),
        text_length: 5,
        number_value: '7'
    };

    let eight = Number {
        number_text: String::from("eight"),
        text_length: 5,
        number_value: '8'
    };

    let nine = Number {
        number_text: String::from("nine"),
        text_length: 4,
        number_value: '9'
    };

    let numbers = vec![one, two, three, four, five, six, seven, eight, nine];

    numbers
}

fn replace_numbers(input: &str, numbers: &Vec<Number>) -> String {
    let mut chars: Vec<char> = input.chars().collect();

    for i in 0..chars.len() {
        for number in numbers {
            let end = i + number.text_length;

            if end <= input.len() {
                let slice = &input[i..end];
                if number.number_text == slice {
                    chars[i] = number.number_value;
                }
            }
        }
    }

    let result = chars.into_iter().collect();

    result
}

macro_rules! replace_numbers_tests {
    ($($name:ident: $value:expr,)*) => {
    $(
        #[test]
        fn $name() {
            let (input, expected) = $value;
            let numbers = get_numbers();
            let actual = replace_numbers(input, &numbers);
            let s2 = get_calibration_value(&actual);
            assert_eq!(expected, s2);
        }
    )*
    }
}

replace_numbers_tests! {
    numbers_start_end: ("two1nine", 29),
    left_to_right: ("eightwothree", 83),
    left_to_right_2: ("abcone2threexyz", 13),
    left_to_right_3: ("xtwone3four", 24),
    left_to_right_4: ("4nineeightseven2", 42),
    left_to_right_5: ("zoneight234", 14),
    left_to_right_6: ("7pqrstsixteen", 76),
    left_to_right_7: ("4568", 48),
    left_to_right_8: ("z2", 22),
    anywhere: ("oneight", 18),
}
