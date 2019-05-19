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
      
      //Show thread of client (Multithread)
      //sb.Append("<div>CurrentThread ID : " + Thread.CurrentThread.ManagedThreadId + "</div><br>"); //Thread ID
      //sb.Append("<div>Thread Status : " + Thread.CurrentThread.ThreadState + "</div><br>"); //Thread Status
      //sb.Append("<div>Thread IsAlive : "  + Thread.CurrentThread.IsAlive + "</div><br>"); //Thread is alive or not
      //sb.Append("<div>Number of threads : " + Process.GetCurrentProcess().Threads.Count + "</div><br>"); //Amount of threads
      
      //ThreadPool
      //Show thread ref : https://stackoverflow.com/questions/5236493/active-thread-number-in-thread-pool
      ThreadPool.GetAvailableThreads(out int worker, out int ioCompletion);
      ThreadPool.GetMaxThreads(out int Maxworker, out int MaxioCompletion);
      ThreadPool.GetMinThreads(out int Minworker, out int MinioCompletion);
      sb.Append("<br><u><div>ThreadPool info</u></div><br>");
      sb.Append("<div>CurrentThread ID : " + Thread.CurrentThread.ManagedThreadId + "</div><br>"); //Thread ID
      sb.Append("<div>IsPoolThread : " + Thread.CurrentThread.IsThreadPoolThread + "</div><br>"); //Is pool Thread ?
      sb.Append("<div>AvaliableThread : " + worker + "</div><br>"); //Avaliable threads
      sb.Append("<div>ActiveThread : " + (Maxworker - worker) + "</div><br>"); //Active Threads
      sb.Append("<div>Max. Thread size : " + Maxworker + "</div><br>"); //Maximum number of threads
      sb.Append("<div>Min. Thread size: " + Minworker + "</div><br>");//Minimum number of threads
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