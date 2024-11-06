using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonCreater
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void Initialize() {

        GameManager.Create();

    }
}
