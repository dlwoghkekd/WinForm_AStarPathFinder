namespace AStarPathFinding
{
    partial class Form1
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.pn_Table = new System.Windows.Forms.Panel();
            this.btn_SetStartPoint = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_SetGoalPoint = new System.Windows.Forms.Button();
            this.btn_SetDefault = new System.Windows.Forms.Button();
            this.btn_SetWallPoint = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.btn_PathFind = new System.Windows.Forms.Button();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.btn_Step = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // pn_Table
            // 
            this.pn_Table.BackColor = System.Drawing.SystemColors.Control;
            this.pn_Table.Location = new System.Drawing.Point(12, 12);
            this.pn_Table.Name = "pn_Table";
            this.pn_Table.Size = new System.Drawing.Size(800, 800);
            this.pn_Table.TabIndex = 0;
            this.pn_Table.Paint += new System.Windows.Forms.PaintEventHandler(this.pn_Table_Paint);
            this.pn_Table.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pn_Table_MouseDown);
            // 
            // btn_SetStartPoint
            // 
            this.btn_SetStartPoint.Font = new System.Drawing.Font("Consolas", 10F);
            this.btn_SetStartPoint.Location = new System.Drawing.Point(818, 47);
            this.btn_SetStartPoint.Name = "btn_SetStartPoint";
            this.btn_SetStartPoint.Size = new System.Drawing.Size(125, 30);
            this.btn_SetStartPoint.TabIndex = 1;
            this.btn_SetStartPoint.Text = "StartPoint";
            this.btn_SetStartPoint.UseVisualStyleBackColor = true;
            this.btn_SetStartPoint.Click += new System.EventHandler(this.btn_SetStartPoint_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.GrayText;
            this.label1.Font = new System.Drawing.Font("Consolas", 18F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.label1.Location = new System.Drawing.Point(818, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(129, 28);
            this.label1.TabIndex = 5;
            this.label1.Text = "SET_POINT";
            // 
            // btn_SetGoalPoint
            // 
            this.btn_SetGoalPoint.Font = new System.Drawing.Font("Consolas", 10F);
            this.btn_SetGoalPoint.Location = new System.Drawing.Point(818, 83);
            this.btn_SetGoalPoint.Name = "btn_SetGoalPoint";
            this.btn_SetGoalPoint.Size = new System.Drawing.Size(125, 30);
            this.btn_SetGoalPoint.TabIndex = 6;
            this.btn_SetGoalPoint.Text = "GoalPoint";
            this.btn_SetGoalPoint.UseVisualStyleBackColor = true;
            this.btn_SetGoalPoint.Click += new System.EventHandler(this.btn_SetGoalPoint_Click);
            // 
            // btn_SetDefault
            // 
            this.btn_SetDefault.Font = new System.Drawing.Font("Consolas", 10F);
            this.btn_SetDefault.Location = new System.Drawing.Point(818, 155);
            this.btn_SetDefault.Name = "btn_SetDefault";
            this.btn_SetDefault.Size = new System.Drawing.Size(125, 30);
            this.btn_SetDefault.TabIndex = 7;
            this.btn_SetDefault.Text = "Default";
            this.btn_SetDefault.UseVisualStyleBackColor = true;
            this.btn_SetDefault.Click += new System.EventHandler(this.btn_SetDefault_Click);
            // 
            // btn_SetWallPoint
            // 
            this.btn_SetWallPoint.Font = new System.Drawing.Font("Consolas", 10F);
            this.btn_SetWallPoint.Location = new System.Drawing.Point(818, 119);
            this.btn_SetWallPoint.Name = "btn_SetWallPoint";
            this.btn_SetWallPoint.Size = new System.Drawing.Size(125, 30);
            this.btn_SetWallPoint.TabIndex = 8;
            this.btn_SetWallPoint.Text = "WallPoint";
            this.btn_SetWallPoint.UseVisualStyleBackColor = true;
            this.btn_SetWallPoint.Click += new System.EventHandler(this.btn_SetWallPoint_Click);
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.label2.Location = new System.Drawing.Point(816, 192);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(129, 2);
            this.label2.TabIndex = 9;
            this.label2.Text = "          ";
            // 
            // btn_PathFind
            // 
            this.btn_PathFind.Font = new System.Drawing.Font("Consolas", 10F);
            this.btn_PathFind.Location = new System.Drawing.Point(818, 202);
            this.btn_PathFind.Name = "btn_PathFind";
            this.btn_PathFind.Size = new System.Drawing.Size(125, 30);
            this.btn_PathFind.TabIndex = 10;
            this.btn_PathFind.Text = "PathFind";
            this.btn_PathFind.UseVisualStyleBackColor = true;
            this.btn_PathFind.Click += new System.EventHandler(this.btn_PathFind_Click);
            // 
            // btn_Step
            // 
            this.btn_Step.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btn_Step.Font = new System.Drawing.Font("Consolas", 10F);
            this.btn_Step.Location = new System.Drawing.Point(818, 782);
            this.btn_Step.Name = "btn_Step";
            this.btn_Step.Size = new System.Drawing.Size(125, 30);
            this.btn_Step.TabIndex = 11;
            this.btn_Step.Text = "Step";
            this.btn_Step.UseVisualStyleBackColor = true;
            this.btn_Step.Click += new System.EventHandler(this.btn_Step_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GrayText;
            this.ClientSize = new System.Drawing.Size(949, 826);
            this.Controls.Add(this.btn_Step);
            this.Controls.Add(this.btn_PathFind);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btn_SetWallPoint);
            this.Controls.Add(this.btn_SetDefault);
            this.Controls.Add(this.btn_SetGoalPoint);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_SetStartPoint);
            this.Controls.Add(this.pn_Table);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.HelpButton = true;
            this.Name = "Form1";
            this.Text = "AStarPathFinder";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pn_Table;
        private System.Windows.Forms.Button btn_SetStartPoint;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_SetGoalPoint;
        private System.Windows.Forms.Button btn_SetDefault;
        private System.Windows.Forms.Button btn_SetWallPoint;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btn_PathFind;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.Button btn_Step;
    }
}

