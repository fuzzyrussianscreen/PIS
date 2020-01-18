using PISController1.Controller;
using PISModel1.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Unity;

namespace PISLawyerView
{
    public partial class FormListClients : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly ControllerClient service;
        private readonly ControllerService serviceS;
        private readonly ControllerSelection serviceR;
        private readonly ControllerValidation serviceV;
        public FormListClients(ControllerClient service, ControllerSelection serviceR, ControllerService serviceS, ControllerValidation serviceV)
        {
            InitializeComponent();
            this.service = service;
            this.serviceR = serviceR;
            this.serviceS = serviceS;
            this.serviceV = serviceV;
        }

        private void FormListClients_Load(object sender, EventArgs e)
        {
            List<ModelServices> listS = serviceS.GetList();
            if (listS != null)
            {
                comboBoxService.DisplayMember = "NameService";
                comboBoxService.ValueMember = "Id";
                comboBoxService.DataSource = listS;
                comboBoxService.SelectedItem = 1;
            }
            dateTimePickerStart.Value = new DateTime().AddYears(2000);
            dateTimePickerEnd.Value = DateTime.Now.AddYears(10);
            LoadData(dateTimePickerStart.Value, dateTimePickerEnd.Value);
        }

        private void LoadData(DateTime start, DateTime end)
        {
            try
            {
                if (serviceV.check("date", start, end))
                {
                    List<ModelClient> list = service.GetList(start, end);
                    if (list != null)
                    {
                        dataGridView.DataSource = list;
                        dataGridView.Columns[1].AutoSizeMode =
                        DataGridViewAutoSizeColumnMode.Fill;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
            }
        }

        private void ToolStripButtonAdd_Click(object sender, EventArgs e)
        {
            if (serviceV.check("Passport", toolStripTextBoxPassport.Text ) && serviceV.check("FIO", toolStripTextBoxFullName.Text))
            {
                service.AddElement(new ModelClient
                {
                    FIO = toolStripTextBoxFullName.Text,
                    Passport = toolStripTextBoxPassport.Text,
                    Date = dateTimePickerClient.Value
                });
            }
            else
            {
                MessageBox.Show("Не корректное имя или пароль", "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
            }
            LoadData(dateTimePickerStart.Value, dateTimePickerEnd.Value);
        }

        private void ToolStripButtonChange_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 1)
            {
                int id = Convert.ToInt32(dataGridView.SelectedRows[0].Cells[0].Value);
                service.UpdElement(new ModelClient
                {
                    Id = id,
                    FIO = toolStripTextBoxFullName.Text,
                    Passport = toolStripTextBoxPassport.Text,
                    Date = dateTimePickerClient.Value
                });
            }
        }

        private void ToolStripButtonDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 1)
            {
                if (MessageBox.Show("Удалить запись", "Вопрос", MessageBoxButtons.YesNo,
               MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    int id = Convert.ToInt32(dataGridView.SelectedRows[0].Cells[0].Value);
                    try
                    {
                        service.DelElement(id);
                        MessageBox.Show("Выполнено", "Успех", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
                       MessageBoxIcon.Error);
                    }
                    LoadData(dateTimePickerStart.Value, dateTimePickerEnd.Value);
                }
            }
        }

        private void ToolStripButtonReload_Click(object sender, EventArgs e)
        {
            LoadData(dateTimePickerStart.Value, dateTimePickerEnd.Value);
        }


        private void ButtonSearch_Click(object sender, EventArgs e)
        {
            try
            {
                List<ModelClient> list = service.SearchByPassport(textBoxSearch.Text);
                if (list != null)
                {
                    dataGridView.DataSource = list;
                    dataGridView.Columns[1].AutoSizeMode =
                    DataGridViewAutoSizeColumnMode.Fill;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
            }
        }

        private void DataGridView_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                ModelClient client = service.GetElement(Convert.ToInt32(dataGridView.SelectedRows[0].Cells[0].Value));

                toolStripTextBoxFullName.Text = client.FIO;
                toolStripTextBoxPassport.Text = client.Passport;
                dateTimePickerClient.Value = client.Date;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Button6_Click(object sender, EventArgs e)
        {
            var list = serviceR.SearchByService(comboBoxService.Text);
            if (list != null)
            {
                dataGridView.DataSource = list;
                dataGridView.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            var list = serviceR.SearchByDate();
            if (list != null)
            {
                dataGridView.DataSource = list;
                dataGridView.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
        }
    }
}
