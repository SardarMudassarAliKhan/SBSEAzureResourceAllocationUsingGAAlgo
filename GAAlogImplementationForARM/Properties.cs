using SBSECloudResourcesGAAlogoResources;

namespace GAAlogImplementationForARM
{
    public class Properties
    {
        public string AdministratorLogin { get; set; }
        public string AdministratorLoginPassword { get; set; }
        public string Version { get; set; }
        public string Collation { get; set; }
        public long MaxSizeBytes { get; set; }
        public string StartIpAddress { get; set; }
        public string EndIpAddress { get; set; }
        public string ServerFarmId { get; set; }
        public Dictionary<string, ConnectionString> ConnectionStrings { get; set; }
        public string Application_Type { get; set; }
    }
}
