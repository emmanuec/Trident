using System;
using System.Collections.Generic;

namespace PainFree.Models.VM
{
    public class VirtualMachineList
    {
        public static List<String> GetAllVirtualMachines()
        {
            var virtualMachines = new List<String>()
           {
                "VMAUTOMATION01",
                "VMTEST01",
                "VMTEST02",
                "VMTEST03",
                "VMTEST04",
                "VMTEST05",
                "VMTEST06",
                "VMTEST07",
                "VMTEST08",
                "VMTEST09",
                "VMTEST10",
                "VMTEST11",
                "VMTEST12"
            };

            return virtualMachines;
        }
    }
}