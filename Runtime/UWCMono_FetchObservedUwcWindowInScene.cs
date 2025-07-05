using System;
using System.Collections.Generic;
using UnityEngine;
using uWindowCapture;

public class UWCMono_FetchObservedUwcWindowInScene : MonoBehaviour
{

    public string m_windowName = "World of Warcraft";
    [SerializeField] List<UwcWindowPixelsAccess> uwcTexturesInScene;

    void Awake()
    {
        Invoke(nameof(Refresh), m_delayInSecondsToStart);
    }

    [ContextMenu("Refresh")]
    public void Refresh()
    {
        FindAllUWcInSceneAndDestroy();
    }

    public float m_delayInSecondsToStart =2f;
    public bool m_disableUwcTextureObservers = false;
    private void FindAllUWcInSceneAndDestroy()
    {
        uwcTexturesInScene = new List<UwcWindowPixelsAccess>();
        var uwcTextures = GameObject. FindObjectsByType<UwcWindowTexture>(FindObjectsSortMode.None);
        foreach (var uwcTexture in uwcTextures)
        {
            if (uwcTexture.window != null)
            {
                if (uwcTexture.window.title.Trim().Equals(m_windowName))
                {
                    uwcTexturesInScene.Add(new UwcWindowPixelsAccess(uwcTexture));
                }
              
                else
                {
                    // disable game object
                    if (m_disableUwcTextureObservers)
                     uwcTexture.gameObject.SetActive(false);
                }
            }
        }
        Debug.Log($"Found {uwcTexturesInScene.Count} UWC textures in scene.");
    }

    public void GetUwcWindowPixelsAccess(int index, out bool found, out UwcWindowPixelsAccess uwcWindowPixelsAccess)
    {
        if (index < 0 || index >= uwcTexturesInScene.Count)
        {
            found = false;
            uwcWindowPixelsAccess = null;
            return;
        }
        uwcWindowPixelsAccess = uwcTexturesInScene[index];
        found = true;
    }

    public void GetWindowCount(out int count)
    {
        count = uwcTexturesInScene.Count;
    }

    public void GetWindowSize(int index, out int m_width, out int m_height)
    {
        if (index < 0 || index >= uwcTexturesInScene.Count)
        {
            m_width = -1;
            m_height = -1;
            return;
        }
        var uwcWindowPixelsAccess = uwcTexturesInScene[index];
        if (uwcWindowPixelsAccess != null && uwcWindowPixelsAccess.m_window != null && uwcWindowPixelsAccess.m_window.window != null)
        {
            m_width = uwcWindowPixelsAccess.m_window.window.width;
            m_height = uwcWindowPixelsAccess.m_window.window.height;
        }
        else
        {
            m_width = -1;
            m_height = -1;
        }
    }
}


[System.Serializable]
public class UwcWindowPixelsAccess
{
    public UwcWindowTexture m_window;
    public Material m_material;
    public Color32[] m_colors;
    public int m_width;
    public int m_height;
    public int m_hWnd32;
    public UwcWindowPixelsAccess(UwcWindowTexture uwcTexture)
    {
        m_window = uwcTexture;
        m_material = uwcTexture.GetComponent<Renderer>().material;
        m_material.mainTexture.filterMode = FilterMode.Point;
        m_material.mainTexture.wrapMode = TextureWrapMode.Clamp;
        m_colors = new Color32[1];
        m_colors[0] = Color.red;
        m_width = uwcTexture.window.width;
        m_height = uwcTexture.window.height;
        m_hWnd32 = uwcTexture.window.handle.ToInt32();
    }

    public void GetHWnd32(out int hWnd32)
    {
        hWnd32 = m_hWnd32;
    }

    public void GetPixels(int x, int y, int width, int height, out bool found, ref Color32[] foundColor)
    {
        var window = m_window.window;
        if (window == null || window.width == 0)
        {
            found = false; return;
        }

        // if out of bounds, return false
        if (x < 0 || y < 0 || x + width > window.width || y + height > window.height)
        {
            found = false; return;
        }

        if (foundColor == null || foundColor.Length != width * height)
        {
            foundColor = new Color32[width * height];
        }
        if (window.GetPixels(foundColor, x, y, width, height))
        {
            found = true;
        }
        else
        {
            found = false;
        }
    }

    public void GetPixelAtPercentDownLeftTopRight(float percentX, float percentY, out bool found, out Color32 foundColor)
    {
        var window = m_window.window;
        if (window == null || window.width == 0) { found = false; foundColor = Color.red; return; }
        int x = (int)(window.width * percentX);
        int y = (int)(window.height *(1f-percentY));

        x = Mathf.Clamp(x, 0, window.width - 1);
        y = Mathf.Clamp(y, 0, window.height - 1);
        GetPixel(x, y, out found, out foundColor);
    }

    public void GetPixelAtPercentDownLeftTopRight(int pixel, float percentY, out bool found, out Color32 foundColor)
    {
        var window = m_window.window;
        if (window == null || window.width == 0) { found = false; foundColor = Color.red; return; }
        int x = (int)(pixel);
        int y = (int)(window.height * (1f - percentY));

        x = Mathf.Clamp(x, 0, window.width - 1);
        y = Mathf.Clamp(y, 0, window.height - 1);
        GetPixel(x, y, out found, out foundColor);
    }
    public void GetPixelAtPercentDownRightTopLeft(int pixel, float percentY, out bool found, out Color32 foundColor, int windowTopPixeld = 31)
    {
        if (m_window == null) { found = false; foundColor = Color.red; return; }
        if (m_window.window == null) { found = false; foundColor = Color.red; return; }
        if (m_window.window.width == 0) { found = false; foundColor = Color.red; return; }
        if (m_window.window.height == 0) { found = false; foundColor = Color.red; return; }

        int heightLessHead = m_window.window.height - windowTopPixeld;


        var window = m_window.window;
        if (window == null || window.width == 0) { found = false; foundColor = Color.red; return; }
        int x = (int)(window.width - pixel);
        int y = (int)(heightLessHead * (1f - percentY));
        y += windowTopPixeld;

        x = Mathf.Clamp(x, 0, window.width - 1);
        y = Mathf.Clamp(y, 0, window.height - 1);
        GetPixel(x, y, out found, out foundColor);
    }

    public void GetPixelAtPercentDownLeftTopRight(int pixel, float percentY, out bool found, out Color32 foundColor, int windowTopPixeld = 31)
    {

        int heightLessHead = m_window.window.height - windowTopPixeld;


        var window = m_window.window;
        if (window == null || window.width == 0) { found = false; foundColor = Color.red; return; }
        int x = (int)( pixel);
        int y = (int)(heightLessHead * (1f - percentY));
        y += windowTopPixeld;

        x = Mathf.Clamp(x, 0, window.width - 1);
        y = Mathf.Clamp(y, 0, window.height - 1);
        GetPixel(x, y, out found, out foundColor);
    }
    public void GetPixelAtPercentTopRightDownLeft(int pixelRightLeft, int pixelTopDown, out bool found, out Color32 foundColor)
    {
        var window = m_window.window;
        if (window == null || window.width == 0) { found = false; foundColor = Color.red; return; }
        int x = (int)(window.width - pixelRightLeft);
        int y = (int)( pixelTopDown);

        x = Mathf.Clamp(x, 0, window.width - 1);
        y = Mathf.Clamp(y, 0, window.height - 1);
        GetPixel(x, y, out found, out foundColor);
    }
    public void GetPixelAtPercentDownRightTopLeft(int pixel, float percentY, out bool found, out Color32 foundColor, out Vector4 xyWidthHeight)
    {
        GetPixelAtPercentDownRightTopLeft(pixel, percentY, out found, out foundColor, out int x, out int y, out int width, out int heigth);
        xyWidthHeight = new Vector4(x, y, width, heigth);

    }
    public void GetPixelAtPercentDownRightTopLeft(int pixel, float percentY, out bool found, out Color32 foundColor, out int x, out int y, out int width, out int height)
    {
        var window = m_window.window;
        if (window == null || window.width == 0) { found = false; foundColor = Color.red;

            x = 0; y = 0; width = 0; height = 0;
            return; }
        x = (int)(window.width - pixel);
        y = (int)(window.height * (1f - percentY));

        x = Mathf.Clamp(x, 0, window.width - 1);
        y = Mathf.Clamp(y, 0, window.height - 1);
        GetPixel(x, y, out found, out foundColor);

        width = window.width;
        height = window.height;
    }

    

    public void GetPixel(int x, int y, out bool found, out Color32 foundColor)
    {
        GetPixels(x, y, 1,1, out found, ref m_colors);
        if (found)
        {
            foundColor = m_colors[0];
        }
        else
        {
            foundColor = Color.yellow;
        }
    }

}