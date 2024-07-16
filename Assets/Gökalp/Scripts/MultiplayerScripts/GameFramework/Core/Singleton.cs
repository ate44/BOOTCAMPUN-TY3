using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Component
{

    private static T _instance;

    public static T Instance
    {
        get {

            if (_instance == null)
            {
                T[] objects = FindObjectsOfType<T>();
                if(objects.Length > 0)
                {
                    T instance = objects[0];
                    _instance = instance;
                }
                else
                {
                    GameObject gameObject = new GameObject();
                    gameObject.name = typeof(T).Name;
                    _instance = gameObject.AddComponent<T>();
                    DontDestroyOnLoad(gameObject);
                }
            }
            
            return _instance; 
        }
    }
}
