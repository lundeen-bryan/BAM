using System;
using System.Collections.Generic;
using BAM.Winforms.Enums;
using BAM.Winforms.Models;

namespace BAM.Winforms.Services
{
    public sealed class CalculatorEngine : ICalculatorEngine
    {
        private readonly List<TapeEntry> _tapeEntries = new List<TapeEntry>();

        private CalculatorOperation _pendingOperation = CalculatorOperation.None;

        private decimal _lastEntryValue;
        private decimal _pendingValue;
        private decimal _mainLedValue;
        private decimal _memoryLedValue;
        private decimal _runningTotal;
        private decimal _currentInputValue;
        private decimal _lastRepeatValue;

        private bool _hasLastEntryValue;
        private bool _hasPendingOperation;
        private bool _hasCurrentInput;
        private bool _hasLastRepeatValue;

        public decimal MainLedValue => _mainLedValue;

        public decimal MemoryLedValue => _memoryLedValue;

        public IReadOnlyList<TapeEntry> TapeEntries => _tapeEntries.AsReadOnly();

        public void SetValue(decimal value)
        {
            _currentInputValue = value;
            _mainLedValue = value;
            _hasCurrentInput = true;
        }

        public void DeleteLastEntry()
        {
            if (_tapeEntries.Count == 0)
            {
                return;
            }

            var lastEntry = _tapeEntries[_tapeEntries.Count - 1];

            RestoreState(lastEntry.PreviousState);

            _tapeEntries.RemoveAt(_tapeEntries.Count - 1);
        }

        private void UndoTapeEntry(TapeEntry entry)
        {
            switch (entry.Operation)
            {
                case CalculatorOperation.Add:
                    _runningTotal -= entry.Value;
                    _mainLedValue = _runningTotal;
                    break;

                case CalculatorOperation.Subtract:
                    _runningTotal += entry.Value;
                    _mainLedValue = _runningTotal;
                    break;

                case CalculatorOperation.MemoryAdd:
                    _memoryLedValue -= entry.Value;
                    break;

                case CalculatorOperation.MemorySubtract:
                    _memoryLedValue += entry.Value;
                    break;

                case CalculatorOperation.Total:
                case CalculatorOperation.Subtotal:
                case CalculatorOperation.Equals:
                case CalculatorOperation.MemoryRecall:
                case CalculatorOperation.MemorySubtotal:
                case CalculatorOperation.MemoryTotal:
                case CalculatorOperation.Clear:
                case CalculatorOperation.ClearAll:
                    // These need deeper state snapshots later.
                    // For now, remove the tape line only.
                    break;
            }
        }

        private void RebuildLastEntryValue()
        {
            for (int i = _tapeEntries.Count - 1; i >= 0; i--)
            {
                var entry = _tapeEntries[i];

                if (entry.Operation == CalculatorOperation.Add ||
                    entry.Operation == CalculatorOperation.Subtract ||
                    entry.Operation == CalculatorOperation.Total ||
                    entry.Operation == CalculatorOperation.Subtotal ||
                    entry.Operation == CalculatorOperation.Equals ||
                    entry.Operation == CalculatorOperation.MemoryAdd ||
                    entry.Operation == CalculatorOperation.MemorySubtract ||
                    entry.Operation == CalculatorOperation.MemorySubtotal ||
                    entry.Operation == CalculatorOperation.MemoryTotal)
                {
                    StoreLastEntryValue(entry.Value);
                    return;
                }
            }

            ClearLastEntryValue();
        }

        private void ClearLastEntryValue()
        {
            _lastEntryValue = 0m;
            _hasLastEntryValue = false;
        }

        private void StoreLastEntryValue(decimal value)
        {
            _lastEntryValue = value;
            _hasLastEntryValue = true;
        }

        public void Add()
        {
            var previousState = CreateStateSnapshot();

            decimal valueToAdd = GetResolvedCommittableValue();

            _runningTotal += valueToAdd;
            _mainLedValue = _runningTotal;

            StoreRepeatValue(valueToAdd);
            StoreLastEntryValue(valueToAdd);

            AddTapeEntry(
                value: valueToAdd,
                operation: CalculatorOperation.Add,
                result: _mainLedValue,
                runningTotal: _runningTotal,
                entryType: TapeEntryType.Operation,
                previousState: previousState);

            ClearCurrentInput();
        }

        public void Subtract()
        {
            var previousState = CreateStateSnapshot();

            decimal valueToSubtract = GetResolvedCommittableValue();

            _runningTotal -= valueToSubtract;
            _mainLedValue = _runningTotal;

            StoreRepeatValue(valueToSubtract);
            StoreLastEntryValue(valueToSubtract);

            AddTapeEntry(
                value: valueToSubtract,
                operation: CalculatorOperation.Subtract,
                result: _mainLedValue,
                runningTotal: _runningTotal,
                entryType: TapeEntryType.Operation,
                previousState: previousState);

            ClearCurrentInput();
        }

        public void Multiply()
        {
            var previousState = CreateStateSnapshot();

            _pendingValue = GetCommittableValue();
            _pendingOperation = CalculatorOperation.Multiply;
            _hasPendingOperation = true;

            _mainLedValue = _pendingValue;

            AddTapeEntry(
                value: _pendingValue,
                operation: CalculatorOperation.Multiply,
                result: _mainLedValue,
                runningTotal: _runningTotal,
                entryType: TapeEntryType.Operation,
                previousState: previousState);

            ClearCurrentInput();
        }

        public void Divide()
        {
            var previousState = CreateStateSnapshot();

            _pendingValue = GetCommittableValue();
            _pendingOperation = CalculatorOperation.Divide;
            _hasPendingOperation = true;

            _mainLedValue = _pendingValue;

            AddTapeEntry(
                value: _pendingValue,
                operation: CalculatorOperation.Divide,
                result: _mainLedValue,
                runningTotal: _runningTotal,
                entryType: TapeEntryType.Operation,
                previousState: previousState);

            ClearCurrentInput();
        }

        public void Equals()
        {
            if (!_hasPendingOperation)
            {
                return;
            }

            var previousState = CreateStateSnapshot();

            _pendingValue = GetCommittableValue();
            decimal secondValue = GetCommittableValue();
            decimal result = ResolvePendingOperation(secondValue);

            _mainLedValue = result;

            StoreLastEntryValue(result);

            AddTapeEntry(
                value: secondValue,
                operation: CalculatorOperation.Equals,
                result: result,
                runningTotal: _runningTotal,
                entryType: TapeEntryType.Result,
                previousState: previousState);

            ClearPendingOperation();
            ClearCurrentInput();
        }

        public void Total()
        {
            var previousState = CreateStateSnapshot();
            _pendingValue = GetCommittableValue();
            _mainLedValue = _runningTotal;

            StoreLastEntryValue(_runningTotal);

            AddTapeEntry(
                value: _runningTotal,
                operation: CalculatorOperation.Total,
                result: _mainLedValue,
                runningTotal: _runningTotal,
                entryType: TapeEntryType.Total,
                previousState: previousState);

            _runningTotal = 0m;
            ClearCurrentInput();
            ClearPendingOperation();
            ClearRepeatValue();
        }

        public void Subtotal()
        {
            var previousState = CreateStateSnapshot();

            _pendingValue = GetCommittableValue();
            _mainLedValue = _runningTotal;

            StoreLastEntryValue(_runningTotal);

            AddTapeEntry(
                value: _runningTotal,
                operation: CalculatorOperation.Subtotal,
                result: _mainLedValue,
                runningTotal: _runningTotal,
                entryType: TapeEntryType.Subtotal,
                previousState: previousState);

            ClearCurrentInput();
        }

        public void Clear()
        {
            var previousState = CreateStateSnapshot();

            _pendingValue = GetCommittableValue();
            _mainLedValue = 0m;
            ClearCurrentInput();
            ClearPendingOperation();

            AddTapeEntry(
                value: 0m,
                operation: CalculatorOperation.Clear,
                result: _mainLedValue,
                runningTotal: _runningTotal,
                entryType: TapeEntryType.Clear,
                previousState: previousState);
        }

        public void ClearAll()
        {
            var previousState = CreateStateSnapshot();

            _mainLedValue = 0m;
            ClearCurrentInput();
            _memoryLedValue = 0m;
            _runningTotal = 0m;

            ClearCurrentInput();
            ClearPendingOperation();
            ClearRepeatValue();
            ClearLastEntryValue();

            AddTapeEntry(
                0m,
                CalculatorOperation.ClearAll,
                _mainLedValue,
                _runningTotal,
                TapeEntryType.Clear,
                previousState);
        }

        public void Negate()
        {
            throw new NotImplementedException("Negate will be implemented in Phase 5.");
        }

        public void Percent()
        {
            throw new NotImplementedException("Percent will be implemented in Phase 5.");
        }

        public void MemoryAdd()
        {
            var previousState = CreateStateSnapshot();

            decimal valueToStore = GetMemoryCommittableValue();

            _memoryLedValue += valueToStore;

            StoreLastEntryValue(valueToStore);

            AddTapeEntry(
                value: valueToStore,
                operation: CalculatorOperation.MemoryAdd,
                result: _memoryLedValue,
                runningTotal: _runningTotal,
                entryType: TapeEntryType.Memory,
                previousState: previousState);

            ClearCurrentInput();
        }

        public void MemorySubtract()
        {
            var previousState = CreateStateSnapshot();

            decimal valueToStore = GetMemoryCommittableValue();

            _memoryLedValue -= valueToStore;

            StoreLastEntryValue(valueToStore);

            AddTapeEntry(
                value: valueToStore,
                operation: CalculatorOperation.MemorySubtract,
                result: _memoryLedValue,
                runningTotal: _runningTotal,
                entryType: TapeEntryType.Memory,
                previousState: previousState);

            ClearCurrentInput();
        }

        public void MemoryRecall()
        {
            _mainLedValue = _memoryLedValue;
            var previousState = CreateStateSnapshot();

            AddTapeEntry(
                value: _memoryLedValue,
                operation: CalculatorOperation.MemoryRecall,
                result: _mainLedValue,
                runningTotal: _runningTotal,
                entryType: TapeEntryType.Memory,
                previousState: previousState);

            ClearCurrentInput();
        }

        public void MemorySubtotal()
        {
            _mainLedValue = _memoryLedValue;
            var previousState = CreateStateSnapshot();

            StoreLastEntryValue(_memoryLedValue);

            AddTapeEntry(
                value: _memoryLedValue,
                operation: CalculatorOperation.MemorySubtotal,
                result: _mainLedValue,
                runningTotal: _runningTotal,
                entryType: TapeEntryType.Memory,
                previousState: previousState);

            ClearCurrentInput();
        }

        public void MemoryTotal()
        {
            _mainLedValue = _memoryLedValue;
            var previousState = CreateStateSnapshot();

            StoreLastEntryValue(_memoryLedValue);

            AddTapeEntry(
                value: _memoryLedValue,
                operation: CalculatorOperation.MemoryTotal,
                result: _mainLedValue,
                runningTotal: _runningTotal,
                entryType: TapeEntryType.Memory,
                previousState: previousState);

            _memoryLedValue = 0m;

            ClearCurrentInput();
        }

        private decimal GetCommittableValue()
        {
            if (_hasCurrentInput)
            {
                return _currentInputValue;
            }

            if (_hasLastRepeatValue)
            {
                return _lastRepeatValue;
            }

            return _mainLedValue;
        }

        private void StoreRepeatValue(decimal value)
        {
            _lastRepeatValue = value;
            _hasLastRepeatValue = true;
        }

        private void ClearCurrentInput()
        {
            _currentInputValue = 0m;
            _hasCurrentInput = false;
        }

        private void AddTapeEntry(
            decimal value,
            CalculatorOperation operation,
            decimal result,
            decimal runningTotal,
            TapeEntryType entryType,
            CalculatorStateSnapshot previousState,
            string comment = null)
        {
            var tapeEntry = new TapeEntry(
                value,
                operation,
                result,
                runningTotal,
                entryType,
                previousState,
                comment);

            _tapeEntries.Add(tapeEntry);
        }

        private CalculatorStateSnapshot CreateStateSnapshot()
        {
            return new CalculatorStateSnapshot(
                _mainLedValue,
                _memoryLedValue,
                _runningTotal,
                _currentInputValue,
                _hasCurrentInput,
                _lastRepeatValue,
                _hasLastRepeatValue,
                _lastEntryValue,
                _hasLastEntryValue,
                _pendingOperation,
                _pendingValue,
                _hasPendingOperation);
        }

        private void RestoreState(CalculatorStateSnapshot snapshot)
        {
            _mainLedValue = snapshot.MainLedValue;
            _memoryLedValue = snapshot.MemoryLedValue;
            _runningTotal = snapshot.RunningTotal;
            _currentInputValue = snapshot.CurrentInputValue;
            _hasCurrentInput = snapshot.HasCurrentInput;
            _lastRepeatValue = snapshot.LastRepeatValue;
            _hasLastRepeatValue = snapshot.HasLastRepeatValue;
            _lastEntryValue = snapshot.LastEntryValue;
            _hasLastEntryValue = snapshot.HasLastEntryValue;
            _pendingOperation = snapshot.PendingOperation;
            _pendingValue = snapshot.PendingValue;
            _hasPendingOperation = snapshot.HasPendingOperation;
        }

        private decimal ResolvePendingOperation(decimal secondValue)
        {
            switch (_pendingOperation)
            {
                case CalculatorOperation.Multiply:
                    return _pendingValue * secondValue;

                case CalculatorOperation.Divide:
                    if (secondValue == 0m)
                    {
                        throw new DivideByZeroException("Cannot divide by zero.");
                    }

                    return _pendingValue / secondValue;

                default:
                    return _mainLedValue;
            }
        }

        private void ClearPendingOperation()
        {
            _pendingValue = 0m;
            _pendingOperation = CalculatorOperation.None;
            _hasPendingOperation = false;
        }
        private decimal GetResolvedCommittableValue()
        {
            if (!_hasPendingOperation)
            {
                return GetCommittableValue();
            }

            decimal secondValue = GetCommittableValue();
            decimal result = ResolvePendingOperation(secondValue);

            ClearPendingOperation();

            return result;
        }

        private void ClearRepeatValue()
        {
            _lastRepeatValue = 0m;
            _hasLastRepeatValue = false;
        }
        private decimal GetMemoryCommittableValue()
        {
            if (_hasCurrentInput)
            {
                return _currentInputValue;
            }

            if (_hasLastEntryValue)
            {
                return _lastEntryValue;
            }

            return _mainLedValue;
        }
    }
}