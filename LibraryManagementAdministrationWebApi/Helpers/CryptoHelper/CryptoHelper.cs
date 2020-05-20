using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagementAdministrationWebApi.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    namespace LibraryManagement.Helpers
    {
        public static class CryptoHelper
        {
            public static string Hash(string value)
            {
                return System.Convert.ToBase64String(
                    System.Security.Cryptography.
                    SHA256.Create().ComputeHash(
                        Encoding.UTF8.GetBytes(value)));
            }
        }
    }


}
