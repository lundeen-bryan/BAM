using System;
using BAM.Winforms.Enums;
using BAM.Winforms.Services;

namespace BAM.Winforms.Models
{
    public sealed class TapeEntry
    {
        public TapeEntry(
            decimal value,
            CalculatorOperation operation,
            decimal result,
            decimal runningTotal,
            TapeEntryType entryType,
            CalculatorStateSnapshot previousState = null,
            string comment = null)
        {
            Value = value;
            Operation = operation;
            Result = result;
            PreviousState = previousState;
            RunningTotal = runningTotal;
            EntryType = entryType;
            Comment = comment;
            CreatedAt = DateTime.Now;
        }

        public decimal Value { get; }

        public CalculatorOperation Operation { get; }
        public CalculatorStateSnapshot PreviousState { get; }

        public decimal Result { get; }

        public decimal RunningTotal { get; }

        public TapeEntryType EntryType { get; }

        public string Comment { get; }

        public DateTime CreatedAt { get; }
    }
}