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
                entryType: TapeEntryType.Operation);

            ClearCurrentInput();
        }

        public void Subtract()
        {
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
                entryType: TapeEntryType.Operation);

            ClearCurrentInput();
        }

        public void Multiply()
        {
            _pendingValue = GetCommittableValue();
            _pendingOperation = CalculatorOperation.Multiply;
            _hasPendingOperation = true;

            _mainLedValue = _pendingValue;

            AddTapeEntry(
                value: _pendingValue,
                operation: CalculatorOperation.Multiply,
                result: _mainLedValue,
                runningTotal: _runningTotal,
                entryType: TapeEntryType.Operation);

            ClearCurrentInput();
        }

        public void Divide()
        {
            _pendingValue = GetCommittableValue();
            _pendingOperation = CalculatorOperation.Divide;
            _hasPendingOperation = true;

            _mainLedValue = _pendingValue;

            AddTapeEntry(
                value: _pendingValue,
                operation: CalculatorOperation.Divide,
                result: _mainLedValue,
                runningTotal: _runningTotal,
                entryType: TapeEntryType.Operation);

            ClearCurrentInput();
        }

        public void Equals()
        {
            if (!_hasPendingOperation)
            {
                return;
            }

            decimal secondValue = GetCommittableValue();
            decimal result = ResolvePendingOperation(secondValue);

            _mainLedValue = result;

            StoreLastEntryValue(result);

            AddTapeEntry(
                value: secondValue,
                operation: CalculatorOperation.Equals,
                result: result,
                runningTotal: _runningTotal,
                entryType: TapeEntryType.Result);

            ClearPendingOperation();
            ClearCurrentInput();
        }

        public void Total()
        {
            _mainLedValue = _runningTotal;

            StoreLastEntryValue(_runningTotal);

            AddTapeEntry(
                value: _runningTotal,
                operation: CalculatorOperation.Total,
                result: _mainLedValue,
                runningTotal: _runningTotal,
                entryType: TapeEntryType.Total);

            _runningTotal = 0m;
            ClearCurrentInput();
            ClearPendingOperation();
            ClearRepeatValue();
        }

        public void Subtotal()
        {
            _mainLedValue = _runningTotal;

            StoreLastEntryValue(_runningTotal);

            AddTapeEntry(
                value: _runningTotal,
                operation: CalculatorOperation.Subtotal,
                result: _mainLedValue,
                runningTotal: _runningTotal,
                entryType: TapeEntryType.Subtotal);

            ClearCurrentInput();
        }

        public void Clear()
        {
            _mainLedValue = 0m;
            ClearCurrentInput();
            ClearPendingOperation();

            AddTapeEntry(
                value: 0m,
                operation: CalculatorOperation.Clear,
                result: _mainLedValue,
                runningTotal: _runningTotal,
                entryType: TapeEntryType.Clear);
        }

        public void ClearAll()
        {
            _mainLedValue = 0m;
            _memoryLedValue = 0m;
            _runningTotal = 0m;

            ClearCurrentInput();
            ClearPendingOperation();
            ClearRepeatValue();
            ClearLastEntryValue();

            _tapeEntries.Clear();

            AddTapeEntry(
                value: 0m,
                operation: CalculatorOperation.ClearAll,
                result: _mainLedValue,
                runningTotal: _runningTotal,
                entryType: TapeEntryType.Clear);
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
            decimal valueToStore = GetMemoryCommittableValue();

            _memoryLedValue += valueToStore;

            StoreLastEntryValue(valueToStore);

            AddTapeEntry(
                value: valueToStore,
                operation: CalculatorOperation.MemoryAdd,
                result: _memoryLedValue,
                runningTotal: _runningTotal,
                entryType: TapeEntryType.Memory);

            ClearCurrentInput();
        }

        public void MemorySubtract()
        {
            decimal valueToStore = GetMemoryCommittableValue();

            _memoryLedValue -= valueToStore;

            StoreLastEntryValue(valueToStore);

            AddTapeEntry(
                value: valueToStore,
                operation: CalculatorOperation.MemorySubtract,
                result: _memoryLedValue,
                runningTotal: _runningTotal,
                entryType: TapeEntryType.Memory);

            ClearCurrentInput();
        }

        public void MemoryRecall()
        {
            _mainLedValue = _memoryLedValue;

            AddTapeEntry(
                value: _memoryLedValue,
                operation: CalculatorOperation.MemoryRecall,
                result: _mainLedValue,
                runningTotal: _runningTotal,
                entryType: TapeEntryType.Memory);

            ClearCurrentInput();
        }

        public void MemorySubtotal()
        {
            _mainLedValue = _memoryLedValue;

            StoreLastEntryValue(_memoryLedValue);

            AddTapeEntry(
                value: _memoryLedValue,
                operation: CalculatorOperation.MemorySubtotal,
                result: _mainLedValue,
                runningTotal: _runningTotal,
                entryType: TapeEntryType.Memory);

            ClearCurrentInput();
        }

        public void MemoryTotal()
        {
            _mainLedValue = _memoryLedValue;

            StoreLastEntryValue(_memoryLedValue);

            AddTapeEntry(
                value: _memoryLedValue,
                operation: CalculatorOperation.MemoryTotal,
                result: _mainLedValue,
                runningTotal: _runningTotal,
                entryType: TapeEntryType.Memory);

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
            string comment = null)
        {
            var tapeEntry = new TapeEntry(
                value,
                operation,
                result,
                runningTotal,
                entryType,
                comment);

            _tapeEntries.Add(tapeEntry);
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