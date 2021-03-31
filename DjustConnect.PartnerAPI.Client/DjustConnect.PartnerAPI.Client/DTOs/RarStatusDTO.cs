﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DjustConnect.PartnerAPI.Client.DTOs
{
    public class RarStatusDTO
    {
        [JsonProperty("partnerName", Required = Required.Always)]
        public string PartnerName { get; set; }
        [JsonProperty("resourceName", Required = Required.Always)]
        public string ResourceName { get; set; }
        [JsonProperty("apiName", Required = Required.Always)]
        public string ApiName { get; set; }
        [JsonProperty("providerName", Required = Required.Always)]
        public string ProviderName { get; set; }
        [JsonProperty("status", Required = Required.Always)]
        public string Status { get; set; }

    }
}
