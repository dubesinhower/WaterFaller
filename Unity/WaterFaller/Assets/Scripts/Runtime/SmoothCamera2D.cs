using UnityEngine;
using System.Collections;

// http://answers.unity3d.com/questions/29183/2d-camera-smooth-follow.html
public class SmoothCamera2D : MonoBehaviour
{

    public float dampTime = .15f;
    private Vector3 velocity = Vector3.zero;
    public Transform target;

    void Start()
    {
        var destination = GetTargetViewPortPosition();
        transform.position = destination;
    }
	
	// Update is called once per frame
	void Update() {
	    if (target)
	    {
	        var destination = GetTargetViewPortPosition();
            transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
        }
	}

    Vector3 GetTargetViewPortPosition()
    {
        Vector3 point = Camera.main.WorldToViewportPoint(target.position);
        Vector3 delta = target.position - Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z)); //(new Vector3(0.5, 0.5, point.z));
        return transform.position + delta;
    }
}
