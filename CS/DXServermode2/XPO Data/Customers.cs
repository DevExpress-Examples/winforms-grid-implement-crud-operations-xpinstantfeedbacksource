// Developer Express Code Central Example:
// How to implement CRUD operations using XtraGrid and XPInstantFeedbackSource
// 
// This example demonstrates how to implement the Create, Update and Delete
// operations using XPInstantFeedbackSource.
// This example works with the standard
// SQL Northwind database.
// 
// You can find sample updates and versions for different programming languages here:
// http://www.devexpress.com/example=E4505

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
    [Persistent("Customers")]
    public class Customers : XPLiteObject
    {
        public Customers(Session session) : base(session) { }
        [Key, DevExpress.Xpo.DisplayName("ID")]
        public string CustomerID;
        [DevExpress.Xpo.DisplayName("Contact Name")]
        public string ContactName;
        [DevExpress.Xpo.DisplayName("Company Name")]
        public string CompanyName;
        [DevExpress.Xpo.DisplayName("Country")]
        public string Country;
        [DevExpress.Xpo.DisplayName("Address")]
        public string Address;
    }
}
