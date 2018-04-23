' Developer Express Code Central Example:
' How to implement CRUD operations using XtraGrid and XPInstantFeedbackSource
' 
' This example demonstrates how to implement the Create, Update and Delete
' operations using XPInstantFeedbackSource.
' This example works with the standard
' SQL Northwind database.
' 
' You can find sample updates and versions for different programming languages here:
' http://www.devexpress.com/example=E4505

Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Text
Imports System.Windows.Forms
Imports DevExpress.XtraEditors

Namespace XPInstantFeedback
	Partial Public Class EditForm
		Inherits DevExpress.XtraEditors.XtraForm
		Public Sub New(ByVal customer As Customers)
			InitializeComponent()
			textEdit4.DataBindings.Add("EditValue", customer, "CompanyName")
			textEdit3.DataBindings.Add("EditValue", customer, "ContactName")
			textEdit2.DataBindings.Add("EditValue", customer, "Address")
			textEdit1.DataBindings.Add("EditValue", customer, "Country")
		End Sub

		Private Sub simpleButton1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles simpleButton1.Click
			DialogResult = System.Windows.Forms.DialogResult.OK
		End Sub

		Private Sub simpleButton2_Click(ByVal sender As Object, ByVal e As EventArgs) Handles simpleButton2.Click
			DialogResult = System.Windows.Forms.DialogResult.Cancel
		End Sub

	End Class
End Namespace