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
    public partial class FormListContracts : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly ControllerContractClient service;
        private readonly ControllerValidation validation;
        public FormListContracts(ControllerContractClient service, ControllerValidation validation)
        {
            InitializeComponent();
            this.service = service;
            this.validation = validation;
        }

        private void FormListClients_Load(object sender, EventArgs e)
        {
            dateTimePicker1.Value = new DateTime().AddYears(2000);
            dateTimePicker2.Value = DateTime.Now.AddYears(10);
            LoadData(dateTimePicker1.Value, dateTimePicker2.Value);

        }

        private void LoadData(DateTime start, DateTime end)
        {
            try
            {
                if (validation.check("date", start, end))
                {
                    List<ModelContractClient> list = service.GetList(start, end);
                    if (list != null)
                    {
                        dataGridView1.DataSource = list;
                        dataGridView1.Columns[6].Visible = false;
                        dataGridView1.Columns[7].Visible = false;
                        dataGridView1.Columns[1].AutoSizeMode =
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

        private void ButtonReload_Click(object sender, EventArgs e)
        {
            LoadData(dateTimePicker1.Value, dateTimePicker2.Value);
        }

        private void DataGridView_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                var form = Container.Resolve<FormFillingContracts>();
                form.ID = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);
                form.ShowDialog();
                LoadData(dateTimePicker1.Value, dateTimePicker2.Value);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
