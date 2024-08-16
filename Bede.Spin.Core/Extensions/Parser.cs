using System.ComponentModel;

namespace Bede.Spin.Core.Extensions;

public static class Parser
{
    public static T Parse<T>(this string value, T whenNull = default(T))
    {
        // if string is empty then we can return whenNull (default value)
        T result = whenNull;
        if (!string.IsNullOrEmpty(value))
        {
            // we are not going to handle exception here
            var tc = TypeDescriptor.GetConverter(typeof(T));
            result = (T)tc.ConvertFrom(value);
        }

        return result;
    }

    public static T ParseSafe<T>(this string value, T def = default(T))
    {
        try
        {
            return value.Parse<T>(def);
        }
        catch
        {
            //we ignore the exception
        }
        return def;
    }
}