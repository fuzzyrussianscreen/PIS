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
    public partial class FormReports : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly ControllerReporting serviceR;
        private readonly ControllerPrinting serviceP;
        private readonly ContractAgent serviceCA;
        private readonly ControllerValidation validation;
        public FormReports(ControllerReporting serviceR, ContractAgent serviceCA, ControllerPrinting serviceP, ControllerValidation validation)
        {
            InitializeComponent();
            this.serviceR = serviceR;
            this.serviceCA = serviceCA;
            this.serviceP = serviceP;
            this.validation = validation;

        }

        private void FormReports_Load(object sender, EventArgs e)
        {
            List<ModelContractAgent> listCA = serviceCA.GetList();
            if (listCA != null)
            {
                comboBoxAgent.DisplayMember = "Id";
                comboBoxAgent.ValueMember = "AgentId";
                comboBoxAgent.DataSource = listCA;
                comboBoxAgent.SelectedItem = 0;
            }

            dateTimePicker1.CustomFormat = "dd.MM.yyyy";
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker2.CustomFormat = "dd.MM.yyyy";
            dateTimePicker2.Format = DateTimePickerFormat.Custom;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            var data = serviceR.SearchByServiceAndMonth(DateTime.Now);
            if ((data != null) && (validation.check("date", dateTimePicker1.Value, dateTimePicker2.Value)))
                    {
                dataGridView.DataSource = data;
                dataGridView.Name = "Анализ распределения договоров по видам юридических услуг и месяцам с " +
                    "" + Convert.ToString(dateTimePicker1.Text) + " по " + Convert.ToString(dateTimePicker2.Text);
            }
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            if (validation.check("date", dateTimePicker1.Value, dateTimePicker2.Value))
            {
                var data = serviceR.CalculationByService(Convert.ToInt32(comboBoxAgent.SelectedValue), dateTimePicker1.Value, dateTimePicker2.Value);
                if (data != null)
                {
                    dataGridView.DataSource = data;
                    dataGridView.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                    dataGridView.Name = "Отчёт по работе агента с " +
                        "" + Convert.ToString(dateTimePicker1.Text) + " по " + Convert.ToString(dateTimePicker2.Text);
                }
            }
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            if (validation.check("date", dateTimePicker1.Value, dateTimePicker2.Value))
            {
                var data = serviceR.GetListClientAndAgent(dateTimePicker1.Value, dateTimePicker2.Value);
                if (data != null)
                {
                    dataGridView.DataSource = data;

                    dataGridView.Name = "Список клиентов для каждого агента с " +
                       "" + Convert.ToString(dateTimePicker1.Text) + " по " + Convert.ToString(dateTimePicker2.Text);
                }
            }
        }

        

        private void Button2_Click(object sender, EventArgs e)
        {
            serviceP.savePDF(dataGridView.Name, dataGridView, "");
        }
    }
}
