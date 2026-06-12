# Factory Production & Logistics Simulation System

## Description

This project is a concurrent multi-stage pipeline simulation of a factory production and logistics line implemented in C#.

The system demonstrates:

* Asynchronous processing
* Backpressure handling
* Safe concurrent access to shared resources
* Pipeline architecture
* Graceful shutdown coordination

When the application starts, it initializes a set of machines (producers), an input buffer queue, a quality control checker, an intermediate warehouse storage array, and a transport loader that delivers items to their final destination.

The simulation runs seamlessly across independent threads and implements an automated graceful shutdown once production plans are met and all pipeline buffers are fully cleared.

---

# System Flow

```text
Machines
    ↓
OrderLine
    ↓
QualityChecker
    ↓
Storage
    ↓
TransportSystem
    ↓
Stock
```

---

# Project Structure

```text
FactorySimulation
│
├── README.md
│
├── FactorySystem.Domain
│   ├── Item.cs
│   └── ItemType.cs
│
└── Factory.System.Components
    ├── Program.cs
    ├── Simulation.cs
    ├── OrderLine.cs
    ├── Machines.cs
    ├── QualityChecker.cs
    ├── Storage.cs
    ├── TransportSystem.cs
    └── Stock.cs
```

---

# Projects & Namespaces

## FactorySystem.Domain

This namespace defines the core data structures traveling through the factory pipeline.

### ItemType

Enum representing categories of produced items:

* Type A
* Type B
* Type C

### Item

Domain entity encapsulating:

* Immutable unique ID
* Assigned category (ItemType)

Example:

```csharp
public class Item
{
    public int Id { get; }
    public ItemType Type { get; }

    public Item(int id, ItemType type)
    {
        Id = id;
        Type = type;
    }
}
```

---

## Factory.System.Components

This namespace contains the operational components responsible for processing items through the production pipeline.

### Program

Application entry point.

Responsibilities:

* Initialize simulation
* Start execution
* Display output

---

### Simulation

Central coordinator of the system.

Responsibilities:

* Build all components
* Configure parameters
* Start worker threads
* Monitor completion conditions
* Perform graceful shutdown

---

### OrderLine

Thread-safe bounded input buffer.

Responsibilities:

* Store incoming items
* Enforce queue capacity
* Support concurrent access
* Apply backpressure policies

---

### Machines

Producer components.

Responsibilities:

* Produce items on independent schedules
* Generate unique item IDs
* Assign item categories
* Push items into OrderLine

---

### QualityChecker

Processor component.

Responsibilities:

* Consume items from OrderLine
* Simulate inspection delay
* Perform randomized quality decisions
* Forward passed items to Storage
* Reject failed items

---

### Storage

Intermediate warehouse.

Responsibilities:

* Hold successfully checked items
* Group items by category
* Provide thread-safe access
* Supply transport requests

---

### TransportSystem

Scheduled logistics component.

Responsibilities:

* Arrive periodically
* Select item groups by category
* Collect warehouse batches
* Deliver items into Stock

---

### Stock

Final repository.

Responsibilities:

* Permanently store delivered items
* Maintain category grouping
* Preserve final inventory

---

# Features

## Continuous Asynchronous Execution

Uses dedicated `System.Threading.Thread` instances to simulate parallel factory roles.

Features:

* Independent machine execution
* Concurrent processing
* Parallel logistics flow

---

## Fixed-Capacity Backpressure Control

The incoming OrderLine enforces strict capacity limits.

Overflow handling:

* Reject incoming items
* Delay production
* Protect downstream stages

Example:

```text
Machine B: OrderLine full! Item 208 dropped.
```

---

## Variable Delay Emulation

Processing stages use randomized delays.

Examples:

* Production latency
* Inspection time
* Transport intervals

This creates realistic production variability.

---

## Custom Array Partitioning

Storage and Stock use dedicated categorized arrays rather than common generic collections.

Benefits:

* Explicit memory layout
* Controlled capacity
* Educational implementation of custom storage structures

---

## Categorized Transport Pickup

TransportSystem periodically:

1. Selects a target category.
2. Collects matching items.
3. Transfers them into Stock.

Transport constraints:

* Arrival interval
* Capacity limit
* Category selection

---

## Graceful Lifecycle Shutdown

The simulation automatically terminates when:

* All machines finish production.
* OrderLine becomes empty.
* QualityChecker completes processing.
* Storage is fully transported.
* No remaining work exists.

This prevents:

* Deadlocks
* Infinite loops
* Forced termination

---

# Controls & Output Parameters

The simulation operates using predefined configuration values.

---

## Production Log

```text
Machine A: Produced Item 101 (1/35)
```

---

## Backpressure Log

```text
Machine B: OrderLine full! Item 208 dropped.
```

---

## Quality Decision Log

```text
Checker: Item 101 PASSED.
```

or

```text
Checker: Item 102 FAILED. Dropped.
```

---

## Transport Log

```text
Transport: Moved 6 items of Type A to Stock.
```

---

# Technical Requirements

The project demonstrates:

* Object-Oriented Programming
* Encapsulation
* Composition
* Abstraction
* Pipeline Architecture
* Thread Safety
* Concurrent Processing
* Backpressure Management
* Graceful Shutdown Patterns

Additional constraints:

* No use of `List<T>` for custom storage structures.
* No use of `Dictionary<TKey, TValue>` for warehouse grouping.
* Uses dedicated arrays for categorized storage.
* Responsibilities are isolated according to the Single Responsibility Principle (SRP).

---

# Main Classes

## OrderLine

### Responsibilities

* Limit queue capacity
* Provide thread-safe insertion
* Provide thread-safe extraction
* Expose queue state information

---

## Machines

### Responsibilities

* Maintain production schedules
* Generate unique items
* Assign categories
* Track production targets

---

## QualityChecker

### Responsibilities

* Consume queued items
* Simulate inspection delays
* Apply quality rules
* Route passed items
* Reject failed items

---

## Storage

### Responsibilities

* Group inventory by category
* Store checked items
* Provide synchronized access
* Supply transport operations

---

## TransportSystem

### Responsibilities

* Execute transport schedules
* Collect category-specific batches
* Transfer inventory safely
* Deliver items to Stock

---

## Stock

### Responsibilities

* Preserve finalized inventory
* Organize items by category
* Maintain permanent records

---

# OOP Concepts Demonstrated

The project showcases the following software engineering concepts:

## Encapsulation

Internal state is hidden using private fields and controlled methods.

Example:

```csharp
private readonly int _capacity;
```

---

## Composition

Large systems are built from smaller specialized components.

Example:

```text
Simulation
 ├── Machines
 ├── OrderLine
 ├── QualityChecker
 ├── Storage
 ├── TransportSystem
 └── Stock
```

---

## Abstraction

Components expose simple public operations while hiding implementation details.

Example:

```csharp
machine.Work();
checker.Work();
transport.Work();
```

---

## Single Responsibility Principle (SRP)

Each class has exactly one primary responsibility.

| Class           | Responsibility     |
| --------------- | ------------------ |
| Machine         | Produce items      |
| OrderLine       | Buffer items       |
| QualityChecker  | Inspect items      |
| Storage         | Hold items         |
| TransportSystem | Move items         |
| Stock           | Preserve inventory |

---

# Summary

Factory Production & Logistics Simulation System is a multithreaded production pipeline demonstrating real-world manufacturing flow through object-oriented design principles, concurrent processing, bounded buffering, categorized warehousing, scheduled logistics, and graceful lifecycle management.
