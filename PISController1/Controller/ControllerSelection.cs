using PISModel1.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PISController1.Controller
{
    public class ControllerSelection
    {
        private PISDbContext context;
        public ControllerSelection(PISDbContext context)
        {
            this.context = context;
        }

        public List<Contract> SearchByService(string Servise)
        {
            int ServiceId = context.Services.FirstOrDefault(recCI => recCI.NameService == Servise).Id;
            var resultDirectories = context.Directories.AsEnumerable().Where(rec => rec.ServiceId == ServiceId).Select(rec => new ModelDirectory
            {
                ContractId = rec.ContractId,
                Data = rec.Data
            })
            .ToList();

            List<ModelContractClient> resultContract = new List<ModelContractClient>();
            foreach (ModelDirectory Dir in resultDirectories)
            {
                List<ModelContractClient> resultContract2 = context.ContractClients.AsEnumerable().Where(rec => rec.Id == Dir.ContractId).Select(rec => new ModelContractClient
                {
                    Id = rec.Id,
                    ClientId = rec.ClientId,
                    DateEnd = rec.DateEnd
                })
                .ToList();

                foreach (ModelContractClient item in resultContract2)
                {
                    resultContract.Add(item);
                }
            }
            List<Contract> result = new List<Contract>();
            foreach (ModelContractClient Contract in resultContract)
            {
                var result2 = context.Clients.AsEnumerable().Where(rec => rec.Id == Contract.ClientId).Select(rec => new ModelClient
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
                .ToList().Join(resultContract, r1 => Contract.Id, r2 => r2.Id, (r1, r2) => new
                {
                    Id = r1.Id,
                    FIO = r1.FIO,
                    Date = r1.Date,
                    Passport = r1.Passport,
                    DateEnd = r2.DateEnd
                });

                foreach (var item in result2)
                {
                    result.Add(new Contract
                    {
                        Id = item.Id,
                        FIO = item.FIO,
                        Date = item.Date,
                        Passport = item.Passport,
                        DateEnd = item.DateEnd
                    });
                }
            }
            return result;

        }

        public List<Contract> SearchByDate()
        {
            List<ModelContractClient> resultContract = new List<ModelContractClient>();

            List<ModelContractClient> resultContract2 = context.ContractClients.AsEnumerable().Where(rec =>
            rec.DateEnd.Year == DateTime.Now.Year || rec.DateStart.Year <= DateTime.Now.Year)
                .Select(rec => new ModelContractClient
                {
                    ClientId = rec.ClientId,
                    DateEnd = rec.DateEnd,
                    Id = rec.Id
                }).OrderBy(rec => rec.DateEnd)
            .ToList();

            List < Contract> result = new List<Contract>();
            foreach (ModelContractClient Contract in resultContract2)
            {
                var result2 = context.Clients.AsEnumerable().Where(rec => rec.Id == Contract.ClientId).Select(rec => new ModelClient
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
                .ToList().Join(resultContract2, r1 => Contract.Id , r2 => r2.Id, (r1, r2) => new
                {
                    Id = r1.Id,
                    FIO = r1.FIO,
                    Date = r1.Date,
                    Passport = r1.Passport,
                    DateEnd = r2.DateEnd
                });

                foreach (var item in result2)
                {
                    result.Add( new Contract
                    {
                        Id = item.Id,
                        FIO = item.FIO,
                        Date = item.Date,
                        Passport = item.Passport,
                        DateEnd = item.DateEnd
                    });
                }
            }
            return result;
        }

        public class Contract
        {
            public int Id { get; set; }
            public string FIO { get; set; }
            public string Passport { get; set; }
            public DateTime Date { get; set; }
            public DateTime DateEnd { get; set; }
        }

    }
}