using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class UWCMono_ImportWorldPosition : MonoBehaviour
{
    public RightBorderPercentFetch m_squarePositionX = new RightBorderPercentFetch(0.40f);
    public RightBorderPercentFetch m_squarePositionY = new RightBorderPercentFetch(0.60f);

    public float worldPositionX = 0;
    public float worldPositionY = 0;
    public void PushIn(Texture2D texture)
    {
        m_squarePositionX.PushIn(texture);
        m_squarePositionY.PushIn(texture);
        Color x = m_squarePositionX.m_colorFetched;
        Color y = m_squarePositionY.m_colorFetched;

        worldPositionY = y.b + y.r * 100 + y.r * 10000;
        worldPositionX = x.b + x.r * 100 + x.r * 10000;
    }
}
