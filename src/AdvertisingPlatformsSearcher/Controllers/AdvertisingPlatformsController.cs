using AdvertisingPlatformsSearcher.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AdvertisingPlatformsSearcher.Controllers;

[ApiController]
[Route("[controller]")]
public class AdvertisingPlatformsController : ControllerBase
{
    private readonly IPlatformParser _parser;
    private readonly IUrlTreeBuilder _treeBuilder;
    private readonly IPlatformSearcher _searcher;
    private readonly IAdvertisingDataStore _advertisingDataStore;
    private readonly ILogger<AdvertisingPlatformsController> _logger;

    public AdvertisingPlatformsController(IPlatformParser parser, IUrlTreeBuilder treeBuilder, 
        IPlatformSearcher searcher, IAdvertisingDataStore advertisingDataStore, ILogger<AdvertisingPlatformsController> logger)
    {
        _parser = parser;
        _treeBuilder = treeBuilder;
        _searcher = searcher;
        _advertisingDataStore = advertisingDataStore;
        _logger = logger;
    }
    
    [HttpPost("upload")]
    public async Task<IActionResult> Upload(IFormFile file)
    {
        string fileName = file?.FileName ?? "unknown";
    
        _logger.LogInformation("Начало загрузки файла: {FileName} (размер: {Size} bytes)", 
            fileName, file?.Length ?? 0);
        
        try
        {
            if (file == null || file.Length == 0)
            {
                _logger.LogWarning("Попытка загрузки пустого файла: {FileName}", fileName);
                return BadRequest("Невозможно загрузить пустой файл");
            }
            
            _logger.LogInformation("Загрузил файл {FileName}", fileName);
            using var reader = new StreamReader(file.OpenReadStream());
            var content = await reader.ReadToEndAsync();

            var platforms = _parser.Parse(content);
            _logger.LogInformation("Парсинг файла {FileName} выполнен успешно. Количество рекламных площадок: {PlatformCount}", 
                fileName, platforms.Count);
            
            var root = _treeBuilder.Build(platforms);
            _advertisingDataStore.UpdateData(root);
            _logger.LogInformation("Дерево локаций построено. Данные в памяти обновлены");
        
            return Ok(new { Message = $"Количество рекламных площадок: {platforms.Count}" });
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Ошибка при обработке файла {FileName}", fileName);
            return StatusCode(500, "Ошибка при обработке файла");
        }
    }
    
    [HttpGet("search")]
    public IActionResult Search([FromQuery] string location)
    {
        _logger.LogInformation("Поиск рекламных площадок для локации: {Location}", location);
        
        try
        {
            if (string.IsNullOrWhiteSpace(location))
            {
                _logger.LogWarning("Попытка поиска без указания параметра location");
                return BadRequest("Параметр 'location' обязателен");
            }
            
            var root = _advertisingDataStore.GetData();
            if (root == null)
            {
                _logger.LogWarning("Попытка поиска без загруженных данных. Локация: {Location}", location);
                return BadRequest("Сначала загрузите файл");
            }

            var platforms = _searcher.Find(root, location);
            _logger.LogInformation("Для локации {Location} найдено {PlatformCount} площадок", 
                location, platforms.Count);
            
            if (platforms.Count == 0)
            {
                _logger.LogDebug("В дереве данных не найдена локация {Location}", location);
            }
            
            return Ok(platforms);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Ошибка поиска площадок для локации {Location}", location);
            return StatusCode(500, "Ошибка при поиске");
        }
    }
}