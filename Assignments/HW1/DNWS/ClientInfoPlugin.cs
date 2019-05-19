using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Diagnostics;

namespace DNWS
{
  class ClientInfoPlugin : IPlugin
  {
    //protected static Dictionary<String, int> statDictionary = null;
    public ClientInfoPlugin()
    {
      
    }

    public void PreProcessing(HTTPRequest request)
    {
      throw new NotImplementedException();
    }
    public HTTPResponse GetResponse(HTTPRequest request)
    {
      HTTPResponse response = null;
      StringBuilder sb = new StringBuilder();
            
        string[] IP= request.getPropertyByKey("RemoteEndPoint").Split(":"); // Split IP value and Port value
          //  request.getPropertyByKey("RemoteEndPoint");
      sb.Append("<html><body><p>Client IP: " +IP[0] + "</p>"); // Show Client IP
      sb.Append("<p>Client Port: " + IP[1]+"</p>"); // Show Client Port
      sb.Append("<p>Browser Information: " + request.getPropertyByKey("User-Agent") + "</p>"); // Show browser information
      sb.Append("<p>Accept-Language: " + request.getPropertyByKey("Accept-Language") + "</p>"); // Show accept-charset
      sb.Append("<p>Accept-Encoding: " + request.getPropertyByKey("Accept-Encoding") + "</p>"); // Show accept encoding
      sb.Append("</body></html>");
      
      Process thisProc = Process.GetCurrentProcess(); // Creates a new Process object and associates it with the currently active process
      int ProcThread = thisProc.Threads.Count; 
      Console.WriteLine("Threads ID : " + Thread.CurrentThread.ManagedThreadId);
      Console.WriteLine("Threads    : " + ProcThread);
      Console.WriteLine("PID        : " + thisProc.Id);

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