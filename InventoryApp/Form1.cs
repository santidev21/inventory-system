using InventoryApp.Logic;
using InventoryApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InventoryApp
{
    public partial class mainWindow : Form
    {
        public mainWindow()
        {
            InitializeComponent();
        }

        #region "Variables"
        int nState = 0;
        int itemId = 0;
        int categoryId = 0;
        #endregion

        #region "Methods"
        private void textboxStates(bool lEstado)
        {
            nameTxt.ReadOnly = !lEstado;
            quantityTxt.ReadOnly = !lEstado;
            brandTxt.ReadOnly = !lEstado;
        }

        private void clearText()
        {
            nameTxt.Clear();
            quantityTxt.Value = 0;
            brandTxt.Clear();
            categoryTxt.Clear();
        }

        private void editButtonToggle(bool lEstado)
        {
            cancelBtn.Visible = lEstado;
            saveBtn.Visible = lEstado;
            categoryBtn.Visible = lEstado;

            searchBtn.Enabled = !lEstado;
            searchTxt.Enabled = !lEstado;
            dgvData.Enabled = !lEstado;
        }

        private void navButtonsToggle(bool lEstado)
        {
            newBtn.Enabled = lEstado;
            editBtn.Enabled = lEstado;
            deleteBtn.Enabled = lEstado;
            reportBtn.Enabled = lEstado;
            logoutBtn.Enabled = lEstado;
        }

        private void dgvItemsFormat()
        {
            dgvData.Columns[0].Width = 20;
            dgvData.Columns[0].HeaderText = "ID";
            dgvData.Columns[1].Width = 120;
            dgvData.Columns[1].HeaderText = "NAME";
            dgvData.Columns[2].Width = 30;
            dgvData.Columns[2].HeaderText = "#";
            dgvData.Columns[3].Width = 80;
            dgvData.Columns[3].HeaderText = "BRAND";
            dgvData.Columns[4].Width = 80;
            dgvData.Columns[4].HeaderText = "CATEGORY";
            dgvData.Columns[5].Width = 130;
            dgvData.Columns[5].HeaderText = "ADDED BY";
        }

        private void loadItems(string cTexto)
        {
            Items Datos = new Items();
            dgvData.DataSource = Datos.loadItems(cTexto);
            this.dgvItemsFormat();
        }
        private void loadCategories()
        {
            Items Datos = new Items();
            dgvCategories.DataSource = Datos.loadCategories();
            this.dgvCategoriesFormat();
        }
        private void dgvCategoriesFormat()
        {
            dgvCategories.Columns[0].Width = 110;
            dgvCategories.Columns[0].HeaderText = "CATEGORY";
            dgvCategories.Columns[1].Visible = false;
        }
        private void categorySelect()
        {
            if (string.IsNullOrEmpty(Convert.ToString(dgvCategories.CurrentRow.Cells["categoryId"].Value)))
            {
                MessageBox.Show("Please select a category!", "System info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            } else
            {
                this.categoryId = Convert.ToInt32(dgvCategories.CurrentRow.Cells["categoryId"].Value);
                categoryTxt.Text = Convert.ToString(dgvCategories.CurrentRow.Cells["category"].Value);
            }
        }
        private void itemSelect()
        {
            if (string.IsNullOrEmpty(Convert.ToString(dgvData.CurrentRow.Cells["id"].Value)))
            {
                MessageBox.Show("Please select an item!",
                                "System warning",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Exclamation);
            }
            else
            {
                this.itemId = Convert.ToInt32(dgvData.CurrentRow.Cells["id"].Value);
                nameTxt.Text = Convert.ToString(dgvData.CurrentRow.Cells["name"].Value);
                quantityTxt.Text = Convert.ToString(dgvData.CurrentRow.Cells["quantity"].Value);
                brandTxt.Text = Convert.ToString(dgvData.CurrentRow.Cells["brand"].Value);
                categoryTxt.Text = Convert.ToString(dgvData.CurrentRow.Cells["category"].Value);
            }
        }
        #endregion


        #region "hide"
        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }
        #endregion


        private void mainWindow_Load(object sender, EventArgs e)
        {
            this.loadItems("%");
            this.loadCategories();
            this.textboxStates(false);
            this.clearText();
            this.editButtonToggle(false);
            this.navButtonsToggle(true);
        }

        private void newBtn_Click(object sender, EventArgs e)
        {
            nState = 1;
            this.textboxStates(true);
            this.clearText();
            this.editButtonToggle(true);
            this.navButtonsToggle(false);
            nameTxt.Focus();
        }


        private void saveBtn_Click(object sender, EventArgs e)
        {
            string Answ = "";
            ItemsModel oAr = new ItemsModel();
            oAr.itemId = itemId;
            oAr.quantityItem = Convert.ToInt32(quantityTxt.Text.Trim());
            oAr.itemName = nameTxt.Text.Trim();
            oAr.brandItem = brandTxt.Text.Trim();
            oAr.categoryItem = categoryTxt.Text.Trim();
            oAr.createdByItem = "Admin";

            Items Data = new Items();
            Answ = Data.saveItem(nState, oAr);
            if (Answ.Equals("OK"))
            {
                this.loadItems("%");
                this.editButtonToggle(false);
                this.navButtonsToggle(true);
                this.textboxStates(false);
                this.clearText();
                nState = 0;
                categoryId = 0;
                MessageBox.Show("Item save correctly!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show(Answ);
            }
        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            this.textboxStates(false);
            this.clearText();
            this.editButtonToggle(false);
            this.navButtonsToggle(true);
            nState = 0;
            categoryId = 0;
        }

        private void editBtn_Click(object sender, EventArgs e)
        {
            if (dgvData.Rows.Count <= 0)
            {
                MessageBox.Show("There are no items for update!",
                                "System info",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Exclamation);
            }
            else
            {
                nState = 2;
                this.textboxStates(true);
                this.editButtonToggle(true);
                this.navButtonsToggle(false);
                nameTxt.Focus();
            }
        }

        private void categoryBtn_Click(object sender, EventArgs e)
        {
            categoryPanel.Location = label5.Location;
            categoryPanel.Visible = true;
        }

        private void selectBtn_Click(object sender, EventArgs e)
        {
            categoryPanel.Visible = false;
        }

        private void dgvCategories_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            this.categorySelect();
            categoryPanel.Visible = false;
        }

        private void dgvData_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            this.itemSelect();
        }

        private void searchBtn_Click(object sender, EventArgs e)
        {
            this.loadItems(searchTxt.Text.Trim());
        }

        private void deleteBtn_Click(object sender, EventArgs e)
        {
            if (dgvData.Rows.Count <= 0)
            {
                MessageBox.Show("There are no items for delete!",
                "System info",
                MessageBoxButtons.OK,
                MessageBoxIcon.Exclamation);
            }
            else
            {
                string Answ = "";
                Items Data = new Items();
                Answ = Data.deleteItem(itemId);
                if (Answ.Equals("OK"))
                {
                    this.loadItems("%");
                    this.clearText();
                    categoryId = 0;
                    nState = 0;
                    MessageBox.Show("The items was deleted correctly!",
                     "System info",
                     MessageBoxButtons.OK,
                     MessageBoxIcon.Exclamation);
                }
            }
        }

        private void reportBtn_Click(object sender, EventArgs e)
        {
            Reports.reportForm oRpt = new Reports.reportForm();
            oRpt.ShowDialog();
        }
    }
}
