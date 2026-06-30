using System;
using System.Windows.Forms;
using BAM.Winforms.Services;

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
            TestAddTenButton.Click += TestAddTenButton_Click;
        }

        private void TestAddTenButton_Click(object sender, EventArgs e)
        {
            _engine.SetValue(25m);
            _engine.Add();

            _engine.SetValue(10m);
            _engine.Add();

            _engine.Total();

            _engine.SetValue(5m);
            _engine.Add();

            UpdateDisplay();
        }

        private void UpdateDisplay()
        {
            TestResultTextBox.Text = _engine.MainLedValue.ToString("G29");
        }
    }
}
