using System;
using System.Collections.Generic;
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
    var matchingCard = cardFilesList.FirstOrDefault(c => c.DataHash == resourceImageHash);

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
    using (var ms = new System.IO.MemoryStream())
    {
        image.Save(ms, image.RawFormat);
        byte[] hash = System.Security.Cryptography.SHA256.Create().ComputeHash(ms.ToArray());
        return Convert.ToBase64String(hash);
    }
}


List<ImageInformation> LoadCardFiles(string folderPath)
{
    var cards = new List<ImageInformation>();
    foreach( string filePath in System.IO.Directory.EnumerateFiles(path: folderPath, searchPattern: "*.bmp", searchOption: SearchOption.AllDirectories))
    {
        var fileImg = System.Drawing.Image.FromFile(filePath);
        string fileHash = GetImageHash(fileImg);
        cards.Add(new ImageInformation
        {
            Index = 0,
            FileName = System.IO.Path.GetFileName(filePath),
            RelativePath = System.IO.Path.GetRelativePath(folderPath, filePath),
            DataHash = fileHash
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
}