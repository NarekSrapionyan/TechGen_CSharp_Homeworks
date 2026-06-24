# 🛡️ Mini Rule Engine

## Description

A configurable validation engine implemented in C#.

The application validates different entity types using dynamically registered business rules and supports two validation strategies:

* Fail-Fast Validation
* Collect-All Validation

The goal of the project is to demonstrate how validation frameworks can be built using delegates, interfaces, and custom exceptions.

---

## Architecture

```text
Entity
   ↓
Rule Engine
   ↓
Applicable Rules
   ↓
Validation Result
```

---

## Supported Entities

### UserEntity

Properties:

* Id
* Age
* Email

### OrderEntity

Properties:

* Id
* TotalAmount

---

## Validation Modes

### Fail-Fast

Stops immediately when the first validation error occurs.

Example:

```text
Rule 1 ✓
Rule 2 ✗
STOP
```

---

### Collect-All

Executes every applicable rule and returns all validation errors together.

Example:

```text
Rule 1 ✗
Rule 2 ✗
Rule 3 ✓
Rule 4 ✗
```

---

## Rule System

Each rule contains:

* Rule Name
* Target Entity Type
* Validation Delegate

Example:

```csharp
new Rule(
    "CheckAdult",
    "User",
    entity => { ... });
```

---

## Custom Exceptions

### RuleViolationException

Represents a single validation failure.

Contains:

* Rule Name
* Error Message

---

### EntityValidationException

Represents all validation failures found for a specific entity.

Contains:

* Entity
* Collection of Violations

---

## Extension Methods

The project includes extension methods for cleaner validation syntax:

```csharp
entity.ValidateFailFast(engine);

entity.ValidateCollectAll(engine);
```

---

## Concepts Demonstrated

* Delegates
* Interfaces
* Extension Methods
* Custom Exceptions
* Dynamic Arrays
* Rule Engine Pattern
* Validation Pipelines
* Fail-Fast Pattern
* Collect-All Pattern
* Separation of Concerns

---

## Example Output

```text
=== Fail-Fast Mode ===

User #2 FAILED fast on rule [CheckAdult]:
User must be 18 or older.

=== Collect-All Mode ===

User #2 has 2 validation error(s).

-> [CheckAdult]
-> [CheckEmail]
```

---

## Learning Goals

This project focuses on building a reusable validation framework while practicing:

* Interface-based design
* Delegate-driven execution
* Error aggregation
* Custom exception hierarchies
* Extensible rule registration
