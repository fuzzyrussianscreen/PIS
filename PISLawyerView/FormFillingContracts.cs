using PISController1.Controller;
using PISController1.ControllerOther;
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
    public partial class FormFillingContracts : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        public int? ID;

        private readonly ControllerDirection serviceD;
        private readonly ControllerContractClient serviceCC;
        private readonly ContractAgent serviceCA;
        private readonly ControllerClient serviceC;
        private readonly ControllerService serviceS;
        private readonly ControllerPrinting serviceP;
        private readonly ControllerValidation validation;

        public FormFillingContracts(ControllerDirection serviceD, ControllerClient serviceC, ControllerContractClient serviceCC, 
            ContractAgent serviceCA, ControllerService serviceS, ControllerPrinting serviceP, ControllerValidation validation)
        {
            InitializeComponent();
            this.serviceD = serviceD;
            this.serviceCC = serviceCC;
            this.serviceC = serviceC;
            this.serviceS = serviceS;
            this.serviceCA = serviceCA;
            this.validation = validation;
            this.serviceP = serviceP;
        }

        private void LoadData()
        {
            try
            {
                List<ModelContractAgent> listCA = serviceCA.GetList();
                if (listCA != null)
                {
                    comboBoxAgent.DisplayMember = "Id";
                    comboBoxAgent.ValueMember = "Id";
                    comboBoxAgent.DataSource = listCA;
                    comboBoxAgent.SelectedItem = null;
                }

                List<ModelClient> listC = serviceC.GetList(new DateTime().AddYears(2000), DateTime.Now.AddYears(10));
                if (listC != null)
                {
                    comboBoxClient.DisplayMember = "FIO";
                    comboBoxClient.ValueMember = "Id";
                    comboBoxClient.DataSource = listC;
                    comboBoxClient.SelectedItem = null;
                }

                List<ModelServices> listS = serviceS.GetList();
                if (listS != null)
                {
                    toolStripComboBoxService.ComboBox.DisplayMember = "NameService";
                    toolStripComboBoxService.ComboBox.ValueMember = "Id";
                    toolStripComboBoxService.ComboBox.DataSource = listS;
                    toolStripComboBoxService.ComboBox.SelectedItem = 0;
                }

                textBoxSum.Text = "0";

                if (ID != null)
                {
                    List<ModelDirectory> list = serviceCC.GetListDirection(ID);
                    if (list != null)
                    {
                        dataGridView1.DataSource = list;
                        dataGridView1.Columns[1].AutoSizeMode =
                        DataGridViewAutoSizeColumnMode.Fill;
                        dataGridView1.Columns[4].Visible = false;
                        dataGridView1.Columns[5].Visible = false;
                    }

                    ModelContractClient elem = serviceCC.GetElement(Convert.ToInt32( ID));
                    if (elem != null)
                    {
                        ModelClient client = serviceC.GetElement(elem.ClientId);
                        comboBoxAgent.Text = elem.AgentId + "";
                        comboBoxClient.Text = client.FIO;
                        dateTimePicker1.Value = elem.DateStart;
                        dateTimePicker2.Value = elem.DateEnd;
                    }

                    textBoxSum.Text = serviceCC.GetSum(Convert.ToInt32(ID));

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
            if (ID != null)
            {
                serviceD.AddElement(new ModelDirectory
                {
                    ContractId = Convert.ToInt32(this.ID),
                    Data = dateTimePicker1.Value,
                    ServiceId = Convert.ToInt32(toolStripComboBoxService.ComboBox.SelectedValue)
                });
                LoadData();
            }
        }

        private void ToolStripButtonChange_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 1)
            {
                int id = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);
                serviceD.UpdElement(new ModelDirectory
                {
                    Id = id,
                    Data = dateTimePicker1.Value,
                    ContractId = Convert.ToInt32(this.ID),
                    ServiceId = Convert.ToInt32(toolStripComboBoxService.ComboBox.SelectedValue)
                });
            }
        }

        private void ToolStripButtonDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 1)
            {
                if (MessageBox.Show("Удалить запись", "Вопрос", MessageBoxButtons.YesNo,
               MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    int id = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);
                    try
                    {
                        serviceD.DelElement(id);
                        MessageBox.Show("Выполнено", "Успех", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
                       MessageBoxIcon.Error);
                    }
                    LoadData();
                }
            }
        }

        private void ButtonSave_Click(object sender, EventArgs e)
        {
            if (ID == null)
            {
                if (validation.check("date", dateTimePicker1.Value, dateTimePicker2.Value))
                {
                    ID = serviceCC.AddElement(new ModelContractClient
                    {
                        AgentId = Convert.ToInt32(comboBoxAgent.SelectedValue),
                        ClientId = Convert.ToInt32(comboBoxClient.SelectedValue),
                        DateStart = dateTimePicker1.Value,
                        DateEnd = dateTimePicker2.Value,
                        Sum = Convert.ToDouble(textBoxSum.Text)
                    });
                    LoadData();
                }
            }
            else
            {
                if (validation.check("date", dateTimePicker1.Value, dateTimePicker2.Value))
                {
                    serviceCC.UpdElement(new ModelContractClient
                    {
                        Id = Convert.ToInt32(ID),
                        AgentId = Convert.ToInt32(comboBoxAgent.SelectedValue),
                        ClientId = Convert.ToInt32(comboBoxClient.SelectedValue),
                        DateStart = dateTimePicker1.Value,
                        DateEnd = dateTimePicker2.Value,
                        Sum = Convert.ToDouble(textBoxSum.Text)
                    });
                    LoadData();
                }
            }
        }

        private void FormFillingContracts_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void DataGridView_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                ModelDirectory direc = serviceD.GetElement(Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value));

                toolStripComboBoxService.SelectedItem = direc.ServiceId;
                dateTimePicker1.Value = direc.Data;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ToolStripComboBoxService_SelectedIndexChanged(object sender, EventArgs e)
        {
            toolStripTextBoxSum.Text = serviceS.GetSum(Convert.ToInt32(toolStripComboBoxService.ComboBox.SelectedValue));
        }

        private void ButtonPrint_Click(object sender, EventArgs e)
        {
            if (validation.check("date", dateTimePicker1.Value, dateTimePicker2.Value))
            {
                dataGridView1.Name = "Договор номер: " + Convert.ToInt32(ID) + " на оказание услуг с " +
                    Convert.ToString(dateTimePicker1.Text) + " по " + Convert.ToString(dateTimePicker2.Text);
                string data = "Агента: " + Convert.ToInt32(comboBoxAgent.SelectedValue) + " Клиента: " + comboBoxClient.Text +
                    " на сумму: " + Convert.ToDouble(textBoxSum.Text);
                serviceP.savePDF(dataGridView1.Name, dataGridView1, data);
            }
        }
    }
}
