using PISModel1.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PISController1.Controller
{
    public class ControllerUser
    {
        private PISDbContext context;
        public ControllerUser(PISDbContext context)
        {
            this.context = context;
        }
        public void AddElement(ModelUser model)
        {
            ModelUser element = context.Users.FirstOrDefault(rec => rec.Login == model.Login);
            if (element != null)
            {
                throw new Exception("Уже есть пользователь с таким Логином");
            }
            element = new ModelUser
            {
                Id = model.Id,
                FIO = model.FIO,
                Login = model.Login,
                Password = model.Password,
                Role = model.Role
            };
            context.Users.Add(element);
            context.SaveChanges();
        }

        public ModelUser GetElement(int id)
        {
            ModelUser element = context.Users.FirstOrDefault(rec => rec.Id == id);
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
            throw new Exception("Элемент не найден");
        }
    }
}
