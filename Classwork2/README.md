# 🔧 Price Engine Reflection Wrapper

## 📌 Description

This project demonstrates how **C# Reflection** can be used to access and execute private methods from a compiled .NET assembly (`ACA.PriceEngine.dll`) without modifying its source code.

The provided DLL contains a `PriceEngine` class responsible for calculating the payable amount for a shopping basket. Since the source code is unavailable, the original calculation cannot be changed directly.

Instead, this project rebuilds the calculation pipeline by invoking the private methods in a custom order using Reflection.

---

## 🎯 Objectives

- Reference an external `.dll` library.
- Access private methods using Reflection.
- Execute the calculation pipeline manually.
- Compare the original calculation with the corrected one.
- Demonstrate the use of Reflection on compiled assemblies.

---

## 🏗 Solution Structure

```text
Classwork2
│
├── PriceEngineWrapper
│   └── CorrectedPriceCalculator.cs
│
├── PriceEngineConsole
│   └── Program.cs
│
└── Lib
    └── ACA.PriceEngine.dll
```

### PriceEngineWrapper

Contains the Reflection logic.

Responsibilities:

- Locate private methods inside `PriceEngine`
- Execute them in the required order
- Return the corrected payable amount

### PriceEngineConsole

Console application used to test the solution.

Responsibilities:

- Create test data
- Execute the original `CalculatePayable()` method
- Execute the corrected calculation
- Display both results

---

## ⚙ Technologies

- C#
- .NET
- Reflection
- Rider

---

## 📚 Reflection

The wrapper accesses private methods using `BindingFlags.NonPublic`.

Example:

```csharp
var method = engineType.GetMethod(
    "ApplyCoupon",
    BindingFlags.NonPublic | BindingFlags.Instance);
```

Private methods are executed dynamically using:

```csharp
method.Invoke(engine, parameters);
```

Static methods are invoked as:

```csharp
method.Invoke(null, parameters);
```

---

## 🔄 Corrected Calculation Order

The wrapper executes the calculation in the following order:

1. ComputeSubtotal
2. CountUnits
3. ApplyVolumeDiscount
4. ApplyLoyaltyDiscount
5. ApplyCoupon
6. ApplyVat
7. RoundMoney

---

## ▶ Running the Project

1. Clone the repository.
2. Place **ACA.PriceEngine.dll** inside the `Lib` folder.
3. Open the solution in Rider or Visual Studio.
4. Build the solution.
5. Run **PriceEngineConsole**.

---

## 💡 Key Concepts

- External DLL references
- Reflection
- MethodInfo
- BindingFlags
- Invoke()
- Project References
- Class Libraries

---

## 📷 Example Output

```text
Price calculation results
-------------------------
Original result:  138.51
Corrected result: 135.90
```

---

## 📄 License

This project was created for educational purposes.