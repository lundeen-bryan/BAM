# BAM CalculatorEngine Description

The CalculatorEngine is the core logic component of the BAM (Bryan’s Adding Machine) application. It is responsible for handling all calculator behavior, state management, and business rules independently of the user interface.

## Purpose

The CalculatorEngine simulates the behavior of a traditional desktop adding machine (inspired by Mark’s Adding Machine), including:

- Maintaining a running total
- Handling arithmetic operations (+, -, X, /)
- Managing pending operations (e.g., multiplication/division sequences)
- Supporting subtotal and total functionality
- Managing memory operations (M+=, M-=, MR, etc.)
- Producing structured "tape" entries representing each calculation step

## Design Principles

- **Separation of Concerns**: No UI logic exists in the engine. It does not know about forms, controls, or events.
- **Stateful**: The engine maintains internal state such as:
  - Current input value
  - Running total
  - Pending operation (if any)
  - Memory total
- **Deterministic**: Given a sequence of inputs, the engine always produces the same results.
- **Extensible**: Designed to support advanced features later (macros, recalculation, editing tape lines).

## Responsibilities

The CalculatorEngine should:

1. Accept numeric input and operations from the UI layer
2. Apply calculator rules consistent with an adding machine
3. Track and update internal state accordingly
4. Return results to the UI for display
5. Generate structured tape entries for each operation

## Non-Responsibilities

The CalculatorEngine must NOT:

- Handle UI events or controls
- Format display text for the UI
- Manage file I/O (saving/loading documents)
- Directly manipulate the tape UI (ListBox, DataGrid, etc.)

## Example Usage Flow

1. User enters "12"
2. UI calls: `engine.SetValue(12)`
3. User presses "X"
4. UI calls: `engine.Multiply()`
5. User enters "5"
6. UI calls: `engine.SetValue(5)`
7. User presses "+"
8. UI calls: `engine.Add()`

Engine:
- Computes 12 × 5 = 60
- Adds 60 to running total
- Creates a tape entry
- Returns updated total

## Key Concept

The CalculatorEngine represents the "machine" behind the calculator.

- The UI is the **dashboard**
- The CalculatorEngine is the **engine**
- The tape is the **record of what happened**

This separation ensures the application remains maintainable, testable, and extensible as complexity increases.