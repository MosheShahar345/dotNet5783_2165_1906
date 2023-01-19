using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Dal;

static class XMLTools
{
    const string s_dir = @"..\xml\";

    static XMLTools()
    {
        if (!Directory.Exists(s_dir))
            Directory.CreateDirectory(s_dir);
    }

    public static void SaveListToXMLElement(XElement root, string entity)
    {
        string filePath = $"{s_dir + entity}.xml";

        try
        {
           root.Save(filePath);
        }
        catch (Exception ex)
        {
            throw new Exception($"fail to create xml file: {filePath}", ex);
        }
    }

    public static XElement LoadListToXMLElement(string entity)
    {
        string filePath = $"{s_dir + entity}.xml";

        try
        {
            if (File.Exists(filePath))
                return XElement.Load(filePath);

            XElement root = new(entity);
            root.Save(filePath);
            return root;
        }
        catch (Exception ex)
        {
            throw new Exception($"fail to load xml file: {filePath}", ex);
        }
    }

    public static List<T?> LoadListFromXMLSerializer<T>(string entity) where T : struct
    {
        string filePath = $"{s_dir + entity}.xml";

        try
        {
            if (!File.Exists(filePath)) return new();

            using FileStream file = new(filePath, FileMode.Open);

            XmlSerializer x = new(typeof(List<T?>));
            return x.Deserialize(file) as List<T?> ?? new();
        }
        catch (Exception ex)
        {
            throw new Exception($"fail to load xml file:{filePath}", ex);
        }
    }

    public static XElement LoadConfig()
    {
        string filePath = $"{s_dir}Config.xml";

        try
        {
            XElement? config = XElement.Load(filePath);
            return config;
        }
        catch (Exception ex)
        {
            throw new Exception($"fail to load xml:{filePath}", ex);
        }
    }

    public static void SaveConfigXml(string entity, int serial)
    {
        string filePath = $"{s_dir}Config.xml";

        try
        {
            XElement? config = XElement.Load(filePath);
            config.Element(entity)!.Value = serial.ToString();
            config.Save(filePath);
        }
        catch (Exception ex)
        {
            throw new Exception($"fail to load xml:{filePath}", ex);
        }
    }

    public static XElement LoadListFromXMLSerializer(string entity)
    {
        string filePath = $"{s_dir + entity}.xml";

        try
        {
            if (File.Exists(filePath))
                return XElement.Load(filePath);

            XElement root = new(entity);
            root.Save(filePath);
            return root;
        }
        catch (Exception ex)
        {
            throw new Exception($"fail to load xml file:{filePath}", ex);
        }
    }

    public static void SaveListToXNLserializer<T>(List<T?> list, string entity) where T: struct
    {
        string filePath = $"{s_dir + entity}.xml";
        try
        {
            using FileStream file = new(filePath, FileMode.Create, FileAccess.Write, FileShare.None);

            XmlSerializer serializer = new(typeof(List<T?>));
            
            serializer.Serialize(file, list);
        }
        catch (Exception ex)
        {

            throw new Exception($"fail to create xml file: {filePath}", ex);
        }
    }

    public static T? ToEnumNullable<T>(this XElement element, string name) where T: struct, Enum =>
        Enum.TryParse<T>((string?)element.Element(name), out var result) ? (T?)result : null;

    public static DateTime? ToDateTimeNullable(this XElement element, string name) =>
       DateTime.TryParse((string?)element.Element(name), out var result) ? (DateTime?)result : null;

    public static double? ToDoubleNullable(this XElement element, string name) =>
      double.TryParse((string?)element.Element(name), out var result) ? (double?)result : null;

    public static int? ToIntNullable(this XElement element, string name) =>
     int.TryParse((string?)element.Element(name), out var result) ? (int?)result : null;
}