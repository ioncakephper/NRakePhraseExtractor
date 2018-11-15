using System.ComponentModel;
using System.Windows.Forms;

namespace WindowsFormsControlLibrary1
{
    public partial class ChangeDetector : Component
    {
        public ChangeDetector()
        {
            InitializeComponent();
        }

        public ChangeDetector(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        [DefaultValue(DialogResult.Cancel)]
        public DialogResult CancelResult { get; set; } = DialogResult.Cancel;

        [DefaultValue(MessageBoxIcon.Warning)]
        public MessageBoxIcon Icon { get; set; } = MessageBoxIcon.Warning;
        [DefaultValue(MessageBoxButtons.YesNoCancel)]
        public MessageBoxButtons Buttons { get; set; } = MessageBoxButtons.YesNoCancel;

        [DefaultValue(false)]
        public bool Changed { get; set; }

        public bool RequestDecision()
        {
            if (Changed)
            {
                DialogResult = MessageBox.Show(Text, Caption, Buttons, Icon);
                if (DialogResult.Equals(CancelResult))
                {
                    return true;
                }
            }
            return false;
        }

        [DefaultValue("Confirm")]
        public string Caption { get; set; } = "Confirm";

        [DefaultValue("Content has changed. Closing the form will lose the changes. Do you want to save the content?")]
        public string Text { get; set; } = "Content has changed. Closing the form will lose the changes. Do you want to save the content?";

        [Browsable(false)]
        public DialogResult DialogResult { get; set; }
    }
}
