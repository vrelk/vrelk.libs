using System.Net.Http;
using Vrelk.Libs.LibreNMS.RRDReST.HttpRequestTypes;

namespace Vrelk.Libs.LibreNMS.RRDReST;

internal class Client
{
    private readonly HttpClient httpClient;

    public Client(string BaseUrl)
    {
        httpClient = new HttpClient();
        httpClient.BaseAddress = new System.Uri(BaseUrl.TrimEnd('/') + '/');
    }

    //public ResponseObj<PortObj> GetPort(string hostname, string port)
    //{
    //    var resp;
    //}
}
