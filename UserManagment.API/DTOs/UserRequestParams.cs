using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace UserManagment.API.DTOs
{
    public class UserRequestParams : RequestParams
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string userName { get; set; }
        public string emailAddress { get; set; }
        public string OrderBy { get; set; }

        //public int status { get; set; }

        [JsonIgnore]
        public bool hasFilters{ get { return !string.IsNullOrEmpty(firstName) || !string.IsNullOrEmpty(lastName) || !string.IsNullOrEmpty(userName) || !string.IsNullOrEmpty(emailAddress); } }
    }
}
