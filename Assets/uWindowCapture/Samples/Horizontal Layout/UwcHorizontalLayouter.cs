using UnityEngine;

namespace uWindowCapture
{

[RequireComponent(typeof(UwcWindowTextureManager))]
public class UwcHorizontalLayouter : MonoBehaviour
{
    UwcWindowTextureManager manager_;

    [SerializeField] 
    [Tooltip("meter / 1000 pixel")]
    float scale = 1f;

    Vector3 globalPos = Vector3.zero;

    void Awake()
    {
        manager_ = GetComponent<UwcWindowTextureManager>();
        manager_.onWindowTextureAdded.AddListener(InitWindow);

        var cube = GameObject.Find("Cube");
        if (cube != null)
        {
            var cubeSize = cube.GetComponent<MeshRenderer>().bounds.size;
            var cubePos = cube.transform.localPosition;
            Debug.Log("cubePos: " + cubePos);
            Debug.Log("cubeSize: " + cubeSize);
        }
    }

    void InitWindow(UwcWindowTexture windowTexture)
    {
        // var xRange = 1.5f;
        // var yRange = 1.5f;
        // var zRange = 0.5f;
        // var randomVector = new Vector3(Random.Range(-xRange, xRange), Random.Range(-yRange, yRange), Random.Range(-zRange, zRange));
        // windowTexture.transform.localPosition = randomVector;
        // ScaleWindow(windowTexture, false);
    }

    void Update()
    {
        var pos = Vector3.zero;


        foreach (var kv in manager_.windows) {
            var windowTexture = kv.Value;
            var width = windowTexture.transform.localScale.x;
            pos += new Vector3(width * 0.5f, 0f, 0f);
            windowTexture.transform.localPosition = pos;
            pos += new Vector3(width * 0.7f, 0f, 0f);
        }
    }

    void ScaleWindow(UwcWindowTexture windowTexture, bool useFilter)
    {
        windowTexture.scaleControlType = WindowTextureScaleControlType.BaseScale;
        windowTexture.scalePer1000Pixel = scale;
    }

}

}