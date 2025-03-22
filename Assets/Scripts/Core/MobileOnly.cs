using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileOnlyObject : MonoBehaviour
{
    void Awake()
    {
#if UNITY_ANDROID || UNITY_IOS
        gameObject.SetActive(true);
#else
        Destroy(gameObject);
#endif
    }
}

