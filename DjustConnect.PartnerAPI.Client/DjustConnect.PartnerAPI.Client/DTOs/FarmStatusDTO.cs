﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DjustConnect.PartnerAPI.Client
{
    public class FarmStatusDTO
    {
        [JsonProperty("farmNumber", Required = Required.Always)]
        public string FarmNumber { get; set; }
        [JsonProperty("status", Required = Required.Always)]
        public string Status { get; set; }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }

        public static FarmStatusDTO FromJson(string data)
        {
            return JsonConvert.DeserializeObject<FarmStatusDTO>(data);
        }
    }
}