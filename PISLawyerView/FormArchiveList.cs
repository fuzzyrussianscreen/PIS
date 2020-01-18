using PISController1.Controller;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Unity;

namespace PISLawyerView
{
    public partial class FormArchiveList : Form
    {

        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly ControllerArchiving service;
        public FormArchiveList(ControllerArchiving service)
        {
            InitializeComponent();
            this.service = service;
        }

        private async void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                //"C:\\Users\\Dimon\\Desktop\\Backup" + DateTime.Now + ".json"
                await service.SaveToJsonAsync(Path.Combine("C:\\Users\\Dimon\\Desktop\\Backup" + DateTime.Now.DayOfYear + ".json"));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
            }
        }
    }
}
