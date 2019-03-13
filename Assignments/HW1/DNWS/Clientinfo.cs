using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Diagnostics;


namespace DNWS
{
  class Clientinfo : IPlugin
  {
    
    public Clientinfo()
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
      string[] ip_port = request.getPropertyByKey("RemoteEndPoint").Split(":");
      string ip_client = ip_port[0];
      string port_client = ip_port[1];
      sb.Append("<html><body>");
      sb.Append("<p>Client IP : " +ip_client+"</p>");
      sb.Append("<p>Client Port : " + port_client+"</p>");
      sb.Append("<p>Browser Information : " + request.getPropertyByKey("User-Agent")+"</p>");
      sb.Append("<p>Accept Language : "+request.getPropertyByKey("Accept-Language")+"</p>");
      sb.Append("<p>Accept Encoding : "+request.getPropertyByKey("Accept-Encoding")+"</p>");
        
      //tracking thread
      sb.Append("<p>Thread-ID: " + Thread.CurrentThread.ManagedThreadId+ "</p>");//Thread-ID
      sb.Append("<p>Thread-State: " + Thread.CurrentThread.ThreadState+ "</p>");//Status of this thread
      sb.Append("<p>#Thread:"+ Process.GetCurrentProcess().Threads.Count+"</p>");//number of this thread on this process
      sb.Append("<p>process-ID:"+ Process.GetCurrentProcess().Id+ "</p>");//PID
      sb.Append("<p>process-Name:"+ Process.GetCurrentProcess().ProcessName+ "</p>");//name of this process

           

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