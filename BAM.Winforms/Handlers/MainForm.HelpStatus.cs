using System;
using System.Windows.Forms;

namespace BAM.Winforms.Forms
{
    public partial class MainForm
    {
        private void WireUpHelpStatusMessages()
        {
            AttachHelpStatusMessage(
                "Number Button (Appends the selected digit to the current input value.)",
                N00Button, N0Button, N1Button, N2Button, N3Button, N4Button, N5Button, N6Button, N7Button, N8Button, N9Button);

            AttachHelpStatusMessage(
                "Plus Button (Adds immediate value, product, or quotient to the current running total.)",
                AddButton);

            AttachHelpStatusMessage(
                "Minus Button (Subtracts immediate value, product, or quotient from the current running total.)",
                SubtractButton);

            AttachHelpStatusMessage(
                "Decimal Button (Appends a decimal point to the current input value.)",
                NDecimalButton);

            AttachHelpStatusMessage(
                "Subtotal Button (Displays the current running total without clearing it.) Shortcut: s, S",
                STButton);

            AttachHelpStatusMessage(
                "Percent Button (Calculates the percentage of the current input value based on the current running total.)",
                PercButton);

            AttachHelpStatusMessage(
                "Divide Button (Divides the current running total by the immediate value.) Shortcut: /",
                DivideButton);

            AttachHelpStatusMessage(
                "Multiply Button (Multiplies the current running total by the immediate value.) Shortcut: *, x, X",
                MultiplyButton);

            AttachHelpStatusMessage(
                "Equals Button (Displays result of multiply or divide. Does not modify current running ttl.) Short: Ctrl+Enter",
                EqualsButton);

            AttachHelpStatusMessage(
                "Del Button (Deletes the currently selected line in the tape box.)",
                DelButton);

            AttachHelpStatusMessage(
                "Clear Button (Clears the current running total.)",
                CButton);

            AttachHelpStatusMessage(
                "Clear All Button (Clears the current input value, memory register, and the running total.)",
                CAButton);

            AttachHelpStatusMessage(
                "Memory Add Button (Adds the current input value to the memory register.)",
                MPlusButton);

            AttachHelpStatusMessage(
                "Memory Subtract Button (Subtracts the current input value from the memory register.)",
                MemorySubtractButton);

            AttachHelpStatusMessage(
                "Memory Recall Button (Recalls the value from the memory register.)",
                MRButton);

            AttachHelpStatusMessage(
                "Memory Subtotal Button (Displays the subtotal of the memory register.)",
                MSTButton);

            AttachHelpStatusMessage(
                "Memory Total Button (Displays the total of the memory register.)",
                MTButton);

            AttachHelpStatusMessage(
                "Up Shift Button (Moves the cursor to the beginning of the current input value.)",
                CaretButton);

            AttachHelpStatusMessage(
                "Shift Button (Moves the cursor to the beginning of the current input value.)",
                ShiftButton);

            AttachHelpStatusMessage(
                "Negative Button (Negates the current input value.)",
                NegButton);

            AttachHelpStatusMessage(
                "Quick Record Button (Records a macro with current value in Main LED.)",
                QRButton);

            AttachHelpStatusMessage(
                "Variable Button (Takes value of Main LED when macro is first invoked.)",
                VButton);

            AttachHelpStatusMessage(
                "Macro Button (User defined macro that can be recorded and played back.)",
                M0Button, M1Button, M2Button, M3Button);

            AttachHelpStatusMessage(
                "Total Button (Displays the current running total and clears it) Shortcut: t, T, Enter",
                TButton);

            AttachHelpStatusMessage(
                "Pound Button (Brings up a dialog to add or edit a comment for the selected line.)",
                PoundButton);

            AttachHelpStatusMessage(
                "Mark up or mark down (Increases or decreases the value of the selected line.)",
                MudButton);

            AttachHelpStatusMessage(
                "Main LED (Displays the current input value, running total, or memory register value.)",
                MainTextBox);

            AttachHelpStatusMessage(
                "Tape Box (Displays the history of calculations and operations performed.)",
                TapeListBox);

            AttachHelpStatusMessage(
                "Memory LED (Displays the current value stored in the memory register.)",
                MemoryTextBox);

            AttachToolStripStatusMessage(
                "New Document Button (Clears the current document and starts a new one.)",
                NewDocumentToolStripButton);

            AttachToolStripStatusMessage(
                "Open Document Button (Opens an existing document.)",
                OpenDocumentToolStripButton);

            AttachToolStripStatusMessage(
                "Delete Button (Deletes all lines.)",
                DeleteToolStripButton);

            AttachToolStripStatusMessage(
                "Print Button (Prints the current document.)",
                PrintToolStripButton);

            // SaveToolStripButton
            AttachToolStripStatusMessage(
                "Save Button (Saves the current document.)",
                SaveToolStripButton);

            AttachToolStripStatusMessage(
                "Edit Button (Edits the current document.)",
                EditToolStripButton);
        }

        private void AttachToolStripStatusMessage(string message, ToolStripButton toolStripButton)
        {
            toolStripButton.MouseEnter += (sender, e) => HelpToolStripStatusLabel.Text = message;
            toolStripButton.MouseLeave += (sender, e) => HelpToolStripStatusLabel.Text = string.Empty;
        }

        private void AttachHelpStatusMessage(string message, params Control[] controls)
        {
            foreach (var control in controls)
            {
                control.MouseEnter += (sender, e) => HelpToolStripStatusLabel.Text = message;
                control.MouseLeave += (sender, e) => HelpToolStripStatusLabel.Text = string.Empty;
            }
        }

    }
}
