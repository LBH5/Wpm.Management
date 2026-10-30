# Wpm.Management

A Domain-Driven Design (DDD) based solution for Veterinary Practice Management.

## Architecture & Bounded Contexts

The solution is divided into distinct bounded contexts and shared components:

- **Wpm.Management (`src/Wpm.Management.*`)**:
  - Handles administrative pet management, breeds, and patient demographics.
- **Wpm.Clinic (`src/Wpm.Clinic.*`)**:
  - Manages clinical workflows, consultations, drug administrations, and vital signs monitoring.
- **Wpm.SharedKernel (`src/Wpm.SharedKernel`)**:
  - Shared domain building blocks, including base `Entity`, `AggregateRoot`, and common value objects like `Weight`.
- **Tests (`tests/`)**:
  - Domain unit tests for business rules and invariants (`Wpm.Management.Domain.Tests` and `Wpm.Clinic.Domain.Tests`).

## Solution Structure

```text
├── src/
│   ├── Wpm.Clinic.Api/              # Clinic Web API
│   ├── Wpm.Clinic.Domain/           # Clinic bounded context domain logic
│   ├── Wpm.Management.Api/          # Management Web API
│   ├── Wpm.Management.Domain/       # Management bounded context domain logic
│   └── Wpm.SharedKernel/            # Shared DDD primitives
├── tests/
│   ├── Wpm.Clinic.Domain.Tests/     # Clinic domain unit tests
│   └── Wpm.Management.Domain.Tests/ # Management domain unit tests
├── Wpm.Management.slnx              # Solution file
└── README.md
```

## Getting Started

### Prerequisites

- [.NET SDK](https://dotnet.microsoft.com/download) (targets `net10.0`)

### Build

To restore dependencies and build the entire solution:

```bash
dotnet build
```

### Run Tests

Execute all unit test projects:

```bash
dotnet test
```

### Run APIs

Run the Management API:

```bash
dotnet run --project src/Wpm.Management.Api
```

Run the Clinic API:

```bash
dotnet run --project src/Wpm.Clinic.Api
```

