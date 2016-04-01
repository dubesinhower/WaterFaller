using UnityEngine;
using System.Collections;

public class ScrollingMovement : MonoBehaviour {

    // Use this for initialization
    public Vector2 ScrollDirection = Vector2.zero;
    public float Speed = 0;
    void Start()
    {
    }
    void Update()
    {
        Vector3 scroll = ScrollDirection*Speed*Time.deltaTime;
        transform.position += scroll; 
    }
}
