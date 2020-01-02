using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using EmployeeScheduler.Win.Views;

namespace EmployeeScheduler.Win.UserControls
{
    public partial class Test2 : UserControl, IViewBase
    {
        Control IViewBase.Control => this;

        public event EventHandler TestEvent;

        public Test2()
        {
            InitializeComponent();

            Controls.Add(new Label
            {
                Text = "Test 2"
            });

            var button = new Button
            {
                Text = "Go to Test 1",
                Location = new Point(40, 40),
                Size = new Size(200, 50)
            };

            button.Click += (s, e) =>
            {
                TestEvent?.Invoke(this, EventArgs.Empty);
            };

            Controls.Add(button);
        }
    }
}
