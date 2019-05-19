using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Diagnostics;

namespace DNWS
{
  class StatPlugin : IPlugin
  {
    protected static Dictionary<String, int> statDictionary = null;
    public StatPlugin()
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
      HTTPResponse response = null;
      StringBuilder sb = new StringBuilder();
     // sb.Append("<html><body><h1>Stat:</h1>");
      foreach (KeyValuePair<String, int> entry in statDictionary)
      {
        //sb.Append(entry.Key + ": " + entry.Value.ToString() + "<br />");
      }
      sb.Append("</body></html>");

            ThreadPool.GetAvailableThreads(out int available, out int io); //Function to get available number of threads
            ThreadPool.GetMaxThreads(out int maximum_thread, out int completionPortThreads); //Function to get maximum number of thread 
            //sb.Append("<div>Number of threads : " +  + "</div><br>");
            //////////////  
            sb.Append("<div>Number of available threads: " + available + "</div><br>"); //Show available number of thread
            sb.Append("<div>Number of maximum usable threads: " + maximum_thread + "</div><br>"); //Show maximum usable thread
            sb.Append("<div>Number of active threads: " + (maximum_thread - available) + "</div><br>"); //Show active thread


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