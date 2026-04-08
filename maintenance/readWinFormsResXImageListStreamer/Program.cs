using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
using System.Xml.Linq;


string blackJackProjectFolderPath = System.IO.Path.Combine(
    AppContext.BaseDirectory,
        "../../../../../blackjack_managed_cpp/"
        );

string resxFilePath = System.IO.Path.Combine(
        blackJackProjectFolderPath,
        "Form1.resx"
    );
string cardsFolderPath = System.IO.Path.Combine(
        blackJackProjectFolderPath,
        "cards"
    );

List<ImageInformation> cardFilesList = LoadCardFiles(folderPath: cardsFolderPath);

byte[] data = ParseDataBytesFromResource(rexFilePath: resxFilePath, key: "cardlist.ImageStream");

var streamer = LoadStreamer(data);
var imgList = new ImageList();
imgList.ImageStream = streamer;

// Save each image
for (int i = 0; i < imgList.Images.Count; i++)
{
    var img = imgList.Images[i];
    string resourceImageHash = GetImageHash(img);
    string resourcePixelHash = GetImageHashByPixels(img);

    var fileDataCardMatches = cardFilesList.Where(c => c.DataHash == resourceImageHash).ToList();

    if(!fileDataCardMatches.Any())
    {
        Console.WriteLine($"No match found for index {i} by file");
    }

    var pixelDataCardMatches = cardFilesList.Where(c => c.PixelHash == resourcePixelHash).ToList();

    if (!pixelDataCardMatches.Any())
    {
        Console.WriteLine($"No match found for index {i} by pixels");
    }

    var matchingCard = fileDataCardMatches.FirstOrDefault() ??
                        pixelDataCardMatches.FirstOrDefault();

    if(matchingCard == null)
    {
        continue;
    }

    Console.WriteLine($"Image[{i}]: Size={img.Size}, PixelFormat={img.PixelFormat}, Filename: {matchingCard.FileName}");

}



ImageListStreamer LoadStreamer(byte[] data)
{
    using var ms = new MemoryStream(data);
    var bf = new BinaryFormatter();
    return (ImageListStreamer)bf.Deserialize(ms);
}


byte[] ParseDataBytesFromResource(string rexFilePath, string key)
{
    // Load XML
    XDocument doc = XDocument.Load(resxFilePath);

    // Find the <data> element with the name "cardlist.ImageStream"
    var dataElement =
        doc.Descendants("data")
           .FirstOrDefault(x => (string)x.Attribute("name") == key);

    if (dataElement == null)
    {
        throw new Exception("ImageStream not found");
    }

    // Extract the <value> element text
    string base64 = dataElement.Element("value")?.Value?.Trim();

    if (string.IsNullOrWhiteSpace(base64))
    {
        throw new Exception("Value element empty");
    }

    // Convert to byte[]
    byte[] bytes = Convert.FromBase64String(base64);
    return bytes;
}



string GetImageHash(System.Drawing.Image image)
{
    // Convert to Bitmap to ensure consistent format
    using (var bitmap = new Bitmap(image))
    {
        using (var ms = new System.IO.MemoryStream())
        {
            // Save as PNG to get consistent, lossless format
            bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            byte[] hash = System.Security.Cryptography.SHA256.Create().ComputeHash(ms.ToArray());
            return Convert.ToBase64String(hash);
        }
    }
}


string GetImageHashByPixels(Image image)
{
    try
    {
        using (var bitmap = new Bitmap(image))
        {
            // Create a simple hash based on pixel data
            var pixelData = new List<byte>();

            for (int y = 0; y < bitmap.Height; y++)
            {
                for (int x = 0; x < bitmap.Width; x++)
                {
                    var pixel = bitmap.GetPixel(x, y);
                    pixelData.Add(pixel.R);
                    pixelData.Add(pixel.G);
                    pixelData.Add(pixel.B);
                    pixelData.Add(pixel.A);
                }
            }

            using (var ms = new System.IO.MemoryStream())
            {
                ms.Write(pixelData.ToArray(), 0, pixelData.Count);
                byte[] hash = System.Security.Cryptography.SHA256.Create().ComputeHash(ms.ToArray());
                return Convert.ToBase64String(hash);
            }
        }
    }
    catch (Exception ex)
    {
        throw new Exception($"Error hashing by pixels: {ex.Message}");
    }
}


List<ImageInformation> LoadCardFiles(string folderPath)
{
    var cards = new List<ImageInformation>();
    foreach( string filePath in System.IO.Directory.EnumerateFiles(path: folderPath, searchPattern: "*.bmp", searchOption: SearchOption.AllDirectories))
    {
        var fileImg = System.Drawing.Image.FromFile(filePath);
        string fileHash = GetImageHash(fileImg);
        string pixelHash = GetImageHashByPixels(fileImg);
        cards.Add(new ImageInformation
        {
            Index = 0,
            FileName = System.IO.Path.GetFileName(filePath),
            RelativePath = System.IO.Path.GetRelativePath(folderPath, filePath),
            DataHash = fileHash,
            PixelHash = pixelHash,
        });
    }
    return cards;
}


class ImageInformation
{
    public int Index { get; set; }
    public string FileName { get; set; }
    public string RelativePath { get; set; }
    public string DataHash { get; set; }
    public string PixelHash { get; set; }
}