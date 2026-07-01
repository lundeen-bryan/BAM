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

            PercButton.Enabled = false;
            NegButton.Enabled = false;
        }
    }
}
