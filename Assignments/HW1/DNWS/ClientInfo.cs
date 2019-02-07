using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq;

namespace DNWS
{
  class ClientInfo : IPlugin
  {
    protected static Dictionary<String, int> statDictionary = null;
    public ClientInfo()
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
    public HTTPResponse GetResponse(HTTPRequest request) //Response Function
    {
        HTTPResponse response = null;
        StringBuilder sb = new StringBuilder(); //Create web page
        string data = request.ClientInfo; //Request data from client(In these case we need : browser infomation,accept language,accept encoding)
        string[] line = Regex.Split(data,"\\n"); //Split data into line with \\n
        string ClientIPandPort = request.getip(); //Request IPandPort from client
        string[] IPandPort = Regex.Split(ClientIPandPort,":"); //Split ClientIPandPort into line with :
        sb.Append("<html><body>Client IP: "+ IPandPort[0] + "<br><br>Client Port: " + IPandPort[1]); //Show Cilent IP and Client Port
        sb.Append("<br><br>Browser Information: " + line[5].Substring(12) + "<br><br>" + line[8] + "<br><br>" + line[7] + "</body></html>"); //Show Browser Information ,Accept Language and Accept Encoding
        response = new HTTPResponse(200); //Response with code 200 (mean ok)
        response.body = Encoding.UTF8.GetBytes(sb.ToString());
        return response;
    }

    public HTTPResponse PostProcessing(HTTPResponse response)
    {
      throw new NotImplementedException();
    }
  }
}