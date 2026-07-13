# 📁 CLI File Copier

A command-line file copying utility written in **C# (.NET)** that efficiently copies files using buffered streams while displaying real-time progress information.

## Features

* Command-line argument parsing
* Argument validation
* Configurable buffer size
* Stream-based file copying using `FileStream`
* Real-time copy progress
* Percentage completed
* Amount of copied data (MB)
* Remaining time estimation
* Automatic destination directory creation
* Exception handling

---

## Command-Line Arguments

| Argument          | Required | Description                              |
| ----------------- | -------- | ---------------------------------------- |
| `--source <path>` | Yes      | Source file path                         |
| `--dest <path>`   | Yes      | Destination file path                    |
| `--bsize <bytes>` | No       | Buffer size in bytes (default: **4 MB**) |

---

## Example

```bash
dotnet run -- --source "C:\Files\movie.mp4" --dest "D:\Backup\movie.mp4"
```

Using a custom buffer size:

```bash
dotnet run -- --source "C:\Files\movie.mp4" --dest "D:\Backup\movie.mp4" --bsize 8192
```

---

## Sample Output

```text
Arguments are valid.

Copy started...
Buffer size: 4194304 bytes

Progress: 46.5% | Copied: 465.0 MB / 1000.0 MB | Remaining Time: 00:00:08

Copy finished.
Destination: D:\Backup\movie.mp4
```

---

## Project Structure

```text
CopyProject
│
├── Program.cs
├── Options.cs
├── ArgumentParser.cs
├── ArgumentValidator.cs
└── Copy.cs
```

---

## Technologies

* C#
* .NET
* FileStream
* File I/O
* Command-Line Arguments
* Buffered File Copy
* DateTime
* TimeSpan

---

## Validation

The application validates:

* Required command-line arguments
* Source file existence
* Buffer size value
* Source and destination are different files
* Destination directory (created automatically if it does not exist)

---

## How It Works

1. Parse command-line arguments.
2. Validate all input parameters.
3. Open source and destination files using `FileStream`.
4. Allocate a buffer.
5. Read and write the file in chunks.
6. Display live progress information during copying.
7. Finish by printing the destination path.

---

## Learning Objectives

This project demonstrates practical usage of:

* Command-line argument parsing
* File and directory handling
* Stream-based file operations
* Buffer management
* Exception handling
* Progress calculation
* Time estimation
* Clean project organization
