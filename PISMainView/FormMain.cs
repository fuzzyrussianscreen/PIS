using PISController1.Controller;
using PISLawyerView;
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

namespace PISMainView
{
    public partial class FormMain : Form
    {
        bool regis;

        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly ControllerUser service;
        private readonly ControllerMain serviceMain;
        public FormMain(ControllerUser service, ControllerMain serviceMain)
        {
            InitializeComponent();
            this.service = service;
            this.serviceMain = serviceMain;
            regis = false;
        }

        private void Form_Load(object sender, EventArgs e)
        {
            comboBoxRoles.Items.Add("Юрист");
            comboBoxRoles.Items.Add("Бухгалтер");
            comboBoxRoles.Items.Add("Руководитель");
            comboBoxRoles.SelectedIndex = 0;

            refreshForm();
        }

        public void refreshForm()
        {
            panel1.Visible = regis;
            panel1.Enabled = regis;
            buttonLogin.Visible = !regis;
        }

        private void ButtonBack_Click(object sender, EventArgs e)
        {
            regis = false;
            refreshForm();
        }

        private void ButtonLogin_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxLogin.Text))
            {
                MessageBox.Show("Заполните логин", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(textBoxPassword.Text))
            {
                MessageBox.Show("Заполните пароль", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
               ModelUser view = serviceMain.Login(textBoxLogin.Text, textBoxPassword.Text);
                if (!string.IsNullOrEmpty(view.FIO))
                {

                    if (view.Role == "Юрист")
                    {
                        var form = Container.Resolve<FormMainLawyer>();
                        form.fio = view.FIO;
                        form.ShowDialog();
                    }
                    if (view.Role == "Бухгалтер")
                    {
                        MessageBox.Show("ARM Бухгалтера в данный момент недоступна", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    if (view.Role == "Руководитель")
                    {
                        MessageBox.Show("АРМ Руководителя в данный момент недоступна", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
            }
            textBoxLogin.Clear();
            textBoxPassword.Clear();
            return;
        }

        private void ButtonRegistration_Click(object sender, EventArgs e)
        {
            if (regis == false)
            {
                regis = true;
            }
            else
            {
                service.AddElement(new ModelUser
                {
                    FIO = textBoxFIO.Text,
                    Login = textBoxLogin.Text,
                    Password = textBoxPassword.Text,
                    Role = comboBoxRoles.Text
                });
            }
            refreshForm();
        }
    }
}
