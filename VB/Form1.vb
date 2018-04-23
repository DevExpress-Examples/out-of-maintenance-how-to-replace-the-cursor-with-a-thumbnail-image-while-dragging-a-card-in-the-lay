Imports Microsoft.VisualBasic
Imports System
Imports System.Drawing
Imports System.Collections
Imports System.ComponentModel
Imports System.Windows.Forms
Imports System.Data
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid.Views.Grid.ViewInfo
Imports DevExpress.XtraGrid.Views.Layout
Imports DevExpress.XtraGrid.Views.Layout.ViewInfo
Imports DevExpress.XtraGrid.Views.Layout.Drawing
Imports System.Reflection
Imports DevExpress.Utils.Drawing
Imports DevExpress.XtraLayout
Imports System.Drawing.Imaging
Imports System.Diagnostics
Imports System.Runtime.InteropServices

Namespace SwapRows
	Public Structure IconInfo
		Public fIcon As Boolean
		Public xHotspot As Integer
		Public yHotspot As Integer
		Public hbmMask As IntPtr
		Public hbmColor As IntPtr
	End Structure
	Public Class Form1
		Inherits Form
		<DllImport("user32.dll")> _
		Public Shared Function GetIconInfo(ByVal hIcon As IntPtr, ByRef pIconInfo As IconInfo) As <MarshalAs(UnmanagedType.Bool)> Boolean
		End Function
		<DllImport("user32.dll")> _
		Public Shared Function CreateIconIndirect(ByRef icon As IconInfo) As IntPtr
		End Function
		Private WithEvents gridControl1 As DevExpress.XtraGrid.GridControl
		Private WithEvents layoutView1 As DevExpress.XtraGrid.Views.Layout.LayoutView
		Private layoutViewCard1 As DevExpress.XtraGrid.Views.Layout.LayoutViewCard
		Private repositoryItemPictureEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit
		''' <summary>
		''' Required designer variable.
		''' </summary>
		Private components As System.ComponentModel.Container = Nothing

		Public Sub New()
			'
			' Required for Windows Form Designer support
			'
			InitializeComponent()

			'
			' TODO: Add any constructor code after InitializeComponent call
			'
		End Sub

		''' <summary>
		''' Clean up any resources being used.
		''' </summary>
		Protected Overrides Overloads Sub Dispose(ByVal disposing As Boolean)
			If disposing Then
				If components IsNot Nothing Then
					components.Dispose()
				End If
			End If
			MyBase.Dispose(disposing)
		End Sub

		#Region "Windows Form Designer generated code"
		''' <summary>
		''' Required method for Designer support - do not modify
		''' the contents of this method with the code editor.
		''' </summary>
		Private Sub InitializeComponent()
			Me.gridControl1 = New DevExpress.XtraGrid.GridControl()
			Me.layoutView1 = New DevExpress.XtraGrid.Views.Layout.LayoutView()
			Me.layoutViewCard1 = New DevExpress.XtraGrid.Views.Layout.LayoutViewCard()
			Me.repositoryItemPictureEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit()
			CType(Me.gridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
			CType(Me.layoutView1, System.ComponentModel.ISupportInitialize).BeginInit()
			CType(Me.layoutViewCard1, System.ComponentModel.ISupportInitialize).BeginInit()
			CType(Me.repositoryItemPictureEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
			Me.SuspendLayout()
			' 
			' gridControl1
			' 
			Me.gridControl1.AllowDrop = True
			Me.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill
			Me.gridControl1.Location = New System.Drawing.Point(0, 0)
			Me.gridControl1.MainView = Me.layoutView1
			Me.gridControl1.Name = "gridControl1"
			Me.gridControl1.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() { Me.repositoryItemPictureEdit1})
			Me.gridControl1.Size = New System.Drawing.Size(847, 608)
			Me.gridControl1.TabIndex = 0
			Me.gridControl1.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() { Me.layoutView1})
'			Me.gridControl1.DragOver += New System.Windows.Forms.DragEventHandler(Me.gridControl1_DragOver);
'			Me.gridControl1.DragDrop += New System.Windows.Forms.DragEventHandler(Me.gridControl1_DragDrop);
'			Me.gridControl1.GiveFeedback += New System.Windows.Forms.GiveFeedbackEventHandler(Me.gridControl1_GiveFeedback);
			' 
			' layoutView1
			' 
			Me.layoutView1.GridControl = Me.gridControl1
			Me.layoutView1.Name = "layoutView1"
			Me.layoutView1.OptionsBehavior.Editable = False
			Me.layoutView1.TemplateCard = Me.layoutViewCard1
'			Me.layoutView1.MouseMove += New System.Windows.Forms.MouseEventHandler(Me.layoutView1_MouseMove);
'			Me.layoutView1.MouseDown += New System.Windows.Forms.MouseEventHandler(Me.layoutView1_MouseDown);
			' 
			' layoutViewCard1
			' 
			Me.layoutViewCard1.CustomizationFormText = "layoutViewCard1"
			Me.layoutViewCard1.ExpandButtonLocation = DevExpress.Utils.GroupElementLocation.AfterText
			Me.layoutViewCard1.Name = "layoutViewCard1"
			Me.layoutViewCard1.Text = "layoutViewCard1"
			' 
			' repositoryItemPictureEdit1
			' 
			Me.repositoryItemPictureEdit1.Name = "repositoryItemPictureEdit1"
			' 
			' Form1
			' 
			Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
			Me.ClientSize = New System.Drawing.Size(847, 608)
			Me.Controls.Add(Me.gridControl1)
			Me.Name = "Form1"
			Me.Text = "Form1"
'			Me.Load += New System.EventHandler(Me.Form1_Load);
			CType(Me.gridControl1, System.ComponentModel.ISupportInitialize).EndInit()
			CType(Me.layoutView1, System.ComponentModel.ISupportInitialize).EndInit()
			CType(Me.layoutViewCard1, System.ComponentModel.ISupportInitialize).EndInit()
			CType(Me.repositoryItemPictureEdit1, System.ComponentModel.ISupportInitialize).EndInit()
			Me.ResumeLayout(False)

		End Sub
		#End Region

		''' <summary>
		''' The main entry point for the application.
		''' </summary>
		<STAThread> _
		Shared Sub Main()
			Application.Run(New Form1())
		End Sub

		Private Function GetBitmap(ByVal size As Size, ByVal color As Color, ByVal textColor As Color, ByVal text As String) As Bitmap
			Dim image As New Bitmap(size.Width, size.Height)
			Dim g As Graphics = Graphics.FromImage(image)
			g.FillRectangle(New SolidBrush(textColor), New Rectangle(New Point(Me.Size.Width\2, Me.Size.Height\2), size))
			g.FillEllipse(New SolidBrush(color), New Rectangle(New Point(0, 0), size))
			g.DrawString(text, New Font(Font.FontFamily, size.Height \2, FontStyle.Bold), New SolidBrush(textColor), New Rectangle(New Point(1, 1), size))
			Return image
		End Function


		Private r As New Random()
		Private Function GetRandomColor() As Color
			Return Color.FromArgb(r.Next(255), r.Next(255), r.Next(255))
		End Function

			 Private Function CreateTable(ByVal RowCount As Integer) As DataTable
			Dim tbl As New DataTable()
			tbl.Columns.Add("Name", GetType(String))
			tbl.Columns.Add("Order", GetType(Decimal))
			tbl.Columns.Add("Number", GetType(Integer))
			tbl.Columns.Add("Date", GetType(DateTime))
			tbl.Columns.Add("Image", GetType(Image))
			For i As Integer = 0 To RowCount - 1
				tbl.Rows.Add(New Object() { String.Format("Name{0}", i), i, 3 - i, DateTime.Now.AddDays(i), GetBitmap(New Size(30, 30), GetRandomColor(), GetRandomColor(), i.ToString()) })
			Next i
			Return tbl
			 End Function


		Private Sub Form1_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
			gridControl1.DataSource = CreateTable(20)
			layoutView1.PopulateColumns()
			layoutView1.Columns(OrderFieldName).SortOrder = DevExpress.Data.ColumnSortOrder.Ascending
			layoutView1.OptionsCustomization.AllowSort = False
			layoutView1.OptionsView.ViewMode = LayoutViewMode.MultiColumn
			layoutView1.Columns("Image").ColumnEdit = repositoryItemPictureEdit1
		End Sub

		Private Const OrderFieldName As String = "Order"



		Private downHitInfo As LayoutViewHitInfo

		Private Sub layoutView1_MouseDown(ByVal sender As Object, ByVal e As MouseEventArgs) Handles layoutView1.MouseDown
			Dim view As LayoutView = TryCast(sender, LayoutView)
			downHitInfo = Nothing

			Dim hitInfo As LayoutViewHitInfo = view.CalcHitInfo(New Point(e.X, e.Y))
			If Control.ModifierKeys <> Keys.None Then
				Return
			End If
			If e.Button = MouseButtons.Left AndAlso hitInfo.InCard Then
				downHitInfo = hitInfo
			End If
			GetPicture()
		End Sub


		Public Shared Function Copy(ByVal srcBitmap As Bitmap, ByVal section As Rectangle) As Bitmap
			Dim bmp As New Bitmap(section.Width, section.Height)
			Using g As Graphics = Graphics.FromImage(bmp)
				g.DrawImage(srcBitmap, 0, 0, section, GraphicsUnit.Pixel)
			End Using
			Return bmp
		End Function


		Private Function GetPicture() As Bitmap
			If downHitInfo Is Nothing Then
				Return Nothing
			End If
			Dim layoutCard As LayoutViewCard = downHitInfo.HitCard
			If layoutCard Is Nothing Then
				Return Nothing
			End If
			Dim r As New Rectangle(0, 0, Width * 2, Height * 2)
			Using bmp As New Bitmap(r.Width, r.Height, PixelFormat.Format32bppArgb)
				Using imgGraphics As Graphics = Graphics.FromImage(bmp)
					Using bufferedGraphics As XtraBufferedGraphics = XtraBufferedGraphicsManager.Current.Allocate(imgGraphics, r)
						Dim cardPainter As ObjectPainter = (TryCast(layoutView1, ILayoutControl)).PaintStyle.GetPainter(layoutCard)
						bufferedGraphics.Graphics.Clear(Color.White)
						layoutCard.ViewInfo.Cache = New GraphicsCache(New DXPaintEventArgs(bufferedGraphics.Graphics, r))
						cardPainter.DrawObject(layoutCard.ViewInfo)
						layoutCard.ViewInfo.Cache = Nothing
						bufferedGraphics.Render()
					End Using
					Dim newImage As Bitmap = Copy(bmp, layoutCard.Bounds)
					Return newImage
				End Using
			End Using
		End Function

		Private Sub layoutView1_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs) Handles layoutView1.MouseMove
			Dim view As LayoutView = TryCast(sender, LayoutView)
			If e.Button = MouseButtons.Left AndAlso downHitInfo IsNot Nothing Then
				Dim dragSize As Size = SystemInformation.DragSize
				Dim dragRect As New Rectangle(New Point(downHitInfo.HitPoint.X - dragSize.Width \ 2, downHitInfo.HitPoint.Y - dragSize.Height \ 2), dragSize)

				If (Not dragRect.Contains(New Point(e.X, e.Y))) Then
					view.GridControl.DoDragDrop(downHitInfo, DragDropEffects.All)
					downHitInfo = Nothing
				End If
			End If
		End Sub

		Private Sub gridControl1_DragOver(ByVal sender As Object, ByVal e As DragEventArgs) Handles gridControl1.DragOver
			e.Effect = DragDropEffects.None

			Dim downHitInfo As LayoutViewHitInfo = TryCast(e.Data.GetData(GetType(LayoutViewHitInfo)), LayoutViewHitInfo)
			If downHitInfo IsNot Nothing Then
				Dim grid As GridControl = TryCast(sender, GridControl)
				Dim view As LayoutView = TryCast(grid.MainView, LayoutView)
				Dim hitInfo As LayoutViewHitInfo = view.CalcHitInfo(grid.PointToClient(New Point(e.X, e.Y)))
				If hitInfo.InCard AndAlso hitInfo.RowHandle <> downHitInfo.RowHandle Then
					e.Effect = DragDropEffects.Move
				End If
			End If
		End Sub

		Private Sub gridControl1_DragDrop(ByVal sender As Object, ByVal e As DragEventArgs) Handles gridControl1.DragDrop
			Dim grid As GridControl = TryCast(sender, GridControl)
			Dim view As LayoutView = TryCast(grid.MainView, LayoutView)
			Dim downHitInfo As LayoutViewHitInfo = TryCast(e.Data.GetData(GetType(LayoutViewHitInfo)), LayoutViewHitInfo)
			Dim hitInfo As LayoutViewHitInfo = view.CalcHitInfo(grid.PointToClient(New Point(e.X, e.Y)))
			Dim sourceRow As Integer = downHitInfo.RowHandle
			Dim targetRow As Integer = hitInfo.RowHandle
			MoveRow(sourceRow, targetRow)
		End Sub

		Private Sub MoveRow(ByVal sourceRow As Integer, ByVal targetRow As Integer)
			If sourceRow = targetRow OrElse sourceRow = targetRow + 1 Then
				Return
			End If

			Dim view As LayoutView = layoutView1
			Dim row1 As DataRow = view.GetDataRow(targetRow)
			Dim row2 As DataRow = view.GetDataRow(targetRow + 1)
			Dim dragRow As DataRow = view.GetDataRow(sourceRow)
			Dim val1 As Decimal = CDec(row1(OrderFieldName))
			If row2 Is Nothing Then
				dragRow(OrderFieldName) = val1 + 1
			Else
				Dim val2 As Decimal = CDec(row2(OrderFieldName))
				dragRow(OrderFieldName) = (val1 + val2) / 2
			End If
		End Sub

		Public Shared Function CreateCursor(ByVal bmp As Bitmap) As Cursor
			If bmp Is Nothing Then
				Return Cursors.Default
			End If
			bmp = CType(bmp.GetThumbnailImage(bmp.Width \ 2, bmp.Height\2, Nothing, IntPtr.Zero), Bitmap)
			Dim ptr As IntPtr = bmp.GetHicon()
			Dim tmp As New IconInfo()
			GetIconInfo(ptr, tmp)
			tmp.fIcon = False
			ptr = CreateIconIndirect(tmp)
			Return New Cursor(ptr)
		End Function

		Private Sub gridControl1_GiveFeedback(ByVal sender As Object, ByVal e As GiveFeedbackEventArgs) Handles gridControl1.GiveFeedback
            e.UseDefaultCursors = False
            Cursor.Current = CreateCursor(GetPicture())
		End Sub
	End Class
End Namespace
