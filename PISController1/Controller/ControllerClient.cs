using PISModel1.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PISController1.Controller
{
    public class ControllerClient
    {
        private PISDbContext context;
        public ControllerClient(PISDbContext context)
        {
            this.context = context;
        }

        public List<ModelClient> GetList(DateTime dateStart, DateTime dateEnd)
        {
            List<ModelClient> result = context.Clients.AsEnumerable().Where(rec => rec.Date >= dateStart && rec.Date <= dateEnd).Select(rec => new ModelClient
            {
                Id = rec.Id,
                FIO = rec.FIO,
                Date = rec.Date,
                Passport = rec.Passport,
                ContractsClient = context.ContractClients
                .Where(recCI => recCI.ClientId == rec.Id).AsEnumerable()
                .Select(recCI => new ModelContractClient
                {
                    Id = recCI.Id,
                    ClientId = recCI.ClientId,
                    AgentId = recCI.AgentId,
                    Sum = recCI.Sum,
                    DateStart = recCI.DateStart,
                    DateEnd = recCI.DateStart,
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
                .ToList()
                
            })
            .ToList();
            return result;
        }


        public ModelClient GetElement(int id)
        {
            ModelClient element = context.Clients.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                return new ModelClient
                {
                    Id = element.Id,
                    FIO = element.FIO,
                    Date = element.Date,
                    Passport = element.Passport,
                    ContractsClient = context.ContractClients
                .Where(recCI => recCI.ClientId == element.Id).AsEnumerable()
                .Select(recCI => new ModelContractClient
                {
                    Id = recCI.Id,
                    ClientId = recCI.ClientId,
                    AgentId = recCI.AgentId,
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
                .ToList(),
                };
            }
            throw new Exception("Элемент не найден");
        }

        public void AddElement(ModelClient model)
        {
            ModelClient element = context.Clients.FirstOrDefault(rec => rec.FIO == model.FIO);
            if (element != null)
            {
                throw new Exception("Уже есть клиент с таким ФИО");
            }
            element = new ModelClient
            {
                FIO = model.FIO,
                Date = model.Date,
                Passport = model.Passport
            };
            context.Clients.Add(element);
            context.SaveChanges();
        }
        public void UpdElement(ModelClient model)
        {
            ModelClient element = context.Clients.FirstOrDefault(rec => rec.FIO == model.FIO && rec.Id != model.Id);
            if (element != null)
            {
                throw new Exception("Уже есть клиент с таким ФИО");
            }
            element = context.Clients.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.FIO = model.FIO;
            element.Date = model.Date;
            element.Passport = model.Passport;
            context.SaveChanges();
        }

        public void DelElement(int id)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    ModelClient element = context.Clients.FirstOrDefault(rec => rec.Id ==
                   id);
                    if (element != null)
                    {
                        context.Clients.Remove(element);
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

        public List<ModelClient> SearchByPassport(string Passport)
        {
            List<ModelClient> result = context.Clients.Where(rec => rec.Passport == Passport || rec.FIO == Passport).AsEnumerable().Select(rec => new ModelClient
            {
                Id = rec.Id,
                FIO = rec.FIO,
                Date = rec.Date,
                Passport = rec.Passport,
                ContractsClient = context.ContractClients
                .Where(recCI => recCI.ClientId == rec.Id).AsEnumerable()
                .Select(recCI => new ModelContractClient
                {
                    Id = recCI.Id,
                    ClientId = recCI.ClientId,
                    AgentId = recCI.AgentId,
                    Sum = recCI.Sum,
                    DateStart = recCI.DateStart,
                    DateEnd = recCI.DateStart,
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
                .ToList()
            })
            .ToList();
            return result;
        }

        public List<ModelClient> SearchByNumber(int Number)
        {
            ModelContractClient Id = context.ContractClients.FirstOrDefault(recCI => recCI.Id == Number);
            List <ModelClient> result = context.Clients.Where(rec => rec.Id == Id.ClientId).Select(rec => new ModelClient
            {
                Id = rec.Id,
                FIO = rec.FIO,
                Date = rec.Date,
                Passport = rec.Passport,
                ContractsClient = context.ContractClients
                .Where(recCI => recCI.ClientId == rec.Id).AsEnumerable()
                .Select(recCI => new ModelContractClient
                {
                    Id = recCI.Id,
                    ClientId = recCI.ClientId,
                    AgentId = recCI.AgentId,
                    Sum = recCI.Sum,
                    DateStart = recCI.DateStart,
                    DateEnd = recCI.DateStart,
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
                .ToList()
            })
            .ToList();
            return result;
        }
    }
}
