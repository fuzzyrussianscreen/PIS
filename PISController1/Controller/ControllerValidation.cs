using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace PISController1.Controller
{
    public class ControllerValidation
    {
        public bool check(string v, DateTime value1, DateTime value2)
        {
            if (v == "date")
            {
                if (value1 <= value2)
                {
                    return true;
                }
                else
                {
                    MessageBox.Show("Неверные даты", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            return false;
        }

        public bool check(string v, string data)
        {
            if (v == "FIO")
            {
                if ((data != "" )&&(data.Length<200)&&(!(new Regex(@"[\d\W!#h]")).Match(data).Success))
                {
                    return true;
                }
                else
                {
                    MessageBox.Show("Неверные ФИО", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            if (v == "Passport")
            {
                if ((data != "") && (data.Length == 8) && (new Regex(@"\d{8}")).Match(data).Success)
                {
                    return true;
                }
                else
                {
                    MessageBox.Show("Неверные паспортные данные", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            return false;
        }
    }
}
