# Generic Specification Pattern

A C# implementation of the **Specification Pattern** using **Generics**, custom collection extensions, and reusable business rules.

The project demonstrates how to separate business rules from application logic and compose them into complex conditions using reusable specifications.

---

# Features

- Generic Specification Pattern
- Predicate-based specifications
- AND / OR / NOT composition
- Specification Factory
- Extension Methods
- Custom IEnumerable extensions (without LINQ)
- Generic reusable specifications
- Multiple business domain examples
- Clean and extensible architecture

---

# Project Structure

```text
Task1
│
├── Core Engine
│   ├── ISpecification.cs
│   ├── PredicateSpecification.cs
│   ├── AndSpecification.cs
│   ├── OrSpecification.cs
│   ├── NotSpecification.cs
│   ├── Specification.cs
│   ├── SpecificationExtensions.cs
│   └── EnumerableExtensions.cs
│
├── Generic Specifications
│   ├── PropertyInRangeSpecification.cs
│   ├── PropertyContainsSpecification.cs
│   ├── HasAnySpecification.cs
│   └── HasAllSpecification.cs
│
├── Domain Models
│   ├── Product.cs
│   ├── LoanApplication.cs
│   ├── Candidate.cs
│   └── ShipmentOrder.cs
│
├── Product Specifications
│
├── Loan Specifications
│
├── Candidate Specifications
│
├── Shipment Specifications
│
├── SpecificationDemo.cs
└── Program.cs
```

---

# Core Engine

The project is built around the `ISpecification<T>` interface.

```csharp
public interface ISpecification<T>
{
    bool IsSatisfiedBy(T candidate);
}
```

Every business rule implements this interface.

Specifications can be freely combined using logical operators.

---

# Specification Composition

Specifications support fluent composition.

```csharp
var promo =
    inStock
        .And(electronics)
        .And(affordable);
```

Supported operators

- AND
- OR
- NOT

Example

```csharp
var backup =
    hasDotNet
        .And(remote)
        .And(shortlist.Not());
```

---

# Generic Specifications

The project also contains reusable specifications that work with any type.

### PropertyInRangeSpecification

Checks whether a property value falls inside a specified range.

Example:

```csharp
new PropertyInRangeSpecification<Product, decimal>(
    p => p.Price,
    0,
    100);
```

---

### PropertyContainsSpecification

Checks whether a string property contains specified text.

Example:

```csharp
new PropertyContainsSpecification<Product>(
    p => p.Name,
    "lap");
```

---

### HasAnySpecification

Checks whether at least one element inside a collection satisfies another specification.

Example:

```csharp
new HasAnySpecification<Candidate, string>(
    c => c.Skills,
    hasDotNetSkill);
```

---

### HasAllSpecification

Checks whether every element satisfies another specification.

Empty collections return `false`.

---

# Custom IEnumerable Extensions

The project reimplements several LINQ methods manually.

Implemented without using `System.Linq`.

- Where
- Any
- All
- FirstOrDefault
- Count

Example

```csharp
products.Where(promoSpecification);

products.FirstOrDefault(premiumSpecification);

products.Count(electronicsSpecification);
```

---

# Demonstrations

The project includes four independent business scenarios.

## Product Filtering

Rules

- In stock
- Out of stock
- Category
- Minimum price
- Maximum price

Example

```csharp
var promo =
    inStock
        .And(electronics)
        .And(affordable);
```

---

## Loan Approval

Rules

- Credit score
- Income
- Employment
- Collateral
- Bankruptcy

Possible results

- APPROVED
- MANUAL REVIEW
- REJECTED

---

## Job Candidate Screening

Rules

- Experience
- Salary expectation
- Remote availability
- Required skills

Used to build

- Shortlist
- Backup pool

---

## Shipment Routing

Rules

- Weight
- Fragile
- Express
- Shipping zone

Routes

- AIR-EXPRESS
- FREIGHT
- STANDARD
- CUSTOM

---

# Example Output

```text
-- E-commerce product filtering

Promo eligible:
- Keyboard ($80, stock=3)
- Monitor ($95, stock=8)

Restock candidates:
- Mouse (stock=0)

Premium electronics (first match):
- Laptop ($1200)

Affordable electronics count: 2
```

---

# Technologies

- C#
- .NET
- Generics
- Interfaces
- Extension Methods
- IEnumerable
- Delegates
- Design Patterns

---

# Design Patterns

This project demonstrates

- Specification Pattern
- Factory Pattern
- Composition

---

# Learning Objectives

This project demonstrates practical usage of

- Generic Interfaces
- Generic Classes
- Generic Constraints
- Delegates
- Predicate-based programming
- Extension Methods
- Collection processing
- Composition over inheritance
- Separation of business rules
- Reusable architecture

---

# Notes

- LINQ is intentionally **not used**.
- All collection operations are implemented manually.
- Specifications can be combined into larger business rules.
- Generic specifications are reusable across different domain models.

---

# Author

Created as part of a C# learning project focused on advanced Generics, reusable business rules, and the Specification design pattern.