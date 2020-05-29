using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PsHelloAzure.Models
{
    public class Course
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        public string Title { get; set; }
        public ICollection<Module> Modules { get; set; }
    }

    public class Module
    {
        public string Title { get; set; }
        public ICollection<Clip> Clips { get; set; }
    }

    public class Clip
    {
        public string Name { get; set; }
        public int Length { get; set; }
    }

    public class SampleData
    {
        public IEnumerable<Course> GetCourses()
        {
            yield return new Course
            {
                Title = "Azure for .NET Developers",
                Modules = new List<Module>
                {
                    new Module
                    {
                        Title = "Introduction to Azure",
                        Clips =  new List<Clip>
                        {
                            new Clip { Length = 30, Name = "Overview" },
                            new Clip { Length = 35, Name = "Azure Portal" },
                            new Clip { Length = 30, Name = "Virtual Machines" }
                        }
                    },
                    new Module
                    {
                        Title = "Cloud Databases",
                        Clips =  new List<Clip>
                        {
                            new Clip { Length = 30, Name = "Overview" },
                            new Clip { Length = 35, Name = "Azure SQL" },
                            new Clip { Length = 30, Name = "Cosmos DB" }
                        }
                    }
                }
            };

            yield return new Course
            {
                Title = "Building Secure Services in Azure",
                Modules = new List<Module>
                {
                    new Module
                    {
                        Title = "Azure AD",
                        Clips =  new List<Clip>
                        {
                            new Clip { Length = 30, Name = "Creating Apps" },
                            new Clip { Length = 35, Name = "Using OIDC" },
                            new Clip { Length = 30, Name = "Managing users" }
                        }
                    },

                    new Module
                    {
                        Title = "Azure Resource Manager",
                        Clips =  new List<Clip>
                        {
                            new Clip { Length = 30, Name = "Automation!" },
                            new Clip { Length = 35, Name = "ARM Templates" },
                        }
                    }
                }                
            };

            yield return new Course
            {
                Title = "ASP.NET Core Fundamentals",
                Modules = new List<Module>
                {
                    new Module
                    {
                        Title = "Introduction to ASP.NET Core",
                        Clips =  new List<Clip>
                        {
                            new Clip { Length = 30, Name = "Overview" },
                            new Clip { Length = 35, Name = "dotnet" },
                            new Clip { Length = 30, Name = "Startup" }
                        }
                    },

                    new Module
                    {
                        Title = "Middleware",
                        Clips =  new List<Clip>
                        {
                            new Clip { Length = 30, Name = "Overview" },
                            new Clip { Length = 35, Name = "RequestDelegates" },
                            new Clip { Length = 30, Name = "HttpContext" },
                            new Clip { Length = 30, Name = "Building a pipeline" }
                        }
                    },

                    new Module
                    {
                        Title = "Controllers",
                        Clips =  new List<Clip>
                        {
                            new Clip { Length = 30, Name = "Overview" },
                            new Clip { Length = 35, Name = "Building APIs" },
                            new Clip { Length = 30, Name = "Processing JSON" }
                        }
                    }
                }
            };
        }
    }
}
