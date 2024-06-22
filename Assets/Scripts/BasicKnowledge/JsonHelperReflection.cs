using System;
using System.Reflection;
using System.Text;
class JsonHelper
{
  public static string ObjectToJson(object obj)
  {
    //获得所有属性名称
    Type type = obj.GetType();
    PropertyInfo[] allProperties = type.GetProperties();
    StringBuilder sb = new StringBuilder();
    sb.AppendFormat("{");
    foreach (var item in allProperties)
    {
      // item.Name
      //item.GetValue(obj);
      sb.AppendFormat("\"{0}\":\"{1}\",", item.Name, item.GetValue(obj));
      //删除最后一个逗号
      sb.Remove(sb.Length - 1, 1);
      sb.Append("}");
      return sb.ToString();
    }
    //拼接字符串
    return null;
  }

  public static T JsonToObject<T>(string json) where T : new()
  {

    //create object
    // Type type = typeof(T);
    // object instance = Activator.CreateInstance(type);
    T instance = new T();
    json.Replace("\"", string.Empty).Replace("{", string.Empty).Replace("}", string.Empty);
    // json.Replace("\"", "");
    string[] keyValue = json.Split(':', ',');
    for (int i = 0; i < keyValue.Length; i++)
    {
      string key = keyValue[i];
      string value = keyValue[i + 1];
      PropertyInfo property = instance.GetType().GetProperty(key);
      property.SetValue(instance, Convert.ChangeType(value, property.PropertyType));
    }
    return default(T);
  }
}

//----------------------------------------------------------------chatgpt
// using System;
// using System.Collections;
// using System.Collections.Generic;
// using System.Reflection;
// using System.Text.Json;
// using System.Text.Json.Serialization;

// public class JsonToObjectConverter
// {
//   public static T ConvertTo<T>(string json) where T : new()
//   {
//     var options = new JsonDocumentOptions
//     {
//       AllowTrailingCommas = true
//     };
//     using var document = JsonDocument.Parse(json, options);
//     var rootElement = document.RootElement;
//     return (T)ConvertToObject(typeof(T), rootElement);
//   }

//   private static object ConvertToObject(Type type, JsonElement element)
//   {
//     if (type == typeof(string))
//     {
//       return element.GetString();
//     }
//     if (type == typeof(int))
//     {
//       return element.GetInt32();
//     }
//     if (type == typeof(double))
//     {
//       return element.GetDouble();
//     }
//     if (type == typeof(bool))
//     {
//       return element.GetBoolean();
//     }
//     if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(List<>))
//     {
//       var itemType = type.GetGenericArguments()[0];
//       var list = (IList)Activator.CreateInstance(type);
//       foreach (var item in element.EnumerateArray())
//       {
//         list.Add(ConvertToObject(itemType, item));
//       }
//       return list;
//     }

//     var obj = Activator.CreateInstance(type);

//     foreach (var property in type.GetProperties(BindingFlags.Public | BindingFlags.Instance))
//     {
//       if (element.TryGetProperty(property.Name, out var propertyElement))
//       {
//         var value = ConvertToObject(property.PropertyType, propertyElement);
//         property.SetValue(obj, value);
//       }
//     }

//     foreach (var field in type.GetFields(BindingFlags.Public | BindingFlags.Instance))
//     {
//       if (element.TryGetProperty(field.Name, out var fieldElement))
//       {
//         var value = ConvertToObject(field.FieldType, fieldElement);
//         field.SetValue(obj, value);
//       }
//     }

//     return obj;
//   }
// }

// public class Address
// {
//   public string Street { get; set; }
//   public string City { get; set; }
// }

// public class Person
// {
//   public string Name { get; set; }
//   public int Age { get; set; }
//   public List<Address> Addresses { get; set; }
// }

// public class Program
// {
//   public static void Main(string[] args)
//   {
//     string json = @"
//         {
//             ""Name"": ""John"",
//             ""Age"": 30,
//             ""Addresses"": [
//                 {
//                     ""Street"": ""123 Main St"",
//                     ""City"": ""New York""
//                 },
//                 {
//                     ""Street"": ""456 Maple Ave"",
//                     ""City"": ""Los Angeles""
//                 }
//             ]
//         }";

//     var person = JsonToObjectConverter.ConvertTo<Person>(json);
//     Console.WriteLine($"Name: {person.Name}, Age: {person.Age}");
//     foreach (var address in person.Addresses)
//     {
//       Console.WriteLine($"Street: {address.Street}, City: {address.City}");
//     }
//   }
// }
