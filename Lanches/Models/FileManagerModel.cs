namespace Lanches.Models;

public class FileManagerModel
{
    public FileInfo[] Files { get; set; }
    public IFormFile IFormFile { get; set; }
    public List<IFormFile> IFormFiles { get; set; }
    public string PathImagesProducts { get; set; }
}
