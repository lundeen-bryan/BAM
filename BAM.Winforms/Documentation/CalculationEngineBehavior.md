# CalculatorEngine Behavior

## Purpose

The `CalculatorEngine` is the core calculation component for BAM. It
manages calculator state, including Main LED behavior, Memory LED
behavior, running totals, pending operations, and tape entries.

The engine must remain independent from the user interface.

------------------------------------------------------------------------

## Design Goals

-   UI-independent
-   Stateful
-   Predictable
-   Testable
-   Extensible

------------------------------------------------------------------------

## Core Concept

BAM is a running-total adding machine:

-   Add/Subtract affect the running total
-   Multiply/Divide are pending operations
-   Equals computes but does not commit
-   Total commits and clears running total
-   Subtotal commits without clearing
-   Memory is a separate running total

------------------------------------------------------------------------

## LED Behavior

### Main LED

Represents the current working value:

-   Input value
-   Result of operations
-   Percent results
-   Memory recall results
-   Subtotal/Total results

### Memory LED

Represents current memory total.

------------------------------------------------------------------------

## Core State

-   Main LED value
-   Memory LED value
-   Running total
-   Current input value
-   Pending operation
-   Pending value
-   Last repeat value
-   Tape entries
-   Item counts

------------------------------------------------------------------------

## Addition / Subtraction

-   Commit values to running total
-   Resolve pending operations first
-   Create tape entries
-   Support auto-repeat

------------------------------------------------------------------------

## Multiplication / Division

-   Create pending operation
-   Do NOT affect running total until committed
-   Equals computes result only
-   Add/Subtract commit result

------------------------------------------------------------------------

## Equals

-   Completes pending operation
-   Updates Main LED
-   Does NOT change running total

------------------------------------------------------------------------

## Subtotal

-   Displays running total
-   Does NOT clear total
-   Creates tape entry

------------------------------------------------------------------------

## Total

-   Displays running total
-   Clears running total
-   Creates tape entry

------------------------------------------------------------------------

## Clear Behavior

### Clear (C)

-   Resets Main LED and running total
-   Keeps memory and tape

### Clear All (CA)

-   Resets everything including tape and memory

------------------------------------------------------------------------

## Negate

-   Flips sign of Main LED value
-   Does not commit automatically

------------------------------------------------------------------------

## Percent

-   Calculates percent of previous value
-   Does NOT commit automatically
-   Requires Add/Subtract to commit

------------------------------------------------------------------------

## Memory Behavior

-   M+= adds value (or resolved operation)
-   M-= subtracts value (or resolved operation)
-   MR loads memory into Main LED
-   MST displays memory without clearing
-   MT displays and clears memory

Memory operations appear on the tape.

------------------------------------------------------------------------

## Auto Repeat

-   Add/Subtract repeat last committed value

------------------------------------------------------------------------

## Tape Behavior

Each entry contains: - Value - Operation - Result - Running total -
Type - Optional comment

------------------------------------------------------------------------

## Comments

-   Attachable to tape entries
-   Stored in engine
-   Displayed by UI

------------------------------------------------------------------------

## Rounding & Precision

-   Affect display only
-   Calculations use full precision

------------------------------------------------------------------------

## Error Handling

-   Division by zero handled safely
-   No UI crashes
-   Engine returns structured result

------------------------------------------------------------------------

## Non-Responsibilities

-   UI handling
-   Formatting
-   File operations

------------------------------------------------------------------------

## Version 1 Scope

Includes: - Full adding machine behavior - Memory - Percent - Negate -
Rounding - Decimal precision - Comments

Excludes: - Macros - MUD - Advanced tape editing

------------------------------------------------------------------------

## Final Decisions (Resolved Questions)

1.  Total clears running total but NOT tape\
2.  Clear and Clear All both supported\
3.  Subtotal creates tape line\
4.  Add/Subtract support auto-repeat\
5.  Negate used for negative numbers\
6.  Multiply/Divide produce single committed result line\
7.  Memory operations appear on tape\
8.  Engine returns structured results (not just exceptions)

------------------------------------------------------------------------

## Guiding Principle

Engine = source of truth\
UI = display only\
Tape = record of actions

---

## CalculationEngine Behavior Examples


### 1. Basic Addition

Input:
12 +
5 +
T

Behavior:
- 12 is added to total → total = 12
- 5 is added → total = 17
- Total displays 17 and resets running total

Result:
Main LED: 17
Running total: 0

---

### 2. Multiplication with Add (Commit)

Input:
5 X
50 +

Behavior:
- 5 stored as pending multiplication
- 50 entered
- Add triggers calculation: 5 × 50 = 250
- 250 added to running total

Result:
Main LED: 250
Running total: 250

---

### 3. Multiplication with Equals (No Commit)

Input:
5 X
50 =

Behavior:
- 5 × 50 = 250
- Result shown in Main LED
- NOT added to running total

Result:
Main LED: 250
Running total: unchanged

---

### 4. Division with Subtract

Input:
100 /
4 -

Behavior:
- 100 ÷ 4 = 25
- 25 is subtracted from running total

Result:
Main LED: 25
Running total: -25

---

### 5. Auto Repeat Addition

Input:
25 +
+
+

Behavior:
- First + adds 25
- Subsequent + repeats last value (25)

Result:
Running total: 75
Tape:
25 +
25 +
25 +

---

### 6. Subtotal vs Total

Input:
10 +
5 +
ST

Behavior:
- Subtotal shows total without clearing

Result:
Main LED: 15
Running total: 15

Then:
T

Result:
Main LED: 15
Running total: 0

---

### 7. Percent Calculation (Addition)

Input:
1000 +
25 %
+

Behavior:
- 25% of 1000 = 250
- Percent updates Main LED to 250
- + commits 250 to running total

Result:
Running total: 1250

---

### 8. Negate Behavior

Input:
25
Neg +
    
Behavior:
- Neg converts 25 → -25
- Add commits -25

Result:
Running total: -25

---

### 9. Memory Add (M+=)

Input:
6 /
3 M+=

Behavior:
- 6 ÷ 3 = 2
- 2 added to memory (not running total)

Result:
Memory LED: 2
Running total: unchanged

---

### 10. Memory Recall (MR)

Input:
(Memory = 85)
3 X
MR +

Behavior:
- MR loads 85 into Main LED
- 3 × 85 = 255
- + commits 255

Result:
Running total: 255

---

### 11. Memory Subtract (M-=)

Input:
7 X
5 M-=

Behavior:
- 7 × 5 = 35
- 35 subtracted from memory

Result:
Memory LED: -35

---

### 12. Clear vs Clear All

Clear (C)

Input:
50 +
C

Result:
Main LED: 0
Running total: 0
Tape: preserved
Memory: preserved

Clear All (CA)

Input:
50 +
CA

Result:
Main LED: 0
Running total: 0
Memory LED: 0
Tape: cleared

---

### 13. Combined Real-World Example

Input:
5 X
50 +
150 +
5 X
2.50 -
25 -
T

Behavior:
- 5×50 = 250 → + → total = 250
- 150 + → total = 400
- 5×2.50 = 12.50 → - → total = 387.50
- 25 - → total = 362.50
- T displays total and resets

Result:
Main LED: 362.50
Running total: 0

---

### 14. Percent Subtraction Example

Input:
49 +
35 % -
T

Behavior:
- 35% of 49 = 17.15
- Subtracted from running total

Result:
Main LED: 31.85
Running total: 0
