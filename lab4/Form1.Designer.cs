
namespace lab4
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.button_new_schema = new System.Windows.Forms.Button();
            this.button_save_schema = new System.Windows.Forms.Button();
            this.button_load_schema = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.TableEditorButtons = new System.Windows.Forms.TableLayoutPanel();
            this.button_operation_block = new System.Windows.Forms.Button();
            this.button_decisive_block = new System.Windows.Forms.Button();
            this.ButtonStartBlock = new System.Windows.Forms.Button();
            this.ButtonLink = new System.Windows.Forms.Button();
            this.ButtonStopBlock = new System.Windows.Forms.Button();
            this.ButtonRemove = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.TextBoxSelected = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.button_polish_lang = new System.Windows.Forms.Button();
            this.button_eng_lang = new System.Windows.Forms.Button();
            this.Canvas = new System.Windows.Forms.PictureBox();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.CanvasPanel = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.TableEditorButtons.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Canvas)).BeginInit();
            this.tableLayoutPanel5.SuspendLayout();
            this.CanvasPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
            this.tableLayoutPanel1.Controls.Add(this.groupBox1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupBox2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.groupBox3, 0, 2);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tableLayoutPanel2);
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // tableLayoutPanel2
            // 
            resources.ApplyResources(this.tableLayoutPanel2, "tableLayoutPanel2");
            this.tableLayoutPanel2.Controls.Add(this.button_new_schema, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.button_save_schema, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.button_load_schema, 0, 2);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            // 
            // button_new_schema
            // 
            resources.ApplyResources(this.button_new_schema, "button_new_schema");
            this.button_new_schema.Name = "button_new_schema";
            this.button_new_schema.UseVisualStyleBackColor = true;
            this.button_new_schema.Click += new System.EventHandler(this.button_new_schema_Click);
            // 
            // button_save_schema
            // 
            resources.ApplyResources(this.button_save_schema, "button_save_schema");
            this.button_save_schema.Name = "button_save_schema";
            this.button_save_schema.UseVisualStyleBackColor = true;
            this.button_save_schema.Click += new System.EventHandler(this.button_save_schema_Click);
            // 
            // button_load_schema
            // 
            resources.ApplyResources(this.button_load_schema, "button_load_schema");
            this.button_load_schema.Name = "button_load_schema";
            this.button_load_schema.UseVisualStyleBackColor = true;
            this.button_load_schema.Click += new System.EventHandler(this.button_load_schema_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tableLayoutPanel4);
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            // 
            // tableLayoutPanel4
            // 
            resources.ApplyResources(this.tableLayoutPanel4, "tableLayoutPanel4");
            this.tableLayoutPanel4.Controls.Add(this.TableEditorButtons, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.label1, 0, 1);
            this.tableLayoutPanel4.Controls.Add(this.TextBoxSelected, 0, 2);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            // 
            // TableEditorButtons
            // 
            resources.ApplyResources(this.TableEditorButtons, "TableEditorButtons");
            this.TableEditorButtons.Controls.Add(this.button_operation_block, 1, 0);
            this.TableEditorButtons.Controls.Add(this.button_decisive_block, 1, 1);
            this.TableEditorButtons.Controls.Add(this.ButtonStartBlock, 0, 0);
            this.TableEditorButtons.Controls.Add(this.ButtonLink, 2, 0);
            this.TableEditorButtons.Controls.Add(this.ButtonStopBlock, 0, 1);
            this.TableEditorButtons.Controls.Add(this.ButtonRemove, 2, 1);
            this.TableEditorButtons.Name = "TableEditorButtons";
            // 
            // button_operation_block
            // 
            resources.ApplyResources(this.button_operation_block, "button_operation_block");
            this.button_operation_block.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.button_operation_block.Name = "button_operation_block";
            this.button_operation_block.UseVisualStyleBackColor = true;
            this.button_operation_block.MouseClick += new System.Windows.Forms.MouseEventHandler(this.button_operation_block_MouseClick);
            // 
            // button_decisive_block
            // 
            resources.ApplyResources(this.button_decisive_block, "button_decisive_block");
            this.button_decisive_block.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.button_decisive_block.Name = "button_decisive_block";
            this.button_decisive_block.UseVisualStyleBackColor = true;
            this.button_decisive_block.MouseClick += new System.Windows.Forms.MouseEventHandler(this.button_decisive_block_MouseClick);
            // 
            // ButtonStartBlock
            // 
            resources.ApplyResources(this.ButtonStartBlock, "ButtonStartBlock");
            this.ButtonStartBlock.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.ButtonStartBlock.Name = "ButtonStartBlock";
            this.ButtonStartBlock.UseVisualStyleBackColor = true;
            this.ButtonStartBlock.Click += new System.EventHandler(this.ButtonStartBlock_Click);
            // 
            // ButtonLink
            // 
            resources.ApplyResources(this.ButtonLink, "ButtonLink");
            this.ButtonLink.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.ButtonLink.Name = "ButtonLink";
            this.ButtonLink.UseVisualStyleBackColor = true;
            this.ButtonLink.Click += new System.EventHandler(this.ButtonLink_Click);
            // 
            // ButtonStopBlock
            // 
            resources.ApplyResources(this.ButtonStopBlock, "ButtonStopBlock");
            this.ButtonStopBlock.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.ButtonStopBlock.Name = "ButtonStopBlock";
            this.ButtonStopBlock.UseVisualStyleBackColor = true;
            this.ButtonStopBlock.Click += new System.EventHandler(this.ButtonStopBlock_Click);
            // 
            // ButtonRemove
            // 
            resources.ApplyResources(this.ButtonRemove, "ButtonRemove");
            this.ButtonRemove.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.ButtonRemove.Name = "ButtonRemove";
            this.ButtonRemove.UseVisualStyleBackColor = true;
            this.ButtonRemove.Click += new System.EventHandler(this.ButtonRemove_Click);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // TextBoxSelected
            // 
            resources.ApplyResources(this.TextBoxSelected, "TextBoxSelected");
            this.TextBoxSelected.Name = "TextBoxSelected";
            this.TextBoxSelected.TextChanged += new System.EventHandler(this.TextBoxSelected_TextChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.tableLayoutPanel3);
            resources.ApplyResources(this.groupBox3, "groupBox3");
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.TabStop = false;
            // 
            // tableLayoutPanel3
            // 
            resources.ApplyResources(this.tableLayoutPanel3, "tableLayoutPanel3");
            this.tableLayoutPanel3.Controls.Add(this.button_polish_lang, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.button_eng_lang, 0, 1);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            // 
            // button_polish_lang
            // 
            resources.ApplyResources(this.button_polish_lang, "button_polish_lang");
            this.button_polish_lang.Name = "button_polish_lang";
            this.button_polish_lang.UseVisualStyleBackColor = true;
            this.button_polish_lang.Click += new System.EventHandler(this.button_polish_lang_Click);
            // 
            // button_eng_lang
            // 
            resources.ApplyResources(this.button_eng_lang, "button_eng_lang");
            this.button_eng_lang.Name = "button_eng_lang";
            this.button_eng_lang.UseVisualStyleBackColor = true;
            this.button_eng_lang.Click += new System.EventHandler(this.button_eng_lang_Click);
            // 
            // Canvas
            // 
            resources.ApplyResources(this.Canvas, "Canvas");
            this.Canvas.Name = "Canvas";
            this.Canvas.TabStop = false;
            this.Canvas.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Canvas_MouseClick);
            this.Canvas.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Canvas_MouseDown);
            this.Canvas.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Canvas_MouseMove);
            this.Canvas.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Canvas_MouseUp);
            // 
            // tableLayoutPanel5
            // 
            resources.ApplyResources(this.tableLayoutPanel5, "tableLayoutPanel5");
            this.tableLayoutPanel5.Controls.Add(this.tableLayoutPanel1, 1, 0);
            this.tableLayoutPanel5.Controls.Add(this.CanvasPanel, 0, 0);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            // 
            // CanvasPanel
            // 
            resources.ApplyResources(this.CanvasPanel, "CanvasPanel");
            this.CanvasPanel.Controls.Add(this.Canvas);
            this.CanvasPanel.Name = "CanvasPanel";
            // 
            // Form1
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel5);
            this.Name = "Form1";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            this.TableEditorButtons.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Canvas)).EndInit();
            this.tableLayoutPanel5.ResumeLayout(false);
            this.CanvasPanel.ResumeLayout(false);
            this.CanvasPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Button button_new_schema;
        private System.Windows.Forms.Button button_save_schema;
        private System.Windows.Forms.Button button_load_schema;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.Button button_operation_block;
        private System.Windows.Forms.Button button_decisive_block;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Button button_polish_lang;
        private System.Windows.Forms.Button button_eng_lang;
        private System.Windows.Forms.PictureBox Canvas;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.Panel CanvasPanel;
        private System.Windows.Forms.TableLayoutPanel TableEditorButtons;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TextBoxSelected;
        private System.Windows.Forms.Button ButtonStartBlock;
        private System.Windows.Forms.Button ButtonLink;
        private System.Windows.Forms.Button ButtonStopBlock;
        private System.Windows.Forms.Button ButtonRemove;
    }
}

