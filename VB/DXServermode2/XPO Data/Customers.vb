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
Imports System.ComponentModel
Imports System.Drawing
Imports DevExpress.Xpo

Namespace XPInstantFeedback

    <Persistent("Customers")>
    Public Class Customers
        Inherits XPLiteObject

        Public Sub New(ByVal session As Session)
            MyBase.New(session)
        End Sub

        <Key, DevExpress.Xpo.DisplayName("ID")>
        Public CustomerID As String

        <DevExpress.Xpo.DisplayName("Contact Name")>
        Public ContactName As String

        <DevExpress.Xpo.DisplayName("Company Name")>
        Public CompanyName As String

        <DevExpress.Xpo.DisplayName("Country")>
        Public Country As String

        <DevExpress.Xpo.DisplayName("Address")>
        Public Address As String
    End Class
End Namespace
