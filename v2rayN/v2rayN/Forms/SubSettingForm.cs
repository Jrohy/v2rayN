using System;
using System.Collections.Generic;
using System.Windows.Forms;
using v2rayN.Handler;
using v2rayN.Mode;

namespace v2rayN.Forms
{
    public partial class SubSettingForm : BaseForm
    {
        public SubSettingForm()
        {
            InitializeComponent();
        }

        private void SubSettingForm_Load(object sender, EventArgs e)
        {
            if (config.subItem == null)
            {
                config.subItem = new List<SubItem>();
            }

            BindingSub();
        }

        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BindingSub()
        {
            for (int k = config.subItem.Count; k < 3; k++)
            {
                var subItem = new SubItem();
                subItem.id =
                subItem.remarks =
                subItem.url = string.Empty;

                config.subItem.Add(subItem);
            }

            txtRemarks.Text = config.subItem[0].remarks.ToString();
            txtUrl.Text = config.subItem[0].url.ToString();

            txtRemarks2.Text = config.subItem[1].remarks.ToString();
            txtUrl2.Text = config.subItem[1].url.ToString();

            txtRemarks3.Text = config.subItem[2].remarks.ToString();
            txtUrl3.Text = config.subItem[2].url.ToString();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            config.subItem[0].remarks = txtRemarks.Text.Trim();
            config.subItem[0].url = txtUrl.Text.Trim();

            config.subItem[1].remarks = txtRemarks2.Text.Trim();
            config.subItem[1].url = txtUrl2.Text.Trim();

            config.subItem[2].remarks = txtRemarks3.Text.Trim();
            config.subItem[2].url = txtUrl3.Text.Trim();

            if (ConfigHandler.SaveSubItem(ref config) == 0)
            {
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                UI.Show("操作失败，请检查重试");
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
