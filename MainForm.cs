using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Xml.Serialization;
using System.IO;
using System.Text.RegularExpressions;

namespace Snippets
{
	public partial class MainForm : Form
	{
		[DllImport("user32.dll")]
		static extern IntPtr GetForegroundWindow();

		[DllImport("user32.dll")]
		static extern bool SetForegroundWindow(IntPtr handle);

		[DllImport("User32.dll")]
		static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int x, int y, int cx, int cy, uint flags);

		const uint SWP_NOSIZE = 0x0001;
		const uint SWP_NOMOVE = 0x0002;
		const int HWND_TOP = 0;

		UserActivityHook m_activityHook;
		IntPtr m_previousWindowHandle;

		readonly string m_dataFile = Application.UserAppDataPath + "\\snippets-data.xml";

		Font m_folderFont;

		[Serializable]
		public class TreeNodeData
		{
			public string text;
			public bool expanded;
			public bool isFolder;
			public List<TreeNodeData> children;
		}

		public MainForm()
		{
			InitializeComponent();

			RestoreTreeView();

			m_activityHook = new UserActivityHook(false, true);
			m_activityHook.KeyDown += new KeyEventHandler(HookKeyDown);

			if (Properties.Settings.Default.showIntroBalloon == true)
			{
				toolBoxIcon.ShowBalloonTip(1000);
				Properties.Settings.Default.showIntroBalloon = false;
			}

		}

		private void MainForm_Load(object sender, EventArgs e)
		{
			treeView.Font = Properties.Settings.Default.treeViewFont;
			treeView_FontChanged(null, null);
		}


		void RestoreTreeView()
		{
			treeView.Nodes.Clear();

			if (!File.Exists(m_dataFile))
				return;

			List<TreeNodeData> root = null;

			try
			{
				XmlSerializer serializer = new XmlSerializer(typeof(List<TreeNodeData>));
				FileStream fs = new FileStream(m_dataFile, FileMode.Open);
				root = (List<TreeNodeData>)serializer.Deserialize(fs);
				fs.Close();
			}
			catch (Exception e)
			{
				string str = String.Format("Failed to read saved snippets from {0}\n{1}", m_dataFile, e.ToString());
				MessageBox.Show(str, "Failed to read saved snippets", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}

			if(root != null)
				AddTreeNodes(treeView.Nodes, root);
		}

		void AddTreeNodes(TreeNodeCollection tnc, List<TreeNodeData> list)
		{
			foreach(TreeNodeData data in list)
			{
				TreeNode n = tnc.Add(data.text);
				n.Tag = data.isFolder;

				if (data.isFolder)
				{
					n.NodeFont = m_folderFont;
					n.Text = data.text; // Reset text to accommodate new font
					if (data.children != null)
						AddTreeNodes(n.Nodes, data.children);
					if (data.expanded)
						n.Expand();
				}
			}
		}

		void SaveTreeView()
		{
			List<TreeNodeData> list = GetTreeNodeData(treeView.Nodes);

			try
			{
				XmlSerializer serializer = new XmlSerializer(typeof(List<TreeNodeData>));
				StreamWriter writer = new StreamWriter(m_dataFile);
				serializer.Serialize(writer, list);
				writer.Close();
			}
			catch(Exception e)
			{
				string str = String.Format("Failed to save snippets to {0}\n{1}", m_dataFile, e.ToString());
				MessageBox.Show(str, "Failed to save snippets", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		List<TreeNodeData> GetTreeNodeData(TreeNodeCollection tnc)
		{
			if (tnc.Count == 0)
				return null;

			List<TreeNodeData> ret = new List<TreeNodeData>(tnc.Count);

			for (TreeNode n = tnc[0]; n != null; n = n.NextNode)
			{
				TreeNodeData d = new TreeNodeData();
				d.text = n.Text;
				d.isFolder = (bool)n.Tag;
				if (d.isFolder == true)
				{
					d.expanded = n.IsExpanded;
					if (n.Nodes != null && n.Nodes.Count > 0)
					{
						d.children = GetTreeNodeData(n.Nodes);
					}
				}

				ret.Add(d);
			}

			return ret;
		}

		void HookKeyDown(object sender, KeyEventArgs e)
		{
			//Console.WriteLine("DOWN {0} ** {1}", e.KeyCode, e.Modifiers);

			if (e.KeyCode == Keys.Q && e.Modifiers == Keys.Control)
			{
				m_previousWindowHandle = GetForegroundWindow();

				if (m_previousWindowHandle == this.Handle)
				{
					m_previousWindowHandle = IntPtr.Zero;
					return;
				}

				Show();
				Activate();
				treeView.Select();

				e.Handled = true;
			}
		}

		private void treeView_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == '\r' && m_previousWindowHandle != IntPtr.Zero)
			{
				if (treeView.SelectedNode == null)
					return;

				string text = treeView.SelectedNode.Text;

				Hide();

				e.Handled = true;

				SetWindowPos(m_previousWindowHandle, (IntPtr)HWND_TOP, 0, 0, 0, 0, SWP_NOMOVE | SWP_NOSIZE);

				text = EscapeSendKeys(text);

				SendKeys.SendWait(text);

				m_previousWindowHandle = IntPtr.Zero;
			}
			else if (e.KeyChar == 0x1b)
			{
				Hide();
				m_previousWindowHandle = IntPtr.Zero;
				e.Handled = true;
			}
		}

		string EscapeSendKeys(string str)
		{
			StringBuilder sb = new StringBuilder(str.Length * 2);

			for (int i = 0; i < str.Length; i++)
			{
				switch (str[i])
				{
					case '+':
					case '^':
					case '%':
					case '~':
					case '(':
					case ')':
					case '}':
						sb.Append("{");
						sb.Append(str[i]);
						sb.Append("}");
						break;

					case '{':
						if(str.IndexOf("{ENTER}", i) == i)
						{
							sb.Append("{ENTER}");
							i += "{ENTER}".Length - 1;
						}
						else
							sb.Append(str[i]);
						break;

					default:
						sb.Append(str[i]);
						break;
				}
			}

			return sb.ToString();
		}

		private void Form1_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (e.CloseReason == CloseReason.UserClosing)
			{
				e.Cancel = true;
				Hide();
			}
		}

		private void toolBoxIcon_DoubleClick(object sender, EventArgs e)
		{
			Show();
			Activate();
			treeView.Select();
		}


		/* Menu items */

		private void exitToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if(m_activityHook != null)
				m_activityHook.Stop();
			SaveTreeView();
			toolBoxIcon.Visible = false;
			Application.Exit();
		}

		private void addFolderToolStripMenuItem_Click(object sender, EventArgs e)
		{
			TreeNode n = new TreeNode("New Folder");
			n.NodeFont = m_folderFont;
			n.Tag = true;

			if (treeView.SelectedNode == null)
			{
				treeView.Nodes.Add(n);
			}
			else if ((bool)treeView.SelectedNode.Tag == true)
			{
				treeView.SelectedNode.Nodes.Add(n);
				treeView.SelectedNode.Expand();
			}
			else if (treeView.SelectedNode.Parent != null)
			{
				treeView.SelectedNode.Parent.Nodes.Add(n);
				treeView.SelectedNode.Parent.Expand();
			}
			else
			{
				treeView.Nodes.Add(n);
			}

			treeView.SelectedNode = n;
			n.Text = n.Text; // Set text again to accommodate font change
			SaveTreeView();
		}

		private void addItemToolStripMenuItem_Click(object sender, EventArgs e)
		{
			TreeNode n = new TreeNode("New Item");
			n.Tag = false;

			if (treeView.SelectedNode == null)
			{
				treeView.Nodes.Add(n);
			}
			else if ((bool)treeView.SelectedNode.Tag == true)
			{
				treeView.SelectedNode.Nodes.Add(n);
				treeView.SelectedNode.Expand();
			}
			else if (treeView.SelectedNode.Parent != null)
			{
				treeView.SelectedNode.Parent.Nodes.Add(n);
				treeView.SelectedNode.Parent.Expand();
			}
			else
			{
				treeView.Nodes.Add(n);
			}

			treeView.SelectedNode = n;
			SaveTreeView();
		}
		
		private void removeToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (treeView.SelectedNode != null)
			{
				DialogResult res = MessageBox.Show("Are you sure you want to delete this item?", "Confirm Deletion", 
					MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);

				if (res == DialogResult.OK)
				{
					treeView.Nodes.Remove(treeView.SelectedNode);
					SaveTreeView();
				}
			}
		}

		private void selectFontToolStripMenuItem_Click(object sender, EventArgs e)
		{
			FontDialog d = new FontDialog();
			d.Font = treeView.Font;
			if (d.ShowDialog(this) == DialogResult.OK)
			{
				treeView.Font = d.Font;
			}
			d.Dispose();
		}

		private void editToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (treeView.SelectedNode != null)
			{
				EditDialog dlg = new EditDialog();

				string str = treeView.SelectedNode.Text;
				str = str.Replace(@"{ENTER}", Environment.NewLine);
				dlg.EditText = str;
				dlg.EditFont = treeView.Font;
				if (dlg.ShowDialog(this) == DialogResult.OK)
				{
					str = dlg.EditText;
					str = str.Replace(Environment.NewLine, @"{ENTER}");
					treeView.SelectedNode.Text = str;
				}
				dlg.Dispose();
			}
		}

		/* Mouse events */

		private void treeView_MouseDown(object sender, MouseEventArgs e)
		{
			TreeNode n = treeView.GetNodeAt(e.Location);
			treeView.SelectedNode = n;
		}

		private void treeContextMenu_Opening(object sender, CancelEventArgs e)
		{
			bool child = treeView.SelectedNode != null ? true : false;
			editToolStripMenuItem1.Enabled = child;
			removeToolStripMenuItem.Enabled = child;
		}


		/* Dragn'drop */

		private void treeView_ItemDrag(object sender, ItemDragEventArgs e)
		{
			DoDragDrop(e.Item, DragDropEffects.Move);
		}

		private void treeView_DragEnter(object sender, DragEventArgs e)
		{
			e.Effect = e.AllowedEffect;
		}

		private void treeView_DragOver(object sender, DragEventArgs e)
		{
			Point targetPoint = treeView.PointToClient(new Point(e.X, e.Y));
			treeView.SelectedNode = treeView.GetNodeAt(targetPoint);
		}

		private void treeView_DragDrop(object sender, DragEventArgs e)
		{
			Point targetPoint = treeView.PointToClient(new Point(e.X, e.Y));
			TreeNode targetNode = treeView.GetNodeAt(targetPoint);

			TreeNode draggedNode = (TreeNode)e.Data.GetData(typeof(TreeNode));

			if (targetNode == null)
			{
				draggedNode.Remove();
				treeView.Nodes.Add(draggedNode);
			}
			else if (!draggedNode.Equals(targetNode) && !ContainsNode(draggedNode, targetNode))
			{
				draggedNode.Remove();

				if ((bool)targetNode.Tag == true)
				{
					targetNode.Nodes.Insert(0, draggedNode);
					targetNode.Expand();
				}
				else
				{
					TreeNodeCollection c;

					if(targetNode.Parent != null)
						c = targetNode.Parent.Nodes;
					else
						c = treeView.Nodes;

					c.Insert(targetNode.Index + 1, draggedNode);
				}

				treeView.SelectedNode = draggedNode;

				SaveTreeView();
			}
		}

		// Determine whether one node is a parent 
		// or ancestor of a second node.
		private bool ContainsNode(TreeNode node1, TreeNode node2)
		{
			// Check the parent node of the second node.
			if (node2.Parent == null) return false;
			if (node2.Parent.Equals(node1)) return true;

			// If the parent node is not null or equal to the first node, 
			// call the ContainsNode method recursively using the parent of 
			// the second node.
			return ContainsNode(node1, node2.Parent);
		}

		private void treeView_FontChanged(object sender, EventArgs e)
		{
			m_folderFont = new Font(treeView.Font, FontStyle.Underline);
			Properties.Settings.Default.treeViewFont = treeView.Font;

			ReSetFonts(treeView.Nodes);
		}

		void ReSetFonts(TreeNodeCollection c)
		{
			foreach (TreeNode n in c)
			{
				if ((bool)n.Tag == true)
				{
					n.NodeFont = m_folderFont;

					ReSetFonts(n.Nodes);
				}
			}
		}

	}
}
