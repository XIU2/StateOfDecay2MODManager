namespace 腐烂国度2_MOD管理小工具
{
    partial class Form
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form));
            this.toolTip_使用说明 = new System.Windows.Forms.ToolTip(this.components);
            this.statusStrip_状态条 = new System.Windows.Forms.StatusStrip();
            this.StatusLabel_已载入数量 = new System.Windows.Forms.ToolStripStatusLabel();
            this.StatusLabel_已安装数量 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel4 = new System.Windows.Forms.ToolStripStatusLabel();
            this.StatusLabel_检查更新 = new System.Windows.Forms.ToolStripStatusLabel();
            this.StatusLabel_使用方法 = new System.Windows.Forms.ToolStripStatusLabel();
            this.listView_MOD列表 = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label1 = new System.Windows.Forms.Label();
            this.button_浏览MOD安装位置 = new System.Windows.Forms.Button();
            this.textBox_MOD安装位置 = new System.Windows.Forms.TextBox();
            this.Menu_列表框右键菜单 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.Menu_安装卸载 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.Menu_查看 = new System.Windows.Forms.ToolStripMenuItem();
            this.Menu_删除 = new System.Windows.Forms.ToolStripMenuItem();
            this.Menu_重命名 = new System.Windows.Forms.ToolStripMenuItem();
            this.checkBox_禁止检查更新 = new System.Windows.Forms.CheckBox();
            this.pictureBox_loading = new System.Windows.Forms.PictureBox();
            this.statusStrip_状态条.SuspendLayout();
            this.Menu_列表框右键菜单.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_loading)).BeginInit();
            this.SuspendLayout();
            // 
            // toolTip_使用说明
            // 
            this.toolTip_使用说明.AutoPopDelay = 10000;
            this.toolTip_使用说明.InitialDelay = 500;
            this.toolTip_使用说明.ReshowDelay = 100;
            this.toolTip_使用说明.ShowAlways = true;
            // 
            // statusStrip_状态条
            // 
            this.statusStrip_状态条.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StatusLabel_已载入数量,
            this.StatusLabel_已安装数量,
            this.toolStripStatusLabel4,
            this.StatusLabel_检查更新,
            this.StatusLabel_使用方法});
            this.statusStrip_状态条.Location = new System.Drawing.Point(0, 345);
            this.statusStrip_状态条.Name = "statusStrip_状态条";
            this.statusStrip_状态条.ShowItemToolTips = true;
            this.statusStrip_状态条.Size = new System.Drawing.Size(608, 22);
            this.statusStrip_状态条.SizingGrip = false;
            this.statusStrip_状态条.TabIndex = 0;
            this.statusStrip_状态条.Text = "statusStrip1";
            // 
            // StatusLabel_已载入数量
            // 
            this.StatusLabel_已载入数量.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.StatusLabel_已载入数量.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.StatusLabel_已载入数量.Name = "StatusLabel_已载入数量";
            this.StatusLabel_已载入数量.Size = new System.Drawing.Size(126, 17);
            this.StatusLabel_已载入数量.Text = "   已载入 MOD：0 个";
            // 
            // StatusLabel_已安装数量
            // 
            this.StatusLabel_已安装数量.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.StatusLabel_已安装数量.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.StatusLabel_已安装数量.Name = "StatusLabel_已安装数量";
            this.StatusLabel_已安装数量.Size = new System.Drawing.Size(126, 17);
            this.StatusLabel_已安装数量.Text = "，已安装 MOD：0 个";
            // 
            // toolStripStatusLabel4
            // 
            this.toolStripStatusLabel4.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.toolStripStatusLabel4.Name = "toolStripStatusLabel4";
            this.toolStripStatusLabel4.Size = new System.Drawing.Size(200, 17);
            this.toolStripStatusLabel4.Text = "                                                ";
            // 
            // StatusLabel_检查更新
            // 
            this.StatusLabel_检查更新.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.StatusLabel_检查更新.ForeColor = System.Drawing.SystemColors.Highlight;
            this.StatusLabel_检查更新.Name = "StatusLabel_检查更新";
            this.StatusLabel_检查更新.Size = new System.Drawing.Size(72, 17);
            this.StatusLabel_检查更新.Text = " [检查更新] ";
            this.StatusLabel_检查更新.Click += new System.EventHandler(this.StatusLabel_检查更新_Click);
            // 
            // StatusLabel_使用方法
            // 
            this.StatusLabel_使用方法.AutoToolTip = true;
            this.StatusLabel_使用方法.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.StatusLabel_使用方法.ForeColor = System.Drawing.SystemColors.Highlight;
            this.StatusLabel_使用方法.Name = "StatusLabel_使用方法";
            this.StatusLabel_使用方法.Size = new System.Drawing.Size(72, 17);
            this.StatusLabel_使用方法.Text = " [使用说明] ";
            this.StatusLabel_使用方法.ToolTipText = resources.GetString("StatusLabel_使用方法.ToolTipText");
            // 
            // listView_MOD列表
            // 
            this.listView_MOD列表.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4});
            this.listView_MOD列表.FullRowSelect = true;
            this.listView_MOD列表.GridLines = true;
            this.listView_MOD列表.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listView_MOD列表.HideSelection = false;
            this.listView_MOD列表.LabelEdit = true;
            this.listView_MOD列表.LabelWrap = false;
            this.listView_MOD列表.Location = new System.Drawing.Point(16, 48);
            this.listView_MOD列表.MultiSelect = false;
            this.listView_MOD列表.Name = "listView_MOD列表";
            this.listView_MOD列表.ShowGroups = false;
            this.listView_MOD列表.Size = new System.Drawing.Size(576, 288);
            this.listView_MOD列表.TabIndex = 1;
            this.listView_MOD列表.TabStop = false;
            this.listView_MOD列表.UseCompatibleStateImageBehavior = false;
            this.listView_MOD列表.View = System.Windows.Forms.View.Details;
            this.listView_MOD列表.AfterLabelEdit += new System.Windows.Forms.LabelEditEventHandler(this.ListView_MOD列表_AfterLabelEdit);
            this.listView_MOD列表.BeforeLabelEdit += new System.Windows.Forms.LabelEditEventHandler(this.ListView_MOD列表_BeforeLabelEdit);
            this.listView_MOD列表.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ListView_MOD列表_MouseClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "MOD 名称";
            this.columnHeader1.Width = 216;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "状态";
            this.columnHeader2.Width = 68;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "载入时间";
            this.columnHeader3.Width = 134;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "安装时间";
            this.columnHeader4.Width = 134;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "游戏数据位置：";
            // 
            // button_浏览MOD安装位置
            // 
            this.button_浏览MOD安装位置.Location = new System.Drawing.Point(520, 10);
            this.button_浏览MOD安装位置.Name = "button_浏览MOD安装位置";
            this.button_浏览MOD安装位置.Size = new System.Drawing.Size(72, 24);
            this.button_浏览MOD安装位置.TabIndex = 3;
            this.button_浏览MOD安装位置.Text = "浏览";
            this.button_浏览MOD安装位置.UseVisualStyleBackColor = true;
            this.button_浏览MOD安装位置.Click += new System.EventHandler(this.Button_浏览MOD安装位置_Click);
            // 
            // textBox_MOD安装位置
            // 
            this.textBox_MOD安装位置.Location = new System.Drawing.Point(104, 12);
            this.textBox_MOD安装位置.Name = "textBox_MOD安装位置";
            this.textBox_MOD安装位置.ReadOnly = true;
            this.textBox_MOD安装位置.Size = new System.Drawing.Size(408, 21);
            this.textBox_MOD安装位置.TabIndex = 4;
            // 
            // Menu_列表框右键菜单
            // 
            this.Menu_列表框右键菜单.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Menu_安装卸载,
            this.toolStripSeparator1,
            this.Menu_查看,
            this.Menu_删除,
            this.Menu_重命名});
            this.Menu_列表框右键菜单.Name = "Menu_列表框右键菜单";
            this.Menu_列表框右键菜单.Size = new System.Drawing.Size(113, 98);
            // 
            // Menu_安装卸载
            // 
            this.Menu_安装卸载.Name = "Menu_安装卸载";
            this.Menu_安装卸载.Size = new System.Drawing.Size(112, 22);
            this.Menu_安装卸载.Text = "安装";
            this.Menu_安装卸载.Click += new System.EventHandler(this.Menu_安装卸载_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(109, 6);
            // 
            // Menu_查看
            // 
            this.Menu_查看.Name = "Menu_查看";
            this.Menu_查看.Size = new System.Drawing.Size(112, 22);
            this.Menu_查看.Text = "查看";
            this.Menu_查看.Click += new System.EventHandler(this.Menu_查看_Click);
            // 
            // Menu_删除
            // 
            this.Menu_删除.Name = "Menu_删除";
            this.Menu_删除.Size = new System.Drawing.Size(112, 22);
            this.Menu_删除.Text = "删除";
            this.Menu_删除.Click += new System.EventHandler(this.Menu_删除_Click);
            // 
            // Menu_重命名
            // 
            this.Menu_重命名.Name = "Menu_重命名";
            this.Menu_重命名.Size = new System.Drawing.Size(112, 22);
            this.Menu_重命名.Text = "重命名";
            this.Menu_重命名.Click += new System.EventHandler(this.Menu_重命名_Click);
            // 
            // checkBox_禁止检查更新
            // 
            this.checkBox_禁止检查更新.AutoSize = true;
            this.checkBox_禁止检查更新.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.checkBox_禁止检查更新.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.checkBox_禁止检查更新.Location = new System.Drawing.Point(368, 348);
            this.checkBox_禁止检查更新.Name = "checkBox_禁止检查更新";
            this.checkBox_禁止检查更新.Size = new System.Drawing.Size(93, 16);
            this.checkBox_禁止检查更新.TabIndex = 5;
            this.checkBox_禁止检查更新.Text = "禁止检查更新";
            this.checkBox_禁止检查更新.UseVisualStyleBackColor = false;
            this.checkBox_禁止检查更新.CheckedChanged += new System.EventHandler(this.CheckBox_禁止检查更新_CheckedChanged);
            // 
            // pictureBox_loading
            // 
            this.pictureBox_loading.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox_loading.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pictureBox_loading.Image = global::腐烂国度2_MOD管理小工具.Properties.Resources.loading;
            this.pictureBox_loading.Location = new System.Drawing.Point(290, 169);
            this.pictureBox_loading.Name = "pictureBox_loading";
            this.pictureBox_loading.Size = new System.Drawing.Size(28, 29);
            this.pictureBox_loading.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox_loading.TabIndex = 6;
            this.pictureBox_loading.TabStop = false;
            this.pictureBox_loading.Visible = false;
            // 
            // Form
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(608, 367);
            this.Controls.Add(this.pictureBox_loading);
            this.Controls.Add(this.checkBox_禁止检查更新);
            this.Controls.Add(this.textBox_MOD安装位置);
            this.Controls.Add(this.button_浏览MOD安装位置);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.listView_MOD列表);
            this.Controls.Add(this.statusStrip_状态条);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "腐烂国度2 MOD管理小工具";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.Form1_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.Form1_DragEnter);
            this.statusStrip_状态条.ResumeLayout(false);
            this.statusStrip_状态条.PerformLayout();
            this.Menu_列表框右键菜单.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_loading)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip_状态条;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button_浏览MOD安装位置;
        private System.Windows.Forms.TextBox textBox_MOD安装位置;
        private System.Windows.Forms.ToolStripStatusLabel StatusLabel_已载入数量;
        private System.Windows.Forms.ToolStripStatusLabel StatusLabel_已安装数量;
        private System.Windows.Forms.ListView listView_MOD列表;
        private System.Windows.Forms.ContextMenuStrip Menu_列表框右键菜单;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem Menu_安装卸载;
        private System.Windows.Forms.ToolStripMenuItem Menu_查看;
        private System.Windows.Forms.ToolStripMenuItem Menu_删除;
        private System.Windows.Forms.ToolStripMenuItem Menu_重命名;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel4;
        private System.Windows.Forms.ToolStripStatusLabel StatusLabel_检查更新;
        private System.Windows.Forms.ToolStripStatusLabel StatusLabel_使用方法;
        private System.Windows.Forms.CheckBox checkBox_禁止检查更新;
        private System.Windows.Forms.PictureBox pictureBox_loading;
        private System.Windows.Forms.ToolTip toolTip_使用说明;
    }
}

