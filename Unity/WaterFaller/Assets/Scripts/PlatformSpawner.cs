using UnityEngine;
using System.Collections;

public class PlatformSpawner : MonoBehaviour
{
    public GameObject PlatformPrefab;
    public float SpawnerWidth;
    public float SpawnTime;

	// Use this for initialization
	void Start () {
	    Spawn();
	}

    void Spawn()
    {
        var offset = new Vector3(Random.Range(-1f, 1f) * (SpawnerWidth/2), 0);
        var platform = SimplePool.Spawn(PlatformPrefab, transform.position + offset, Quaternion.identity);
        platform.transform.SetParent(this.transform);
        Invoke("Spawn", SpawnTime);
    }
}
