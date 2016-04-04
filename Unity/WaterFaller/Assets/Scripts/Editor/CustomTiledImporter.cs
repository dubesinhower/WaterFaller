using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

// Example custom importer:
[Tiled2Unity.CustomTiledImporter]
class CustomTiledImporter : Tiled2Unity.ICustomTiledImporter
{
    public void HandleCustomProperties(GameObject gameObject,
        IDictionary<string, string> customProperties)
    {
        Debug.Log("Handle custom properties from Tiled map");
        //if (!customProperties.ContainsKey("Handler")) return;
        //switch (customProperties["Handler"])
        //{
        //    case "Platform":
        //        // Get array of one way platform box colliders
        //        var polyColliders = gameObject.GetComponentsInChildren<PolygonCollider2D>();

        //        // Add platform effector to each box colliders' game object
        //        foreach (var collider in polyColliders)
        //        {
        //            var platformEffector = collider.gameObject.AddComponent<PlatformEffector2D>();
        //            platformEffector.useOneWay = false;
        //            collider.usedByEffector = true;
        //        }
        //        break;
        //    case "OneWayPlatform":
        //        // Get array of one way platform box colliders
        //        var boxColliders = gameObject.GetComponentsInChildren<BoxCollider2D>();

        //        // Add platform effector to each box colliders' game object
        //        foreach (var collider in from collider in boxColliders let platformEffector = collider.gameObject.AddComponent<PlatformEffector2D>() select collider)
        //        {
        //            collider.usedByEffector = true;
        //        }
        //        break;
        //}
    }

    public void CustomizePrefab(GameObject prefab)
    {
        Debug.Log("Customize prefab");
        var waterfallGameObject = GameObject.FindGameObjectWithTag("Waterfall");

        if (waterfallGameObject.GetComponent<WaterfallController>())
        {
            Debug.LogError("Waterfall Game Object already has a WaterfallScript component.");
            return;
        }

        waterfallGameObject.AddComponent<WaterfallController>();
    }
}
