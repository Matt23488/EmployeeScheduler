using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace EmployeeScheduler.Win
{
    public partial class Container : Form
    {
        private Controller _controller;

        public Control CurrentControl
        {
            get => Controls.Count > 0 ? Controls[0] : null;
            set
            {
                Controls.Clear();
                Controls.Add(value);
                //value.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
                value.Dock = DockStyle.Fill;
            }
        }

        public Container()
        {
            InitializeComponent();
            _controller = new Controller(this);
        }
    }
}
