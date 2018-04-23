Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Text
Imports System.Windows.Forms
Imports DevExpress.Skins
Imports DevExpress.LookAndFeel
Imports DevExpress.UserSkins
Imports DevExpress.XtraEditors
Imports DevExpress.Xpo
Imports DevExpress.Xpo.DB


Namespace XPInstantFeedback
	Partial Public Class Form1
		Inherits XtraForm
		Public Sub New()
			InitializeComponent()
			AddHandler gridView1.AsyncCompleted, AddressOf gridView1_AsyncCompleted
			gridView1.OptionsView.WaitAnimationOptions = WaitAnimationOptions.Panel
			gridControl.DataSource = xpInstantFeedbackSource1
		End Sub
		Private customerToEdit As Customers
		Private f1 As EditForm
		Private oldRowsCount As Integer
		Private Sub gridView1_AsyncCompleted(ByVal sender As Object, ByVal e As EventArgs)
			If customerToEdit IsNot Nothing AndAlso gridView1.DataRowCount > oldRowsCount Then
				For i As Integer = 0 To gridView1.DataRowCount - 1
					If customerToEdit.CustomerID = gridView1.GetRowCellValue(i, gridView1.Columns("CustomerID")).ToString() Then
						gridView1.FocusedRowHandle = i
						oldRowsCount = gridView1.DataRowCount
						customerToEdit = Nothing
						Exit For
					End If
				Next i
			End If
		End Sub
		Private Sub xpInstantFeedbackSource1_DismissSession(ByVal sender As Object, ByVal e As ResolveSessionEventArgs) Handles xpInstantFeedbackSource1.DismissSession
			Dim session1 As IDisposable = TryCast(e.Session, IDisposable)
			If session1 IsNot Nothing Then
				session1.Dispose()
			End If
		End Sub
		Private Sub xpInstantFeedbackSource1_ResolveSession(ByVal sender As Object, ByVal e As ResolveSessionEventArgs) Handles xpInstantFeedbackSource1.ResolveSession
			session1 = New Session() With {.ConnectionString = MSSqlConnectionProvider.GetConnectionString("(local)", "NorthWind")}
			session1.Connect()
			e.Session = session1

		End Sub

		Private Sub button1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles simpleButton1.Click
			session1.BeginTrackingChanges()
			oldRowsCount = gridView1.DataRowCount
			customerToEdit = CreateCustomer()
			EditCustomer(customerToEdit, "NewCustomer", AddressOf CloseNewCustomerHandler)
		End Sub
		Private Function CreateCustomer() As Customers
			Dim idString As String
			Dim newCustomer = New Customers(session1)
			Do
				idString = GenerateCustomerID()
				If (Not String.IsNullOrEmpty(idString)) Then
					newCustomer.CustomerID = idString
					Exit Do
				End If
			Loop
			Return newCustomer
		End Function
		Private Function GenerateCustomerID() As String
			Const IDLength As Integer = 5
			Dim result = String.Empty
			Dim rnd = New Random()
			Dim collisionFlag As Boolean = False
			For i = 0 To IDLength - 1
				result += Convert.ToChar(rnd.Next(65, 90))
			Next i
			For i As Integer = 0 To gridView1.DataRowCount - 1
				If result Is gridView1.GetRowCellValue(i, gridView1.Columns("CustomerID")).ToString() Then
					collisionFlag = True
					Exit For
				End If
			Next i
			If collisionFlag Then
				Return String.Empty
			Else
				Return result
			End If
		End Function
		Private Sub button2_Click(ByVal sender As Object, ByVal e As EventArgs) Handles simpleButton2.Click
			session1.BeginTrackingChanges()
			Dim key As String = GetCustomerIDByRowHandle(gridView1.FocusedRowHandle)
			customerToEdit = TryCast(session1.GetObjectByKey(GetType(Customers), key), Customers)
			EditCustomer(customerToEdit, "EditInfo", AddressOf CloseEditCustomerHandler)
		End Sub
		Private Sub EditCustomer(ByVal customer As Customers, ByVal windowTitle As String, ByVal closedDelegate As FormClosingEventHandler)
			f1 = New EditForm(customer) With {.Text = windowTitle}
			AddHandler f1.FormClosing, closedDelegate
			f1.ShowDialog()
		End Sub
		Private Function GetCustomerIDByRowHandle(ByVal rowHandle As Integer) As String
			Return CStr(gridView1.GetRowCellValue(rowHandle, "CustomerID"))
		End Function
		Private Sub CloseEditCustomerHandler(ByVal sender As Object, ByVal e As EventArgs)
			If (CType(sender, EditForm)).DialogResult = System.Windows.Forms.DialogResult.OK Then
				Try
					customerToEdit.Save()
					session1.CommitTransaction()
					xpInstantFeedbackSource1.Refresh()
				Catch ex As Exception
					HandleExcepton(ex)
				End Try
			End If
			customerToEdit = Nothing
		End Sub
		Private Sub CloseNewCustomerHandler(ByVal sender As Object, ByVal e As FormClosingEventArgs)

			If (CType(sender, EditForm)).DialogResult = System.Windows.Forms.DialogResult.OK Then
				Try
					customerToEdit.Save()
					session1.CommitTransaction()
					xpInstantFeedbackSource1.Refresh()

				Catch ex As Exception
					HandleExcepton(ex)
				End Try

				For i As Integer = 0 To gridView1.DataRowCount - 1
					If customerToEdit.CustomerID = gridView1.GetRowCellValue(i, gridView1.Columns("CustomerID")).ToString() Then
						gridView1.FocusedRowHandle = i
						Exit For
					End If
				Next i
			End If
		End Sub
		Private Sub HandleExcepton(ByVal ex As Exception)
			MessageBox.Show(ex.Message)
		End Sub

		Private Sub button3_Click(ByVal sender As Object, ByVal e As EventArgs) Handles simpleButton3.Click
			session1.BeginTrackingChanges()
			DeleteCustomer(gridView1.FocusedRowHandle)

		End Sub
		Private Sub DeleteCustomer(ByVal focusedRowHandle As Integer)
			If focusedRowHandle < 0 Then
				Return
			End If
			If MessageBox.Show("Do you really want to delete the selected customer?", "Delete Customer", MessageBoxButtons.OKCancel) <> System.Windows.Forms.DialogResult.OK Then
				Return
			End If
			Dim victim As Customers = TryCast(session1.GetObjectByKey(GetType(Customers), GetCustomerIDByRowHandle(gridView1.FocusedRowHandle)), Customers)
			Try
				If victim IsNot Nothing Then
					victim.Delete()
					session1.CommitTransaction()
					xpInstantFeedbackSource1.Refresh()
				End If
			Catch ex As Exception
				HandleExcepton(ex)
			End Try
			gridView1.FocusedRowHandle = focusedRowHandle
			customerToEdit = Nothing
		End Sub
	End Class
End Namespace