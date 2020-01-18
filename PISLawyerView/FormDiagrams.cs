using PISController1.Controller;
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
    public partial class FormDiagrams : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly ControllerValidation validation;
        private readonly ControllerDiagrams service;
        private readonly ControllerPrinting serviceP;
        public FormDiagrams(ControllerDiagrams service, ControllerValidation validation, ControllerPrinting serviceP)
        {
            InitializeComponent();
            this.service = service;
            this.serviceP = serviceP;
            this.validation = validation;
        }

        private void FormSchedule_Load(object sender, EventArgs e)
        {
            if (validation.check("date", dateTimePicker1.Value, dateTimePicker2.Value))
            {
                this.chartProduct = service.GetDiagramm(chartProduct, dateTimePicker1.Value, dateTimePicker2.Value);

            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            if (validation.check("date", dateTimePicker1.Value, dateTimePicker2.Value))
            {

                string title = "диаграмма – количество договоров по видам юридических услуг с" +
                    Convert.ToString(dateTimePicker1.Text) + " по " + Convert.ToString(dateTimePicker2.Text);
                serviceP.saveDiagramm(title, chartProduct);
            }

        }
    }
}
