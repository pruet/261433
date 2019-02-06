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
      string[] cli_info = request.getPropertyByKey("RemoteEndpoint").Split(':');
      sb.Append("<html><body><p>Client IP: "+ cli_info[0] +"</p>");
      foreach (KeyValuePair<String, int> entry in statDictionary)
      {
        sb.Append(entry.Key + ": " + entry.Value.ToString() + "<br />");
      }
      string[] split1 = box.Split("User-Agent:");
      string[] split2 = box.Split("Accept-Encoding");
      string[] split3 = box.Split("Accept-Language");
      string[] ac_lang = split2[1].Split("Accept-Language");
      string[] browser = split1[1].Split("Accept");
      sb.Append("</body><p>Client Port: "+ cli_info[1] +"</p></html>");
      sb.Append("</body><p>Browser Information: "+ browser[0] + "</p></html>");
      sb.Append("</body><p>Acept-Language " + split3[1] + "</p></html>");
      sb.Append("</body><p>Acept-Encoding "+ ac_lang[0] +"</p></html>");
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
