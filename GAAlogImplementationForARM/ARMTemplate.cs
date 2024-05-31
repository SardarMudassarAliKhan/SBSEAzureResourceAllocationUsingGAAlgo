using SBSECloudResourcesGAAlogoResources;

namespace GAAlogImplementationForARM
{
    public class ARMTemplate
    {
        public string Schema { get; set; }
        public string ContentVersion { get; set; }
        public Parameters Parameters { get; set; }
        public Variables Variables { get; set; }
        public List<Resource> Resources { get; set; }
    }
}
