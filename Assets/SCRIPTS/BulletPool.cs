using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class BulletPool : MonoBehaviour
{
    public static BulletPool Instance;
    public List<GameObject> pooledObjects;
    public GameObject objectToPool;
    [FormerlySerializedAs("bulletstartPos")] public Transform turret;
    public int amountToPool;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        pooledObjects = new List<GameObject>();
        GameObject tmp;
        for(int i = 0; i < amountToPool; i++)
        {
            tmp = Instantiate(objectToPool);
            tmp.SetActive(false);
            pooledObjects.Add(tmp);
        }
    }
    public GameObject GetPooledObject()
    {
        for(int i = 0; i < amountToPool; i++)
        {
            if(!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }
        return null;
    }

    public void InstantiateBullet(Vector2 targetAngle)
    {
        var bullet = GetPooledObject(); 
        if (bullet != null) {
            bullet.transform.position = turret.position;
            //bullet.transform.rotation = turret;
            bullet.SetActive(true);
            bullet.SendMessage("Fire", targetAngle, SendMessageOptions.DontRequireReceiver);
        }
    }

   
}
