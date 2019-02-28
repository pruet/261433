using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Diagnostics;

namespace DNWS
{
    class clientinfoPlugin : IPlugin //create the contructor
    {
        protected static Dictionary<String, int> statDictionary = null;
        public clientinfoPlugin()
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
      string box= request.info; //use .info to return the request's information
      HTTPResponse response = null;
      StringBuilder sb = new StringBuilder();
      ///SPLITTING///
      string[] cli_info = request.getPropertyByKey("RemoteEndpoint").Split(':'); //get the information from request which are stored in the RemoteEndpoint
      ///
      ///DISPLAY///
      sb.Append("<html><body><p>Client IP: "+ cli_info[0] +"</p>"); //Show the information before the colon 
      sb.Append("</body><p>Client Port: "+ cli_info[1] +"</p></html>"); ////Show the information after the colon 
      /////////////
      foreach (KeyValuePair<String, int> entry in statDictionary)
      {
        sb.Append(entry.Key + ": " + entry.Value.ToString() + "<br />");
      }
      ///SPLITTING///
      string[] split1 = box.Split("User-Agent:"); //split the string at "User-Agent"
      string[] split2 = box.Split("Accept-Encoding"); //split the string at "Accept-Encoding"
      string[] split3 = box.Split("Accept-Language"); //split the string at "Accept-Language"
      string[] ac_lang = split2[1].Split("Accept-Language"); //Resplit again to cut the ending part
      string[] browser = split1[1].Split("Accept"); //Resplit again to cut the ending part
      ////////////////
      ///DISPLAY///
      sb.Append("</body><p>Browser Information: "+ browser[0] + "</p></html>"); //Display the browser info -use the string that has already splited
      sb.Append("</body><p>Acept-Language " + split3[1] + "</p></html>"); //Display the Accept-Language info -use the string that has already splited
      sb.Append("</body><p>Acept-Encoding "+ ac_lang[0] +"</p></html>"); //Display the Accept-Encoding info -use the string that has already splited
      /////////////
      ///Tracking/// ADVICE FROM 600611001
      sb.Append("<div>CurrentThread ID : " + Thread.CurrentThread.ManagedThreadId + "</div><br>"); //check whether 1 thread is for 1 connection or not 
      sb.Append("<div>Thread Status : " + Thread.CurrentThread.ThreadState + "</div><br>"); //Display the thread's status
      sb.Append("<div>Thread IsAlive : " + Thread.CurrentThread.IsAlive + "</div><br>"); //Display whether thread is alive or not
      sb.Append("<div>Number of threads : " + Process.GetCurrentProcess().Threads.Count + "</div><br>"); //Display the numbers of thread
      //////////////  
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
