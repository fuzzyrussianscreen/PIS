using PISModel1.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace PISController1.Controller
{
    public class ControllerArchiving
    {
        private PISDbContext context;
        public ControllerArchiving(PISDbContext context)
        {
            this.context = context;
        }

        public async Task SaveToJsonAsync(string fileName)
        {
            DataContractJsonSerializer formatterClient = new DataContractJsonSerializer(typeof(List<ModelClient>));
            MemoryStream msClient = new MemoryStream();
            formatterClient.WriteObject(msClient, await context.Clients.ToListAsync());
            msClient.Position = 0;
            StreamReader srClient = new StreamReader(msClient);
            string ClientJSON = srClient.ReadToEnd();
            srClient.Close();
            msClient.Close();

            DataContractJsonSerializer formatterContractClient = new DataContractJsonSerializer(typeof(List<ModelContractClient>));
            MemoryStream msContractClient = new MemoryStream();
            formatterContractClient.WriteObject(msContractClient, await context.ContractClients.ToListAsync());
            msContractClient.Position = 0;
            StreamReader srContractClient = new StreamReader(msContractClient);
            string ContractClientJSON = srContractClient.ReadToEnd();
            srContractClient.Close();
            msContractClient.Close();

            DataContractJsonSerializer formatterDirection = new DataContractJsonSerializer(typeof(List<ModelContractClient>));
            MemoryStream msDirection = new MemoryStream();
            formatterDirection.WriteObject(msDirection, await context.ContractClients.ToListAsync());
            msDirection.Position = 0;
            StreamReader srDirection = new StreamReader(msDirection);
            string DirectionJSON = srDirection.ReadToEnd();
            srDirection.Close();
            msDirection.Close();

            File.WriteAllText(fileName, "{\n" +
                "    \"Client\": " + ClientJSON + ", \n" +
                "    \"ContractClient\": " + ContractClientJSON + ", \n" +
                "    \"Direction\": " + DirectionJSON + ", \n" +
                "}");

            List<ModelContractClient> result = context.ContractClients.AsEnumerable().Where(rec => rec.DateEnd.Month < DateTime.Now.Month && rec.DateEnd.Year == DateTime.Now.Year)
                .Select(recCI => new ModelContractClient
                {
                    Id = recCI.Id,
                }).ToList();

            foreach (var item in result)
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        ModelContractClient element = context.ContractClients.FirstOrDefault(rec => rec.Id == item.Id);
                        if (element != null)
                        {
                            //context.ContractClients.Remove(element);
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
}
