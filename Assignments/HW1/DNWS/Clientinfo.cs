using System;
using System.Collections.Generic;
using System.Text;

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
      sb.Append("<p1>Client IP : " +ip_client+"</p>");
      sb.Append("<p1>Client Port : " + port_client+"</p>");
      sb.Append("<p1>Browser Information : " + request.getPropertyByKey("User-Agent")+"</p>");
      sb.Append("<p1>Accept Language : "+request.getPropertyByKey("Accept-Language")+"</p>");
     
      sb.Append("<p1>Accept Encoding : " + request.getPropertyByKey("Accept-Encoding") + "</p>");
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