using UnityEngine;

[System.Serializable]
public class RLTDSquareColorFetch {

    public RLTDPixelCoordinate m_coordinate;
    public Color m_colorFetched;
    public Texture2D m_texture;

    public RLTDSquareColorFetch(int rightToLeftPixel, int topToBottomPixel)
    {
        m_coordinate = new RLTDPixelCoordinate(rightToLeftPixel, topToBottomPixel);
    }

    public void PushIn(Texture2D texture)
    {
        m_texture = texture;
        if(m_texture == null)
        {
            return;
        }
        m_colorFetched = m_coordinate.GetColorFrom(ref m_texture);
    }
}




[System.Serializable]
public class RightBorderPixelCoordinate
{
    [Range(0,1)]
    public float m_topToBottomPercent = 1;

    public RightBorderPixelCoordinate(float  topToBottomPixel)
    {
        m_topToBottomPercent = topToBottomPixel;
    }

    public Color GetColorFrom(ref Texture2D texture)
    {
        int width = texture.width;
        int height = texture.height;
        return texture.GetPixel(width - 2, (int)(height * m_topToBottomPercent));

    }
}

[System.Serializable]
public class RightBorderPercentFetch
{

    public RightBorderPixelCoordinate m_coordinate;
    public Color m_colorFetched;
    public Texture2D m_texture;

    public RightBorderPercentFetch(float topToBottomPercent)
    {
        m_coordinate = new RightBorderPixelCoordinate(topToBottomPercent);
    }

    public void PushIn(Texture2D texture)
    {
        m_texture = texture;
        if (m_texture == null)
        {
            return;
        }
        m_colorFetched = m_coordinate.GetColorFrom(ref m_texture);
    }
}