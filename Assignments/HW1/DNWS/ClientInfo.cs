using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq;
using System.Threading;
using System.Diagnostics;

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
        sb.Append("<br><br>Browser Information: " + line[5].Substring(12) + "<br><br>" + line[8] + "<br><br>" + line[7]); //Show Browser Information ,Accept Language and Accept Encoding
        //This is multi thread information showing that we not use now
        //sb.Append("<br><br>Thread ID: " + Thread.CurrentThread.ManagedThreadId); //Show thread ID
        //sb.Append("<br><br>Thread state: " + Thread.CurrentThread.ThreadState); //Show thread state
        //sb.Append("<br><br>Current number of thread: " + Process.GetCurrentProcess().Threads.Count); //Show number of thread
        //Below these will be thread pool information showing which the code is guide by these reference
        //Ref. https://stackoverflow.com/questions/5236493/active-thread-number-in-thread-pool
        ThreadPool.GetAvailableThreads(out int available, out int ioCompletion);
        ThreadPool.GetMinThreads(out int Minworker, out int MinioCompletion);
        ThreadPool.GetMaxThreads(out int Maxworker, out int MaxioCompletion);
        sb.Append("<br><br>Thread ID: " + Thread.CurrentThread.ManagedThreadId);
        sb.Append("<br><br>Min Thread: " + Minworker);
        sb.Append("<br><br>Max Thread: " + Maxworker);
        sb.Append("<br><br>Available Thread: " + available);
        sb.Append("<br><br>Current running Thread: " + (Maxworker - available));
        sb.Append("</body></html>");
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