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
    public partial class FormMainLawyer : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        public string fio;

        public FormMainLawyer()
        {
            InitializeComponent();
        }

        private void ButtonArch_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormArchiveList>();
            form.ShowDialog();
        }

        private void ButtonDIagr_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormDiagrams>();
            form.ShowDialog();
        }

        private void ButtonClient_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormListClients>();
            form.ShowDialog();
        }

        private void ButtonContract_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormFillingContracts>();
            form.ID = null;
            form.ShowDialog();
        }

        private void ButtonContractList_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormListContracts>();
            form.ShowDialog();
        }

        private void ButtonReport_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormReports>();
            
            form.ShowDialog();
        }

        private void ButtonProgramm_Click(object sender, EventArgs e)
        {
            MessageBox.Show( "АРМ Юриста\nАвтоматизация отделения юридической компании" +
                "\nКурило Дмитрий, ИСЭ-31\nг.Ульяновск, 2020г", "О программе", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
