using EmployeeScheduler.Win.Utilities;
using EmployeeScheduler.Win.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace EmployeeScheduler.Win
{
    public partial class Container : Form, IViewContainer
    {
        private Controller _controller;

        public IViewBase CurrentView
        {
            get => (Controls.Count > 0 ? Controls[0] : null) as IViewBase;
            set
            {
                var control = value.AsControl;
                Controls.Clear();
                Controls.Add(control);
                control.Dock = DockStyle.Fill;
                Text = control.Text;
            }
        }

        public Container(DependencyResolver dependencyResolver)
        {
            InitializeComponent();
            _controller = new Controller(this, dependencyResolver);
        }
    }

    public interface IViewContainer
    {
        IViewBase CurrentView { get; set; }
    }
}
