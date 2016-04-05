using UnityEngine;
using System.Collections;

public class PlatformDisabler : MonoBehaviour {
    // this game object must be on the same layer as platform
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Platform")
        {
            SimplePool.Despawn(other.gameObject);
        }
    }
}
