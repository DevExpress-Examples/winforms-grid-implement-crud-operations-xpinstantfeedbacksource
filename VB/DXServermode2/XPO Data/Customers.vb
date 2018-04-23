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
	<Persistent("Customers")> _
	Public Class Customers
		Inherits XPLiteObject
		Public Sub New(ByVal session As Session)
			MyBase.New(session)
		End Sub
		<Key, DevExpress.Xpo.DisplayName("ID")> _
		Public CustomerID As String
		<DevExpress.Xpo.DisplayName("Contact Name")> _
		Public ContactName As String
		<DevExpress.Xpo.DisplayName("Company Name")> _
		Public CompanyName As String
		<DevExpress.Xpo.DisplayName("Country")> _
		Public Country As String
		<DevExpress.Xpo.DisplayName("Address")> _
		Public Address As String
	End Class
End Namespace
