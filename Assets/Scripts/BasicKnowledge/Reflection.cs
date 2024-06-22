using System;
using System.Reflection;
namespace Reflection.Examples
{
  class User
  {
    public int ID { get; set; }
    public string LoginID { get; set; }

    public void Print()
    {
      Console.WriteLine("编号：{0},账号{1}", ID, LoginID);
    }
  }

  class Program
  {
    static void Main(string[] args)
    {
      //编译时获取信息
      User user01 = new User();
      user01.ID = 1001;
      user01.LoginID = "user01";
      user01.Print();

      //获取类型 Type (不是数据类型，不是User，int, string这种数据类型)

      //根据字符串获取类型  //要命名空间和.类型
      Type type = Type.GetType("Reflection.Examples.User");
      //根据对象获取类型
      // Type type1 = user01.GetType();
      //根据数据类型获取类型
      // Type type2 = typeof(User);



      //创建对象
      object instance = Activator.CreateInstance(type);


      //访问成员
      // type.GetProperties
      PropertyInfo IDProperty = type.GetProperty("ID");
      // IDProperty.SetValue(instance, 1001);
      object idValue = Convert.ChangeType("1001", IDProperty.PropertyType);
      IDProperty.SetValue(instance, idValue);
      PropertyInfo logIDProperty = type.GetProperty("LoginID");
      logIDProperty.SetValue(instance, "user01");
      MethodInfo printMethod = type.GetMethod("Print");
      printMethod.Invoke(instance, null);

      /*反射的特点是 运行 时获取信息，是动态的
      比如 Type type = Type.GetType(“Reflection.Examples.User”);中的命名空间参数，就是变量，那就可以动态的改变要访问的类
      */
      /* JSON 文件转json对象就是这种方式
      {"ID": "1001", "Name": "ZS"}
      string ObjectToJson(object obj){
        获取属性（名称，值）
        根据规则拼接字符串（StringBuilder去做，MSDN搜索文档）
      }
      2 json-->对象
      T JsonToObject<T>(string json){
        创建对象
        字符串解析（提取属性名，属性值）
        根据属性名设置属性值
      }
      */
    }
  }
}