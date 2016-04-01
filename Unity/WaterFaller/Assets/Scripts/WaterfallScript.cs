using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WaterfallScript : MonoBehaviour
{
    public float ScrollSpeed = 2f;
    private readonly Queue<GameObject> _waterfallMeshes = new Queue<GameObject>();
    private float _waterfallOffset;

	// Use this for initialization
	void Start ()
	{
        ConfigureWaterfallMeshes();
    }
	
	// Update is called once per frame
	void Update () {
        UpdateWaterfallMeshes();
	}

    void ConfigureWaterfallMeshes()
    {
        // Select the waterfall mesh
        var firstMesh = this.gameObject.transform.GetChild(0).gameObject;
        var meshRenderer = firstMesh.GetComponent<MeshRenderer>();

        // Find out how tall the waterfall mesh is in pixels
        _waterfallOffset = meshRenderer.bounds.size.y;

        // Create copies of the waterfall mesh, offset them by the height of the waterfall, set their parents, and scale them correctly
        var secondMesh = SimplePool.Spawn(firstMesh, firstMesh.transform.position + (Vector3.up * _waterfallOffset), Quaternion.identity);
        var thirdMesh = SimplePool.Spawn(firstMesh, firstMesh.transform.position + (Vector3.up * _waterfallOffset * 2), Quaternion.identity);
        secondMesh.transform.SetParent(this.transform);
        secondMesh.transform.localScale = new Vector3(1 / firstMesh.transform.localScale.x, 1 / firstMesh.transform.localScale.y, 0);
        thirdMesh.transform.SetParent(this.transform);
        thirdMesh.transform.localScale = new Vector3(1 / firstMesh.transform.localScale.x, 1 / firstMesh.transform.localScale.y, 0);

        // Add meshes to the queue
        _waterfallMeshes.Enqueue(firstMesh);
        _waterfallMeshes.Enqueue(secondMesh);
        _waterfallMeshes.Enqueue(thirdMesh);

        // Add scrolling script to meshes
        foreach (var mesh in _waterfallMeshes)
        {
            var scrollingScript = mesh.AddComponent<ScrollingMovement>();
            scrollingScript.ScrollDirection = Vector2.down;
            scrollingScript.Speed = ScrollSpeed;
        }
    }

    void UpdateWaterfallMeshes()
    {
        // Check if the lowest mesh is below the waterfall
        if (IsBelowWaterfall(_waterfallMeshes.Peek()))
        {
            var mesh = _waterfallMeshes.Dequeue();
            mesh.transform.position += Vector3.up*_waterfallOffset*3;
            _waterfallMeshes.Enqueue(mesh);
        }
    }

    bool IsBelowWaterfall(GameObject obj)
    {
        return obj.transform.position.y < -_waterfallOffset;
    }
}
