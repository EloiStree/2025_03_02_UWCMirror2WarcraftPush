using UnityEngine;

public class TextureMono_NativeArray2DColor32To16PixelsBasicInfo : MonoBehaviour


{
    public void SetNativeArrayColor32(NativeArray2DColor32WH source) {
        m_source = source;

    }

    public NativeArray2DColor32WH m_source;
    public int m_borderPixelPaddingLeftRight = 2;
    public int m_borderPixelPaddingTop = 37;
    public int m_colorBlockCount =8;
    public Color32[] m_colorLeftTopDown = new Color32[8];
    public Color32[] m_colorRightTopDown = new Color32[8];


    public int m_blockHeight;
    public int m_startBlockTopY;
    public bool m_useUpdate=true;


    public void Update()
    {
        if(!m_useUpdate) return;

        Refresh();
    }


    public int m_1dIndex;
    public Color32 m_foundColor1D;

    public int m_x;
    public int m_y;
    public Color32 m_foundColorLRTD;




    private void Refresh()
    {

        if (m_source == null)
        {
            return;
        }
        if (m_source.m_nativeArray == null || m_source.m_nativeArray.Length == 0)
        {
            return;
        }
        if (m_source.m_width <= 0 || m_source.m_height <= 0)
        {
            return;
        }

        m_foundColor1D = m_source.m_nativeArray[m_1dIndex];
        int testX = m_x;
        int testY = m_y;
        m_foundColorLRTD = m_source.m_nativeArray[testX + (testY * m_source.m_width)];


        m_blockHeight = (m_source.m_height - m_borderPixelPaddingTop) / m_colorBlockCount;
        m_startBlockTopY = m_borderPixelPaddingTop + (int) (m_blockHeight / 4.0f);

        for (int i = 0; i < m_colorBlockCount; i++)
        {
            int y = m_startBlockTopY + (i * m_blockHeight);
            bool found = false;
            m_source.GetColorAtIndexLRTD2D(m_borderPixelPaddingLeftRight,y, out found, out m_colorLeftTopDown[i]);
            m_source.GetColorAtIndexRLTD2D(m_borderPixelPaddingLeftRight,y, out found, out m_colorRightTopDown[i]);
        }

        
    }
}
