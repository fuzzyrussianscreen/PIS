using PISModel1.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PISController1.Controller
{
    public class ControllerReporting
    {
        private PISDbContext context;
        public ControllerReporting(PISDbContext context)
        {
            this.context = context;
        }

        public List<AgentClient> GetListClientAndAgent(DateTime dateStart, DateTime dateEnd)
        {
            List<ModelClient> resultClient = context.Clients.AsEnumerable().Select(rec => new ModelClient
            {
                Id = rec.Id,
                FIO = rec.FIO,
                Date = rec.Date,
                Passport = rec.Passport,
            })
            .ToList();

            var resultAgent = context.ContractClients.Where(rec => rec.DateStart >= dateStart && rec.DateStart <= dateEnd).AsEnumerable().Select(recCI => new ModelContractClient
            {
                Id = recCI.Id,
                AgentId = recCI.AgentId,
                ClientId = recCI.ClientId
            })
            .ToList().Join(resultClient, r1 => r1.ClientId, r2 => r2.Id, (r1, r2) => new
            {
                Id = r2.Id,
                AgentId = r1.AgentId,
                FIO = r2.FIO,
                Date = r2.Date,
                Passport = r2.Passport

            }).ToList();

            List<AgentClient> result = new List<AgentClient>();
            foreach (var r2 in resultAgent)
            {
                result.Add(new AgentClient
                {
                    Id = r2.Id,
                    AgentId = r2.AgentId,
                    FIO = r2.FIO,
                    Date = r2.Date,
                    Passport = r2.Passport
                });
            }

            return result;
        }
        public List<table> SearchByServiceAndMonth(DateTime year)
        {
            DateTime date1 = new DateTime();

            List<ModelContractClient> resultSum = context.ContractClients.AsEnumerable().Select(rec => new ModelContractClient
            {
                Id = rec.Id,
                Sum = rec.Sum
            })
            .ToList();

            List<ModelServices> resultService = context.Services.AsEnumerable().Select(rec => new ModelServices
            {
                Id = rec.Id,
                NameService = rec.NameService,
                Price = rec.Price
            })
            .ToList();

            var result01 = context.Directories.AsEnumerable().Where(rec => rec.Data.Month == date1.Month && rec.Data.Year == year.Year).Select(rec => new ModelDirectory
            {
                ContractId = rec.ContractId,
                ServiceId = rec.ServiceId
            })
            .ToList().Join(resultSum,
                r1 => r1.ContractId,
                r2 => r2.Id,
                (r1, r2) => new
                {
                    ServiceId = r1.ServiceId
                }).Join(resultService, r1 => r1.ServiceId, r2 => r2.Id, (r1, r2) => new
                {
                    ServiceId = r2.Id,
                    NameService = r2.NameService,
                    Price = Convert.ToDouble(r2.Price)
                }).ToList().GroupBy(r1 => r1.NameService).Select(rec => new
                {
                    NameService = rec.Key,
                    Price = rec.Sum(r => r.Price),
                }
                    );

            var list2 = new[] { result01 }.ToList();
            for (int i = 1; i < 12; i++)
            {
                date1 = date1.AddMonths(1);
                var res = context.Directories.AsEnumerable().Where(rec => rec.Data.Month == date1.Month && rec.Data.Year == year.Year).Select(rec => new ModelDirectory
                {
                    ContractId = rec.ContractId,
                    ServiceId = rec.ServiceId
                }).ToList().Join(resultSum,
                r1 => r1.ContractId,
                r2 => r2.Id,
                (r1, r2) => new
                {
                    ServiceId = r1.ServiceId
                }).Join(resultService, r1 => r1.ServiceId, r2 => r2.Id, (r1, r2) => new
                {
                    ServiceId = r2.Id,
                    NameService = r2.NameService,
                    Price = Convert.ToDouble(r2.Price)
                }).ToList().GroupBy(r1 => r1.NameService).Select(rec => new
                {
                    NameService = rec.Key,
                    Price = rec.Sum(r => r.Price),
                }
                    );

                list2.Add(res);
            }
            var list = new[] { from c in resultService
                    join p in list2[0] on c.NameService equals p.NameService into ps
                    from p in ps.DefaultIfEmpty()
                    select new { ServiceId = c.Id, Sum = p == null ? 0 : p.Price, NameService = p.NameService }}.ToList();
            for (int i = 1; i < 12; i++)
            {
                var query =
                    from c in resultService
                    join e in list2[i] on c.NameService equals e.NameService into ps
                    from p in ps.DefaultIfEmpty()
                    select new { ServiceId = c.Id, Sum = p == null ? 0 : p.Price, NameService = c.NameService, };
                list.Add(query);
            }


            #region
            var result = list[0].Join(list[1], r1 => r1.ServiceId, r2 => r2.ServiceId, (r1, r2) => new
            {
                ServiceId = r1.ServiceId,
                NameService = r1.NameService,
                Sum1 = r1.Sum,
                Sum2 = r2.Sum
            }).Join(list[2], r1 => r1.ServiceId, r2 => r2.ServiceId, (r1, r2) => new
            {
                ServiceId = r1.ServiceId,
                NameService = r1.NameService,
                Sum1 = r1.Sum1,
                Sum2 = r1.Sum2,
                Sum3 = r2.Sum
            }).Join(list[3], r1 => r1.ServiceId, r2 => r2.ServiceId, (r1, r2) => new
            {
                ServiceId = r1.ServiceId,
                NameService = r1.NameService,
                Sum1 = r1.Sum1,
                Sum2 = r1.Sum2,
                Sum3 = r1.Sum3,
                Sum4 = r2.Sum
            }).Join(list[4], r1 => r1.ServiceId, r2 => r2.ServiceId, (r1, r2) => new
            {
                ServiceId = r1.ServiceId,
                NameService = r1.NameService,
                Sum1 = r1.Sum1,
                Sum2 = r1.Sum2,
                Sum3 = r1.Sum3,
                Sum4 = r1.Sum4,
                Sum5 = r2.Sum
            }).Join(list[5], r1 => r1.ServiceId, r2 => r2.ServiceId, (r1, r2) => new
            {
                ServiceId = r1.ServiceId,
                NameService = r1.NameService,
                Sum1 = r1.Sum1,
                Sum2 = r1.Sum2,
                Sum3 = r1.Sum3,
                Sum4 = r1.Sum4,
                Sum5 = r1.Sum5,
                Sum6 = r2.Sum
            }).Join(list[6], r1 => r1.ServiceId, r2 => r2.ServiceId, (r1, r2) => new
            {
                ServiceId = r1.ServiceId,
                NameService = r1.NameService,
                Sum1 = r1.Sum1,
                Sum2 = r1.Sum2,
                Sum3 = r1.Sum3,
                Sum4 = r1.Sum4,
                Sum5 = r1.Sum5,
                Sum6 = r1.Sum6,
                Sum7 = r2.Sum
            }).Join(list[7], r1 => r1.ServiceId, r2 => r2.ServiceId, (r1, r2) => new
            {
                ServiceId = r1.ServiceId,
                NameService = r1.NameService,
                Sum1 = r1.Sum1,
                Sum2 = r1.Sum2,
                Sum3 = r1.Sum3,
                Sum4 = r1.Sum4,
                Sum5 = r1.Sum5,
                Sum6 = r1.Sum6,
                Sum7 = r1.Sum7,
                Sum8 = r2.Sum
            }).Join(list[8], r1 => r1.ServiceId, r2 => r2.ServiceId, (r1, r2) => new
            {
                ServiceId = r1.ServiceId,
                NameService = r1.NameService,
                Sum1 = r1.Sum1,
                Sum2 = r1.Sum2,
                Sum3 = r1.Sum3,
                Sum4 = r1.Sum4,
                Sum5 = r1.Sum5,
                Sum6 = r1.Sum6,
                Sum7 = r1.Sum7,
                Sum8 = r1.Sum8,
                Sum9 = r2.Sum
            }).Join(list[9], r1 => r1.ServiceId, r2 => r2.ServiceId, (r1, r2) => new
            {
                ServiceId = r1.ServiceId,
                NameService = r1.NameService,
                Sum1 = r1.Sum1,
                Sum2 = r1.Sum2,
                Sum3 = r1.Sum3,
                Sum4 = r1.Sum4,
                Sum5 = r1.Sum5,
                Sum6 = r1.Sum6,
                Sum7 = r1.Sum7,
                Sum8 = r1.Sum8,
                Sum9 = r1.Sum9,
                Sum10 = r2.Sum
            }).Join(list[10], r1 => r1.ServiceId, r2 => r2.ServiceId, (r1, r2) => new
            {
                ServiceId = r1.ServiceId,
                NameService = r1.NameService,
                Sum1 = r1.Sum1,
                Sum2 = r1.Sum2,
                Sum3 = r1.Sum3,
                Sum4 = r1.Sum4,
                Sum5 = r1.Sum5,
                Sum6 = r1.Sum6,
                Sum7 = r1.Sum7,
                Sum8 = r1.Sum8,
                Sum9 = r1.Sum9,
                Sum10 = r1.Sum10,
                Sum11 = r2.Sum
            }).Join(list[11], r1 => r1.ServiceId, r2 => r2.ServiceId, (r1, r2) => new
            {
                ServiceId = r1.ServiceId,
                NameService = r1.NameService,
                Sum1 = r1.Sum1,
                Sum2 = r1.Sum2,
                Sum3 = r1.Sum3,
                Sum4 = r1.Sum4,
                Sum5 = r1.Sum5,
                Sum6 = r1.Sum6,
                Sum7 = r1.Sum7,
                Sum8 = r1.Sum8,
                Sum9 = r1.Sum9,
                Sum10 = r1.Sum10,
                Sum11 = r1.Sum11,
                Sum12 = r2.Sum
            });
            #endregion

            List<table> tables = new List<table>();
            table table1 = new table
            {
                NameService = "Всего",
                Jan = 0,
                Feb = 0,
                Mar = 0,
                Apr = 0,
                May = 0,
                Jun = 0,
                Jul = 0,
                Aug = 0,
                Sep = 0,
                Oct = 0,
                Nov = 0,
                Dec = 0,
            };
            foreach (var r1 in result)
            {
                tables.Add(new table
                {
                    NameService = r1.NameService,
                    Jan = r1.Sum1,
                    Feb = r1.Sum2,
                    Mar = r1.Sum3,
                    Apr = r1.Sum4,
                    May = r1.Sum5,
                    Jun = r1.Sum6,
                    Jul = r1.Sum7,
                    Aug = r1.Sum8,
                    Sep = r1.Sum9,
                    Oct = r1.Sum10,
                    Nov = r1.Sum11,
                    Dec = r1.Sum12
                });

                table1.Jan = table1.Jan + r1.Sum1;
                table1.Feb = table1.Feb + r1.Sum2;
                table1.Mar = table1.Mar + r1.Sum3;
                table1.Apr = table1.Apr + r1.Sum4;
                table1.May = table1.May + r1.Sum5;
                table1.Jun = table1.Jun + r1.Sum6;
                table1.Jul = table1.Jul + r1.Sum7;
                table1.Aug = table1.Aug + r1.Sum8;
                table1.Sep = table1.Sep + r1.Sum9;
                table1.Oct = table1.Oct + r1.Sum10;
                table1.Nov = table1.Nov + r1.Sum11;
                table1.Dec = table1.Dec + r1.Sum12;

            }
            tables.Add(table1);
            return tables;
        }

        public class table
        {
            public string NameService { get; set; }
            public double Jan { get; set; }
            public double Feb { get; set; }
            public double Mar { get; set; }
            public double Apr { get; set; }
            public double May { get; set; }
            public double Jun { get; set; }
            public double Jul { get; set; }
            public double Aug { get; set; }
            public double Sep { get; set; }
            public double Oct { get; set; }
            public double Nov { get; set; }
            public double Dec { get; set; }
        }

        public List<Contract> CalculationByService(int AgentId, DateTime dateStart, DateTime dateEnd)
        {
            int ContractId = context.ContractAgents.FirstOrDefault(recCI => recCI.AgentId == AgentId).Id;

            List<ModelServices> resultService = context.Services.AsEnumerable().Select(rec => new ModelServices
            {
                Id = rec.Id,
                NameService = rec.NameService,
                Price = rec.Price
            })
            .ToList();

            var resultDirect = context.Directories.AsEnumerable().Where(recCI => recCI.Data >= dateStart && recCI.Data <= dateEnd).Select(rec => new ModelDirectory
            {
                ContractId = rec.ContractId,
                ServiceId = rec.ServiceId,
            }).ToList()
            .Join(resultService,
                r1 => r1.ServiceId,
                r2 => r2.Id,
                (r1, r2) => new
                {
                    ServiceId = r1.ServiceId,
                    NameService = r2.NameService,
                    ContractId = r1.ContractId,
                    Price = r2.Price
                });

            var result2 = context.ContractClients.AsEnumerable().Where(recCI => recCI.AgentId == ContractId)
                .Select(rec => new ModelContractClient
                {
                    Id = rec.Id,
                    DateStart = rec.DateStart,
                    DateEnd = rec.DateEnd,
                    Sum = rec.Sum
                }).ToList().Join(resultDirect,
                r1 => r1.Id,
                r2 => r2.ContractId,
                (r1, r2) => new
                {
                    Id = r1.Id,
                    DateStart = r1.DateStart,
                    DateEnd = r1.DateEnd,
                    Price = r2.Price,
                    NameService = r2.NameService
                });

            var result3 = result2.GroupBy(r1 => r1.NameService).Select(rec => new
            {
                NameService = rec.Key,
                Sum = rec.Sum(r => Convert.ToDouble(r.Price))
            });

            List<Contract> result = new List<Contract>();
            Contract itog = new Contract
            {
                NameService = "Итого",
                Sum = 0
            };
            foreach (var r1 in result3)
            {
                result.Add(new Contract
                {
                    NameService = r1.NameService,
                    Sum = r1.Sum
                });
                itog.Sum = itog.Sum + r1.Sum;
            }
            result.Add(itog);
            return result;
        }

        public class Contract
        {
            public string NameService { get; set; }
            public double Sum { get; set; }
        }

        public class AgentClient
        {
            public int Id { get; set; }

            public int AgentId { get; set; }
            public string FIO { get; set; }
            public string Passport { get; set; }
            public DateTime Date { get; set; }
        }
    }
}
