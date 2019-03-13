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
            ThreadPool.GetAvailableThreads(out int workers, out int completion);
            ThreadPool.GetMaxThreads(out int a, out int b);
            StringBuilder sb = new StringBuilder();
            sb.Append("<html><body><h1>Stat:</h1><br/>");
            sb.Append("AvailableWorkerThreads: " + workers + "<br /><br />");
            sb.Append("AvailableCompletionPortThreads: " + completion + "<br /><br />");
            sb.Append("MaxWorkerThreads: " + a + "<br /><br />");
            sb.Append("MaxCompletionPortThreads: " + b + "<br /><br />");
            sb.Append("Number of active thread: " + Process.GetCurrentProcess().Threads.Count + "<br /><br />");
            foreach (KeyValuePair<String, int> entry in statDictionary)
      {
        sb.Append(entry.Key + ": " + entry.Value.ToString() + "<br />");
      }
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