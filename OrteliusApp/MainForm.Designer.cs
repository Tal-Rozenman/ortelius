/*
 * Created by SharpDevelop.
 * User: moe
 * Date: 01-06-2007
 * Time: 19:38
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace Ortelius
{
	partial class MainForm : System.Windows.Forms.Form
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.button1 = new System.Windows.Forms.Button();
			this.progressBar1 = new System.Windows.Forms.ProgressBar();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.filesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.newProjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.saveProjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.loadProjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.buildToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.addClassFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.addFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.chooseDestinationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.buildDocumentationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.goToDocumentationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.button2 = new System.Windows.Forms.Button();
			this.listBox1 = new System.Windows.Forms.ListBox();
			this.button3 = new System.Windows.Forms.Button();
			this.xmlPath = new System.Windows.Forms.TextBox();
			this.button4 = new System.Windows.Forms.Button();
			this.styleCB = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.BuildButton = new System.Windows.Forms.Button();
			this.label3 = new System.Windows.Forms.Label();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.pictureBox2 = new System.Windows.Forms.PictureBox();
			this.pictureBox3 = new System.Windows.Forms.PictureBox();
			this.label4 = new System.Windows.Forms.Label();
			this.introText = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.introHeader = new System.Windows.Forms.TextBox();
			this.showAfterBuildCB = new System.Windows.Forms.CheckBox();
			this.menuStrip1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
			this.SuspendLayout();
			// 
			// button1
			// 
			this.button1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button1.BackgroundImage")));
			this.button1.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.button1.ForeColor = System.Drawing.Color.White;
			this.button1.Location = new System.Drawing.Point(352, 564);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(127, 22);
			this.button1.TabIndex = 16;
			this.button1.Text = "Add Class file";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.AddASFile);
			// 
			// progressBar1
			// 
			this.progressBar1.Enabled = false;
			this.progressBar1.Location = new System.Drawing.Point(75, 679);
			this.progressBar1.Name = "progressBar1";
			this.progressBar1.Size = new System.Drawing.Size(400, 10);
			this.progressBar1.Step = 1;
			this.progressBar1.TabIndex = 2;
			// 
			// menuStrip1
			// 
			this.menuStrip1.BackColor = System.Drawing.Color.Transparent;
			this.menuStrip1.Dock = System.Windows.Forms.DockStyle.None;
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.filesToolStripMenuItem,
									this.buildToolStripMenuItem});
			this.menuStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
			this.menuStrip1.Location = new System.Drawing.Point(4, 22);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
			this.menuStrip1.Size = new System.Drawing.Size(139, 24);
			this.menuStrip1.TabIndex = 3;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// filesToolStripMenuItem
			// 
			this.filesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.newProjectToolStripMenuItem,
									this.saveToolStripMenuItem,
									this.saveProjectToolStripMenuItem,
									this.loadProjectToolStripMenuItem,
									this.closeToolStripMenuItem});
			this.filesToolStripMenuItem.Name = "filesToolStripMenuItem";
			this.filesToolStripMenuItem.Size = new System.Drawing.Size(40, 20);
			this.filesToolStripMenuItem.Text = "Files";
			// 
			// newProjectToolStripMenuItem
			// 
			this.newProjectToolStripMenuItem.Name = "newProjectToolStripMenuItem";
			this.newProjectToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
			this.newProjectToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
			this.newProjectToolStripMenuItem.Text = "New project";
			this.newProjectToolStripMenuItem.Click += new System.EventHandler(this.NewProjectToolStripMenuItemClick);
			// 
			// saveToolStripMenuItem
			// 
			this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
			this.saveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
			this.saveToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
			this.saveToolStripMenuItem.Text = "Save";
			this.saveToolStripMenuItem.Click += new System.EventHandler(this.SaveToolStripMenuItemClick);
			// 
			// saveProjectToolStripMenuItem
			// 
			this.saveProjectToolStripMenuItem.Name = "saveProjectToolStripMenuItem";
			this.saveProjectToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
			this.saveProjectToolStripMenuItem.Text = "Save project as...";
			this.saveProjectToolStripMenuItem.Click += new System.EventHandler(this.SaveProjectToolStripMenuItemClick);
			// 
			// loadProjectToolStripMenuItem
			// 
			this.loadProjectToolStripMenuItem.Name = "loadProjectToolStripMenuItem";
			this.loadProjectToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
			this.loadProjectToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
			this.loadProjectToolStripMenuItem.Text = "Open project";
			this.loadProjectToolStripMenuItem.Click += new System.EventHandler(this.LoadProjectToolStripMenuItemClick);
			// 
			// closeToolStripMenuItem
			// 
			this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
			this.closeToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Q)));
			this.closeToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
			this.closeToolStripMenuItem.Text = "Exit";
			this.closeToolStripMenuItem.Click += new System.EventHandler(this.CloseToolStripMenuItemClick);
			// 
			// buildToolStripMenuItem
			// 
			this.buildToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.addClassFileToolStripMenuItem,
									this.addFolderToolStripMenuItem,
									this.chooseDestinationToolStripMenuItem,
									this.buildDocumentationToolStripMenuItem,
									this.goToDocumentationToolStripMenuItem});
			this.buildToolStripMenuItem.Name = "buildToolStripMenuItem";
			this.buildToolStripMenuItem.Size = new System.Drawing.Size(91, 20);
			this.buildToolStripMenuItem.Text = "Documentation";
			// 
			// addClassFileToolStripMenuItem
			// 
			this.addClassFileToolStripMenuItem.Name = "addClassFileToolStripMenuItem";
			this.addClassFileToolStripMenuItem.Size = new System.Drawing.Size(219, 22);
			this.addClassFileToolStripMenuItem.Text = "Add Class file";
			this.addClassFileToolStripMenuItem.Click += new System.EventHandler(this.AddASFile);
			// 
			// addFolderToolStripMenuItem
			// 
			this.addFolderToolStripMenuItem.Name = "addFolderToolStripMenuItem";
			this.addFolderToolStripMenuItem.Size = new System.Drawing.Size(219, 22);
			this.addFolderToolStripMenuItem.Text = "Add folder with class files";
			this.addFolderToolStripMenuItem.Click += new System.EventHandler(this.AddFolder);
			// 
			// chooseDestinationToolStripMenuItem
			// 
			this.chooseDestinationToolStripMenuItem.Name = "chooseDestinationToolStripMenuItem";
			this.chooseDestinationToolStripMenuItem.Size = new System.Drawing.Size(219, 22);
			this.chooseDestinationToolStripMenuItem.Text = "Choose destination";
			this.chooseDestinationToolStripMenuItem.Click += new System.EventHandler(this.ChooseDestination);
			// 
			// buildDocumentationToolStripMenuItem
			// 
			this.buildDocumentationToolStripMenuItem.Name = "buildDocumentationToolStripMenuItem";
			this.buildDocumentationToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.B)));
			this.buildDocumentationToolStripMenuItem.Size = new System.Drawing.Size(219, 22);
			this.buildDocumentationToolStripMenuItem.Text = "Build documentation";
			this.buildDocumentationToolStripMenuItem.Click += new System.EventHandler(this.BuildDocumentationToolStripMenuItemClick);
			// 
			// goToDocumentationToolStripMenuItem
			// 
			this.goToDocumentationToolStripMenuItem.Name = "goToDocumentationToolStripMenuItem";
			this.goToDocumentationToolStripMenuItem.Size = new System.Drawing.Size(219, 22);
			this.goToDocumentationToolStripMenuItem.Text = "Go to documentation";
			this.goToDocumentationToolStripMenuItem.Click += new System.EventHandler(this.GoToDocumentationToolStripMenuItemClick);
			// 
			// button2
			// 
			this.button2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button2.BackgroundImage")));
			this.button2.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.button2.ForeColor = System.Drawing.Color.White;
			this.button2.Location = new System.Drawing.Point(76, 591);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(110, 24);
			this.button2.TabIndex = 6;
			this.button2.Text = "Choose destination";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new System.EventHandler(this.ChooseDestination);
			// 
			// listBox1
			// 
			this.listBox1.FormattingEnabled = true;
			this.listBox1.Location = new System.Drawing.Point(74, 235);
			this.listBox1.Name = "listBox1";
			this.listBox1.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
			this.listBox1.Size = new System.Drawing.Size(404, 329);
			this.listBox1.TabIndex = 7;
			// 
			// button3
			// 
			this.button3.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button3.BackgroundImage")));
			this.button3.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.button3.ForeColor = System.Drawing.Color.White;
			this.button3.Location = new System.Drawing.Point(74, 564);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(157, 22);
			this.button3.TabIndex = 8;
			this.button3.Text = "Remove class file";
			this.button3.UseVisualStyleBackColor = true;
			this.button3.Click += new System.EventHandler(this.RemoveClass);
			// 
			// xmlPath
			// 
			this.xmlPath.BackColor = System.Drawing.SystemColors.Window;
			this.xmlPath.Location = new System.Drawing.Point(183, 592);
			this.xmlPath.Name = "xmlPath";
			this.xmlPath.ReadOnly = true;
			this.xmlPath.Size = new System.Drawing.Size(292, 20);
			this.xmlPath.TabIndex = 10;
			// 
			// button4
			// 
			this.button4.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button4.BackgroundImage")));
			this.button4.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.button4.ForeColor = System.Drawing.Color.White;
			this.button4.Location = new System.Drawing.Point(228, 564);
			this.button4.Name = "button4";
			this.button4.Size = new System.Drawing.Size(125, 22);
			this.button4.TabIndex = 11;
			this.button4.Text = "Add folder";
			this.button4.UseVisualStyleBackColor = true;
			this.button4.Click += new System.EventHandler(this.AddFolder);
			// 
			// styleCB
			// 
			this.styleCB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.styleCB.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.styleCB.FormattingEnabled = true;
			this.styleCB.Location = new System.Drawing.Point(76, 621);
			this.styleCB.Name = "styleCB";
			this.styleCB.Size = new System.Drawing.Size(205, 21);
			this.styleCB.TabIndex = 12;
			this.styleCB.SelectedIndexChanged += new System.EventHandler(this.StyleCBSelectedIndexChanged);
			// 
			// label1
			// 
			this.label1.BackColor = System.Drawing.Color.Transparent;
			this.label1.Location = new System.Drawing.Point(17, 624);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(32, 18);
			this.label1.TabIndex = 13;
			this.label1.Text = "Style";
			// 
			// label2
			// 
			this.label2.BackColor = System.Drawing.Color.Transparent;
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label2.Location = new System.Drawing.Point(12, 238);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(56, 19);
			this.label2.TabIndex = 14;
			this.label2.Text = "Class files";
			this.label2.Click += new System.EventHandler(this.Label2Click);
			// 
			// BuildButton
			// 
			this.BuildButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("BuildButton.BackgroundImage")));
			this.BuildButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this.BuildButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.BuildButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.BuildButton.ForeColor = System.Drawing.Color.White;
			this.BuildButton.Location = new System.Drawing.Point(74, 650);
			this.BuildButton.Name = "BuildButton";
			this.BuildButton.Size = new System.Drawing.Size(402, 22);
			this.BuildButton.TabIndex = 1;
			this.BuildButton.Text = "Build actionscript documentation";
			this.BuildButton.UseVisualStyleBackColor = true;
			this.BuildButton.Click += new System.EventHandler(this.BuildDocumentationToolStripMenuItemClick);
			// 
			// label3
			// 
			this.label3.BackColor = System.Drawing.Color.Transparent;
			this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.label3.Location = new System.Drawing.Point(4, 5);
			this.label3.Margin = new System.Windows.Forms.Padding(0);
			this.label3.Name = "label3";
			this.label3.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
			this.label3.Size = new System.Drawing.Size(487, 41);
			this.label3.TabIndex = 17;
			this.label3.Text = "Ortelius";
			this.label3.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.label3.MouseMove += new System.Windows.Forms.MouseEventHandler(this.frm_MouseMove);
			this.label3.MouseDown += new System.Windows.Forms.MouseEventHandler(this.frm_MouseDown);
			this.label3.MouseUp += new System.Windows.Forms.MouseEventHandler(this.FrmGlavna_MouseUp);
			// 
			// pictureBox1
			// 
			this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
			this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
			this.pictureBox1.Location = new System.Drawing.Point(462, 7);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(16, 16);
			this.pictureBox1.TabIndex = 18;
			this.pictureBox1.TabStop = false;
			this.pictureBox1.Click += new System.EventHandler(this.CloseToolStripMenuItemClick);
			// 
			// pictureBox2
			// 
			this.pictureBox2.BackColor = System.Drawing.Color.Transparent;
			this.pictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
			this.pictureBox2.Location = new System.Drawing.Point(437, 7);
			this.pictureBox2.Name = "pictureBox2";
			this.pictureBox2.Size = new System.Drawing.Size(16, 16);
			this.pictureBox2.TabIndex = 19;
			this.pictureBox2.TabStop = false;
			this.pictureBox2.Click += new System.EventHandler(this.MinimizeForm);
			// 
			// pictureBox3
			// 
			this.pictureBox3.BackColor = System.Drawing.Color.Transparent;
			this.pictureBox3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			this.pictureBox3.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox3.Image")));
			this.pictureBox3.Location = new System.Drawing.Point(16, 6);
			this.pictureBox3.Name = "pictureBox3";
			this.pictureBox3.Size = new System.Drawing.Size(16, 16);
			this.pictureBox3.TabIndex = 20;
			this.pictureBox3.TabStop = false;
			this.pictureBox3.Click += new System.EventHandler(this.PictureBox3Click);
			// 
			// label4
			// 
			this.label4.BackColor = System.Drawing.Color.Transparent;
			this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label4.Location = new System.Drawing.Point(18, 90);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(50, 15);
			this.label4.TabIndex = 22;
			this.label4.Text = "Intro text";
			// 
			// introText
			// 
			this.introText.Location = new System.Drawing.Point(74, 89);
			this.introText.Multiline = true;
			this.introText.Name = "introText";
			this.introText.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.introText.Size = new System.Drawing.Size(404, 140);
			this.introText.TabIndex = 21;
			this.introText.TextChanged += new System.EventHandler(this.IntroTextTextChanged);
			// 
			// label5
			// 
			this.label5.BackColor = System.Drawing.Color.Transparent;
			this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label5.Location = new System.Drawing.Point(18, 67);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(50, 16);
			this.label5.TabIndex = 24;
			this.label5.Text = "Title";
			this.label5.Click += new System.EventHandler(this.Label5Click);
			// 
			// introHeader
			// 
			this.introHeader.Location = new System.Drawing.Point(74, 63);
			this.introHeader.Name = "introHeader";
			this.introHeader.Size = new System.Drawing.Size(404, 20);
			this.introHeader.TabIndex = 23;
			this.introHeader.TextChanged += new System.EventHandler(this.IntroHeaderTextChanged);
			// 
			// showAfterBuildCB
			// 
			this.showAfterBuildCB.BackColor = System.Drawing.Color.Transparent;
			this.showAfterBuildCB.Location = new System.Drawing.Point(299, 621);
			this.showAfterBuildCB.Name = "showAfterBuildCB";
			this.showAfterBuildCB.Size = new System.Drawing.Size(181, 24);
			this.showAfterBuildCB.TabIndex = 25;
			this.showAfterBuildCB.Text = "Show documentation when build";
			this.showAfterBuildCB.UseVisualStyleBackColor = false;
			this.showAfterBuildCB.CheckedChanged += new System.EventHandler(this.CheckBox1CheckedChanged);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.Control;
			this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
			this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.ClientSize = new System.Drawing.Size(500, 700);
			this.Controls.Add(this.showAfterBuildCB);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.introHeader);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.introText);
			this.Controls.Add(this.pictureBox3);
			this.Controls.Add(this.menuStrip1);
			this.Controls.Add(this.pictureBox2);
			this.Controls.Add(this.pictureBox1);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.BuildButton);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.styleCB);
			this.Controls.Add(this.button4);
			this.Controls.Add(this.xmlPath);
			this.Controls.Add(this.button3);
			this.Controls.Add(this.listBox1);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.progressBar1);
			this.Controls.Add(this.button1);
			this.DoubleBuffered = true;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MainMenuStrip = this.menuStrip1;
			this.MaximizeBox = false;
			this.MaximumSize = new System.Drawing.Size(500, 700);
			this.MinimumSize = new System.Drawing.Size(500, 700);
			this.Name = "MainForm";
			this.Text = "Ortelius";
			this.TransparencyKey = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(204)))));
			this.Load += new System.EventHandler(this.MainFormLoad);
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OrteliusClosing);
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();
		}
		private System.Windows.Forms.ToolStripMenuItem goToDocumentationToolStripMenuItem;
		private System.Windows.Forms.CheckBox showAfterBuildCB;
		private System.Windows.Forms.TextBox introHeader;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox introText;
		private System.Windows.Forms.PictureBox pictureBox3;
		private System.Windows.Forms.PictureBox pictureBox2;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Button BuildButton;
		private System.Windows.Forms.ToolStripMenuItem chooseDestinationToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem addFolderToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem addClassFileToolStripMenuItem;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ComboBox styleCB;
		private System.Windows.Forms.Button button4;
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.ListBox listBox1;
		private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem newProjectToolStripMenuItem;
		private System.Windows.Forms.TextBox xmlPath;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.ToolStripMenuItem loadProjectToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem buildDocumentationToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem buildToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem saveProjectToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem filesToolStripMenuItem;
		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ProgressBar progressBar1;
		private System.Windows.Forms.Button button1;
	}
}
