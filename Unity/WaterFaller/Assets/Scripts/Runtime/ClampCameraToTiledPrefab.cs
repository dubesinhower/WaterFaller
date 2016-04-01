using UnityEngine;
using System.Collections;
using Tiled2Unity;

// http://answers.unity3d.com/questions/501893/calculating-2d-camera-bounds.html
public class ClampCameraToTiledPrefab : MonoBehaviour
{
    public GameObject Prefab;
    private float _minX, _maxX, _minY, _maxY;

	// Use this for initialization
	void Start ()
	{
	    var tiledMap = Prefab.GetComponent<TiledMap>();

	    var mapX = tiledMap.NumTilesWide;
	    var mapY = tiledMap.NumTilesHigh;
	    var vertExtent = Camera.main.orthographicSize;
	    var horzExtent = vertExtent * Screen.width / Screen.height;

        // Assumes the prefab extends to the bottom right
	    _minX = Prefab.transform.position.x + horzExtent;
	    _maxX = mapX - horzExtent;
	    _minY = vertExtent - mapY;
	    _maxY = Prefab.transform.position.y - vertExtent;
	}
	
	// Update is called once per frame
	void LateUpdate () {
        var v3 = transform.position;
        v3.x = Mathf.Clamp(v3.x, _minX, _maxX);
        v3.y = Mathf.Clamp(v3.y, _minY, _maxY);
        transform.position = v3;
    }
}
