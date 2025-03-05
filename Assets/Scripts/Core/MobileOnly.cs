using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileOnlyObject : MonoBehaviour
{
    void Start()
    {
        #if UNITY_ANDROID || UNITY_IOS
            gameObject.SetActive(true);
        #else
            gameObject.SetActive(false);
        #endif
    }
}

