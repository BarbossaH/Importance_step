using UnityEngine;
using UnityEngine.Networking;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.IO;

namespace GameName.Common
{
    public class ResourceManager
    {
        private static Dictionary<string, string> configMap;
        //加载资源文件（注意，要使用这个方法就要保证资源文件不要重名，因为资源文件名是作为key去查找路径的，可以考虑在文件命名的时候加上文件夹的路径，这样就不会导致重名）

        //解析资源文件string----》Dictionary<string,string>

        //以上两件事必须在Load函数之前执行，而且只执行一次。请注意，Load函数是静态函数，它会优先于对象函数先执行，所以就必须在静态构造函数去执行。（脚本生命周期函数都是实例化后的，就是后于静态函数执行的）
        static ResourceManager()
        {
            //加载资源文件
            string fileContent = GetConfigFile();
            BuildMap(fileContent);
            //解析资源文件

            #region chatgpt
            // 异步加载资源文件
            // LoadConfigFileAsync().ContinueWith(task =>
            // {
            //     if (task.Exception != null)
            //     {
            //         Debug.LogError(task.Exception);
            //     }
            //     else
            //     {
            //         configFileContent = task.Result;
            //         // 解析资源文件
            //     }
            // });
            #endregion
        }
        #region chatgpt
        // // 异步加载资源文件
        // LoadConfigFileAsync().ContinueWith(task =>
        // {
        //     if (task.Exception != null)
        //     {
        //         Debug.LogError(task.Exception);
        //     }
        //     else
        //     {
        //         configFileContent = task.Result;
        //         // 解析资源文件
        //     }
        // });
        //     public static string GetConfigFileContent()
        // {
        //     if (string.IsNullOrEmpty(configFileContent))
        //     {
        //         Debug.LogWarning("Config file not loaded yet.");
        //         return null;
        //     }
        //     return configFileContent;
        // }
        #endregion
        public static string GetConfigFile(string filename = "ConfigMap.txt")
        {
            //这个地方是不确定的，因为unity改版了，所以以后碰到就需要就研究一下
            //TODO: Research

            //下面的这行代码有些手机读不出来
            // string url = "file://" + Application.streamingAssetsPath + "/ConfigMap.txt";

            /*如果在编译器下，在ios平台，在android怎么做*/
            // if (Application.platform == RuntimePlatform.WindowsEditor) { }
            // if (Application.platform == RuntimePlatform.IPhonePlayer) { }
            // if (Application.platform == RuntimePlatform.Android) { }
            string url;
#if UNITY_EDITOR || UNITY_STANDALONE
            //编译器和pc版的
            // Application.dataPath 就是StreamingAssets
            // url = "file://" + Application.dataPath + "/StreamingAssets/ConfigMap.txt";
            //真实项目下面写，用变量替代文件名
            url = "file://" + Application.dataPath + "/StreamingAssets/" + filename;
#elif UNITY_IPHONE
            url = "file://" + Application.dataPath + "/Raw/"+filename;
#elif UNITY_ANDROID
            url = "jar:file://" + Application.dataPath + "!/assets/"+filename;
#endif

            UnityWebRequest unityWebRequest = UnityWebRequest.Get(url);
            while (true)
            {
                if (unityWebRequest.isDone)
                {
                    return unityWebRequest.downloadHandler.text;
                }
            }
        }

        public static void BuildMap(string fileContent)
        {
            // fileContent的内容是 "filename=filepath/r/nfilename=filepath/r/nfilename=filepath/r/n"
            configMap = new Dictionary<string, string>();
            // fileContent.Split()
            using (StringReader reader = new StringReader(fileContent))
            {
                // string line = reader.ReadLine();

                // while (line != null)
                // {
                //     string[] keyAndValue = line.Split('=');
                //     configMap.Add(keyAndValue[0], keyAndValue[1]);
                //     line = reader.ReadLine();
                // }
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] keyAndValue = line.Split('=');
                    configMap.Add(keyAndValue[0], keyAndValue[1]);
                }

            } //当程序退出using代码块，将自大调用 reader.Dispose();//最后一定要释放
        }
        public static T Load<T>(string prefabName) where T : Object
        {
            //将prefab的名字转变成路径,这是这个类最大的作用，名称变路径
            return Resources.Load<T>("");
        }
    }
}