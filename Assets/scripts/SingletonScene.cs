﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SingletonScene<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;
    public static T Instance
    {
        get
        {
            return instance;
        }

    }

    public void Awake()
    {
        instance = FindObjectOfType<T>();
       
    }




}
