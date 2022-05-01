using UnityEngine;

namespace uWindowCapture
{

[RequireComponent(typeof(UwcWindowTextureManager))]
public class UwcHorizontalLayouter : MonoBehaviour
{
    UwcWindowTextureManager manager_;

    void Awake()
    {
        manager_ = GetComponent<UwcWindowTextureManager>();
        manager_.onWindowTextureAdded.AddListener(InitWindow);
    }

    void InitWindow(UwcWindowTexture windowTexture)
    {
    }

    Bounds GetMaxBounds(GameObject g) {
        var renderers = g.GetComponentsInChildren<Renderer>();
        if (renderers.Length == 0) return new Bounds(g.transform.position, Vector3.zero);
        var b = renderers[0].bounds;
        foreach (Renderer r in renderers) {
            b.Encapsulate(r.bounds);
        }
        return b;
    }

    void Update()
    {
        var pos = Vector3.zero;
        var currentWidth = 0f;
        var maxWidth = 3 * Lib.GetScreenWidth();

        foreach (var kv in manager_.windows) {
            var windowTexture = kv.Value;
            var width = windowTexture.transform.localScale.x;
            var height = windowTexture.transform.localScale.y;

            currentWidth += windowTexture.window.width;
            bool isMaxWidth = currentWidth > maxWidth;
            var newPos = new Vector3(pos.x + width * 0.5f, pos.y, pos.z);
            if (isMaxWidth) {
                newPos.x = width * 0.5f;
                newPos.y += height * 1.2f;
                currentWidth = 0f;
            }

            windowTexture.transform.localPosition = newPos;
            newPos.x += width * 0.7f;
            pos = newPos;
        }

        var windows = GameObject.Find("Windows");
        if (windows != null)
        {
            var bounds = GetMaxBounds(windows);
            var cube = GameObject.Find("Cube");
            if (cube != null)
            {
                cube.transform.position = bounds.center + new Vector3(-0.02f, -0.02f, 0.1f);
                cube.transform.localScale = bounds.size + new Vector3(0.2f, 0.2f, 0.1f);
            }
        }
    }

}

}