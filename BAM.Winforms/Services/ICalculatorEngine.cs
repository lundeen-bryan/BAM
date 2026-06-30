using System.Collections.Generic;
using BAM.Winforms.Models;

namespace BAM.Winforms.Services
{
    public interface ICalculatorEngine
    {
        void SetValue(decimal value);

        void Add();

        void Subtract();

        void Multiply();

        void Divide();

        void Equals();

        void Total();

        void Subtotal();

        void Clear();

        void ClearAll();

        void Negate();

        void Percent();

        void MemoryAdd();

        void MemorySubtract();

        void MemoryRecall();

        void MemorySubtotal();

        void MemoryTotal();

        decimal MainLedValue { get; }

        decimal MemoryLedValue { get; }

        IReadOnlyList<TapeEntry> TapeEntries { get; }
    }
}