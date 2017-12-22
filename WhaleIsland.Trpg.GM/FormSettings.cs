using System;
using System.Windows.Forms;

namespace WhaleIsland.Trpg.GM
{
    public partial class FormSettings : Form
    {
        public FormSettings()
        {
            InitializeComponent();

            //加载标题。
            this.Text = System.Reflection.Assembly.GetAssembly(this.GetType()).GetName().Name + " 参数设置";
        }

        /// <summary>
        /// 退出按钮事件处理方法。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 保存按钮事件处理方法。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            //参数保存处理代码。

            this.btnExit_Click(null, null);
        }
    }
}
