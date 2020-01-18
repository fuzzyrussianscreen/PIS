using PISModel1.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PISController1.Controller
{
    public class ControllerService
    {
        private PISDbContext context;
        public ControllerService(PISDbContext context)
        {
            this.context = context;
        }

        public List<ModelServices> GetList()
        {
            List<ModelServices> result = context.Services.AsEnumerable().Select(rec => new ModelServices
            {
                Id = rec.Id,
                NameService = rec.NameService,
                Price = rec.Price,
                Directories = context.Directories
                .Where(recD => recD.ServiceId == rec.Id).AsEnumerable()
                .Select(recD => new ModelDirectory
                {
                    Id = recD.Id,
                    ContractId = recD.ContractId,
                    Data = recD.Data,
                    ServiceId = recD.ServiceId
                })
                .ToList()
            })
            .ToList();
            return result;
        }
        public ModelServices GetElement(int id)
        {
            ModelServices element = context.Services.AsEnumerable().FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                return new ModelServices
                {
                    Id = element.Id,
                    NameService = element.NameService,
                    Price = element.Price,
                    Directories = context.Directories
                .Where(recD => recD.ServiceId == element.Id).AsEnumerable()
                .Select(recD => new ModelDirectory
                {
                    Id = recD.Id,
                    ContractId = recD.ContractId,
                    Data = recD.Data,
                    ServiceId = recD.ServiceId
                })
                .ToList()
                };
            }
            throw new Exception("Элемент не найден");
        }

        public string GetSum(int id)
        {
            ModelServices element = context.Services.AsEnumerable().FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                return element.Price;
            }
            throw new Exception("Элемент не найден");
        }

        public void AddElement(ModelServices model)
        {
            ModelServices element = context.Services.AsEnumerable().FirstOrDefault(rec => rec.NameService == model.NameService);
            if (element != null)
            {
                throw new Exception("Уже есть услуга с таким Названием");
            }
            element = new ModelServices
            {
                Id = model.Id,
                NameService = model.NameService,
                Price = model.Price
            };
            context.Services.Add(element);
            context.SaveChanges();
        }
        public void UpdElement(ModelServices model)
        {
            ModelServices element = context.Services.AsEnumerable().FirstOrDefault(rec => rec.NameService == model.NameService && rec.Id != model.Id);
            if (element != null)
            {
                throw new Exception("Уже есть услуга с таким Названием");
            }
            element = context.Services.AsEnumerable().FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.NameService = model.NameService;
            element.Price = model.Price;
            context.SaveChanges();
        }

        public void DelElement(int id)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    ModelServices element = context.Services.AsEnumerable().FirstOrDefault(rec => rec.Id == id);
                    if (element != null)
                    {
                        context.Services.Remove(element);
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
