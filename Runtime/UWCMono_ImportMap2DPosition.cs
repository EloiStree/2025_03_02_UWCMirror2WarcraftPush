using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class UWCMono_ImportMap2DPosition : MonoBehaviour
{
    public RightBorderPercentFetch m_squarePosition = new RightBorderPercentFetch(0.2f);

    [Range(0, 100)]
    public float m_rMapX;
    [Range(0, 100)]
    public float m_gMapY;
    [Range(0, 360)]
    public float m_bRotationAngle;

    public void PushIn(Texture2D texture)
    {

        m_squarePosition.PushIn(texture);
        Color c = m_squarePosition.m_colorFetched;
        m_rMapX = c.r * 100f;
        m_gMapY = c.g * 100f;
        m_bRotationAngle = c.b * 360f;
    }
}

