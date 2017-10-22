using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace JIF.CMS.Core.Configuration
{
    class JIFConfigManager
    {
        internal static JIFConfig GetConfig()
        {
            //var config = HttpRuntime.Cache.Get("JIFConfig") as JIFConfig;
            //if (config != null)
            //    return config;

            //var appsettings = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "appsettings.json");

            //var cdp = new CacheDependency(appsettings);
            //var setting = JsonConvert.DeserializeObject<JIFConfig>(System.IO.File.ReadAllText(appsettings));

            //HttpContext.Cache.Insert("JIFConfig", setting, cdp);

            //return setting;

            ObjectCache cache = MemoryCache.Default;
            var config = cache["JIFConfig"] as JIFConfig;

            if (config == null)
            {
                CacheItemPolicy policy = new CacheItemPolicy();
                var appSettingsFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "appsettings.json");
                policy.ChangeMonitors.Add(new HostFileChangeMonitor(new[] { appSettingsFile }));

                config = JsonConvert.DeserializeObject<JIFConfig>(System.IO.File.ReadAllText(appSettingsFile));

                cache.Add("JIFConfig", config, policy);
            }
            return config;
        }
    }
}
