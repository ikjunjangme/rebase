using Newtonsoft.Json;
using NKAPIService.API.Converter;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NKAPIService.API.ComputingNode.Models
{
    public class Node
    {
        [JsonProperty("nodeId")]
        public string NodeId { get; set; }
        [JsonProperty("httpHost")]
        public string HttpHost { get; set; }
        [JsonProperty("httpPort")]
        public int HttpPort { get; set; }
        [JsonProperty("rpcHost")]
        public string RpcHost { get; set; }
        [JsonProperty("rpcPort")]
        public int RpcPort { get; set; }
        [JsonProperty("nodeName")]
        public string NodeName { get; set; }
        [JsonProperty("releaseDate")]
        public string ReleaseDate { get; set; }
        [JsonProperty("productCode")]
        public string ProductCode { get; set; }
        [JsonProperty("productVersion")]
        public string ProductVersion { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("licenseValidity")]
        public bool LicenseValidity { get; set; }
        [JsonProperty("licenseExpired")]
        [JsonConverter(typeof(StringToDateTimeConverter))]
        public DateTime LicenseExpired { get; set; }
        [JsonProperty("functions")]
        public List<Functions> Functions { get; set; }

        public void CopyFrom(Node node)
        {
            NodeId = node.NodeId;
            HttpHost = node.HttpHost;
            HttpPort = node.HttpPort;
            RpcHost = node.RpcHost;
            RpcPort = node.RpcPort;
            NodeName = node.NodeName;
            ReleaseDate = node.ReleaseDate;
            ProductCode = node.ProductCode;
            ProductVersion = node.ProductVersion;
            Description = node.Description;
            LicenseValidity = node.LicenseValidity;
            LicenseExpired = node.LicenseExpired;
            Functions = node.Functions.ToList();
        }
    }
}
