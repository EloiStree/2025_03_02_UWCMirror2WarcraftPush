using UnityEngine;

public class UWCMono_ImportHealXp : MonoBehaviour
{
    public RightBorderPercentFetch m_squarePosition = new RightBorderPercentFetch(0.8f);

    public int m_rPlayerLevel ;
    [Range(0,1)]
    public float m_gPercentLife;
    [Range(0, 1)]
    public float m_bPercentXp;

    public void PushIn(Texture2D texture) {

        m_squarePosition.PushIn(texture);
        Color c = m_squarePosition.m_colorFetched;
        m_rPlayerLevel = (int)(c.r * 100f);
        m_gPercentLife = c.g;
        m_bPercentXp = c.b;

    }
}

