using UnityEngine;
using System.IO;

public class ScreenshotCapture : MonoBehaviour
{
    public Camera renderCamera; // Assign in Inspector
    public RenderTexture renderTexture; // Assign in Inspector
    public string folderPath = "Assets/Render Textures/KeyScreenCaps"; // ? Location to store PNGs

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G)) // ? Press G to capture
        {
            CaptureScreenshot();
        }
    }

    public void CaptureScreenshot()
    {
        RenderTexture activeRenderTexture = RenderTexture.active;
        RenderTexture.active = renderTexture;
        renderCamera.Render();

        Texture2D screenshot = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.ARGB32, false);
        screenshot.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
        screenshot.Apply();

        RenderTexture.active = activeRenderTexture; // Reset active texture

        byte[] bytes = screenshot.EncodeToPNG();
        string fileName = $"CapturedImage_{System.DateTime.Now:yyyyMMdd_HHmmss}.png"; // ? Unique filename
        string fullPath = Path.Combine(folderPath, fileName);

        File.WriteAllBytes(fullPath, bytes);
        Debug.Log($"Saved Screenshot at: {fullPath}");
    }

    private void OnDrawGizmos()
    {
        if (renderCamera != null)
        {
            // Get camera frustum bounds
            Gizmos.color = Color.yellow;
            Gizmos.matrix = Matrix4x4.TRS(renderCamera.transform.position, renderCamera.transform.rotation, Vector3.one);
            Gizmos.DrawWireCube(Vector3.forward * renderCamera.nearClipPlane, new Vector3(renderTexture.width / 100f, renderTexture.height / 100f, 0.01f));
        }
    }
}
