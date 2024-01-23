using System.Text;
using AsyncEnumerable.Core;
using Azure;
using Microsoft.AspNetCore.Mvc;

namespace AsynEnumerableWeb.Api.Controllers;

[ApiController]
[Route("download")]
public class DownloadController : ControllerBase
{
    private readonly DataBrowser _dataBrowser;

    public DownloadController(DataBrowser dataBrowser)
    {
        _dataBrowser = dataBrowser;
    }
    
    [HttpGet("download-stream")]
    public async Task DownloadStreamFile()
    {
        Response.ContentType = "text/plain";
        Response.Headers.ContentDisposition = "attachment;filename=data.txt";
        var data = _dataBrowser.GetUsersNames();

        await using var streamWriter =
            new StreamWriter(Response.Body, new UTF8Encoding(encoderShouldEmitUTF8Identifier: false));
        
        await foreach (var item in data)
        {
            await streamWriter.WriteLineAsync(item);
            await streamWriter.FlushAsync();
        }
    }

    [HttpGet("download-file")]
    public FileContentResult DownloadFile()
    {
        var data = _dataBrowser.GetUsersNames().ToBlockingEnumerable();
        
        return new FileContentResult(GetBytes(data), "text/plain")
        {
            FileDownloadName = "data-1.txt"
        };
    }

    private static byte[] GetBytes(IEnumerable<string> values)
    {
        var fileContent = string.Join('\n', values);
        return Encoding.UTF8.GetBytes(fileContent);
    }
}