using System;
using System.Windows.Forms;
using BAM.Winforms.Services;
using BAM.Winforms.Enums;

namespace BAM.Winforms.Forms
{
    public partial class MainForm : Form
    {
        private readonly ICalculatorEngine _engine;
        public MainForm()
        {
            InitializeComponent();
            WireUpEvents();
            _engine = new CalculatorEngine();
        }

        private void WireUpEvents()
        {
            //TestAddButton.Click += TestAddTenButton_Click;
        }

        private void TestAddTenButton_Click(object sender, EventArgs e)
        {
            _engine.ClearAll();

            _engine.SetValue(25m);
            _engine.Add();

            _engine.SetValue(10m);
            _engine.Add();

            _engine.MemoryAdd();
            _engine.MemoryAdd();

            UpdateDisplay();
        }

        private void UpdateDisplay()
        {
            //TestMainLedTextBox.Text = _engine.MainLedValue.ToString("G29");
            //TestMemoryLedTextBox.Text = _engine.MemoryLedValue.ToString("G29");

            //TestTapeListBox.Items.Clear();

            //foreach (var entry in _engine.TapeEntries)
            //{
            //    TestTapeListBox.Items.Add(
            //        $"{entry.Value,10:0.00}  {GetTapeSymbol(entry.Operation)}");
            //}
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
                    return "Sub";

                case CalculatorOperation.Clear:
                    return "Clear";

                case CalculatorOperation.ClearAll:
                    return "ClearAll";
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

                default:
                    return operation.ToString();
            }
        }
    }
}
