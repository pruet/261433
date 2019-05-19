using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

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
      sb.Append("<html><body><h1>Stat:</h1>");
      foreach (KeyValuePair<String, int> entry in statDictionary)
      {
        sb.Append(entry.Key + ": " + entry.Value.ToString() + "<br />");
      }


            //threadpool 
            ThreadPool.GetAvailableThreads(out int thread_available, out int io); //get available number of worker and io
            ThreadPool.GetMaxThreads(out int max_thread, out int max_io); //get max number of worker and io

            sb.Append("<p>Available Thread : " + thread_available + "</p>"); //show available thread
            sb.Append("<p>Maximum Thread : " + max_thread + "</p>"); //show maximum thread
            sb.Append("<p>Active Thread : " + (max_thread - thread_available) + "</p>"); //display active thread
            sb.Append("</body></html>");
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