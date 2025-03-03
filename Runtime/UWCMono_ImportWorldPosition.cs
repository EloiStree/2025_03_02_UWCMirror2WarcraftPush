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
        Color32 x32= x;
        Color32 y32 = y;
        bool isNegativeX = x32.r >= 100;
        bool isNegativeY = y32.r >= 100;
        x32.r = (byte)(x32.r % 100);
        y32.r = (byte)(y32.r % 100);

        m_bx = (int)(x32.b );
        m_by = (int)(y32.b );

        m_gx = ((int)(x32.g )) * 100;
        m_gy = ((int)(y32.g )) * 100;
        
        m_rx = ((int)(x32.r )) * 10000;
        m_ry = ((int)(y32.r )) * 10000;

        m_worldPositionX = (m_rx + m_gx + m_bx) * (isNegativeX ? -1 : 1);
        m_worldPositionY = (m_ry + m_gy + m_by) * (isNegativeY ? -1 : 1);
    }
    /*
     function getWorldPosition(trueXFalseY)
    local isInDonjon = IsInInstance()
    local px, py, pz = 0, 0, 0

    -- Fetch player position if not in an instance
    if not isInDonjon then
        px, py, pz = UnitPosition("player")
    end

    -- Choose the coordinate to encode (x or y)
    local coordinate = trueXFalseY and px or py
    local r, g, b = 0, 0, 0
    -- -347321 
    -- r =34
    -- g = 73
    -- b = 21
    -- if is negative r = r+100
    local isNegative = coordinate < 0
    print ("Coordinate "..coordinate)
    coordinate = math.abs(coordinate)
    local b = math.floor(coordinate) %100 
    local g = math.floor(coordinate / 100.0)%100
    local r = math.floor(coordinate / 10000.0)%100
    if isNegative then
        r = r + 100
    end
    print ("RGB "..r.." "..g.." "..b)
    return r/100.0, g/100.0, b/100.0

end

     */

}
