# BAM CalculatorEngine Implementation Guide for AI Assistants

## Purpose

This document provides guidance to AI assistants such as GitHub Copilot for implementing the CalculatorEngine component of the BAM (Bryan’s Adding Machine) project.

The goal is to ensure all generated code follows the defined behavior rules, maintains separation of concerns, and produces a maintainable and testable design.

## Project Context

BAM is a WinForms application that emulates a traditional desktop adding machine modeled after Mark’s Adding Machine (MAM).

The system is organized into layers

Forms handle UI controls and display

Handlers coordinate UI behavior

Services contain business logic including the CalculatorEngine

Models define data structures such as TapeEntry

The CalculatorEngine belongs in the Services layer and must not depend on UI components

## Core Objective

Implement a fully functional CalculatorEngine that

Mimics adding machine behavior rather than a standard calculator

Maintains internal state

Processes operations such as Add, Subtract, Multiply, Divide, and Percent

Manages Main LED and Memory LED values

Generates structured tape entries

Returns results that the UI can display

## Key Architectural Rules

### No UI Logic

The engine must not reference WinForms controls

The engine must not handle button click events

The engine must not format strings for display

The engine must not manage layout, fonts, or colors

The engine only processes data and returns results

### Engine is the Source of Truth

All calculation logic must live inside the engine

The UI must never calculate totals

The UI must never duplicate business logic

The UI must never directly modify engine state

The UI interacts only through method calls such as

engine.SetValue(25m)
engine.Add()

### Stateful Design

The engine must maintain internal state including

Main LED value

Memory LED value

Running total

Current input value

Pending operation

Pending value

Last repeat value for auto repeat

Tape entries

Item counts

Rounding mode

Decimal precision

## Core Behavioral Model

BAM is a running total machine

Add and Subtract affect the running total

Multiply and Divide create pending operations

Equals computes results but does not commit to the running total

Total displays and clears the running total

Subtotal displays without clearing the running total

Memory acts as a separate running total

## Pending Operations

Multiply and Divide must store a pending value

The operation is resolved only when a committing action occurs

Committing actions include Add, Subtract, Equals, Memory Add, and Memory Subtract

## LED Behavior

Main LED represents the current working value

This includes input values, results of operations, percent calculations, memory recall values, and totals

Memory LED represents the current memory total

The engine owns both LED values and the UI displays them

## Auto Repeat

Add and Subtract must support repeat behavior

If the user presses Add or Subtract repeatedly without entering a new value the engine repeats the last committed value

## Percent Behavior

Percent calculates a percentage of a previous value

Percent does not commit to the running total

The user must press Add or Subtract to commit the result

## Memory Behavior

The engine must support

Memory Add M+=

Memory Subtract M-=

Memory Recall MR

Memory Subtotal MST

Memory Total MT

Memory is independent from the running total

Memory operations must appear in the tape entries

## Negate Behavior

Negate flips the sign of the current Main LED value

Negate does not automatically commit to the running total

## Rounding and Precision

All calculations must use full precision values

Rounding and decimal precision affect only display or tape values

## Tape System

The engine must generate structured tape entries

Each tape entry should include

Value

Operation

Result

Running total after the operation

Entry type

Optional comment

The engine must not format the tape for display

## Suggested Interface

public interface ICalculatorEngine
{
    void SetValue(decimal value)
    void Add()
    void Subtract()
    void Multiply()
    void Divide()
    void Equals()
    void Total()
    void Subtotal()
    void Clear()
    void ClearAll()
    void Negate()
    void Percent()
    void MemoryAdd()
    void MemorySubtract()
    void MemoryRecall()
    void MemorySubtotal()
    void MemoryTotal()
    decimal MainLedValue { get; }
    decimal MemoryLedValue { get; }
    IReadOnlyList<TapeEntry> TapeEntries { get; }
}

## Implementation Strategy

Implement the engine in phases

Phase 1 includes SetValue, Add, Subtract, running total, and basic tape entries

Phase 2 includes Multiply, Divide, and pending operation logic

Phase 3 includes Total, Subtotal, Clear, and ClearAll

Phase 4 includes memory operations

Phase 5 includes Percent and Negate

Phase 6 includes rounding, precision, and comments

## Testing Guidance

Use the Examples section from CalculatorEngineBehavior.md as test cases

Each example should eventually become a unit test

## What to Avoid

Do not mix UI and engine logic

Do not hardcode display formatting

Do not ignore state tracking

Do not implement everything in a single large method

Do not skip edge case handling such as division by zero

## Final Goal

Produce a clean, modular, testable CalculatorEngine that

Accurately mimics MAM behavior

Can be reused in a future WPF application

Can be unit tested independently

Serves as the core logic of the BAM application