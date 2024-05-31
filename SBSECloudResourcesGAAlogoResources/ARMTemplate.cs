namespace SBSECloudResourcesGAAlogoResources
{
    public class ARMTemplate
    {
        public string Schema { get; set; }
        public string ContentVersion { get; set; }
        public Parameters Parameters { get; set; }
        public Variables Variables { get; set; }
        public List<Resource> Resources { get; set; }
    }

    public class Parameters
    {
        public SkuNameParameter SkuName { get; set; }
        public SkuCapacityParameter SkuCapacity { get; set; }
        public SqlAdministratorLoginParameter SqlAdministratorLogin { get; set; }
        public SqlAdministratorLoginPasswordParameter SqlAdministratorLoginPassword { get; set; }
        public LocationParameter Location { get; set; }
    }

    public class SkuNameParameter
    {
        public string Type { get; set; }
        public string DefaultValue { get; set; }
        public List<string> AllowedValues { get; set; }
        public Metadata Metadata { get; set; }
    }

    public class SkuCapacityParameter
    {
        public string Type { get; set; }
        public int DefaultValue { get; set; }
        public int MaxValue { get; set; }
        public int MinValue { get; set; }
        public Metadata Metadata { get; set; }
    }

    public class SqlAdministratorLoginParameter
    {
        public string Type { get; set; }
        public Metadata Metadata { get; set; }
    }

    public class SqlAdministratorLoginPasswordParameter
    {
        public string Type { get; set; }
        public Metadata Metadata { get; set; }
    }

    public class LocationParameter
    {
        public string Type { get; set; }
        public string DefaultValue { get; set; }
        public Metadata Metadata { get; set; }
    }

    public class Metadata
    {
        public string Description { get; set; }
    }

    public class Variables
    {
        public string HostingPlanName { get; set; }
        public string WebsiteName { get; set; }
        public string SqlserverName { get; set; }
        public string DatabaseName { get; set; }
    }

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

    public class Sku
    {
        public string Name { get; set; }
        public string Capacity { get; set; }
    }

    public class ConnectionString
    {
        public string Value { get; set; }
        public string Type { get; set; }
    }

    public class ARMTemplateExample
    {
        public static void Main()
        {
            var armTemplate = new ARMTemplate
            {
                Schema = "https://schema.management.azure.com/schemas/2019-04-01/deploymentTemplate.json#",
                ContentVersion = "1.0.0.0",
                Parameters = new Parameters
                {
                    SkuName = new SkuNameParameter
                    {
                        Type = "string",
                        DefaultValue = "F1",
                        AllowedValues = new List<string>
                        {
                            "F1", "D1", "B1", "B2", "B3", "S1", "S2", "S3", "P1", "P2", "P3", "P4"
                        },
                        Metadata = new Metadata
                        {
                            Description = "Describes plan's pricing tier and instance size. Check details at https://azure.microsoft.com/en-us/pricing/details/app-service/"
                        }
                    },
                    SkuCapacity = new SkuCapacityParameter
                    {
                        Type = "int",
                        DefaultValue = 1,
                        MaxValue = 3,
                        MinValue = 1,
                        Metadata = new Metadata
                        {
                            Description = "Describes plan's instance count"
                        }
                    },
                    SqlAdministratorLogin = new SqlAdministratorLoginParameter
                    {
                        Type = "string",
                        Metadata = new Metadata
                        {
                            Description = "The admin user of the SQL Server"
                        }
                    },
                    SqlAdministratorLoginPassword = new SqlAdministratorLoginPasswordParameter
                    {
                        Type = "secureString",
                        Metadata = new Metadata
                        {
                            Description = "The password of the admin user of the SQL Server"
                        }
                    },
                    Location = new LocationParameter
                    {
                        Type = "string",
                        DefaultValue = "[resourceGroup().location]",
                        Metadata = new Metadata
                        {
                            Description = "Location for all resources."
                        }
                    }
                },
                Variables = new Variables
                {
                    HostingPlanName = "[format('hostingplan{0}', uniqueString(resourceGroup().id))]",
                    WebsiteName = "[format('website{0}', uniqueString(resourceGroup().id))]",
                    SqlserverName = "[format('sqlServer{0}', uniqueString(resourceGroup().id))]",
                    DatabaseName = "sampledb"
                },
                Resources = new List<Resource>
                {
                    new Resource
                    {
                        Type = "Microsoft.Sql/servers",
                        ApiVersion = "2021-02-01-preview",
                        Name = "[variables('sqlserverName')]",
                        Location = "[parameters('location')]",
                        Tags = new Dictionary<string, string>
                        {
                            { "displayName", "SQL Server" }
                        },
                        Properties = new Properties
                        {
                            AdministratorLogin = "[parameters('sqlAdministratorLogin')]",
                            AdministratorLoginPassword = "[parameters('sqlAdministratorLoginPassword')]",
                            Version = "12.0"
                        }
                    },
                    new Resource
                    {
                        Type = "Microsoft.Sql/servers/databases",
                        ApiVersion = "2021-02-01-preview",
                        Name = "[format('{0}/{1}', variables('sqlserverName'), variables('databaseName'))]",
                        Location = "[parameters('location')]",
                        Tags = new Dictionary<string, string>
                        {
                            { "displayName", "Database" }
                        },
                        Sku = new Sku
                        {
                            Name = "Basic"
                        },
                        Properties = new Properties
                        {
                            Collation = "SQL_Latin1_General_CP1_CI_AS",
                            MaxSizeBytes = 1073741824
                        },
                        DependsOn = new List<string>
                        {
                            "[resourceId('Microsoft.Sql/servers', variables('sqlserverName'))]"
                        }
                    },
                    new Resource
                    {
                        Type = "Microsoft.Sql/servers/firewallRules",
                        ApiVersion = "2021-02-01-preview",
                        Name = "[format('{0}/{1}', variables('sqlserverName'), 'AllowAllWindowsAzureIps')]",
                        Properties = new Properties
                        {
                            EndIpAddress = "0.0.0.0",
                            StartIpAddress = "0.0.0.0"
                        },
                        DependsOn = new List<string>
                        {
                            "[resourceId('Microsoft.Sql/servers', variables('sqlserverName'))]"
                        }
                    },
                    new Resource
                    {
                        Type = "Microsoft.Web/serverfarms",
                        ApiVersion = "2020-12-01",
                        Name = "[variables('hostingPlanName')]",
                        Location = "[parameters('location')]",
                        Tags = new Dictionary<string, string>
                        {
                            { "displayName", "HostingPlan" }
                        },
                        Sku = new Sku
                        {
                            Name = "[parameters('skuName')]",
                            Capacity = "[parameters('skuCapacity')]"
                        }
                    },
                    new Resource
                    {
                        Type = "Microsoft.Web/sites",
                        ApiVersion = "2020-12-01",
                        Name = "[variables('websiteName')]",
                        Location = "[parameters('location')]",
                        Tags = new Dictionary<string, string>
                        {
                            { "[format('hidden-related:{0}', resourceId('Microsoft.Web/serverfarms', variables('hostingPlanName')))]", "empty" },
                            { "displayName", "Website" }
                        },
                        Properties = new Properties
                        {
                            ServerFarmId = "[resourceId('Microsoft.Web/serverfarms', variables('hostingPlanName'))]"
                        },
                        DependsOn = new List<string>
                        {
                            "[resourceId('Microsoft.Web/serverfarms', variables('hostingPlanName'))]"
                        }
                    },
                    new Resource
                    {
                        Type = "Microsoft.Web/sites/config",
                        ApiVersion = "2020-12-01",
                        Name = "[format('{0}/{1}', variables('websiteName'), 'connectionstrings')]",
                        Properties = new Properties
                        {
                            ConnectionStrings = new Dictionary<string, ConnectionString>
                            {
                                {
                                    "DefaultConnection",
                                    new ConnectionString
                                    {
                                        Value = "[format('Data Source=tcp:{0},1433;Initial Catalog={1};User Id={2}@{3};Password={4};', reference(resourceId('Microsoft.Sql/servers', variables('sqlserverName'))).fullyQualifiedDomainName, variables('databaseName'), parameters('sqlAdministratorLogin'), reference(resourceId('Microsoft.Sql/servers', variables('sqlserverName'))).fullyQualifiedDomainName, parameters('sqlAdministratorLoginPassword'))]",
                                        Type = "SQLAzure"
                                    }
                                }
                            }
                        },
                        DependsOn = new List<string>
                        {
                            "[resourceId('Microsoft.Sql/servers', variables('sqlserverName'))]",
                            "[resourceId('Microsoft.Web/sites', variables('websiteName'))]"
                        }
                    },
                    new Resource
                    {
                        Type = "Microsoft.Insights/components",
                        ApiVersion = "2020-02-02",
                        Name = "[format('AppInsights{0}', variables('websiteName'))]",
                        Location = "[parameters('location')]",
                        Tags = new Dictionary<string, string>
                        {
                            { "[format('hidden-link:{0}', resourceId('Microsoft.Web/sites', variables('websiteName')))]", "Resource" },
                            { "displayName", "AppInsightsComponent" }
                        },
                        Properties = new Properties
                        {
                            Application_Type = "web"
                        },
                        DependsOn = new List<string>
                        {
                            "[resourceId('Microsoft.Web/sites', variables('websiteName'))]"
                        }
                    }
                }
            };
        }
    }
}
