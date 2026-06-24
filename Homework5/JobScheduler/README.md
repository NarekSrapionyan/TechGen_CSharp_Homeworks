# ⚙️ Event-Driven Job Scheduler

## Description

A console-based event-driven job scheduler implemented in C#.

The application manages a collection of jobs, executes them through delegated executors, publishes lifecycle events, and tracks execution statistics using independent subscriber services.

The project demonstrates how delegates, events, custom collections, and iterators can be combined to build a simple scheduling system.

---

## Workflow

```text
JobQueue
    ↓
Scheduler
    ↓
Executor
    ↓
Events
    ↓
Subscribers
 ├── MonitoringService
 ├── LoggerService
 └── StatisticsService
```

---

## Features

### Job Lifecycle

Each job transitions through the following states:

```text
Pending
   ↓
Running
   ↓
Completed
```

or

```text
Pending
   ↓
Running
   ↓
Failed
```

---

### Event System

The scheduler publishes events whenever a job changes state:

* JobStarted
* JobCompleted
* JobFailed

Multiple services subscribe to these events independently.

---

### Custom Queue

Implemented without using `List<Job>`.

Features:

* Internal array storage
* Dynamic resizing
* Manual capacity management
* Custom enumerator support

---

### Custom Enumerator

Implements:

* IEnumerable
* IEnumerator

The iterator processes only jobs whose status is `Pending`.

---

### Logging & Monitoring

#### MonitoringService

Displays:

* Event name
* Job identifier
* Current status

#### LoggerService

Displays:

* Timestamp
* Event information
* Error details

#### StatisticsService

Tracks:

* Started jobs
* Completed jobs
* Failed jobs

---

## Executors

### FastExecutor

Executes lightweight jobs.

### SafeExecutor

Handles internal exceptions and rethrows wrapped errors.

### RetryExecutor

Simulates retry behavior with multiple execution attempts.

---

## Concepts Demonstrated

* Delegates
* Events
* EventHandler<T>
* Dynamic Arrays
* IEnumerable
* IEnumerator
* Exception Handling
* Event-Driven Architecture
* Single Responsibility Principle (SRP)

---

## Example Output

```text
[Monitor] JobStarted | Job 1 is now Running

[14:20:55] [Log] JobCompleted - Job 1

[Monitor] JobFailed | Job 2 is now Failed

=== Final Statistics ===
Started: 4
Completed: 2
Failed: 2
```
