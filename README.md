# YppCompiler

> A compiler implementation for the **Y++** language, built with C# and .NET.

---

## Table of Contents

- [Overview](#overview)
- [Features](#features)
- [Project Structure](#project-structure)
- [Prerequisites](#prerequisites)
- [Getting Started](#getting-started)
  - [Clone the Repository](#clone-the-repository)
  - [Build the Project](#build-the-project)
  - [Run the Compiler](#run-the-compiler)
- [Usage](#usage)
- [Contributing](#contributing)
- [License](#license)

---

## Overview

**YppCompiler** is a compiler for the Y++ programming language, implemented entirely in C#. The project demonstrates core compiler construction concepts — including lexical analysis, parsing, semantic analysis, and code generation — within a clean, maintainable .NET solution.

Whether you're studying compiler design or looking to extend the Y++ language, this project serves as a solid foundation.

---

## Features

- Full compiler pipeline for the Y++ language
- Written in idiomatic, modern C#
- Organized as a Visual Studio solution (`.slnx`)
- Modular architecture for easy extension

---

## Project Structure

```
YppCompiler/
├── Ypp Compiler/          # Core compiler source code
│   ├── ...                # Lexer, Parser, Semantic Analyzer, Code Generator
├── Ypp Compiler.slnx      # Visual Studio solution file
├── .gitignore
└── README.md
```

---

## Prerequisites

Before building or running the compiler, ensure you have the following installed:

- [.NET SDK](https://dotnet.microsoft.com/download) (version 10.0 or later recommended)
- [Visual Studio 2022+](https://visualstudio.microsoft.com/) **or** any IDE with C# support (e.g., VS Code with the C# extension)

---

## Getting Started

### Clone the Repository

```bash
git clone https://github.com/joewaleed/YppCompiler.git
cd YppCompiler
```

### Build the Project

**Using the .NET CLI:**

```bash
dotnet build "Ypp Compiler.slnx"
```

**Using Visual Studio:**

1. Open `Ypp Compiler.slnx` in Visual Studio.
2. Select **Build → Build Solution** (or press `Ctrl+Shift+B`).

### Run the Compiler

```bash
dotnet run --project "Ypp Compiler" -- <path-to-source-file>
```

Replace `<path-to-source-file>` with the path to your Y++ source file (e.g., `hello.ypp`).

---

## Usage

```
YppCompiler <source-file> [options]

Arguments:
  <source-file>    Path to the Y++ source file to compile

Options:
  --help           Show help and usage information
  --version        Show version information
```

**Example:**

```bash
dotnet run --project "Ypp Compiler" -- examples/hello.ypp
```

---

## Contributing

Contributions are welcome! To get started:

1. Fork the repository.
2. Create a new branch: `git checkout -b feature/your-feature-name`
3. Commit your changes: `git commit -m "Add your feature"`
4. Push to your branch: `git push origin feature/your-feature-name`
5. Open a Pull Request.

Please ensure your code follows existing conventions and includes relevant tests or examples where applicable.

---

## License

This project is open source. See the repository for licensing details.

---

*Built with C# and .NET — by [joewaleed](https://github.com/joewaleed)*
