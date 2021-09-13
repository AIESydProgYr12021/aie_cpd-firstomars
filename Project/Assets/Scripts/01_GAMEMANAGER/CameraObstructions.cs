using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CameraObstructions : MonoBehaviour
{
    //GameObject player;
    [SerializeField] private GameObject player;
    List<GameObject> objectsObstructingView = new List<GameObject>();
    int layer_mask;

    // Start is called before the first frame update
    void Start()
    {
        player = PlayerManager.instance.player;
        layer_mask = LayerMask.GetMask("Obstacle");
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null) return;
        
        //raycast variables
        Vector3 pos = transform.position;
        Vector3 dir = (player.transform.position - transform.position).normalized;
        float distance = Vector3.Distance(player.transform.position, transform.position);
        RaycastHit[] res = new RaycastHit[5];

        //raycast
        int raycastHits = Physics.RaycastNonAlloc(pos, dir, res, distance, layer_mask);

        //list of objs to keep track of
        List<GameObject> currentObjectsInView = res.Take(raycastHits).Select(obj => obj.collider.gameObject).ToList();
        List<GameObject> notInView = objectsObstructingView.Except(currentObjectsInView).ToList();

        // HACK / SLOW / GROSE
        objectsObstructingView.AddRange(currentObjectsInView);
        objectsObstructingView = objectsObstructingView.Distinct().ToList();

        //if no longer obstructing return alpha to default
        foreach (var objNotInView in notInView)
        {
            var material = objNotInView.GetComponent<Renderer>().material;

            ChangeRenderingModeToOpaque(material);

            var color = material.color;
            color.a += 0.01f;
            material.color = color;
        }

        //if obstructing decrease alpha
        foreach (var objInView in currentObjectsInView)
        {
            var material = objInView.GetComponent<Renderer>().material;

            ChangeRenderingModeToTransparent(material);

            var color = material.color;
            color.a -= 0.01f;

            if (color.a < 0.5f) color.a = 0.5f;

            material.color = color;
        }

        //remove objects from list that have had their alpha reduced below 100%
        objectsObstructingView = objectsObstructingView.Where(z => z.GetComponent<Renderer>().material.color.a < 1.0f).ToList();

    }

    private static void ChangeRenderingModeToOpaque(Material material)
    {
        //change from transparent to opaque in runtime
        material.SetFloat("_Mode", 0);
        material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
        material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
        material.SetInt("_ZWrite", 1);
        material.DisableKeyword("_ALPHATEST_ON");
        material.DisableKeyword("_ALPHABLEND_ON");
        material.EnableKeyword("_ALPHAPREMULTIPLY_ON");
        material.renderQueue = -1;
    }

    private static void ChangeRenderingModeToTransparent(Material material)
    {
        //change from opaque to transparent in runtime
        material.SetFloat("_Mode", 3);
        material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
        material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        material.SetInt("_ZWrite", 0);
        material.DisableKeyword("_ALPHATEST_ON");
        material.DisableKeyword("_ALPHABLEND_ON");
        material.EnableKeyword("_ALPHAPREMULTIPLY_ON");
        material.renderQueue = 3000;
    }
}
//}
