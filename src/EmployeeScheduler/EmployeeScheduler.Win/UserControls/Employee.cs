using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using EmployeeScheduler.Win.Views;
using EmployeeScheduler.Lib.Services;
using EmployeeScheduler.Win.Utilities;

namespace EmployeeScheduler.Win.UserControls
{
    public class Employee : UserControlBase, IEmployeeView
    {
        private Lib.DTO.Employee _employee;

        //Control IViewBase.AsControl => this;
        private IEmployeeView AsView => this;
        Lib.DTO.Employee IEmployeeView.Employee
        {
            get => _employee;
            set
            {
                _employee = value;
                _firstNameTextBox.Text = value.FirstName;
                _lastNameTextBox.Text = value.LastName;
                _isActiveCheckBox.Checked = value.Active;
            }
        }
        // TODO: This might not belong here. Might need to keep track of the state of the
        // view in the controller itself.
        public EditingMode EditingMode { get; set; }

        public event EventHandler Cancel;
        public event EventHandler Save;

        public Employee()
        {
            InitializeComponent();
        }

        private void IsActiveCheckBox_CheckStateChanged(object sender, EventArgs e)
        {
            _employee.Active = _isActiveCheckBox.Checked;
        }

        private void BackToEmployeeListButton_Click(object sender, EventArgs e)
            => Cancel?.Invoke(this, EventArgs.Empty);

        private void SaveButton_Click(object sender, EventArgs e)
        {
            AsView.Employee.FirstName = _firstNameTextBox.Text;
            AsView.Employee.LastName = _lastNameTextBox.Text;
            AsView.Employee.EmailAddress = "";
            Save?.Invoke(this, EventArgs.Empty);
        }

        #region Controls and Setup
        private TextBox _firstNameTextBox;
        private TextBox _lastNameTextBox;
        private CheckBox _isActiveCheckBox;

        private void InitializeComponent()
        {
            _firstNameTextBox = new TextBox();
            _lastNameTextBox = new TextBox();
            _isActiveCheckBox = new CheckBox();
            var backToEmployeeListButton = new Button();
            var firstNameLabel = new Label();
            var lastNameLabel = new Label();
            var saveButton = new Button();
            SuspendLayout();

            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(_firstNameTextBox);
            Controls.Add(_lastNameTextBox);
            Controls.Add(backToEmployeeListButton);
            Controls.Add(firstNameLabel);
            Controls.Add(lastNameLabel);
            Controls.Add(_isActiveCheckBox);
            Controls.Add(saveButton);
            Name = nameof(Employee);
            Text = "Edit Employee";
            // 
            // backToEmployeeListButton
            // 
            backToEmployeeListButton.Location = new System.Drawing.Point(12, 12);
            backToEmployeeListButton.Name = "backToEmployeeListButton";
            backToEmployeeListButton.Size = new System.Drawing.Size(162, 23);
            backToEmployeeListButton.TabIndex = 0;
            backToEmployeeListButton.Text = "< Back to Employee List";
            backToEmployeeListButton.UseVisualStyleBackColor = true;
            backToEmployeeListButton.Click += BackToEmployeeListButton_Click;
            // 
            // firstNameLabel
            // 
            firstNameLabel.AutoSize = true;
            firstNameLabel.Location = new System.Drawing.Point(12, 44);
            firstNameLabel.Name = "firstNameLabel";
            firstNameLabel.Size = new System.Drawing.Size(67, 15);
            firstNameLabel.TabIndex = 1;
            firstNameLabel.Text = "First Name:";
            // 
            // _firstNameTextBox
            // 
            this._firstNameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this._firstNameTextBox.Location = new System.Drawing.Point(85, 41);
            this._firstNameTextBox.Name = "_firstNameTextBox";
            this._firstNameTextBox.Size = new System.Drawing.Size(208, 23);
            this._firstNameTextBox.TabIndex = 2;
            // 
            // lastNameLabel
            // 
            lastNameLabel.AutoSize = true;
            lastNameLabel.Location = new System.Drawing.Point(13, 73);
            lastNameLabel.Name = "lastNameLabel";
            lastNameLabel.Size = new System.Drawing.Size(66, 15);
            lastNameLabel.TabIndex = 3;
            lastNameLabel.Text = "Last Name:";
            // 
            // _lastNameTextBox
            // 
            this._lastNameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this._lastNameTextBox.Location = new System.Drawing.Point(85, 70);
            this._lastNameTextBox.Name = "_lastNameTextBox";
            this._lastNameTextBox.Size = new System.Drawing.Size(208, 23);
            this._lastNameTextBox.TabIndex = 2;
            // 
            // _isActiveCheckBox
            // 
            _isActiveCheckBox.AutoSize = true;
            _isActiveCheckBox.Location = new System.Drawing.Point(13, 99);
            _isActiveCheckBox.Name = "isActiveCheckBox";
            _isActiveCheckBox.Size = new System.Drawing.Size(70, 19);
            _isActiveCheckBox.TabIndex = 4;
            _isActiveCheckBox.Text = "Is Active";
            _isActiveCheckBox.UseVisualStyleBackColor = true;
            _isActiveCheckBox.CheckStateChanged += IsActiveCheckBox_CheckStateChanged;
            // 
            // saveButton
            // 
            saveButton.Location = new System.Drawing.Point(13, 124);
            saveButton.Name = "saveButton";
            saveButton.Size = new System.Drawing.Size(75, 23);
            saveButton.TabIndex = 5;
            saveButton.Text = "Save";
            saveButton.UseVisualStyleBackColor = true;
            saveButton.Click += SaveButton_Click;

            ResumeLayout(true);
        }
        #endregion
    }
}
