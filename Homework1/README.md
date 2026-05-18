\# Homework 1 — C# Basics



This homework contains three C# tasks focused on low-level number representation, custom arithmetic for very large numbers, and implementing a simple dynamic list.



\---



\## Task 1 — IEEE 754 Format for 32-bit Float



The goal of this task is to manually convert a `float` value to its 32-bit IEEE 754 binary representation and convert it back from binary to `float`.



A 32-bit float consists of three parts:



| Part | Size | Description |

|---|---:|---|

| Sign | 1 bit | `0` means positive, `1` means negative |

| Exponent | 8 bits | Stored with a bias of `127` |

| Mantissa / Fraction | 23 bits | Stores the fractional part of the normalized number |



If the real exponent is `E`, the stored exponent value is:



```text

E + 127



Example:



12.375 -> 01000001010001100000000000000000



Pretty format example:



0 | 10000010 | 10001100000000000000000

Requirements



Implement two methods:



string FloatToBinary(float number, bool pretty = false)

float BinaryToFloat(string bits)



The first method should return a 32-character binary string.



The second method should accept a 32-bit binary string and return the corresponding float value.



The input string may contain spaces or | separators.



Restrictions



The following are not allowed:



BitConverter

Convert.\*

Bonus



Implement the same functionality for double.



A double uses 64 bits:



1 sign bit / 11 exponent bits / 52 mantissa bits



The exponent bias for double is 1023.



Task 2 — Very Large Numbers Beyond Overflow



The goal of this task is to demonstrate integer overflow and then implement arithmetic for numbers that are too large for built-in integer types.



Step 1 — Demonstrate the Problem



In Main, print the following examples:



int a = int.MaxValue;

Console.WriteLine(unchecked(a + 1)); // -2147483648



long b = long.MaxValue;

Console.WriteLine(unchecked(b + 1)); // -9223372036854775808



These examples show that when a numeric type exceeds its maximum value, it overflows.



Step 2 — Custom Arithmetic



A number should be stored as a string of decimal digits.



Example:



"99999999999999999999"



Implement three methods:



string Add(string a, string b)

string Subtract(string a, string b)

string Multiply(string a, string b)

Method Requirements

Add



Adds two non-negative numbers.



Example:



Add("9999", "1") -> "10000"

Subtract



Subtracts two non-negative numbers, assuming a >= b.



Example:



Subtract("10000", "1") -> "9999"

Multiply



Multiplies two non-negative numbers.



Example:



Multiply("123", "456") -> "56088"

Restrictions



Use only int.



The following are not allowed:



long

ulong

decimal

BigInteger

Convert.\*

Input Validation



If the input string is:



null

empty

contains non-digit characters



then it should be treated as:



0

Bonus



Support negative numbers.



Task 3 — MyList



The goal of this task is to implement a simple custom list for integers.



Required Class

class MyList

{

&#x20;   void Add(int item);

&#x20;   void AddRange(int\[] items);

&#x20;   bool Remove(int item);

&#x20;   bool TryGet(int index, out int value);

}

Requirements

The internal storage must be a single int\[] \_items array.

ArrayList is not allowed.

Initial capacity must be 4.

When the array is full, its capacity must be doubled.

Remove should remove the first matching element.

After removing an item, all following elements should be shifted one position to the left.

Count should be decreased after successful removal.

TryGet(index, out int value) should return false if the index is outside the \[0, Count) range.

AddRange(null) should be ignored.

Bonus



Add the following members:



int IndexOf(int item);

bool Contains(int item);

void Clear();

int this\[int index] { get; set; }

Project Structure

Homework1/

├── Task1/

│   └── Program.cs

├── Task2/

│   └── Program.cs

├── Task3/

│   └── Program.cs

├── Homework1.sln

└── README.md

Technologies Used

C#

.NET

JetBrains Rider

Notes



This homework was implemented without using restricted built-in helpers such as BitConverter, Convert.\*, BigInteger, or built-in dynamic collections where they were not allowed by the task requirements.

