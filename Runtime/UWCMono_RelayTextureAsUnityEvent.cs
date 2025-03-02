using UnityEngine;
using UnityEngine.Events;
using uWindowCapture;

public class UWCMono_RelayTextureAsUnityEvent : MonoBehaviour
{
    public UwcWindowTexture m_source;
    public Texture2D m_linkedTexture;
    public Texture2D m_linkedTextureCopy;
    public UnityEvent<Texture2D> m_onTextureRelayed;
    bool isInitialized = false;
    public void LateUpdate()
    {
        Refresh();
    }

    private void Refresh()
    {

        if (m_source == null
            || m_source.window == null
            || m_source.window.texture == null)
        {
            return;
        }

        if (!isInitialized)
        {
            isInitialized = true;
            m_source.window.onCaptured.AddListener(Refresh);
            m_linkedTexture = m_source.window.texture;
        }
        m_linkedTexture = m_source.window.texture;
        CopyTexture(ref m_linkedTexture, ref m_linkedTextureCopy);
        m_onTextureRelayed.Invoke(m_linkedTextureCopy);
    }
    private void CopyTexture(ref Texture2D source, ref Texture2D copy)
    {
        if (source == null) return;
        if (copy == null || copy.width != source.width || copy.height != source.height)
        {
            copy = new Texture2D(source.width, source.height, source.format, false);
        }

        RenderTexture tempRT = new RenderTexture(source.width, source.height, 0);
        Graphics.Blit(source, tempRT);
        RenderTexture.active = tempRT;
        copy.ReadPixels(new Rect(0, 0, source.width, source.height), 0, 0);
        copy.Apply();
        RenderTexture.active = null;
        tempRT.Release();
    }
}
