using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCOnlyObject : MonoBehaviour
{
    void Awake()
    {
#if UNITY_STANDALONE_WIN || UNITY_STANDALONE_OSX || UNITY_STANDALONE_LINUX
            gameObject.SetActive(true);
#else
        Destroy(gameObject);
#endif
    }
}
