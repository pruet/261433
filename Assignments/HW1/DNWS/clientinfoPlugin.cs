using System;
using System.Collections.Generic;
using System.Text;

namespace DNWS
{
    class clientinfoPlugin : IPlugin
    {
        protected static Dictionary<String, int> statDictionary = null;
        public clientinfoPlugin()
        {
            if (statDictionary == null)
            {
                statDictionary = new Dictionary<String, int>();

            }
        }

        public void PreProcessing(HTTPRequest request)
    {
      if (statDictionary.ContainsKey(request.Url))
      {
        statDictionary[request.Url] = (int)statDictionary[request.Url] + 1;
      }
      else
      {
        statDictionary[request.Url] = 1;
      }
    }
    public HTTPResponse GetResponse(HTTPRequest request)
    {
      string box= request.info;
      HTTPResponse response = null;
      StringBuilder sb = new StringBuilder();
      sb.Append("<html><body><h2>Client IP:</h2>");
      foreach (KeyValuePair<String, int> entry in statDictionary)
      {
        sb.Append(entry.Key + ": " + entry.Value.ToString() + "<br />");
      }
      sb.Append("</body><h2>Client Port:</h2></html>");
      sb.Append("</body><h2>Browser Information:</h2></html>");
      sb.Append("</body><h2>Acept Language: "+ box +" </h2></html>");
      sb.Append("</body><h2>Acept Encoding:</h2></html>");
      response = new HTTPResponse(200);
      response.body = Encoding.UTF8.GetBytes(sb.ToString());
      return response;
    }

    public HTTPResponse PostProcessing(HTTPResponse response)
    {
      throw new NotImplementedException();
    }
    }
}
