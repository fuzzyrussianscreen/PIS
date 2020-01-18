using PISModel1.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PISController1.Controller
{
    public class ControllerDirection
    {

        private PISDbContext context;
        public ControllerDirection(PISDbContext context)
        {
            this.context = context;
        }

        public List<ModelDirectory> GetList()
        {
            List<ModelDirectory> result = context.Directories.Select(rec => new ModelDirectory
            {
                Id = rec.Id,
                ContractId = rec.ContractId,
                ServiceId = rec.ServiceId,
                Data = rec.Data
            })
            .ToList();
            return result;
        }
        public ModelDirectory GetElement(int id)
        {
            ModelDirectory element = context.Directories.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                return new ModelDirectory
                {
                    Id = element.Id,
                    ContractId = element.ContractId,
                    ServiceId = element.ServiceId,
                    Data = element.Data
                };
            }
            throw new Exception("Элемент не найден");
        }

        public void AddElement(ModelDirectory model)
        {
            ModelDirectory element = new ModelDirectory
            {
                ContractId = model.ContractId,
                ServiceId = model.ServiceId,
                Data = model.Data
            };
            context.Directories.Add(element);
            context.SaveChanges();
        }
        public void UpdElement(ModelDirectory model)
        {
            ModelDirectory element = context.Directories.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            

            element.ContractId = model.ContractId;
            element.ServiceId = model.ServiceId;
            element.Data = model.Data;

            context.SaveChanges();
        }

        public void DelElement(int id)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    ModelDirectory element = context.Directories.FirstOrDefault(rec => rec.Id == id);
                    if (element != null)
                    {
                        context.Directories.Remove(element);
                        context.SaveChanges();
                    }
                    else
                    {
                        throw new Exception("Элемент не найден");
                    }
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
    }
}
