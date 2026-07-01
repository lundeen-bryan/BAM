using BAM.Winforms.Enums;

namespace BAM.Winforms.Models
{
    public sealed class CalculatorStateSnapshot
    {
        public CalculatorStateSnapshot(
            decimal mainLedValue,
            decimal memoryLedValue,
            decimal runningTotal,
            decimal currentInputValue,
            bool hasCurrentInput,
            decimal lastRepeatValue,
            bool hasLastRepeatValue,
            decimal lastEntryValue,
            bool hasLastEntryValue,
            CalculatorOperation pendingOperation,
            decimal pendingValue,
            bool hasPendingOperation)
        {
            MainLedValue = mainLedValue;
            MemoryLedValue = memoryLedValue;
            RunningTotal = runningTotal;
            CurrentInputValue = currentInputValue;
            HasCurrentInput = hasCurrentInput;
            LastRepeatValue = lastRepeatValue;
            HasLastRepeatValue = hasLastRepeatValue;
            LastEntryValue = lastEntryValue;
            HasLastEntryValue = hasLastEntryValue;
            PendingOperation = pendingOperation;
            PendingValue = pendingValue;
            HasPendingOperation = hasPendingOperation;
        }

        public decimal MainLedValue { get; }
        public decimal MemoryLedValue { get; }
        public decimal RunningTotal { get; }
        public decimal CurrentInputValue { get; }
        public bool HasCurrentInput { get; }
        public decimal LastRepeatValue { get; }
        public bool HasLastRepeatValue { get; }
        public decimal LastEntryValue { get; }
        public bool HasLastEntryValue { get; }
        public CalculatorOperation PendingOperation { get; }
        public decimal PendingValue { get; }
        public bool HasPendingOperation { get; }
    }
}