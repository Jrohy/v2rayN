using System;
using System.Collections.Generic;
using System.Windows.Forms;
using v2rayN.Handler;
using v2rayN.Mode;

namespace v2rayN.Forms
{
    public partial class SubSettingForm : BaseForm
    {
        private SubItem curItem = null;

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

            InitSubsView();
            RefreshSubsView();
        }


        /// <summary>
        /// 初始化列表
        /// </summary>
        private void InitSubsView()
        {
            lvSubs.Items.Clear();

            lvSubs.GridLines = true;
            lvSubs.FullRowSelect = true;
            lvSubs.View = View.Details;
            lvSubs.Scrollable = true;
            lvSubs.MultiSelect = false;
            lvSubs.HeaderStyle = ColumnHeaderStyle.Nonclickable;

            lvSubs.Columns.Add("", 30, HorizontalAlignment.Center);
            lvSubs.Columns.Add("备注", 100, HorizontalAlignment.Left);
            lvSubs.Columns.Add("地址", 400, HorizontalAlignment.Left);
        }

        /// <summary>
        /// 刷新列表
        /// </summary>
        private void RefreshSubsView()
        {
            lvSubs.Items.Clear();

            for (int k = 0; k < config.subItem.Count; k++)
            {
                var item = config.subItem[k];
                ListViewItem lvItem = new ListViewItem(new string[]
                {
                    (k+1).ToString(),
                    item.remarks,
                    item.url
                });
                lvSubs.Items.Add(lvItem);
            }
        }


        /// <summary>
        /// 取得ListView选中的行
        /// </summary>
        /// <returns></returns>
        private int GetLvSelectedIndex()
        {
            int index = -1;
            try
            {
                if (lvSubs.SelectedIndices.Count <= 0)
                {
                    UI.Show("请先选择");
                    return index;
                }
                index = lvSubs.SelectedIndices[0];
                return index;
            }
            catch
            {
                return index;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (config.subItem.Count <= 0)
            {
                AddSub();
            }

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

        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddSub();

            RefreshSubsView();
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            int index = GetLvSelectedIndex();
            if (index < 0)
            {
                return;
            }
            config.subItem.RemoveAt(index);

            RefreshSubsView();
        }

        private void lvSubs_SelectedIndexChanged(object sender, EventArgs e)
        {
            curItem = null;
            int index = -1;
            try
            {
                if (lvSubs.SelectedIndices.Count > 0)
                {
                    index = lvSubs.SelectedIndices[0];
                }
                curItem = config.subItem[index];
                BindingSub();
            }
            catch
            {
            }
        }
        private void AddSub()
        {
            var subItem = new SubItem();
            subItem.id =
            subItem.remarks =
            subItem.url = string.Empty;
            config.subItem.Add(subItem);
        }


        private void BindingSub()
        {
            if (curItem != null)
            {
                txtRemarks.Text = curItem.remarks.ToString();
                txtUrl.Text = curItem.url.ToString();
            }
        }

        private void EndBindingSub()
        {
            if (curItem != null)
            {
                curItem.remarks = txtRemarks.Text.Trim();
                curItem.url = txtUrl.Text.Trim();

                RefreshSubsView();
            }
        }

        private void txtRemarks_Leave(object sender, EventArgs e)
        {
            EndBindingSub();
        }
    }
}
