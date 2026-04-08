using System;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
using System.Xml.Linq;


string resxFilePath = System.IO.Path.Combine(
        AppContext.BaseDirectory,
        "../../../../../blackjack_managed_cpp/Form1.resx"
    );

byte[] data = ParseDataBytesFromResource(rexFilePath: resxFilePath, key: "cardlist.ImageStream");

var streamer = LoadStreamer(data);
var imgList = new ImageList();
imgList.ImageStream = streamer;

// Save each image
for (int i = 0; i < imgList.Images.Count; i++)
{
    var img = imgList.Images[i];
    Console.WriteLine($"Image[{i}]: Size={img.Size}, PixelFormat={img.PixelFormat};");
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


