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
Namespace XPInstantFeedback

    Partial Class Form1

        ''' <summary>
        ''' Required designer variable.
        ''' </summary>
        Private components As System.ComponentModel.IContainer = Nothing

        ''' <summary>
        ''' Clean up any resources being used.
        ''' </summary>
        ''' <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            If disposing AndAlso (Me.components IsNot Nothing) Then
                Me.components.Dispose()
            End If

            MyBase.Dispose(disposing)
        End Sub

#Region "Windows Form Designer generated code"
        ''' <summary>
        ''' Required method for Designer support - do not modify
        ''' the contents of this method with the code editor.
        ''' </summary>
        Private Sub InitializeComponent()
            Me.components = New System.ComponentModel.Container()
            Me.gridControl = New DevExpress.XtraGrid.GridControl()
            Me.gridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
            Me.colCustomerID = New DevExpress.XtraGrid.Columns.GridColumn()
            Me.colContactName = New DevExpress.XtraGrid.Columns.GridColumn()
            Me.colCompanyName = New DevExpress.XtraGrid.Columns.GridColumn()
            Me.colCountry = New DevExpress.XtraGrid.Columns.GridColumn()
            Me.colAddress = New DevExpress.XtraGrid.Columns.GridColumn()
            Me.session1 = New DevExpress.Xpo.Session(Me.components)
            Me.xpInstantFeedbackSource1 = New DevExpress.Xpo.XPInstantFeedbackSource(Me.components)
            Me.simpleButton1 = New DevExpress.XtraEditors.SimpleButton()
            Me.simpleButton2 = New DevExpress.XtraEditors.SimpleButton()
            Me.simpleButton3 = New DevExpress.XtraEditors.SimpleButton()
            CType((Me.gridControl), System.ComponentModel.ISupportInitialize).BeginInit()
            CType((Me.gridView1), System.ComponentModel.ISupportInitialize).BeginInit()
            CType((Me.session1), System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            ' 
            ' gridControl
            ' 
            Me.gridControl.Dock = System.Windows.Forms.DockStyle.Top
            Me.gridControl.Location = New System.Drawing.Point(0, 0)
            Me.gridControl.LookAndFeel.SkinName = "Xmas 2008 Blue"
            Me.gridControl.LookAndFeel.UseDefaultLookAndFeel = False
            Me.gridControl.MainView = Me.gridView1
            Me.gridControl.Name = "gridControl"
            Me.gridControl.Size = New System.Drawing.Size(823, 487)
            Me.gridControl.TabIndex = 0
            Me.gridControl.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gridView1})
            ' 
            ' gridView1
            ' 
            Me.gridView1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
            Me.gridView1.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.colCustomerID, Me.colContactName, Me.colCompanyName, Me.colCountry, Me.colAddress})
            Me.gridView1.GridControl = Me.gridControl
            Me.gridView1.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
            Me.gridView1.Name = "gridView1"
            Me.gridView1.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.Click
            ' 
            ' colCustomerID
            ' 
            Me.colCustomerID.FieldName = "CustomerID"
            Me.colCustomerID.Name = "colCustomerID"
            Me.colCustomerID.Visible = True
            Me.colCustomerID.VisibleIndex = 0
            ' 
            ' colContactName
            ' 
            Me.colContactName.FieldName = "ContactName"
            Me.colContactName.Name = "colContactName"
            Me.colContactName.Visible = True
            Me.colContactName.VisibleIndex = 1
            ' 
            ' colCompanyName
            ' 
            Me.colCompanyName.FieldName = "CompanyName"
            Me.colCompanyName.Name = "colCompanyName"
            Me.colCompanyName.Visible = True
            Me.colCompanyName.VisibleIndex = 2
            ' 
            ' colCountry
            ' 
            Me.colCountry.FieldName = "Country"
            Me.colCountry.Name = "colCountry"
            Me.colCountry.Visible = True
            Me.colCountry.VisibleIndex = 3
            ' 
            ' colAddress
            ' 
            Me.colAddress.FieldName = "Address"
            Me.colAddress.Name = "colAddress"
            Me.colAddress.Visible = True
            Me.colAddress.VisibleIndex = 4
            ' 
            ' session1
            ' 
            Me.session1.TrackPropertiesModifications = False
            ' 
            ' xpInstantFeedbackSource1
            ' 
            Me.xpInstantFeedbackSource1.ObjectType = GetType(XPInstantFeedback.Customers)
            AddHandler Me.xpInstantFeedbackSource1.ResolveSession, New System.EventHandler(Of DevExpress.Xpo.ResolveSessionEventArgs)(AddressOf Me.xpInstantFeedbackSource1_ResolveSession)
            AddHandler Me.xpInstantFeedbackSource1.DismissSession, New System.EventHandler(Of DevExpress.Xpo.ResolveSessionEventArgs)(AddressOf Me.xpInstantFeedbackSource1_DismissSession)
            ' 
            ' simpleButton1
            ' 
            Me.simpleButton1.Location = New System.Drawing.Point(236, 493)
            Me.simpleButton1.Name = "simpleButton1"
            Me.simpleButton1.Size = New System.Drawing.Size(75, 23)
            Me.simpleButton1.TabIndex = 2
            Me.simpleButton1.Text = "Add"
            AddHandler Me.simpleButton1.Click, New System.EventHandler(AddressOf Me.button1_Click)
            ' 
            ' simpleButton2
            ' 
            Me.simpleButton2.Location = New System.Drawing.Point(373, 493)
            Me.simpleButton2.Name = "simpleButton2"
            Me.simpleButton2.Size = New System.Drawing.Size(75, 23)
            Me.simpleButton2.TabIndex = 3
            Me.simpleButton2.Text = "Edit"
            AddHandler Me.simpleButton2.Click, New System.EventHandler(AddressOf Me.button2_Click)
            ' 
            ' simpleButton3
            ' 
            Me.simpleButton3.Location = New System.Drawing.Point(517, 493)
            Me.simpleButton3.Name = "simpleButton3"
            Me.simpleButton3.Size = New System.Drawing.Size(75, 23)
            Me.simpleButton3.TabIndex = 4
            Me.simpleButton3.Text = "Delete"
            AddHandler Me.simpleButton3.Click, New System.EventHandler(AddressOf Me.button3_Click)
            ' 
            ' Form1
            ' 
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6F, 13F)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(823, 528)
            Me.Controls.Add(Me.simpleButton3)
            Me.Controls.Add(Me.simpleButton2)
            Me.Controls.Add(Me.simpleButton1)
            Me.Controls.Add(Me.gridControl)
            Me.LookAndFeel.SkinName = "Office 2013"
            Me.LookAndFeel.UseDefaultLookAndFeel = False
            Me.Name = "Form1"
            Me.Text = "Form1"
            CType((Me.gridControl), System.ComponentModel.ISupportInitialize).EndInit()
            CType((Me.gridView1), System.ComponentModel.ISupportInitialize).EndInit()
            CType((Me.session1), System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)
        End Sub

#End Region
        Private gridControl As DevExpress.XtraGrid.GridControl

        Private gridView1 As DevExpress.XtraGrid.Views.Grid.GridView

        Private colCustomerID As DevExpress.XtraGrid.Columns.GridColumn

        Private colContactName As DevExpress.XtraGrid.Columns.GridColumn

        Private colCompanyName As DevExpress.XtraGrid.Columns.GridColumn

        Private colCountry As DevExpress.XtraGrid.Columns.GridColumn

        Private colAddress As DevExpress.XtraGrid.Columns.GridColumn

        Private session1 As DevExpress.Xpo.Session

        Private xpInstantFeedbackSource1 As DevExpress.Xpo.XPInstantFeedbackSource

        Private simpleButton1 As DevExpress.XtraEditors.SimpleButton

        Private simpleButton2 As DevExpress.XtraEditors.SimpleButton

        Private simpleButton3 As DevExpress.XtraEditors.SimpleButton
    End Class
End Namespace
