# 📦 Generics & Generic Programming in C#

## 📖 Description

This project contains five independent exercises focused on mastering **Generics** and related C# language features.

The exercises gradually introduce generic classes, generic methods, delegates, generic constraints, comparer-based ordering, and retry execution patterns while avoiding LINQ and relying on manual implementations.

---

# 📂 Exercises

## 🔹 Ex01 — Generic Pair

Implemented a generic `Pair<TFirst, TSecond>` type capable of storing two values of different types.

### Features

- Generic class with two type parameters
- Immutable properties
- `SwapSides()` method returning a new pair with reversed generic types

Example

```text
(7, "seven")

↓

("seven", 7)
```

---

## 🔹 Ex02 — Filter & Project

Implemented generic filtering and projection methods without using LINQ.

### Filter<T>

Keeps only elements matching a specified condition.

Uses:

- Generic Methods
- Predicate<T>
- Loops
- Arrays

Example

```text
Input:
1,2,3,4,5

↓

Filter:
n % 2 == 0

↓

2,4
```

---

### Project<TInput, TResult>

Transforms each element into another type.

Uses:

- Generic Methods
- Func<TInput, TResult>

Example

```text
2

↓

"N2"
```

---

## 🔹 Ex03 — Generic Constraints

Implemented a generic factory method using constraints.

### Features

- Generic Constraints
- Interface Constraint
- new() Constraint
- Automatic object initialization

---

## 🔹 Ex04 — Top-N Buffer

Implemented a generic buffer that stores only the best **N** elements.

### Features

- Generic Class
- IComparer<T>
- Bubble Sort
- Snapshot generation
- Capacity management

Example

```text
Input:
5,1,9,3,7,2

Capacity = 3

↓

9,7,5
```

---

## 🔹 Ex05 — Retry Executor

Implemented a retry execution mechanism for unreliable operations.

### Features

- Generic Result<T>
- Func<T>
- Exception Handling
- Retry Logic
- Configurable Retry Predicate

Example

```text
Attempt 1 ❌

Attempt 2 ❌

Attempt 3 ✅

↓

Success = true
Attempts = 3
```

---

# 🧠 Concepts Practiced

## C# Fundamentals

- Classes
- Arrays
- Constructors
- Properties
- Exception Handling
- Loops

---

## Generics

- Generic Classes
- Generic Methods
- Multiple Generic Parameters
- Generic Constraints (`where`)
- Generic Return Types

---

## Delegates

- Predicate<T>
- Func<T>
- Func<TInput, TResult>

---

## Interfaces

- IComparable<T>
- IComparer<T>

---

## Object-Oriented Programming

- Encapsulation
- Composition
- Abstraction
- Separation of Responsibilities

---

## Algorithms

- Filtering
- Projection
- Bubble Sort
- Retry Logic
- Top-N Selection

---

# 🛠 Technologies

- C#
- .NET
- Console Application

---

# 🎯 Learning Goals

This homework was designed to strengthen understanding of:

- Generic Programming
- Delegate-based algorithms
- Constraints
- Comparers
- Reusable generic components
- Clean object-oriented design

while implementing everything manually without relying on LINQ.
