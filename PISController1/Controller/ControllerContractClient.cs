using PISController1;
using PISModel1.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PISController1.Controller
{
    public class ControllerContractClient
    {
        private PISDbContext context;
        public ControllerContractClient(PISDbContext context)
        {
            this.context = context;
        }

        public List<ModelContractClient> GetList(DateTime dateStart, DateTime dateEnd)
        {
            List<ModelContractClient> result = context.ContractClients.AsEnumerable().Where(rec => rec.DateStart >= dateStart && rec.DateStart <= dateEnd).Select(recCI => new ModelContractClient
            {
                Id = recCI.Id,
                AgentId = recCI.AgentId,
                ClientId = recCI.ClientId,
                Sum = recCI.Sum,
                DateStart = recCI.DateStart,
                DateEnd = recCI.DateEnd,
                Directories = context.Directories
                .Where(recD => recD.ContractId == recCI.Id).AsEnumerable()
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

        public List<ModelDirectory> GetListDirection(int? Id)
        {
            /*
            List<ModelContractClient> result = context.ContractClients.AsEnumerable().Where(r => r.ClientId == Id).Select(recCI => new ModelContractClient
            {
                Id = recCI.Id,
                AgentId = recCI.AgentId,
                ClientId = recCI.ClientId,
                Sum = recCI.Sum,
                DateStart = recCI.DateStart,
                DateEnd = recCI.DateEnd,
                Directories = context.Directories
                .Where(recD => recD.ContractId == recCI.Id).AsEnumerable()
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
            */
            List<ModelDirectory> element = context.Directories.Where(recD => recD.ContractId == Id).AsEnumerable()
                .Select(recD => new ModelDirectory
                {
                    Id = recD.Id,
                    ContractId = recD.ContractId,
                    Data = recD.Data,
                    ServiceId = recD.ServiceId,

                }).ToList();

            return element;
        }

        public ModelContractClient GetElement(int id)
        {
            ModelContractClient element = context.ContractClients.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                return new ModelContractClient
                {
                    Id = element.Id,
                    AgentId = element.AgentId,
                    ClientId = element.ClientId,
                    Sum = element.Sum,
                    DateStart = element.DateStart,
                    DateEnd = element.DateEnd,
                    Directories = context.Directories
                .Where(recD => recD.ContractId == element.Id).AsEnumerable()
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
            List<ModelDirectory> element = context.Directories.Where(recD => recD.ContractId == id).AsEnumerable()
                .Select(recD => new ModelDirectory
                {
                    Id = recD.Id,
                    ContractId = recD.ContractId,
                    Data = recD.Data,
                    ServiceId = recD.ServiceId,

                }).ToList();
            double sum = 0;
            foreach (var item in element)
            {
                ModelServices serv = context.Services.FirstOrDefault(rec => rec.Id == item.ServiceId);
                sum = sum + Convert.ToDouble(serv.Price);
            }
            return sum+"";
            throw new Exception("Элемент не найден");
        }

        public int AddElement(ModelContractClient model)
        {
            ModelContractClient element = new ModelContractClient
            {
                Id = model.Id,
                AgentId = model.AgentId,
                ClientId = model.ClientId,
                Sum = model.Sum,
                DateStart = model.DateStart,
                DateEnd = model.DateEnd
            };
            context.ContractClients.Add(element);
            context.SaveChanges();
            return element.Id;
        }
        public void UpdElement(ModelContractClient model)
        {
            ModelContractClient element = context.ContractClients.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.AgentId = model.AgentId;
            element.ClientId = model.ClientId;
            element.Sum = model.Sum;
            element.DateStart = model.DateStart;
            element.DateEnd = model.DateEnd;
            context.SaveChanges();
        }

        public void DelElement(int id)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    ModelContractClient element = context.ContractClients.FirstOrDefault(rec => rec.Id ==
                   id);
                    if (element != null)
                    {
                        context.ContractClients.Remove(element);
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
