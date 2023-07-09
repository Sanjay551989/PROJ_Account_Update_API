using Newtonsoft.Json;
using System;

namespace PROJ_Synchronise_Account_Api.Models.Data.Domain
{
    public class BaseEntity
    {
        [JsonIgnore]
        public DateTimeOffset? DateCreated { get; set; }

        [JsonIgnore]                
        public string UserCreated { get; set; }

        [JsonIgnore]
        public DateTimeOffset? DateModified { get; set; }

        [JsonIgnore]        
        public string UserModified { get; set; }

        [JsonIgnore]
        public bool IsDeleted { get; set; }
    }
}
