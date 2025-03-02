using UnityEngine;

[System.Serializable]
public class RLTDPixelCoordinate {
    public int m_rightToLeftPixel = 25;
    public int m_topToBottomPixel = 100;

    public RLTDPixelCoordinate(int rightToLeftPixel, int topToBottomPixel)
    {
        m_rightToLeftPixel = rightToLeftPixel;
        m_topToBottomPixel = topToBottomPixel;
    }

    public Color GetColorFrom(ref Texture2D texture) { 
        return texture.GetPixel(texture.width - m_rightToLeftPixel,  m_topToBottomPixel);

    }
}

