using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraGrid.Views.Layout;
using DevExpress.XtraGrid.Views.Layout.ViewInfo;
using DevExpress.XtraGrid.Views.Layout.Drawing;
using System.Reflection;
using DevExpress.Utils.Drawing;
using DevExpress.XtraLayout;
using System.Drawing.Imaging;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace SwapRows
{
    public struct IconInfo
    {
        public bool fIcon;
        public int xHotspot;
        public int yHotspot;
        public IntPtr hbmMask;
        public IntPtr hbmColor;
    }
    public class Form1 : Form
    {
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetIconInfo(IntPtr hIcon, ref IconInfo pIconInfo);
        [DllImport("user32.dll")]
        public static extern IntPtr CreateIconIndirect(ref IconInfo icon);
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Layout.LayoutView layoutView1;
        private DevExpress.XtraGrid.Views.Layout.LayoutViewCard layoutViewCard1;
        private DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit repositoryItemPictureEdit1;
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components = null;

        public Form1()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

            //
            // TODO: Add any constructor code after InitializeComponent call
            //
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.layoutView1 = new DevExpress.XtraGrid.Views.Layout.LayoutView();
            this.layoutViewCard1 = new DevExpress.XtraGrid.Views.Layout.LayoutViewCard();
            this.repositoryItemPictureEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutViewCard1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemPictureEdit1)).BeginInit();
            this.SuspendLayout();
            // 
            // gridControl1
            // 
            this.gridControl1.AllowDrop = true;
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(0, 0);
            this.gridControl1.MainView = this.layoutView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemPictureEdit1});
            this.gridControl1.Size = new System.Drawing.Size(847, 608);
            this.gridControl1.TabIndex = 0;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.layoutView1});
            this.gridControl1.DragOver += new System.Windows.Forms.DragEventHandler(this.gridControl1_DragOver);
            this.gridControl1.DragDrop += new System.Windows.Forms.DragEventHandler(this.gridControl1_DragDrop);
            this.gridControl1.GiveFeedback += new System.Windows.Forms.GiveFeedbackEventHandler(this.gridControl1_GiveFeedback);
            // 
            // layoutView1
            // 
            this.layoutView1.GridControl = this.gridControl1;
            this.layoutView1.Name = "layoutView1";
            this.layoutView1.OptionsBehavior.Editable = false;
            this.layoutView1.TemplateCard = this.layoutViewCard1;
            this.layoutView1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.layoutView1_MouseMove);
            this.layoutView1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.layoutView1_MouseDown);
            // 
            // layoutViewCard1
            // 
            this.layoutViewCard1.CustomizationFormText = "layoutViewCard1";
            this.layoutViewCard1.ExpandButtonLocation = DevExpress.Utils.GroupElementLocation.AfterText;
            this.layoutViewCard1.Name = "layoutViewCard1";
            this.layoutViewCard1.Text = "layoutViewCard1";
            // 
            // repositoryItemPictureEdit1
            // 
            this.repositoryItemPictureEdit1.Name = "repositoryItemPictureEdit1";
            // 
            // Form1
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(847, 608);
            this.Controls.Add(this.gridControl1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutViewCard1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemPictureEdit1)).EndInit();
            this.ResumeLayout(false);

        }
        #endregion

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.Run(new Form1());
        }

        Bitmap GetBitmap(Size size, Color color, Color textColor, string text)
        {
            Bitmap image = new Bitmap(size.Width, size.Height);
            Graphics g = Graphics.FromImage(image);
            g.FillRectangle(new SolidBrush(textColor), new Rectangle(new Point(Size.Width/2, Size.Height/2), size));
            g.FillEllipse(new SolidBrush(color), new Rectangle(new Point(0, 0), size));
            g.DrawString(text, new Font(Font.FontFamily, size.Height /2, FontStyle.Bold), new SolidBrush(textColor), new Rectangle(new Point(1, 1), size));
            return image;
        }


        Random r = new Random();
        Color GetRandomColor()
        {
            return Color.FromArgb(r.Next(255), r.Next(255), r.Next(255));
        }

             private DataTable CreateTable(int RowCount)
        {
            DataTable tbl = new DataTable();
            tbl.Columns.Add("Name", typeof(string));
            tbl.Columns.Add("Order", typeof(decimal));
            tbl.Columns.Add("Number", typeof(int));
            tbl.Columns.Add("Date", typeof(DateTime));
            tbl.Columns.Add("Image", typeof(Image));
            for (int i = 0; i < RowCount; i++)
                tbl.Rows.Add(new object[] { String.Format("Name{0}", i), i, 3 - i, DateTime.Now.AddDays(i),   GetBitmap(new Size(30, 30), GetRandomColor(), GetRandomColor(), i.ToString()) });
            return tbl;
        }
        

        private void Form1_Load(object sender, EventArgs e)
        {
            gridControl1.DataSource = CreateTable(20);
            layoutView1.PopulateColumns();
            layoutView1.Columns[OrderFieldName].SortOrder = DevExpress.Data.ColumnSortOrder.Ascending;
            layoutView1.OptionsCustomization.AllowSort = false;
            layoutView1.OptionsView.ViewMode = LayoutViewMode.MultiColumn;
            layoutView1.Columns["Image"].ColumnEdit = repositoryItemPictureEdit1;
        }

        const string OrderFieldName = "Order";



        LayoutViewHitInfo downHitInfo;

        private void layoutView1_MouseDown(object sender, MouseEventArgs e)
        {
            LayoutView view = sender as LayoutView;
            downHitInfo = null;

            LayoutViewHitInfo hitInfo = view.CalcHitInfo(new Point(e.X, e.Y));
            if (Control.ModifierKeys != Keys.None) return;
            if (e.Button == MouseButtons.Left && hitInfo.InCard)
                downHitInfo = hitInfo;
            GetPicture();
        }


        static public Bitmap Copy(Bitmap srcBitmap, Rectangle section)
        {
            Bitmap bmp = new Bitmap(section.Width, section.Height);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.DrawImage(srcBitmap, 0, 0, section, GraphicsUnit.Pixel);
            }
            return bmp;
        }


        private Bitmap GetPicture()
        {
            if (downHitInfo == null) return null;
            LayoutViewCard layoutCard = downHitInfo.HitCard;
            if (layoutCard == null) return null;
            Rectangle r = new Rectangle(0, 0, Width * 2, Height * 2);
            using (Bitmap bmp = new Bitmap(r.Width, r.Height, PixelFormat.Format32bppArgb))
            {
                using (Graphics imgGraphics = Graphics.FromImage(bmp))
                {
                    using (XtraBufferedGraphics bufferedGraphics = XtraBufferedGraphicsManager.Current.Allocate(imgGraphics, r))
                    {
                        ObjectPainter cardPainter = (layoutView1 as ILayoutControl).PaintStyle.GetPainter(layoutCard);
                        bufferedGraphics.Graphics.Clear(Color.White);
                        layoutCard.ViewInfo.Cache = new GraphicsCache(new DXPaintEventArgs(bufferedGraphics.Graphics, r));
                        cardPainter.DrawObject(layoutCard.ViewInfo);
                        layoutCard.ViewInfo.Cache = null;
                        bufferedGraphics.Render();
                    }
                    Bitmap newImage = Copy(bmp, layoutCard.Bounds);
                    return newImage;
                }
            }
        }

        private void layoutView1_MouseMove(object sender, MouseEventArgs e)
        {
            LayoutView view = sender as LayoutView;
            if (e.Button == MouseButtons.Left && downHitInfo != null)
            {
                Size dragSize = SystemInformation.DragSize;
                Rectangle dragRect = new Rectangle(new Point(downHitInfo.HitPoint.X - dragSize.Width / 2,
                    downHitInfo.HitPoint.Y - dragSize.Height / 2), dragSize);

                if (!dragRect.Contains(new Point(e.X, e.Y)))
                {
                    view.GridControl.DoDragDrop(downHitInfo, DragDropEffects.All);
                    downHitInfo = null;
                }
            }
        }

        private void gridControl1_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.None;

            LayoutViewHitInfo downHitInfo = e.Data.GetData(typeof(LayoutViewHitInfo)) as LayoutViewHitInfo;
            if (downHitInfo != null)
            {
                GridControl grid = sender as GridControl;
                LayoutView view = grid.MainView as LayoutView;
                LayoutViewHitInfo hitInfo = view.CalcHitInfo(grid.PointToClient(new Point(e.X, e.Y)));
                if (hitInfo.InCard && hitInfo.RowHandle != downHitInfo.RowHandle)
                    e.Effect = DragDropEffects.Move;
            }
        }

        private void gridControl1_DragDrop(object sender, DragEventArgs e)
        {
            GridControl grid = sender as GridControl;
            LayoutView view = grid.MainView as LayoutView;
            LayoutViewHitInfo downHitInfo = e.Data.GetData(typeof(LayoutViewHitInfo)) as LayoutViewHitInfo;
            LayoutViewHitInfo hitInfo = view.CalcHitInfo(grid.PointToClient(new Point(e.X, e.Y)));
            int sourceRow = downHitInfo.RowHandle;
            int targetRow = hitInfo.RowHandle;
            MoveRow(sourceRow, targetRow);
        }

        private void MoveRow(int sourceRow, int targetRow)
        {
            if (sourceRow == targetRow || sourceRow == targetRow + 1) return;

            LayoutView view = layoutView1;
            DataRow row1 = view.GetDataRow(targetRow);
            DataRow row2 = view.GetDataRow(targetRow + 1);
            DataRow dragRow = view.GetDataRow(sourceRow);
            decimal val1 = (decimal)row1[OrderFieldName];
            if (row2 == null)
                dragRow[OrderFieldName] = val1 + 1;
            else
            {
                decimal val2 = (decimal)row2[OrderFieldName];
                dragRow[OrderFieldName] = (val1 + val2) / 2;
            }
        }

        public static Cursor CreateCursor(Bitmap bmp)
        {
            if (bmp == null) return Cursors.Default;
            bmp = (Bitmap)bmp.GetThumbnailImage(bmp.Width / 2, bmp.Height/2, null, IntPtr.Zero);
            IntPtr ptr = bmp.GetHicon();
            IconInfo tmp = new IconInfo();
            GetIconInfo(ptr, ref tmp);
            tmp.fIcon = false;
            ptr = CreateIconIndirect(ref tmp);
            return new Cursor(ptr);
        }

        private void gridControl1_GiveFeedback(object sender, GiveFeedbackEventArgs e)
        {
            e.UseDefaultCursors = false;
            Cursor.Current = CreateCursor(GetPicture());
        }
    }
}
