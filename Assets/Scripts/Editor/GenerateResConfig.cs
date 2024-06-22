using UnityEngine;
using UnityEditor;
using System.IO;
/*这个目录Editor是unity规定的，会在编辑的时候执行这个代码
MenuItem: 用于修饰在Unity编译器中产生菜单按钮
AssetDatabase：包含了只用于编译器中操作资源的相关功能
StreamingAssets:（这个只是用于运行时读，比如不可更改的配置） Unity特殊目录之一，该目录中文件不会被压缩。适合在移动端读取资源（PC端也可以写入），目录是只读的，没有写入权限。
持久化路径Application.persistentDataPath 路径可以在运行时进行读写操作。这是unity外部目录。安装程序的时候才会产生这个路径。这就是为什么第一次打开游戏会很慢。
*/
public class GenerateResConfig : Editor
{
    [MenuItem("Tools/Resources/Generate ResConfig File")]
    public static void Generate()
    {
        // Debug.Log("Generating ResConfig");
        //生成资源配置文件
        string[] resFiles = AssetDatabase.FindAssets("t:prefab", new string[] { "Assets/Resources" });
        //1查找Resources目录下所有预制件完整路径
        //会得到GUID 
        for (int i = 0; i < resFiles.Length; i++)
        {
            resFiles[i] = AssetDatabase.GUIDToAssetPath(resFiles[i]);
            //2生成对应关系： 名称=路径
            string fileName = Path.GetFileNameWithoutExtension(resFiles[i]);
            string filePath = resFiles[i].Replace("Assets/Resources/", string.Empty).Replace(".prefab", string.Empty);
            resFiles[i] = fileName + "=" + filePath;
            //3写入文件 如果想要ios和安卓只能用StreamingAssets
            // File.WriteAllLines("StreamingAssets", resFiles);
            //StreamingAssets 需要自己创建这个目录
            File.WriteAllLines("Assets/StreamingAssets/ConfigMap.txt", resFiles);
            //refresh
            AssetDatabase.Refresh();
        }
    }
}
