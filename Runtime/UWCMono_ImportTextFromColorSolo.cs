using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;
using uWindowCapture;


public class UWCMono_ImportTextFromColorSolo : MonoBehaviour
{
    public int m_lineAdjustement = 31;
    public int m_columnAdjustement = 1;
    public int m_lineMaxLenght = 4;
    public int m_columnMaxLenght = 600;
    public Texture2D m_linkedTextureCopy;
    public Color[] m_pixels;

    public int m_maxChar = 0;

    [TextArea(2, 30)]
    public string m_text;

    public bool m_isInitialized = false;
    public bool m_catchException;

    public void PushIn(Texture2D texture)
    {
        if (m_catchException) {

            try
            {
                PushInNoCatch(texture);
            }
            catch (Exception)
            {
            }
        }
        else
        {
            PushInNoCatch(texture);
        }

    }
    public void PushInNoCatch(Texture2D texture) { 
    
    
        // NEED TO BE CHANGED TO BE STRECHABLE
        if (texture == null)
        {
            return;
        }

        m_linkedTextureCopy = texture;
        m_pixels = m_linkedTextureCopy.GetPixels(m_columnAdjustement, m_lineAdjustement, m_lineMaxLenght, m_columnMaxLenght);
        m_text = "";
        m_maxChar = m_pixels.Length * 3;
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < m_pixels.Length; i++)
        {
            if (m_pixels[i].r > 0.999f || m_pixels[i].g > 0.999f || m_pixels[i].b > 0.999f)
            {
            }
            else
            {
                char x255 = (char)(int)(m_pixels[i].r * 255);
                char y255 = (char)(int)(m_pixels[i].g * 255);
                char z255 = (char)(int)(m_pixels[i].b * 255);
                sb.Append(x255);
                sb.Append(y255);
                sb.Append(z255);
            }
        }
        m_text = sb.ToString();
    }
}
