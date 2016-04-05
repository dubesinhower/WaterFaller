using UnityEngine;
using System.Collections;
using Tiled2Unity;

// http://answers.unity3d.com/questions/501893/calculating-2d-camera-bounds.html
public class ClampCameraToObject : MonoBehaviour
{
    public GameObject Object;
    private float _minX, _maxX, _minY, _maxY;

	// Use this for initialization
	void Start ()
	{
	    var vertExtent = Camera.main.orthographicSize;
	    var horzExtent = vertExtent * Screen.width / Screen.height;

        // Assumes the object is positioned at the origin
	    _minX = horzExtent - Object.transform.localScale.x / 2.0f;
	    _maxX = Object.transform.localScale.x / 2.0f - horzExtent;
	    _minY = vertExtent - Object.transform.localScale.y / 2.0f;
	    _maxY = Object.transform.localScale.y / 2.0f - vertExtent;
	}
	
	// Update is called once per frame
	void LateUpdate () {
        var v3 = transform.position;
        v3.x = Mathf.Clamp(v3.x, _minX, _maxX);
        v3.y = Mathf.Clamp(v3.y, _minY, _maxY);
        transform.position = v3;
    }
}
