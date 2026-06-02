# C# Homework — TechGen 💻

Hi there! 👋

Welcome to my C# Homework repository! 🚀

Here, you'll find my homework assignments from the **TechGen C# / .NET course**.

This repository is a place where I store my practice projects, improve my programming skills, and track my progress while learning C#.

The main goal of this repository is to keep all my homework organized and easy to review.

Each homework folder contains tasks, solutions, and related files created during the course.

As I continue learning, this repository will grow with new homework assignments, code examples, and improvements. 🧠✨

Happy coding! 🎯
# Homework 3 — Algorithms and Recursion

This folder contains solutions for Homework 3 from the TechGen C# / .NET course.

The homework includes two tasks focused on:

* stack-based validation of bracket sequences;
* recursive and iterative flood fill algorithms.

---

# Task 1 — Valid Parentheses

The goal of this task is to determine whether a string containing only brackets is correctly balanced.

Supported bracket types:

* ()
* {}
* []

A valid sequence must satisfy two conditions:

1. Every opening bracket must have a matching closing bracket.
2. Brackets must be closed in the correct order.

Examples:

```text
"()"         -> true
"([])"       -> true
"([)]"       -> false
"{[()()]}"   -> true
"((("        -> false
```

## Requirements

Implement a method:

```csharp
bool IsValid(string input);
```

The method should return:

* `true` if the bracket sequence is valid;
* `false` otherwise.

## Suggested Approach

Use a stack:

* Push opening brackets onto the stack.
* When a closing bracket is encountered:

  * check whether the stack is empty;
  * verify that the top element matches the corresponding opening bracket.
* At the end, the stack must be empty.

## Complexity

Time Complexity:

```text
O(n)
```

Space Complexity:

```text
O(n)
```

---

# Task 2 — Flood Fill

The goal of this task is to implement the classic Flood Fill algorithm.

Given:

* a two-dimensional array (`n × m`);
* a starting coordinate `(x, y)`;
* a value `v`;

the algorithm should replace all connected cells that belong to the same region as the starting cell with the new value `v`.

A cell is considered connected only through:

* Up
* Down
* Left
* Right

Diagonal connections are not allowed.

Example:

Initial region:

```text
1 5 5
1 5 5
1 5 5
```

Starting point:

```text
(1,1)
```

New value:

```text
9
```

Result:

```text
1 9 9
1 9 9
1 9 9
```

## Requirements

Implement the solution in two different ways.

### Recursive Version

```csharp
void FloodFillRecursive(int[,] matrix, int x, int y, int value);
```

The algorithm should:

1. Check boundaries.
2. Verify that the current cell belongs to the target region.
3. Replace the value.
4. Recursively process neighboring cells.

### Iterative Version

```csharp
void FloodFillIterative(int[,] matrix, int x, int y, int value);
```

The algorithm should:

1. Use an explicit stack or queue.
2. Process neighboring cells until the entire connected region is filled.

## Complexity

Time Complexity:

```text
O(n × m)
```

Space Complexity:

```text
O(n × m)
```

depending on the size of the connected region.

---

# Project Structure

```text
Homework3/
├── Task1/
│   ├── Program.cs
│   └── Task1.csproj
├── Task2/
│   ├── Program.cs
│   └── Task2.csproj
├── Homework3.sln
└── README.md
```

---

# Technologies Used

* C#
* .NET
* JetBrains Rider

---

# Notes

This homework was completed as part of the TechGen C# / .NET course.

The goal of the assignment was to practice:

* stack-based algorithms;
* recursion;
* graph and matrix traversal techniques;
* iterative alternatives to recursive solutions;
* algorithmic problem solving.
