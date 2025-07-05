using System;
using UnityEngine;
using UnityEngine.Events;

public class UWCMono_FetchWindowToColor32WH : MonoBehaviour
{
    public UWCMono_FetchObservedUwcWindowInScene m_source;

    public int m_windowIndexToFetch = 0;

    public int m_width = -1;
    public int m_height = -1;
    public RenderTexture m_renderTexture;
    public UnityEvent<RenderTexture> m_onRenderTextureCreated;
    public UnityEvent<RenderTexture> m_onRenderTextureUpdated;
    public ComputeBuffer m_computeBufferOfRenderTexture;
    public Unity.Collections.NativeArray<Color32> m_color32Array;

    public float m_refreshRate = 0.1f;

    private void Awake()
    {
        InvokeRepeating(nameof(FetchWindowToColor32), 0f, m_refreshRate);
    }

    private void FetchWindowToColor32()
    {
        m_source.GetWindowCount(out int windowCount);
        if (windowCount <= m_windowIndexToFetch)
        {
            return;
        }
        m_source.GetWindowSize(m_windowIndexToFetch, out m_width, out m_height);
        m_source.GetUwcWindowPixelsAccess(m_windowIndexToFetch, out bool found, out UwcWindowPixelsAccess uwcWindowPixelsAccess);

        if (!found || uwcWindowPixelsAccess == null)
        {

            return;
        }
        if (m_color32Array == null || m_color32Array.Length != m_width * m_height)
        {
            if (m_renderTexture == null || m_renderTexture.width != m_width || m_renderTexture.height != m_height)
            {
                if (m_renderTexture != null)
                {
                    m_renderTexture.Release();
                }
                if (m_computeBufferOfRenderTexture != null)
                {
                    m_computeBufferOfRenderTexture.Release();
                }
                if (m_color32Array.IsCreated)
                {
                    m_color32Array.Dispose();
                }
                m_renderTexture = new RenderTexture(m_width, m_height, 0);
                m_renderTexture.enableRandomWrite = true;
                m_renderTexture.Create();
                m_onRenderTextureCreated?.Invoke(m_renderTexture);
            }
            m_computeBufferOfRenderTexture = new ComputeBuffer(m_width * m_height, sizeof(float) * 4);
            m_color32Array = new Unity.Collections.NativeArray<Color32>(m_width * m_height, Unity.Collections.Allocator.Persistent);
        }
        if (m_renderTexture != null)
        {
            Graphics.Blit(uwcWindowPixelsAccess.m_material.mainTexture, m_renderTexture);
            m_onRenderTextureUpdated?.Invoke(m_renderTexture);
        }
      
    }
}
