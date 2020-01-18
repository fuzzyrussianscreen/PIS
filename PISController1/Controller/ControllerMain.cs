using PISModel1.Model;
using System;
using System.Linq;

namespace PISController1.Controller
{
    public class ControllerMain
    {
        private PISDbContext context;
        public ControllerMain(PISDbContext context)
        {
            this.context = context;
        }
        public ModelUser Login(string login, string password)
        {
            ModelUser element = context.Users.FirstOrDefault(rec => rec.Login == login && rec.Password == password);
            if (element != null)
            {
                return new ModelUser
                {
                    Id = element.Id,
                    FIO = element.FIO,
                    Login = element.Login,
                    Password = element.Password,
                    Role = element.Role
                };
            }
            throw new Exception("Неверный логин или пароль");
        }
    }
}
