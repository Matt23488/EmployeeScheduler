namespace EmployeeScheduler.Win
{
    partial class TestForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.Button backToEmployeeListButton;
            System.Windows.Forms.Label firstNameLabel;
            System.Windows.Forms.Label lastNameLabel;
            System.Windows.Forms.CheckBox isActiveCheckBox;
            System.Windows.Forms.Button saveButton;
            this._firstNameTextBox = new System.Windows.Forms.TextBox();
            this._lastNameTextBox = new System.Windows.Forms.TextBox();
            backToEmployeeListButton = new System.Windows.Forms.Button();
            firstNameLabel = new System.Windows.Forms.Label();
            lastNameLabel = new System.Windows.Forms.Label();
            isActiveCheckBox = new System.Windows.Forms.CheckBox();
            saveButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // backToEmployeeListButton
            // 
            backToEmployeeListButton.Location = new System.Drawing.Point(12, 12);
            backToEmployeeListButton.Name = "backToEmployeeListButton";
            backToEmployeeListButton.Size = new System.Drawing.Size(162, 23);
            backToEmployeeListButton.TabIndex = 0;
            backToEmployeeListButton.Text = "< Back to Employee List";
            backToEmployeeListButton.UseVisualStyleBackColor = true;
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
            // lastNameLabel
            // 
            lastNameLabel.AutoSize = true;
            lastNameLabel.Location = new System.Drawing.Point(13, 73);
            lastNameLabel.Name = "lastNameLabel";
            lastNameLabel.Size = new System.Drawing.Size(66, 15);
            lastNameLabel.TabIndex = 3;
            lastNameLabel.Text = "Last Name:";
            // 
            // isActiveCheckBox
            // 
            isActiveCheckBox.AutoSize = true;
            isActiveCheckBox.Location = new System.Drawing.Point(13, 99);
            isActiveCheckBox.Name = "isActiveCheckBox";
            isActiveCheckBox.Size = new System.Drawing.Size(70, 19);
            isActiveCheckBox.TabIndex = 4;
            isActiveCheckBox.Text = "Is Active";
            isActiveCheckBox.UseVisualStyleBackColor = true;
            // 
            // saveButton
            // 
            saveButton.Location = new System.Drawing.Point(13, 124);
            saveButton.Name = "saveButton";
            saveButton.Size = new System.Drawing.Size(75, 23);
            saveButton.TabIndex = 5;
            saveButton.Text = "Save";
            saveButton.UseVisualStyleBackColor = true;
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
            // _lastNameTextBox
            // 
            this._lastNameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._lastNameTextBox.Location = new System.Drawing.Point(85, 70);
            this._lastNameTextBox.Name = "_lastNameTextBox";
            this._lastNameTextBox.Size = new System.Drawing.Size(208, 23);
            this._lastNameTextBox.TabIndex = 2;
            // 
            // TestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(737, 429);
            this.Controls.Add(saveButton);
            this.Controls.Add(isActiveCheckBox);
            this.Controls.Add(this._lastNameTextBox);
            this.Controls.Add(this._firstNameTextBox);
            this.Controls.Add(lastNameLabel);
            this.Controls.Add(firstNameLabel);
            this.Controls.Add(backToEmployeeListButton);
            this.Name = "TestForm";
            this.Text = "TestForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox _firstNameTextBox;
        private System.Windows.Forms.TextBox _lastNameTextBox;
    }
}