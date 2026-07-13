# 📂 Homework 9 — File I/O, Streams & Serialization in C#

This project contains solutions for Homework 9 focused on **File Input/Output**, **Streams**, and **Text Processing** in C#.

The goal of these exercises is to understand how .NET works with files, directories, streams, text encoding, and binary data while following good programming practices.

---

## 📖 Topics Covered

- File and Directory operations
- Path manipulation
- FileStream
- StreamReader
- StreamWriter
- UTF-8 Encoding
- Binary file processing
- File verification
- Working with file offsets
- Reading files line by line

---

# 📚 Exercises

## ✅ Exercise 1 — Daily Report Archiver

### Scenario

A small team creates a daily report that must be saved to disk and verified after writing.

### Implemented

- Create a `reports` directory if it does not exist
- Save a text report using `File.WriteAllText()`
- Read the report back using `File.ReadAllText()`
- Compare the original and loaded content
- Display a confirmation message when the content matches

### Concepts

- `File.WriteAllText()`
- `File.ReadAllText()`
- `Directory.CreateDirectory()`
- `Path.Combine()`

---

## ✅ Exercise 2 — Inbox Scanner

### Scenario

Scan an inbox folder and display information about every file stored inside.

### Implemented

- Create the inbox directory if needed
- Enumerate files using lazy enumeration
- Display:
  - File name
  - File size (bytes)
- Print the total number of files

### Concepts

- `Directory.CreateDirectory()`
- `Directory.EnumerateFiles()`
- `Path.GetFileName()`
- `FileInfo`

---

## ✅ Exercise 3 — Partial Binary Downloader

### Scenario

Simulate a resumable download by writing binary blocks into specific positions of a file.

### Implemented

- Open or create a binary file
- Write the first block at offset `0`
- Write the second block at offset `1024`
- Reopen the file
- Read both blocks back
- Verify that each block was written to the correct position

### Concepts

- `FileStream`
- `FileMode.OpenOrCreate`
- `FileAccess`
- `Seek()`
- `Read()`
- `Write()`
- `Flush()`

---

## ✅ Exercise 4 — UTF-8 Log Processor

### Scenario

Write UTF-8 encoded log entries and analyze the log file line by line.

### Implemented

- Write log entries using `StreamWriter`
- Use UTF-8 encoding
- Include both English and Cyrillic text
- Read the file line by line using `StreamReader`
- Count log entries containing the word **ERROR**

### Concepts

- `StreamWriter`
- `StreamReader`
- `Encoding.UTF8`
- `ReadLine()`
- Text processing

---

# 🛠 Technologies

- C#
- .NET
- System.IO
- UTF-8 Encoding

---

# 📂 Project Structure

```
Homework9
│
├── Task1
│   └── Daily Report Archiver
│
├── Task2
│   └── Inbox Scanner
│
├── Task3
│   └── Partial Binary Downloader
│
└── Task4
    └── UTF-8 Log Processor
```

---

# 🎯 Learning Objectives

This project demonstrates practical usage of the .NET `System.IO` namespace, including:

- Working with files and directories
- Reading and writing text files
- Processing binary data
- Managing file streams
- Working with UTF-8 encoding
- Sequential and random file access
- Basic file verification techniques

---

# 📌 Key APIs Used

| Class | Purpose |
|--------|----------|
| `File` | Read and write files |
| `Directory` | Create and enumerate directories |
| `Path` | Build and manipulate file paths |
| `FileInfo` | Retrieve file metadata |
| `FileStream` | Low-level binary file access |
| `StreamReader` | Read text files |
| `StreamWriter` | Write text files |
| `Encoding.UTF8` | UTF-8 text encoding |

---

## Author

Developed as part of C# File I/O and Streams practice exercises.
