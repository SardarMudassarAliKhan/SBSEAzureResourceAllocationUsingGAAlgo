using SBSECloudResourcesGAAlogoResources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GAAlogImplementationForARM
{
    public class Resource
    {
        public string Type { get; set; }
        public string ApiVersion { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public Dictionary<string, string> Tags { get; set; }
        public Properties Properties { get; set; }
        public Sku Sku { get; set; }
        public List<string> DependsOn { get; set; }
    }
}
