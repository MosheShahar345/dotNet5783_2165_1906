using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Dal;

//static class XMLTools
//{
//    const string s_dir = @"..\xml\";

//    static XMLTools()
//    {
//        if (!Directory.Exists(s_dir))
//            Directory.CreateDirectory(s_dir);
//    }

//    public static void SaveListToXMLSerializer<T>(List<T?> list, string entity)
//    {
//        string filePath = $"{s_dir + entity}.xml";

//        try
//        {
//            using FileStream file = new(filePath, FileMode.Create, FileAccess.Write, FileShare.None);

//            XmlSerializer serializer = new XmlSerializer(typeof(List<T?>));

//            serializer.Serialize(file, list);
//        }
//        catch (Exception e)
//        {

//            throw new Exception($"fail to create xml file: {filePath}", e);
//        }
//    }
    
//    public static List<T?> LoadListFromXMLSerializer<T>(string entity) where T: struct
//    {
//        string filePath = $"{s_dir + entity}.xml";

//        try
//        {
//            if (!File.Exists(filePath)) return new();

//            using FileStream file = new(filePath, FileMode.Open);

//            XmlSerializer x = new(typeof(List<T?>));
//            return x.Deserialize(file) as List<T?> ?? new();
//        }
//        catch (Exception e)
//        {

//            throw new Exception($"fail to load xml file:{filePath}", e);
//        }
//    }


    


//}