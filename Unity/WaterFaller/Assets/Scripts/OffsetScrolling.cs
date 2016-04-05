using UnityEngine;
using System.Collections;

public class OffsetScrolling : MonoBehaviour
{
    public float ScrollSpeed;
    private MeshRenderer _renderer;

	// Use this for initialization
	void Start ()
	{
	    _renderer = GetComponent<MeshRenderer>();
	}
	
	// Update is called once per frame
	void Update ()
	{
	    float y = Mathf.Repeat(Time.time*ScrollSpeed, 1);
        Vector2 offset = new Vector2(0, y);
        _renderer.sharedMaterial.SetTextureOffset("_MainTex", offset);
	}
}
