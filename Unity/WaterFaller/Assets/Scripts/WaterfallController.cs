using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Tiled2Unity;

public class Indices
{
    public int X { get; set; }
    public int Y { get; set; }

    public Indices(int x, int y)
    {
        X = x;
        Y = y;
    }
}

public class WaterfallController : MonoBehaviour
{
    public float ScrollSpeed = 2f;
    public float PlatformDistance = 2f;
    public float PlatformMargin = 5f;
    public float PlatformPadding = 1.5f;
    public GameObject PlatformGameObject;

    private float _waterfallX;
    private float _waterfallY;

    private readonly Queue<GameObject> _waterfallMeshes = new Queue<GameObject>();
    private readonly Queue<GameObject> _platforms = new Queue<GameObject>();

    private TiledMap _tiledMap;

    void Awake()
    {
        _tiledMap = GetComponentInParent<TiledMap>();
        var waterfallMeshRenderer = this.GetComponentInChildren<MeshRenderer>();
        _waterfallX = waterfallMeshRenderer.bounds.size.x;
        _waterfallY = waterfallMeshRenderer.bounds.size.y;
    }

	// Use this for initialization
	void Start ()
	{
        ConfigureWaterfallMeshes();
	    GenerateInitialPlatforms();
	}
	
	// Update is called once per frame
	void Update () {
        UpdateWaterfallMeshes();
	}

    void ConfigureWaterfallMeshes()
    {
        // Select the waterfall mesh
        var firstMesh = this.gameObject.transform.GetChild(0).gameObject;

        // Create copies of the waterfall mesh, offset them by the height of the waterfall, set their parents, and scale them correctly
        var secondMesh = SimplePool.Spawn(firstMesh, firstMesh.transform.position + (Vector3.up * _waterfallY), Quaternion.identity);
        var thirdMesh = SimplePool.Spawn(firstMesh, firstMesh.transform.position + (Vector3.up * _waterfallY * 2), Quaternion.identity);
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

    void GenerateInitialPlatforms()
    {
        // Using Poisson disk sampling
        // http://www.cs.ubc.ca/~rbridson/docs/bridson-siggraph07-poissondisk.pdf

        var sampler = new PoissonDiscSampler(_waterfallX - PlatformPadding * 2, _waterfallY * 3, PlatformDistance);
        foreach (var vector2 in sampler.Samples())
        {
            var newPlatform = SimplePool.Spawn(PlatformGameObject,
                vector2 + new Vector2(PlatformMargin + PlatformPadding, -_waterfallY), Quaternion.identity);
            newPlatform.transform.SetParent(_tiledMap.gameObject.transform);
            _platforms.Enqueue(newPlatform);
        }
    }

    void UpdateWaterfallMeshes()
    {
        // Check if the lowest mesh is below the waterfall
        if (IsBelowWaterfall(_waterfallMeshes.Peek()))
        {
            var mesh = _waterfallMeshes.Dequeue();
            mesh.transform.position += Vector3.up*_waterfallY*3;
            _waterfallMeshes.Enqueue(mesh);
        }
    }

    bool IsBelowWaterfall(GameObject obj)
    {
        return obj.transform.position.y < -_waterfallY;
    }
}
