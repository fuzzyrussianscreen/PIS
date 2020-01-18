using PISModel1.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms.DataVisualization.Charting;

namespace PISController1.Controller
{
    public class ControllerDiagrams
    {
        private PISDbContext context;
        public ControllerDiagrams(PISDbContext context)
        {
            this.context = context;
        }

        public Chart GetDiagramm(Chart chart, DateTime dateStart, DateTime dateEnd)
        {
           
            List<ModelServices> resultService = context.Services.AsEnumerable().Select(rec => new ModelServices
            {
                Id = rec.Id,
                NameService = rec.NameService
            })
            .ToList();

            var result01 = context.Directories.AsEnumerable().Where(rec => rec.Data >= dateStart && rec.Data <= dateEnd).Select(rec => new ModelDirectory
            {
                ContractId = rec.ContractId,
                ServiceId = rec.ServiceId
            })
            .ToList().Join(resultService,
                r1 => r1.ServiceId,
                r2 => r2.Id,
                (r1, r2) => new
                {
                    ContractId = r1.ContractId,
                    NameService = r2.NameService
                }).GroupBy(r1 => r1.NameService).Select(rec => new
                {
                    NameService = rec.Key,
                    Count = rec.Count()
                });

            chart.Series["Count"].XValueMember = "NameService";
            chart.Series["Count"].YValueMembers = "Count";

            chart.DataSource = result01;
            chart.DataBind();

            return chart;
        }
    }
}
