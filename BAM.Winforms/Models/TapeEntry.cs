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
            string comment = null)
        {
            Value = value;
            Operation = operation;
            Result = result;
            RunningTotal = runningTotal;
            EntryType = entryType;
            Comment = comment;
            CreatedAt = DateTime.Now;
        }

        public decimal Value { get; }

        public CalculatorOperation Operation { get; }

        public decimal Result { get; }

        public decimal RunningTotal { get; }

        public TapeEntryType EntryType { get; }

        public string Comment { get; }

        public DateTime CreatedAt { get; }
    }
}