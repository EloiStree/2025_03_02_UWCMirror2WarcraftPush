
using System.Collections.Generic;
using Eloi.WatchAndDate;
using UnityEngine;
using UnityEngine.Events;

public class UWCMono_LookForSquareColorInRenderTexture : MonoBehaviour
{
    public NativeArray2DColor32WH m_squareColor;
    public Color32 m_minColorToLookFor;
    public Color32 m_maxColorToLookFor;

    public Vector2Int m_downLeftColorPosition;
    public Vector2Int m_upRightColorPosition;
    public int m_widthSquare;
    public int m_heightSquare;

    public List<Vector2Int> m_foundPositions = new List<Vector2Int>();
    public UnityEvent<RectInt> m_onSquareFound;
    public int m_topBorder = 0;
    public int m_leftBorder = 0;
    public int m_borderSize= 0;

    public WatchAndDateTimeActionResult m_timeToProcess = new WatchAndDateTimeActionResult();

    public RectInt foundSquareWithBorder;
    public RectInt foundSquareWithoutBorder;


    public void SetWithNativeArray(NativeArray2DColor32WH squareColor)
    {
        m_squareColor = squareColor;
      

    }

    [ContextMenu("Refresh")]
    public void Refresh()
    {
        m_timeToProcess.StartCounting();
        m_foundPositions.Clear();
        for (int x = 0; x < m_squareColor.m_width; x++)
        {
            for (int y = 0; y < m_squareColor.m_height; y++)
            {
                m_squareColor.GetColorAtIndexLRDT2D(x, y, out bool found, out Color32 color);
                if (color.r >= m_minColorToLookFor.r && color.g >= m_minColorToLookFor.g && color.b >= m_minColorToLookFor.b &&
                    color.r <= m_maxColorToLookFor.r && color.g <= m_maxColorToLookFor.g && color.b <= m_maxColorToLookFor.b)
                {
                    m_foundPositions.Add(new Vector2Int(x, y));
                }
            }
        }

        if (m_foundPositions.Count > 0)
        {
            m_downLeftColorPosition = m_foundPositions[0];
            m_upRightColorPosition = m_foundPositions[0];
            foreach (var pos in m_foundPositions)
            {
                if (pos.x < m_downLeftColorPosition.x) m_downLeftColorPosition.x = pos.x;
                if (pos.y < m_downLeftColorPosition.y) m_downLeftColorPosition.y = pos.y;
                if (pos.x > m_upRightColorPosition.x) m_upRightColorPosition.x = pos.x;
                if (pos.y > m_upRightColorPosition.y) m_upRightColorPosition.y = pos.y;
            }
            m_widthSquare = m_upRightColorPosition.x - m_downLeftColorPosition.x + 1;
            m_heightSquare = m_upRightColorPosition.y - m_downLeftColorPosition.y + 1;

            m_leftBorder = 0;
            for (int y = m_downLeftColorPosition.y; y <= m_upRightColorPosition.y; y++)
            {
                // check fron left to right for the first pixel that is not a wanted color
                for (int x = m_downLeftColorPosition.x; x <= m_upRightColorPosition.x; x++)
                {
                    m_squareColor.GetColorAtIndexLRDT2D(x, y, out bool found, out Color32 color);
                    bool isColorInRange = color.r >= m_minColorToLookFor.r && color.g >= m_minColorToLookFor.g && color.b >= m_minColorToLookFor.b &&
                        color.r <= m_maxColorToLookFor.r && color.g <= m_maxColorToLookFor.g && color.b <= m_maxColorToLookFor.b;
                    if (!isColorInRange)
                    {
                        m_leftBorder = x - m_downLeftColorPosition.x;
                        break;
                    }
                }
            }
            m_borderSize = m_leftBorder;
             foundSquareWithBorder       = new RectInt(m_downLeftColorPosition.x, m_downLeftColorPosition.y, m_widthSquare, m_heightSquare);
             foundSquareWithoutBorder = new RectInt(
                m_downLeftColorPosition.x + m_borderSize,
                m_downLeftColorPosition.y + m_borderSize,
                m_widthSquare - (m_borderSize * 2), 
                m_heightSquare - (m_borderSize * 2));

            m_onSquareFound.Invoke(foundSquareWithoutBorder);
        }
        m_timeToProcess.StopCounting();
    }

}
