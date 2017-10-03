using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace License3DotNet.licensor
{
    class HttpHandler
    {
        int getResponseCode(HttpWebRequest httpUrlConnection)
        {
            HttpWebResponse wr = (HttpWebResponse)httpUrlConnection.GetResponse();
            return int.Parse(Convert.ToString(wr.StatusCode));
        }

        HttpWebRequest openConnection(Uri url)
        {
            HttpWebRequest wr = (HttpWebRequest)WebRequest.Create(url);
            return wr;
        }
    }
}
