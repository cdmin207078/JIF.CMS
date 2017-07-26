using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace JIF.CMS.Core.Configuration
{
    [Serializable]
    [XmlRoot("JIFConfig")]
    /// <summary>
    /// Represents a JIFConfig
    /// </summary>
    public partial class JIFConfig : IConfigurationSectionHandler
    {
        /// <summary>
        /// Creates a configuration section handler.
        /// </summary>
        /// <param name="parent">Parent object.</param>
        /// <param name="configContext">Configuration context object.</param>
        /// <param name="section">Section XML node.</param>
        /// <returns>The created section handler object.</returns>
        public object Create(object parent, object configContext, XmlNode section)
        {
            var config = new JIFConfig();

            //config.AttachmentUploadFTPAddress = section.SelectSingleNode("AttachmentUploadFTPAddress").InnerText;
            //config.AttachmentUploadFTPAccount = section.SelectSingleNode("AttachmentUploadFTPAccount").InnerText;
            //config.AttachmentUploadFTPPwd = section.SelectSingleNode("AttachmentUploadFTPPwd").InnerText;


            //string typeName = ((XmlElement)section).GetAttribute("type");
            XmlSerializer xz = new XmlSerializer(typeof(JIFConfig));
            using (StringReader sr = new StringReader(section.OuterXml))
            {
                return xz.Deserialize(sr);
            }

            //var startupNode = section.SelectSingleNode("Startup");
            //config.IgnoreStartupTasks = GetBool(startupNode, "IgnoreStartupTasks");

            //var redisCachingNode = section.SelectSingleNode("RedisCaching");
            //config.RedisCachingEnabled = GetBool(redisCachingNode, "Enabled");
            //config.RedisCachingConnectionString = GetString(redisCachingNode, "ConnectionString");

            //var userAgentStringsNode = section.SelectSingleNode("UserAgentStrings");
            //config.UserAgentStringsPath = GetString(userAgentStringsNode, "databasePath");

            //var supportPreviousJIFcommerceVersionsNode = section.SelectSingleNode("SupportPreviousJIFcommerceVersions");
            //config.SupportPreviousJIFcommerceVersions = GetBool(supportPreviousJIFcommerceVersionsNode, "Enabled");

            //var webFarmsNode = section.SelectSingleNode("WebFarms");
            //config.MultipleInstancesEnabled = GetBool(webFarmsNode, "MultipleInstancesEnabled");
            //config.RunOnAzureWebsites = GetBool(webFarmsNode, "RunOnAzureWebsites");

            //var azureBlobStorageNode = section.SelectSingleNode("AzureBlobStorage");
            //config.AzureBlobStorageConnectionString = GetString(azureBlobStorageNode, "ConnectionString");
            //config.AzureBlobStorageContainerName = GetString(azureBlobStorageNode, "ContainerName");
            //config.AzureBlobStorageEndPoint = GetString(azureBlobStorageNode, "EndPoint");

            //var installationNode = section.SelectSingleNode("Installation");
            //config.DisableSampleDataDuringInstallation = GetBool(installationNode, "DisableSampleDataDuringInstallation");
            //config.UseFastInstallationService = GetBool(installationNode, "UseFastInstallationService");
            //config.PluginsIgnoredDuringInstallation = GetString(installationNode, "PluginsIgnoredDuringInstallation");

            return config;
        }

        private string GetString(XmlNode node, string attrName)
        {
            return SetByXElement<string>(node, attrName, Convert.ToString);
        }

        private bool GetBool(XmlNode node, string attrName)
        {
            return SetByXElement<bool>(node, attrName, Convert.ToBoolean);
        }

        private T SetByXElement<T>(XmlNode node, string attrName, Func<string, T> converter)
        {
            if (node == null || node.Attributes == null) return default(T);
            var attr = node.Attributes[attrName];
            if (attr == null) return default(T);
            var attrVal = attr.Value;
            return converter(attrVal);
        }

        /// <summary>
        /// Indicates whether we should ignore startup tasks
        /// </summary>
        public string AttachmentUploadFTPAddress { get; set; }


        public string AttachmentUploadFTPAccount { get; set; }


        public string AttachmentUploadFTPPwd { get; set; }


    }
}
