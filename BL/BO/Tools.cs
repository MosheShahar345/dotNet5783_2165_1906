using System.Collections;
using System.Reflection;

namespace BO;

internal static class Tools
{
    public static string ToStringProperty<T>(this T t)
    {
        string str = "";
        foreach (PropertyInfo item in t.GetType().GetProperties())
        {
            if (item.PropertyType == typeof(IEnumerable))
            {
                foreach (var it in (IEnumerable)item)
                {
                    str += (it + " ");
                }
            }
            else
            {
                str += "\n" + item.Name + ", " + item.GetValue(t, null);
            }
        }

        return str;
    }
}