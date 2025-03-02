using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class UWCMono_ImportWorldPosition : MonoBehaviour
{
    public RightBorderPercentFetch m_squarePositionX = new RightBorderPercentFetch(0.40f);
    public RightBorderPercentFetch m_squarePositionY = new RightBorderPercentFetch(0.60f);


    [Header("Position")]
    public float m_worldPositionX = 0;
    public float m_worldPositionY = 0;

    [Header("X")]
    public int m_rx;
    public int m_gx;
    public int m_bx;

    [Header("Y")]
    public int m_ry;
    public int m_gy;
    public int m_by;

    public void PushIn(Texture2D texture)
    {
        m_squarePositionX.PushIn(texture);
        m_squarePositionY.PushIn(texture);
        Color x = m_squarePositionX.m_colorFetched;
        Color y = m_squarePositionY.m_colorFetched;

        m_rx = (int)(x.r * 100f);
        m_ry = (int)(y.r * 100f);

        m_gx = ((int)(x.g * 100f)) * 100;
        m_gy = ((int)(y.g * 100f)) * 100;
        
        m_bx = ((int)(x.b * 100f)) * 10000;
        m_by = ((int)(y.b * 100f)) * 10000;

        m_worldPositionX = m_rx + m_gx + m_bx;
        m_worldPositionY = m_ry + m_gy + m_by;
    }
}
