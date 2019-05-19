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
          
            // tracking thread
            sb.Append("<p>Thread-ID: " + Thread.CurrentThread.ManagedThreadId + "</p>");//Thread-ID
            sb.Append("<p>Thread-State: " + Thread.CurrentThread.ThreadState + "</p>");//Status of this thread
            sb.Append("<p>#Thread:" + Process.GetCurrentProcess().Threads.Count + "</p>");//number of this thread on this process
            sb.Append("<p>process-ID:" + Process.GetCurrentProcess().Id + "</p>");//PID
            sb.Append("<p>process-Name:" + Process.GetCurrentProcess().ProcessName + "</p>");//name of this process
            //Thradpool status
            ThreadPool.GetAvailableThreads(out int wk, out int io);
            ThreadPool.GetMaxThreads(out int Max_wk,out int Max_io);
            sb.Append("<p>#ThreadPool(worker)" + wk + "(available)</p>");//Max #worker thread in threadpool
            sb.Append("<p>#ThreadPool(asyn I/O)" + io + "</p>");//Max #asynchronous I/O threads in threadpool.
            sb.Append("<p>#ThreadActives"+(Max_wk-wk)+"</p>");//#Actives threads
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