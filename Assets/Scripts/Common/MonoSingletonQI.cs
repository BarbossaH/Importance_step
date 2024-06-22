// using UnityEngine;

// public class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
// {
//   private static T instance;

//   public static T Instance
//   {
//     get
//     {
//       if (instance == null)
//       {
//         instance = FindObjectOfType<T>();

//         if (instance == null)
//         {
//           GameObject singletonObject = new GameObject("Singleton of " + typeof(T));
//           instance = singletonObject.AddComponent<T>();
//         }

//         instance.Init();
//       }
//       return instance;
//     }
//   }

//   // 可选的初始化方法，子类可以覆盖这个方法进行自定义初始化
//   protected virtual void Init()
//   {
//   }

//   // 确保在应用程序退出时清除单例实例
//   private void OnApplicationQuit()
//   {
//     instance = null;
//   }
// }
// /*
// 正常的游戏开发中，是否采用将所有单例对象放在一个永远不会销毁的场景下的做法取决于具体情况和需求：

// 对于一些需要持久性和全局性的单例对象，比如游戏设置、全局管理器等，放在一个特定的场景中进行管理是合理的做法。
// 对于一些轻量级的、只在特定场景中使用的单例对象，可以考虑根据实际需要在不同的场景中进行管理或者创建。
// */