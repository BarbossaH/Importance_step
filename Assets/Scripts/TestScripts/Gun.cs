using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

public class Gun : MonoBehaviour
{
    public GameObject bulletPrefab;

    public void Shoot()
    {
        // Instantiate(bulletPrefab, transform.position, transform.rotation);
        GameObjectPool.Instance.CreateObject("bullet", bulletPrefab, transform.position, transform.rotation);
    }

    private void OnGUI()
    {
        if (GUILayout.Button("Shoot"))
        {
            Shoot();
        }
    }
}
