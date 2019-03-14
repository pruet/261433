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
      sb.Append("<html><body><h1>Stat:</h1>");
      foreach (KeyValuePair<String, int> entry in statDictionary)
      {
        sb.Append(entry.Key + ": " + entry.Value.ToString() + "<br />");
      }

      sb.Append("<html><body><h3>CurrentThread ID : " + Thread.CurrentThread.ManagedThreadId + "</h3>"); //Show thread ID
      sb.Append("<html><body><h3>Thread Status : " + Thread.CurrentThread.ThreadState + "</h3>"); //Show thread Status 
      sb.Append("<html><body><h3>Thread IsAlive : " + Thread.CurrentThread.IsAlive + "</h3>"); //Show thread is alive or not
      sb.Append("<html><body><h3>Number of threads : " + Process.GetCurrentProcess().Threads.Count + "</h3>");//Show Amount of thread

      ThreadPool.GetAvailableThreads(out int workers, out int completion);
      ThreadPool.GetMaxThreads(out int maxWorkers, out int maxCompletion);
      ThreadPool.GetMinThreads(out int minWorkers, out int minCompletion);

      sb.Append("<html><body><h3>Size of thread pool: " + maxWorkers + "</h3>");
      sb.Append("<html><body><h3>Available threads in thread pool: " + workers + " </h3>");
      sb.Append("<html><body><h3>Active threads in thread pool: " + (maxWorkers - workers) + "</h3>");
      sb.Append("<html><body><h3>Max Thread size: " + maxWorkers + "</h3>");
      sb.Append("<html><body><h3>MIn Thread size: " + minWorkers + "</h3>");
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