using System;
using System.Collections.Generic;
using System.Text;

/// <summary>
/// Summary description for infoClient
/// </summary>
namespace DNWS
{
    class infoClient : IPlugin
    {
        protected static Dictionary<String, int> infoClientDictionary = null;
        public infoClient()
        {
            if(infoClientDictionary == null)
            {
                infoClientDictionary = new Dictionary<String, int>();
            }
        }
    }

}
