using System.Runtime.CompilerServices;
using System.Text;
using AsyncEnumerable.Core;
using AsynEnumerableWeb.Api;
using Microsoft.AspNetCore.Mvc;

namespace AsyncEnumerableWeb.Api.Controllers;

[ApiController]
[Route("api/download")]
public class DownloadController : ControllerBase
{
    private readonly DataBrowser _dataBrowser;

    public DownloadController(DataBrowser dataBrowser)
    {
        _dataBrowser = dataBrowser;
    }

    [HttpGet("download-stream")]
    public async Task DownloadStreamFile(CancellationToken cancellationToken)
    {
        try
        {
            Response.ContentType = "text/plain";
            Response.Headers.ContentDisposition = "attachment;filename=data.txt";
            //var data = _dataBrowser.GetUsersNames(cancellationToken);

            var data = Enumerable.Range(0, 100).Select(x => x.ToString()).AsAsyncEnumerable();

            await using var streamWriter =
                new StreamWriter(Response.Body, new UTF8Encoding(encoderShouldEmitUTF8Identifier: false));

            await foreach (var item in data)
            {
                await streamWriter.WriteLineAsync(item);
                await streamWriter.FlushAsync(cancellationToken);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    [HttpGet("download-file")]
    public async Task DownloadRealFile(CancellationToken cancellationToken)
    {
        var filePath = "d:\\Zdjęcia Grupa 4-20230415T052407Z-001.zip";

        try
        {
            var fileInfo = new FileInfo(filePath);

            Response.ContentType = "application/zip";
            Response.ContentLength = fileInfo.Length; // ✅ set the correct content length
            Response.Headers.ContentDisposition = "attachment; filename=\"dst.zip\"";

            await foreach (var chunk in ReadFileChunksAsync(filePath, cancellationToken))
            {
                await Response.Body.WriteAsync(chunk, cancellationToken);
                await Response.Body.FlushAsync(cancellationToken);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            Response.StatusCode = 500;
            await Response.WriteAsync("Error occurred while downloading the file.");
        }
    }

    private static async IAsyncEnumerable<ReadOnlyMemory<byte>> ReadFileChunksAsync(
        string filePath,
        [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        const int chunkSize = 100 * 1024; 

        await using var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
        var buffer = new byte[chunkSize];
        int bytesRead;

        while ((bytesRead = await fileStream.ReadAsync(buffer.AsMemory(0, chunkSize), cancellationToken)) > 0)
        {
            yield return buffer.AsMemory(0, bytesRead);
        }
    }

    // [HttpGet("download-file")]
    // public FileContentResult DownloadFile(CancellationToken cancellationToken)
    // {
    //     var data = _dataBrowser.GetUsersNames(cancellationToken).ToBlockingEnumerable();
    //
    //     return new FileContentResult(GetBytes(data), "text/plain")
    //     {
    //         FileDownloadName = "data-1.txt"
    //     };
    // }

    private static byte[] GetBytes(IEnumerable<string> values)
    {
        var fileContent = string.Join('\n', values);
        return Encoding.UTF8.GetBytes(fileContent);
    }
}