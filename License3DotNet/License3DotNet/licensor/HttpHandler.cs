using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

/**
 * A simple wrapper class to make it possible to mock the network use when revocation
 * is tested. In tests a mock class extending this is injected.
 *
 */
namespace License3DotNet.licensor
{
    class HttpHandler
    {
        int getResponseCode(HttpWebRequest httpUrlConnection)
        {
            HttpWebResponse wr = (HttpWebResponse)httpUrlConnection.GetResponse();
            return int.Parse(Convert.ToString(wr.StatusCode));
        }

        /**
         * This should be mocked when testing revocation not to wait for a
         * connection build up to a remote server.
         * 
         * @param url the url to which connection is to be opened
         * @return the connection
         * @throws IOException if the connection can not be made
         */
        HttpWebRequest openConnection(Uri url)
        {
            HttpWebRequest wr = (HttpWebRequest)WebRequest.Create(url);
            return wr;
        }
    }
}
