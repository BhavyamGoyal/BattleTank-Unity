using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class NonMonoSingleton<T> 
    where T : NonMonoSingleton<T>,new()
{
   
    private static T instance=new T();
    public static T Instance
    {
        get
        {
            return instance;
        }
    }
}

