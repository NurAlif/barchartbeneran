using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ObjectPool : MonoBehaviour
{

    [SerializeField]
    private GameObject gameObject;

    private int size = 50;

    [HideInInspector]
    public List<GameObject> objects = new List<GameObject>();

    void Awake()
    {
        GameObject go;
        for (int i = 0; i < size; i++)
        {
            go = Instantiate(gameObject, transform);
            go.SetActive(false);
            objects.Add(go);
        }
    }

    public GameObject GetPooled()
    {
        for (int i = 0; i < size; i++)
        {
            if (!objects[i].activeInHierarchy)
            {
                objects[i].SetActive(true);
                return objects[i];
            }
        }
        return null;
    }

    public void Return(GameObject go)
    {
        go.transform.parent = transform;
    }


}
