using System;
using System.Net.Http;

MeuServicoHTTP http = new MeuServicoHTTP();
http.SendRequest();
// http.call("www.bing.com.br"); erro call é protegido
//var client = http.client; erro client é privado

public abstract class ServicoHTTP
{
    private HttpClient client = new HttpClient();
    protected HttpResponseMessage call(string url)
    {
        HttpResponseMessage result = client.GetAsync(url).Result;
        return result;
    }

    public abstract HttpResponseMessage SendRequest();
}

public class MeuServicoHTTP : ServicoHTTP
{
    public override HttpResponseMessage SendRequest()
    {
        return call("www.google.com.br");
    }
}