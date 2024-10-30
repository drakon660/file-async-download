using Microsoft.AspNetCore.Mvc;

namespace AsynEnumerableWeb.Api.Controllers;

[Route("data")]
[ApiController]
public class DataController : ControllerBase
{
    public class DataItem
    {
        public int Id { get; set; }
        public string Content { get; set; }
    }
   
    [HttpGet]
    public IActionResult GetData(int page, int pageSize)
    {
        var dataList = new List<DataItem>();
        for (int i = 1; i <= 200; i++)
        {
            dataList.Add(new DataItem
            {
                Id = i,
                Content = $"Random Content {i}"
            });
        }

        // Implement pagination
        var paginatedData = dataList
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        return Ok(paginatedData);
    }
}