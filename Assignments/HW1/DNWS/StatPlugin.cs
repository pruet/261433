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
            var th = Thread.CurrentThread;
            int worker = 0;
            int io = 0;
            ThreadPool.GetAvailableThreads(out worker, out io);
            sb.Append("<html><body><h1>Stat:</h1>");
   
            sb.Append("<p>Thread ID : " + th.ManagedThreadId + "</p>");
            sb.Append("<p>Thread is alive : " + th.IsAlive + "</p>");//This thread is still alive or not 
            sb.Append("<p>Thread run on background : " + th.IsBackground + "</p>");//This thread has run on background or not
            sb.Append("<p>Thread status : " + th.ThreadState + "</p>");
            sb.Append("<p>Thread available: " + worker + "</p>");
            sb.Append("<p>Size of thread: " + io + "</p>");
            sb.Append("<p>Active thread: " + (io - worker) + "</p>");
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