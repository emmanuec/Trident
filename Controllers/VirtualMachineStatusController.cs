using PainFree.Models.Repository;
using PainFree.Models.VM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PainFree.Controllers
{
    public class VirtualMachineStatusController : ApiController
    {
        private VirtualMachineDataRepository VirtualMachineRepo = new VirtualMachineDataRepository();

        [Route("vmstatusreport")]
        public IEnumerable<VirtualMachineStatusReport> Get(int page = 0, int pageSize = 10)
        {
            return VirtualMachineRepo.GetVmFailureReports();
        }

    }
}
