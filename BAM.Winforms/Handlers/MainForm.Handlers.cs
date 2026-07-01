using System;
using System.Globalization;
using System.Windows.Forms;
using BAM.Winforms.Enums;

namespace BAM.Winforms.Forms
{
    public partial class MainForm
    {
        private void NumberButton_Click(object sender, EventArgs e)
        {
            var button = sender as Button;

            if (button == null)
            {
                return;
            }

            AppendInput(button.Text);
        }

        private void DecimalButton_Click(object sender, EventArgs e)
        {
            if (_currentInputText.Contains("."))
            {
                return;
            }

            if (string.IsNullOrEmpty(_currentInputText))
            {
                _currentInputText = "0";
            }

            AppendInput(".");
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            CommitInputToEngine();
            _engine.Add();
            ResetInputState();
            UpdateDisplay();
        }

        private void SubtractButton_Click(object sender, EventArgs e)
        {
            CommitInputToEngine();
            _engine.Subtract();
            ResetInputState();
            UpdateDisplay();
        }

        private void MultiplyButton_Click(object sender, EventArgs e)
        {
            CommitInputToEngine();
            _engine.Multiply();
            ResetInputState();
            UpdateDisplay();
        }

        private void DivideButton_Click(object sender, EventArgs e)
        {
            CommitInputToEngine();
            _engine.Divide();
            ResetInputState();
            UpdateDisplay();
        }

        private void EqualsButton_Click(object sender, EventArgs e)
        {
            CommitInputToEngine();
            _engine.Equals();
            ResetInputState();
            UpdateDisplay();
        }

        private void TotalButton_Click(object sender, EventArgs e)
        {
            _engine.Total();
            ResetInputState();
            UpdateDisplay();
        }

        private void SubtotalButton_Click(object sender, EventArgs e)
        {
            _engine.Subtotal();
            ResetInputState();
            UpdateDisplay();
        }

        private void MemoryAddButton_Click(object sender, EventArgs e)
        {
            CommitInputToEngine();
            _engine.MemoryAdd();
            ResetInputState();
            UpdateDisplay();
        }

        private void MemorySubtractButton_Click(object sender, EventArgs e)
        {
            CommitInputToEngine();
            _engine.MemorySubtract();
            ResetInputState();
            UpdateDisplay();
        }

        private void MemoryRecallButton_Click(object sender, EventArgs e)
        {
            _engine.MemoryRecall();
            ResetInputState();
            UpdateDisplay();
        }

        private void MemorySubtotalButton_Click(object sender, EventArgs e)
        {
            _engine.MemorySubtotal();
            ResetInputState();
            UpdateDisplay();
        }

        private void MemoryTotalButton_Click(object sender, EventArgs e)
        {
            _engine.MemoryTotal();
            ResetInputState();
            UpdateDisplay();
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            _engine.Clear();
            ResetInputState();
            UpdateDisplay();
        }

        private void ClearAllButton_Click(object sender, EventArgs e)
        {
            _engine.ClearAll();
            ResetInputState();
            UpdateDisplay();
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_currentInputText))
            {
                return;
            }

            _currentInputText = _currentInputText.Substring(0, _currentInputText.Length - 1);

            if (string.IsNullOrEmpty(_currentInputText))
            {
                ResetInputState();
                UpdateDisplay();
                return;
            }

            MainTextBox.Text = _currentInputText;
        }

        private void AppendInput(string text)
        {
            _currentInputText += text;
            _isEnteringNumber = true;
            MainTextBox.Text = _currentInputText;
        }

        private void CommitInputToEngine()
        {
            if (!_isEnteringNumber)
            {
                return;
            }

            decimal value;

            if (!decimal.TryParse(_currentInputText, NumberStyles.Number, CultureInfo.CurrentCulture, out value))
            {
                MessageBox.Show("Invalid number entered.", "BAM", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _engine.SetValue(value);
        }

        private void ResetInputState()
        {
            _currentInputText = "";
            _isEnteringNumber = false;
        }

        private void UpdateDisplay()
        {
            MainTextBox.Text = _engine.MainLedValue.ToString("G29");
            MemoryTextBox.Text = _engine.MemoryLedValue.ToString("G29");

            TapeListBox.Items.Clear();

            foreach (var entry in _engine.TapeEntries)
            {
                TapeListBox.Items.Add(
                    string.Format("{0,12:0.00}  {1}", entry.Value, GetTapeSymbol(entry.Operation)));
            }

            ScrollTapeToBottom();
        }

        private void ScrollTapeToBottom()
        {
            if (TapeListBox.Items.Count > 0)
            {
                TapeListBox.TopIndex = TapeListBox.Items.Count - 1;
            }
        }

        private string GetTapeSymbol(CalculatorOperation operation)
        {
            switch (operation)
            {
                case CalculatorOperation.Add:
                    return "+";

                case CalculatorOperation.Subtract:
                    return "-";

                case CalculatorOperation.Multiply:
                    return "x";

                case CalculatorOperation.Divide:
                    return "/";

                case CalculatorOperation.Equals:
                    return "=";

                case CalculatorOperation.Total:
                    return "Ttl";

                case CalculatorOperation.Subtotal:
                    return "ST";

                case CalculatorOperation.MemoryAdd:
                    return "M+";

                case CalculatorOperation.MemorySubtract:
                    return "M-";

                case CalculatorOperation.MemoryRecall:
                    return "MR";

                case CalculatorOperation.MemorySubtotal:
                    return "MS";

                case CalculatorOperation.MemoryTotal:
                    return "MT";

                case CalculatorOperation.Clear:
                    return "C";

                case CalculatorOperation.ClearAll:
                    return "CA";

                default:
                    return operation.ToString();
            }
        }
    }
}