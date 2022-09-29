using System.Net;
using Azure.Core;

namespace WebAPIBiz4Company.Error;

public class Error
{
    public HttpStatusCode Status { set; get; }
    public string? Content { set; get; }
}