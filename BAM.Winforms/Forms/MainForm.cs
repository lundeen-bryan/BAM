using System;
using System.Windows.Forms;
using BAM.Winforms.Services;

namespace BAM.Winforms.Forms
{
    public partial class MainForm : Form
    {
        private readonly ICalculatorEngine _engine;

        private string _currentInputText = "";
        private bool _isEnteringNumber;

        public MainForm()
        {
            InitializeComponent();

            this.ShowIcon = false;

            _engine = new CalculatorEngine();

            WireUpEvents();
            UpdateDisplay();
            WireUpHelpStatusMessages();

            this.ActiveControl = MainTextBox;
        }

        private void WireUpEvents()
        {
            N0Button.Click += NumberButton_Click;
            N1Button.Click += NumberButton_Click;
            N2Button.Click += NumberButton_Click;
            N3Button.Click += NumberButton_Click;
            N4Button.Click += NumberButton_Click;
            N5Button.Click += NumberButton_Click;
            N6Button.Click += NumberButton_Click;
            N7Button.Click += NumberButton_Click;
            N8Button.Click += NumberButton_Click;
            N9Button.Click += NumberButton_Click;
            N00Button.Click += NumberButton_Click;
            NDecimalButton.Click += DecimalButton_Click;

            AddButton.Click += AddButton_Click;
            SubtractButton.Click += SubtractButton_Click;
            MultiplyButton.Click += MultiplyButton_Click;
            DivideButton.Click += DivideButton_Click;
            EqualsButton.Click += EqualsButton_Click;

            TButton.Click += TotalButton_Click;
            STButton.Click += SubtotalButton_Click;

            MPlusButton.Click += MemoryAddButton_Click;
            MemorySubtractButton.Click += MemorySubtractButton_Click;
            MRButton.Click += MemoryRecallButton_Click;
            MSTButton.Click += MemorySubtotalButton_Click;
            MTButton.Click += MemoryTotalButton_Click;

            CButton.Click += ClearButton_Click;
            CAButton.Click += ClearAllButton_Click;
            DelButton.Click += DeleteButton_Click;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            // Top row numbers
            if (keyData >= Keys.D0 && keyData <= Keys.D9)
            {
                AppendNumber((keyData - Keys.D0).ToString());
                return true;
            }

            // Numpad numbers
            if (keyData >= Keys.NumPad0 && keyData <= Keys.NumPad9)
            {
                AppendNumber((keyData - Keys.NumPad0).ToString());
                return true;
            }

            switch (keyData)
            {
                case Keys.Add:
                case Keys.Oemplus:
                    AddButton_Click(this, EventArgs.Empty);
                    return true;

                case Keys.Subtract:
                case Keys.OemMinus:
                    SubtractButton_Click(this, EventArgs.Empty);
                    return true;

                case Keys.Multiply:
                    MultiplyButton_Click(this, EventArgs.Empty);
                    return true;

                case Keys.X:
                case Keys.Shift | Keys.D8: // Shift + 8 is *
                    MultiplyButton_Click(this, EventArgs.Empty);
                    return true;

                case Keys.Divide:
                case Keys.OemQuestion:
                    DivideButton_Click(this, EventArgs.Empty);
                    return true;

                case Keys.S:
                    SubtotalButton_Click(this, EventArgs.Empty);
                    return true;

                case Keys.T:
                    TotalButton_Click(this, EventArgs.Empty);
                    return true;

                case Keys.Enter:
                    TotalButton_Click(this, EventArgs.Empty);
                    return true;

                case Keys.OemPeriod:
                    DecimalButton_Click(this, EventArgs.Empty);
                    return true;

                // Equals button is Ctrl + Enter
                case Keys.Control | Keys.Enter:
                    EqualsButton_Click(this, EventArgs.Empty);
                    return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

    }
}
