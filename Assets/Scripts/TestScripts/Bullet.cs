using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
public class Bullet : MonoBehaviour, IResetable
{
    private Vector3 targetPos;

    private void Awake()
    {
        //计算正前方50m的世界坐标.写在这里肯定有问题，因为这次一次性赋值，每次生成的朝向都是不同的.也不能在OnEnable中执行，第一次就会出问题，因为第一次就是生成这个子弹对象，同时给他传入了位置和旋转
        // targetPos = transform.TransformPoint(0, 0, 50);

        //TransformPoint这个函数是将局部坐标转变成世界坐标，它就会受父物体的影响，父物体的坐标朝向是它的起点。如果放在awake里，就会每次重新获得父物体的状态。
        //为什么放在OnEnable不行呢？如果这个物体没有被及时销毁，但是父物体改变了方向和位置，就获取不到了
    }
    public void SetTargetPos(Vector3 vector3Target)
    {
        targetPos = transform.TransformPoint(vector3Target);
    }
    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPos, Time.deltaTime * 1000);
        if (Vector3.Distance(transform.position, targetPos) < 0.1f)
        {
            // Destroy(gameObject);
            GameObjectPool.Instance.RecycleObject(gameObject);
        }
    }

    public void OnReset()
    {
        targetPos = transform.TransformPoint(0, 0, 50);
    }
}
