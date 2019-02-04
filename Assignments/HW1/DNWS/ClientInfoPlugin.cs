using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace DNWS
{
    class ClientInfoPlugin : IPlugin 
    {
        protected static Dictionary<String, int> statDictionary = null;
        public ClientInfoPlugin() //constructor
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

        public HTTPResponse GetResponse(HTTPRequest request) //response to client web browser
        {
            HTTPResponse response = null; //Declare response with HTTPResponse type
            StringBuilder sb = new StringBuilder(); //Build web page
            string info = request.ClientInfo; //Get infomation of Client such as Browser information etc.
            string[] lines = Regex.Split(info, "\\n"); //Split line with \\n
            string ClientIPport = request.GetIP(); //Get Client Ip and port number
            string[] IPport = Regex.Split(ClientIPport, ":"); //Slipt line ip and port with :
            sb.Append("<html><body><h1>Client IP:" + IPport[0]+"</h1>"); //IP
            sb.Append("<h1>Client Port:"+IPport[1] +"</h1>"); //Port
            sb.Append("<h1>" + lines[5] + "</h1>"); //Browser Information
            sb.Append("<h1>" + lines[8] + "</h1>"); //Accept Language
            sb.Append("<h1>"+ lines[7] + "</h1>"); //Accpet Encoding
            sb.Append("</body></html>"); //End tag html
            response = new HTTPResponse(200); //200 ok
            response.body = Encoding.UTF8.GetBytes(sb.ToString()); //Encode UTF8
            return response; //Return response
        }

        public HTTPResponse PostProcessing(HTTPResponse response)
        {
            throw new NotImplementedException();
        }
    }
}
