using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

#region 第一版
// public class MonoSingletionExercise<T> : MonoBehaviour where T : class
// {
//     private static T instance;
//     public static T Instance
//     {
//         get
//         {
//             return instance;
//         }
//     }
//     private void Awake()
//     {
//         //这里有一个巨大的隐患就是当别的类，比如别的管理类也可能调用它，但是它的awake未必被先调用了，所以就会报错，因为instance是空的
//         instance = this as T;
//     }
// }
#endregion

#region 第二版
public class MonoSingletionExercise<T> : MonoBehaviour where T : MonoSingletionExercise<T>
{
    private static T instance;
    public static T Instance
    {
        get
        {
            // instance = this as T;//this cannot exist in a static class.
            if (instance == null)
            {
                //下面代码的风险是如果有脚本，但是没有作为组件挂载在对象上，就为空，获取不到
                instance = FindObjectOfType<T>();
                if (instance == null)
                {
                    new GameObject($"Singleton of {0}", typeof(T)).AddComponent<T>();
                    //because addcomponent will call the Awake method, so there is no need to call Init() again
                }
                else
                {
                    // instance.Init();这句话如果不加约束是不能调用的
                    instance.Init();//加上约束MonoSingletionExercise就出来了。
                }
            }
            return instance;
        }
    }

    private void Awake()
    {
        //这个是正常调的，就是awake先执行了
        if (instance == null)
        {
            instance = this as T;
            instance.Init();
        }
    }
    public virtual void Init()
    {

    }
}
#endregion