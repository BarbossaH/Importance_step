using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Common
{
    public interface IResetable
    {
        void OnReset();
    }
    public class GameObjectPool : MonoSingleton<GameObjectPool>
    {
        private Dictionary<string, List<GameObject>> cache;

        protected override void Awake()
        {
            base.Awake();
            cache = new Dictionary<string, List<GameObject>>();
        }
        public GameObject CreateObject(string key, GameObject prefab, Vector3 position, Quaternion rotation)
        {
            GameObject go = FindUsableGameObject(key);

            if (go == null)
            {
                // 没有可用的对象，创建一个新的对象
                go = AddObject(key, prefab); //在这里执行了子弹的awake函数。如果把子弹的方向放到awake里，就会只执行一次，而后面出现了父物体的位置变化，则就会出现射击方向出现了偏差，所以应该在使用物体的时候再设置子弹的方向
            }

            // 使用这个对象
            UseObject(position, rotation, go);
            return go;
        }

        //回收对象
        public void RecycleObject(GameObject go, float delayTime = 0)
        {
            StartCoroutine(RecycleObjectRoutine(go, delayTime));
        }

        //清空对象池（如果切换场景，可能就不需要一些对象池的对象了）
        public void Clear(string key)
        {
            if (cache.ContainsKey(key))
            {
                foreach (GameObject go in cache[key])
                {
                    Destroy(go);//销毁了对象数据，但是指针数据还是在那里，所以需要下面的clear
                }
                cache[key].Clear();//清空列表，释放引用指针。
                cache.Remove(key);//从字典中移除 key 及其关联的列表。

            }
            //下面的方法有风险，最好用foreach
            // for (int i = 0; i < cache[key].Count; i++)
            // {
            //     Destroy(cache[key][i]);
            // }
            // cache.Remove(key);
        }
        public void ClearAll()
        {
            #region chatgpt
            // foreach (var kvp in cache)
            // {
            //     // 可选：销毁所有GameObject
            //     foreach (var go in kvp.Value)
            //     {
            //         Destroy(go);
            //     }
            //     // 清空列表
            //     kvp.Value.Clear();
            // }

            // // 清空字典
            // cache.Clear();
            #endregion
            #region 容易出现的错误做法
            // foreach (var key in cache.Keys) //会出异常，无效的操作
            // {
            //     Clear(key); //异常是因为这个函数里移除了key，而foreach是不能改变列表，是只读的 
            // }
            foreach (var key in new List<string>(cache.Keys))
            {
                Clear(key);
            }
            #endregion
        }

        private IEnumerator RecycleObjectRoutine(GameObject go, float delayTime)
        {
            yield return new WaitForSeconds(delayTime);
            go.SetActive(false);
        }

        private GameObject FindUsableGameObject(string key)
        {
            if (cache.ContainsKey(key))
            {
                GameObject go = cache[key].Find(g => !g.activeInHierarchy);
                return go;
            }
            return null;
        }

        private GameObject AddObject(string key, GameObject prefab)
        {
            GameObject go = Instantiate(prefab);

            if (!cache.ContainsKey(key))
            {
                cache.Add(key, new List<GameObject>());
            }
            else
            {
                cache[key].Add(go);
            }

            return go;
        }
        private void UseObject(Vector3 pos, Quaternion rot, GameObject go)
        {
            go.transform.position = pos;
            go.transform.rotation = rot;
            go.SetActive(true);
            //设置目标点,
            //下面的代码也是不行的，因为写死了只能为子弹类服务
            // go.GetComponent<Bullet>().SetTargetPos(new Vector3(0, 0, 50));
            //所以应该把这个行为变成抽象的，委托或者事件也是不行的，因为需要太多的事件了，一个钩子不能服务于多个子弹
            // go.GetComponent<IResetable>()?.OnReset();
            foreach (var item in go.GetComponentsInChildren<IResetable>())
            {
                item.OnReset();
            }
        }
    }
}