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
Imports System
Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms
Imports DevExpress.Skins
Imports DevExpress.XtraEditors
Imports DevExpress.Xpo
Imports DevExpress.Xpo.DB

Namespace XPInstantFeedback

    Public Partial Class Form1
        Inherits XtraForm

        Public Sub New()
            InitializeComponent()
            AddHandler gridView1.AsyncCompleted, New EventHandler(AddressOf gridView1_AsyncCompleted)
            gridView1.OptionsView.WaitAnimationOptions = WaitAnimationOptions.Panel
            gridControl.DataSource = xpInstantFeedbackSource1
        End Sub

        Private customerToEdit As Customers

        Private f1 As EditForm

        Private oldRowsCount As Integer

        Private Sub gridView1_AsyncCompleted(ByVal sender As Object, ByVal e As EventArgs)
            If customerToEdit IsNot Nothing AndAlso gridView1.DataRowCount > oldRowsCount Then
                For i As Integer = 0 To gridView1.DataRowCount - 1
                    If Equals(customerToEdit.CustomerID, gridView1.GetRowCellValue(i, gridView1.Columns("CustomerID")).ToString()) Then
                        gridView1.FocusedRowHandle = i
                        oldRowsCount = gridView1.DataRowCount
                        customerToEdit = Nothing
                        Exit For
                    End If
                Next
            End If
        End Sub

        Private Sub xpInstantFeedbackSource1_DismissSession(ByVal sender As Object, ByVal e As ResolveSessionEventArgs)
            Dim session1 As IDisposable = TryCast(e.Session, IDisposable)
            If session1 IsNot Nothing Then
                session1.Dispose()
            End If
        End Sub

        Private Sub xpInstantFeedbackSource1_ResolveSession(ByVal sender As Object, ByVal e As ResolveSessionEventArgs)
            session1 = New Session() With {.ConnectionString = MSSqlConnectionProvider.GetConnectionString("(local)", "NorthWind")}
            session1.Connect()
            e.Session = session1
        End Sub

        Private Sub button1_Click(ByVal sender As Object, ByVal e As EventArgs)
            session1.BeginTrackingChanges()
            oldRowsCount = gridView1.DataRowCount
            customerToEdit = CreateCustomer()
            EditCustomer(customerToEdit, "NewCustomer", New FormClosingEventHandler(AddressOf CloseNewCustomerHandler))
        End Sub

        Private Function CreateCustomer() As Customers
            Dim idString As String
            Dim newCustomer = New Customers(session1)
            While True
                idString = GenerateCustomerID()
                If Not String.IsNullOrEmpty(idString) Then
                    newCustomer.CustomerID = idString
                    Exit While
                End If
            End While

            Return newCustomer
        End Function

        Private Function GenerateCustomerID() As String
            Const IDLength As Integer = 5
            Dim result = String.Empty
            Dim rnd = New Random()
            Dim collisionFlag As Boolean = False
            For i = 0 To IDLength - 1
                result += Convert.ToChar(rnd.Next(65, 90))
            Next

            For i As Integer = 0 To gridView1.DataRowCount - 1
                If Equals(result, gridView1.GetRowCellValue(i, gridView1.Columns("CustomerID")).ToString()) Then
                    collisionFlag = True
                    Exit For
                End If
            Next

            If collisionFlag Then
                Return String.Empty
            Else
                Return result
            End If
        End Function

        Private Sub button2_Click(ByVal sender As Object, ByVal e As EventArgs)
            session1.BeginTrackingChanges()
            Dim key As String = GetCustomerIDByRowHandle(gridView1.FocusedRowHandle)
            customerToEdit = TryCast(session1.GetObjectByKey(GetType(Customers), key), Customers)
            EditCustomer(customerToEdit, "EditInfo", New FormClosingEventHandler(AddressOf CloseEditCustomerHandler))
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
            If CType(sender, EditForm).DialogResult = DialogResult.OK Then
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
            If CType(sender, EditForm).DialogResult = DialogResult.OK Then
                Try
                    customerToEdit.Save()
                    session1.CommitTransaction()
                    xpInstantFeedbackSource1.Refresh()
                Catch ex As Exception
                    HandleExcepton(ex)
                End Try

                For i As Integer = 0 To gridView1.DataRowCount - 1
                    If Equals(customerToEdit.CustomerID, gridView1.GetRowCellValue(i, gridView1.Columns("CustomerID")).ToString()) Then
                        gridView1.FocusedRowHandle = i
                        Exit For
                    End If
                Next
            End If
        End Sub

        Private Sub HandleExcepton(ByVal ex As Exception)
            MessageBox.Show(ex.Message)
        End Sub

        Private Sub button3_Click(ByVal sender As Object, ByVal e As EventArgs)
            session1.BeginTrackingChanges()
            DeleteCustomer(gridView1.FocusedRowHandle)
        End Sub

        Private Sub DeleteCustomer(ByVal focusedRowHandle As Integer)
            If focusedRowHandle < 0 Then Return
            If MessageBox.Show("Do you really want to delete the selected customer?", "Delete Customer", MessageBoxButtons.OKCancel) <> DialogResult.OK Then
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
