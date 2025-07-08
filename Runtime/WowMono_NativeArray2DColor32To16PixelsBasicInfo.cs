using UnityEngine;
using UnityEngine.Events;

public class WowMono_NativeArray2DColor32To16PixelsBasicInfo : MonoBehaviour
{
    public void SetNativeArrayColor32(NativeArray2DColor32WH source) {
        m_source = source;

    }

    public NativeArray2DColor32WH m_source;
    public int m_borderPixelPaddingLeftRight = 2;
    public int m_borderPixelPaddingTop = 37;
    public int m_colorBlockCount = 8;
    public Color32[] m_colorLeftTopDown = new Color32[8];
    public Color32[] m_colorRightTopDown = new Color32[8];
    public Color32[] m_colorClockwise = new Color32[8];

    public UnityEvent<Color32[]> m_onColorUpdateClockwise = new UnityEvent<Color32[]>();


    public int m_blockHeight;
    public int m_startBlockTopY;
    public bool m_useUpdate=true;


    public void Update()
    {
        if(!m_useUpdate) return;

        Refresh();
    }



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



        m_blockHeight = (m_source.m_height - m_borderPixelPaddingTop) / m_colorBlockCount;
        m_startBlockTopY = m_borderPixelPaddingTop + (int)(m_blockHeight / 4.0f);

        for (int i = 0; i < m_colorBlockCount; i++)
        {
            int y = m_startBlockTopY + (i * m_blockHeight);
            bool found = false;
            m_source.GetColorAtIndexLRTD2D(m_borderPixelPaddingLeftRight, y, out found, out m_colorLeftTopDown[i]);
            m_source.GetColorAtIndexRLTD2D(m_borderPixelPaddingLeftRight, y, out found, out m_colorRightTopDown[i]);
        }

        if (m_colorClockwise == null || m_colorClockwise.Length != 16)
        {
            m_colorClockwise = new Color32[16];
        }
        m_colorClockwise[0] = m_colorRightTopDown[0];
        m_colorClockwise[1] = m_colorRightTopDown[1];
        m_colorClockwise[2] = m_colorRightTopDown[2];
        m_colorClockwise[3] = m_colorRightTopDown[3];
        m_colorClockwise[4] = m_colorRightTopDown[4];
        m_colorClockwise[5] = m_colorRightTopDown[5];
        m_colorClockwise[6] = m_colorRightTopDown[6];
        m_colorClockwise[7] = m_colorRightTopDown[7];
        m_colorClockwise[8] = m_colorLeftTopDown[7];
        m_colorClockwise[9] = m_colorLeftTopDown[6];
        m_colorClockwise[10] = m_colorLeftTopDown[5];
        m_colorClockwise[11] = m_colorLeftTopDown[4];
        m_colorClockwise[12] = m_colorLeftTopDown[3];
        m_colorClockwise[13] = m_colorLeftTopDown[2];
        m_colorClockwise[14] = m_colorLeftTopDown[1];
        m_colorClockwise[15] = m_colorLeftTopDown[0];
        m_onColorUpdateClockwise.Invoke(m_colorClockwise);




    }
}
