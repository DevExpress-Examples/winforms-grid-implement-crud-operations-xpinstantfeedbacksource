using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.Skins;
using DevExpress.LookAndFeel;
using DevExpress.UserSkins;
using DevExpress.XtraEditors;
using DevExpress.Xpo;
using DevExpress.Xpo.DB;


namespace XPInstantFeedback
{
    public partial class Form1 : XtraForm
    {
        public Form1()
        {         
            InitializeComponent();
            gridView1.AsyncCompleted += new EventHandler(gridView1_AsyncCompleted);
            gridView1.OptionsView.WaitAnimationOptions = WaitAnimationOptions.Panel;
            gridControl.DataSource = xpInstantFeedbackSource1;
        }
        Customers customerToEdit;
        EditForm f1;
        private int oldRowsCount;  
        void gridView1_AsyncCompleted(object sender, EventArgs e)
        {
            if (customerToEdit != null && gridView1.DataRowCount > oldRowsCount)
            {
                for (int i = 0; i < gridView1.DataRowCount; i++)
                {
                    if (customerToEdit.CustomerID == gridView1.GetRowCellValue(i, gridView1.Columns["CustomerID"]).ToString())
                    {
                        gridView1.FocusedRowHandle = i;
                        oldRowsCount = gridView1.DataRowCount;
                        customerToEdit = null;
                        break;
                    }
                }
            }
        }         
        private void xpInstantFeedbackSource1_DismissSession(object sender, ResolveSessionEventArgs e)
        {
            IDisposable session1 = e.Session as IDisposable;
            if (session1 != null)
            {
                session1.Dispose();
            }
        }
        private void xpInstantFeedbackSource1_ResolveSession(object sender, ResolveSessionEventArgs e)
        {
            session1 = new Session() { ConnectionString = MSSqlConnectionProvider.GetConnectionString("(local)", "NorthWind") };
            session1.Connect();
            e.Session = session1;
            
        }
    
        private void button1_Click(object sender, EventArgs e)
        {
            session1.BeginTrackingChanges();
            oldRowsCount = gridView1.DataRowCount;
            customerToEdit = CreateCustomer();
            EditCustomer(customerToEdit, "NewCustomer", CloseNewCustomerHandler);
        }
        private Customers CreateCustomer()
        {
            string idString;
            var newCustomer = new Customers(session1);
            while (true)
            {
                idString = GenerateCustomerID();
                if (!String.IsNullOrEmpty(idString))
                {
                    newCustomer.CustomerID = idString;
                    break;
                }
            }             
            return newCustomer;
        }
        private string GenerateCustomerID()
        {
            const int IDLength = 5;
            var result = String.Empty;
            var rnd = new Random();
            bool collisionFlag = false;
            for (var i = 0; i < IDLength; i++)
            {
                result += Convert.ToChar(rnd.Next(65, 90));
            }
            for (int i = 0; i <gridView1.DataRowCount; i++)
            {
                if (result == gridView1.GetRowCellValue(i, gridView1.Columns["CustomerID"]).ToString())
                {
                    collisionFlag = true;
                    break;
                }
            }
            if (collisionFlag)
            {
                return String.Empty;
            }               
            else
                return result;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            session1.BeginTrackingChanges();
            string key = GetCustomerIDByRowHandle(gridView1.FocusedRowHandle);
            customerToEdit = session1.GetObjectByKey(typeof(Customers), key) as Customers;
            EditCustomer(customerToEdit, "EditInfo", CloseEditCustomerHandler);            
        }
        private void EditCustomer(Customers customer, string windowTitle, FormClosingEventHandler closedDelegate)
        {
            f1 = new EditForm(customer) { Text = windowTitle };
            f1.FormClosing += closedDelegate;
            f1.ShowDialog();
        }
        private string GetCustomerIDByRowHandle(int rowHandle)
        {
            return (string)gridView1.GetRowCellValue(rowHandle, "CustomerID");
        }
        private void CloseEditCustomerHandler(object sender, EventArgs e)
        {
            if (((EditForm)sender).DialogResult == DialogResult.OK)
            {
                try
                {
                    customerToEdit.Save();
                    session1.CommitTransaction();                    
                    xpInstantFeedbackSource1.Refresh();                 
                }
                catch (Exception ex)
                {
                    HandleExcepton(ex);
                }
            }
            customerToEdit = null;
        }
        private void CloseNewCustomerHandler(object sender, FormClosingEventArgs e)
        {

            if (((EditForm)sender).DialogResult == DialogResult.OK)
            {
                try
                {
                    customerToEdit.Save();
                    session1.CommitTransaction();
                    xpInstantFeedbackSource1.Refresh();  
                         
                }
                catch (Exception ex)
                {
                    HandleExcepton(ex);
                }
        
                for (int i = 0; i < gridView1.DataRowCount; i++)
                {
                    if (customerToEdit.CustomerID == gridView1.GetRowCellValue(i, gridView1.Columns["CustomerID"]).ToString())
                    {
                        gridView1.FocusedRowHandle = i;
                        break;
                    }
                }
            }
        }
        private void HandleExcepton(Exception ex)
        {
            MessageBox.Show(ex.Message);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            session1.BeginTrackingChanges();
            DeleteCustomer(gridView1.FocusedRowHandle);

        }        
        private void DeleteCustomer(int focusedRowHandle)
        {
            if (focusedRowHandle < 0)
                return;
            if (MessageBox.Show("Do you really want to delete the selected customer?", "Delete Customer", MessageBoxButtons.OKCancel) != DialogResult.OK)
            {
                return;
            }
            Customers victim = session1.GetObjectByKey(typeof(Customers), GetCustomerIDByRowHandle(gridView1.FocusedRowHandle)) as Customers;
            try
            {
                if (victim != null)
                {
                    victim.Delete();
                    session1.CommitTransaction();
                    xpInstantFeedbackSource1.Refresh(); 
                }
            }
            catch (Exception ex)
            {
                HandleExcepton(ex);                 
            }           
            gridView1.FocusedRowHandle = focusedRowHandle;
            customerToEdit = null;
        }
    }
}