namespace Snippets
{
	partial class MainForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.treeView = new System.Windows.Forms.TreeView();
			this.treeContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.addFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.addItemToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.editToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.removeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.selectFontToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolBoxIcon = new System.Windows.Forms.NotifyIcon(this.components);
			this.iconContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.treeContextMenu.SuspendLayout();
			this.iconContextMenu.SuspendLayout();
			this.SuspendLayout();
			// 
			// treeView
			// 
			this.treeView.AllowDrop = true;
			this.treeView.ContextMenuStrip = this.treeContextMenu;
			this.treeView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.treeView.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.treeView.FullRowSelect = true;
			this.treeView.HideSelection = false;
			this.treeView.Location = new System.Drawing.Point(0, 0);
			this.treeView.Name = "treeView";
			this.treeView.ShowLines = false;
			this.treeView.Size = new System.Drawing.Size(654, 466);
			this.treeView.TabIndex = 1;
			this.treeView.FontChanged += new System.EventHandler(this.treeView_FontChanged);
			this.treeView.DragDrop += new System.Windows.Forms.DragEventHandler(this.treeView_DragDrop);
			this.treeView.DragOver += new System.Windows.Forms.DragEventHandler(this.treeView_DragOver);
			this.treeView.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.treeView_KeyPress);
			this.treeView.DragEnter += new System.Windows.Forms.DragEventHandler(this.treeView_DragEnter);
			this.treeView.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.treeView_ItemDrag);
			this.treeView.MouseDown += new System.Windows.Forms.MouseEventHandler(this.treeView_MouseDown);
			// 
			// treeContextMenu
			// 
			this.treeContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addFolderToolStripMenuItem,
            this.addItemToolStripMenuItem,
            this.editToolStripMenuItem1,
            this.removeToolStripMenuItem,
            this.selectFontToolStripMenuItem});
			this.treeContextMenu.Name = "treeContextMenu";
			this.treeContextMenu.ShowImageMargin = false;
			this.treeContextMenu.Size = new System.Drawing.Size(151, 114);
			this.treeContextMenu.Opening += new System.ComponentModel.CancelEventHandler(this.treeContextMenu_Opening);
			// 
			// addFolderToolStripMenuItem
			// 
			this.addFolderToolStripMenuItem.Name = "addFolderToolStripMenuItem";
			this.addFolderToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F)));
			this.addFolderToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
			this.addFolderToolStripMenuItem.Text = "Add Folder";
			this.addFolderToolStripMenuItem.Click += new System.EventHandler(this.addFolderToolStripMenuItem_Click);
			// 
			// addItemToolStripMenuItem
			// 
			this.addItemToolStripMenuItem.Name = "addItemToolStripMenuItem";
			this.addItemToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
			this.addItemToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
			this.addItemToolStripMenuItem.Text = "Add Item";
			this.addItemToolStripMenuItem.Click += new System.EventHandler(this.addItemToolStripMenuItem_Click);
			// 
			// editToolStripMenuItem1
			// 
			this.editToolStripMenuItem1.Name = "editToolStripMenuItem1";
			this.editToolStripMenuItem1.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E)));
			this.editToolStripMenuItem1.Size = new System.Drawing.Size(150, 22);
			this.editToolStripMenuItem1.Text = "Edit";
			this.editToolStripMenuItem1.Click += new System.EventHandler(this.editToolStripMenuItem_Click);
			// 
			// removeToolStripMenuItem
			// 
			this.removeToolStripMenuItem.Name = "removeToolStripMenuItem";
			this.removeToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D)));
			this.removeToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
			this.removeToolStripMenuItem.Text = "Remove";
			this.removeToolStripMenuItem.Click += new System.EventHandler(this.removeToolStripMenuItem_Click);
			// 
			// selectFontToolStripMenuItem
			// 
			this.selectFontToolStripMenuItem.Name = "selectFontToolStripMenuItem";
			this.selectFontToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
			this.selectFontToolStripMenuItem.Text = "Select Font...";
			this.selectFontToolStripMenuItem.Click += new System.EventHandler(this.selectFontToolStripMenuItem_Click);
			// 
			// toolBoxIcon
			// 
			this.toolBoxIcon.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
			this.toolBoxIcon.BalloonTipText = "Snippets is here.";
			this.toolBoxIcon.BalloonTipTitle = "Snippets";
			this.toolBoxIcon.ContextMenuStrip = this.iconContextMenu;
			this.toolBoxIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("toolBoxIcon.Icon")));
			this.toolBoxIcon.Text = "Snippets";
			this.toolBoxIcon.Visible = true;
			this.toolBoxIcon.DoubleClick += new System.EventHandler(this.toolBoxIcon_DoubleClick);
			// 
			// iconContextMenu
			// 
			this.iconContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
			this.iconContextMenu.Name = "contextMenuStrip1";
			this.iconContextMenu.ShowImageMargin = false;
			this.iconContextMenu.Size = new System.Drawing.Size(79, 26);
			// 
			// exitToolStripMenuItem
			// 
			this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
			this.exitToolStripMenuItem.Size = new System.Drawing.Size(78, 22);
			this.exitToolStripMenuItem.Text = "Exit";
			this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(654, 466);
			this.Controls.Add(this.treeView);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "MainForm";
			this.ShowInTaskbar = false;
			this.Text = "Snippets";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
			this.Load += new System.EventHandler(this.MainForm_Load);
			this.treeContextMenu.ResumeLayout(false);
			this.iconContextMenu.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TreeView treeView;
		private System.Windows.Forms.NotifyIcon toolBoxIcon;
		private System.Windows.Forms.ContextMenuStrip iconContextMenu;
		private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
		private System.Windows.Forms.ContextMenuStrip treeContextMenu;
		private System.Windows.Forms.ToolStripMenuItem addFolderToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem removeToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem addItemToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem selectFontToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem1;
	}
}

