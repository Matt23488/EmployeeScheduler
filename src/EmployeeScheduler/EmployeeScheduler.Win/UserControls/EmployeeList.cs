using EmployeeScheduler.Lib.Extensions;
using EmployeeScheduler.Win.Views;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EmployeeScheduler.Win.UserControls
{
    public class EmployeeList : UserControlBase, IEmployeeListView
    {
        public List<Lib.DAL.Employee> Employees
        {
            get => _employeeListBox.Items.OfType<Lib.DAL.Employee>().ToList();
            set => _employeeListBox.DataSource = value;
        }

        public Lib.DAL.Employee SelectedEmployee
        {
            get => _employeeListBox.SelectedItem as Lib.DAL.Employee;
            set => _employeeListBox.SelectedItem = value;
        }

        public event EventHandler AddNewEmployee;
        public event EventHandler EditEmployee;

        public EmployeeList()
        {
            InitializeComponent();
        }

        private void AddEmployeeButton_Click(object sender, EventArgs e)
            => AddNewEmployee?.Invoke(this, EventArgs.Empty);

        private void EditEmployeeButton_Click(object sender, EventArgs e)
            => EditEmployee?.Invoke(this, EventArgs.Empty);

        #region Controls and Setup
        private ListBox _employeeListBox;
        private void InitializeComponent()
        {
            _employeeListBox = new ListBox();
            var employeeLabel = new Label();
            var addButton = new Button();
            var editButton = new Button();
            SuspendLayout();

            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(employeeLabel);
            Controls.Add(_employeeListBox);
            Controls.Add(addButton);
            Controls.Add(editButton);
            Name = nameof(EmployeeList);
            Text = "Employees";

            employeeLabel.AutoSize = true;
            employeeLabel.Location = new Point(12, 9);
            employeeLabel.Name = nameof(employeeLabel);
            employeeLabel.Size = new Size(38, 15);
            employeeLabel.Text = "Employees:";
            employeeLabel.Anchor = AnchorStyles.Top | AnchorStyles.Left;

            _employeeListBox.FormattingEnabled = true;
            _employeeListBox.ItemHeight = 15;
            _employeeListBox.Location = new Point(12, 27);
            _employeeListBox.Name = nameof(_employeeListBox);
            _employeeListBox.Size = new Size(ClientSize.Width - 24, ClientSize.Height - 74);
            _employeeListBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            //_employeeListBox.DisplayMember = nameof(Lib.DTO.Employee.FormattedName);
            _employeeListBox.DrawMode = DrawMode.OwnerDrawFixed;
            // TODO: Look into ListView instead.
            _employeeListBox.DrawItem += (s, e) =>
            {
                if (e.Index < 0) return;
                var employee = Employees[e.Index];
                e.DrawBackground();
                var foreColor = employee.Active ? Color.Black : Color.Red;
                e.Graphics.DrawString(employee.FormattedName(), e.Font, new SolidBrush(foreColor), new PointF(e.Bounds.X, e.Bounds.Y));
                e.DrawFocusRectangle();
            };


            addButton.Location = new Point(12, ClientSize.Height - 32);
            addButton.Name = nameof(addButton);
            addButton.Size = new Size(125, 23);
            addButton.Text = "Add New Employee";
            addButton.UseVisualStyleBackColor = true;
            addButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            addButton.Click += AddEmployeeButton_Click;

            editButton.Location = new Point(addButton.Location.X + addButton.Size.Width + 12, ClientSize.Height - 32);
            editButton.Name = nameof(editButton);
            editButton.Size = new Size(145, 23);
            editButton.Text = "Edit Selected Employee";
            editButton.UseVisualStyleBackColor = true;
            editButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            editButton.Click += EditEmployeeButton_Click;

            ResumeLayout(true);
        }
        #endregion
    }
}
