//------------------------------------------------------------------------------
// <copyright file="ChangeDetector.cs" company="Ion Gireada">
//      Copyright (c) Ion Gireada. All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

namespace WindowsFormsControlLibrary1
{
    using System.ComponentModel;
    using System.Windows.Forms;

    /// <summary>
    /// Defines the <see cref="ChangeDetector" />
    /// </summary>
    public partial class ChangeDetector : Component
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ChangeDetector"/> class.
        /// </summary>
        public ChangeDetector()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ChangeDetector"/> class.
        /// </summary>
        /// <param name="container">The container<see cref="IContainer"/></param>
        public ChangeDetector(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        /// <summary>
        /// Gets or sets the Buttons
        /// </summary>
        [DefaultValue(MessageBoxButtons.YesNoCancel)]
        public MessageBoxButtons Buttons { get; set; } = MessageBoxButtons.YesNoCancel;

        /// <summary>
        /// Gets or sets the CancelResult
        /// </summary>
        [DefaultValue(DialogResult.Cancel)]
        public DialogResult CancelResult { get; set; } = DialogResult.Cancel;

        /// <summary>
        /// Gets or sets the Caption
        /// </summary>
        [DefaultValue("Confirm")]
        public string Caption { get; set; } = "Confirm";

        /// <summary>
        /// Gets or sets a value indicating whether Changed
        /// </summary>
        [DefaultValue(false)]
        public bool Changed { get; set; }

        /// <summary>
        /// Gets or sets the DialogResult
        /// </summary>
        [Browsable(false)]
        public DialogResult DialogResult { get; set; }

        /// <summary>
        /// Gets or sets the Icon
        /// </summary>
        [DefaultValue(MessageBoxIcon.Warning)]
        public MessageBoxIcon Icon { get; set; } = MessageBoxIcon.Warning;

        /// <summary>
        /// Gets or sets the Text
        /// </summary>
        [DefaultValue("Content has changed. Closing the form will lose the changes. Do you want to save the content?")]
        public string Text { get; set; } = "Content has changed. Closing the form will lose the changes. Do you want to save the content?";

        /// <summary>
        /// The RequestDecision
        /// </summary>
        /// <returns>The <see cref="bool"/></returns>
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
    }
}
