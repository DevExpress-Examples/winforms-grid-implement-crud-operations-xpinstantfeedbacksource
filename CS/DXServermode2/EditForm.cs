using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace XPInstantFeedback
{
    public partial class EditForm : DevExpress.XtraEditors.XtraForm
    {
        public EditForm(Customers customer)
        {
            InitializeComponent();
            textEdit4.DataBindings.Add("EditValue", customer, "CompanyName");
            textEdit3.DataBindings.Add("EditValue", customer, "ContactName");
            textEdit2.DataBindings.Add("EditValue", customer, "Address");
            textEdit1.DataBindings.Add("EditValue", customer, "Country");    
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

    }
}