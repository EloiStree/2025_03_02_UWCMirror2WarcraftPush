using System.Collections.Generic;
using Unity.Plastic.Newtonsoft.Json.Bson;
using UnityEngine;

public class FetchBasicWowPixelCoordinate : MonoBehaviour {


    public SleepyExctractMono m_pixelAccess;

    public Vector2 m_topRight = new Vector2(0, 15f/ 16);
    public Vector2 m_topRightMiddle = new Vector2(0, 13f/ 16);
    public Vector2 m_downRightMiddle = new Vector2(0,11f / 16);
    public Vector2 m_downRight = new Vector2(0, 9f/ 16);
    public Vector2 m_playerPartOne = new Vector2(0, 7f/ 16);
    public Vector2 m_playerPartTwo = new Vector2(0, 5f / 16);
    public Vector2 m_undefinedYet = new Vector2(0, 3f / 16);
    public Vector2 m_undefinedYet2 = new Vector2(0, 1f / 16);


    public float m_timeBetweenFetch = 0.1f;

    public void Awake()
    {
     InvokeRepeating(nameof(Fetch), m_timeBetweenFetch, m_timeBetweenFetch);
    }

    private void Fetch()
    {
        if (m_pixelAccess == null) return;
        m_pixelAccess.GetWindowCount(out int count);
        if (m_fourPixelsBasicWowInfo.Count != count)
        {
            m_fourPixelsBasicWowInfo = new List<FourPixelsBasicWowInfo>();
            for (int i = 0; i < count; i++)
            {
                m_fourPixelsBasicWowInfo.Add(new FourPixelsBasicWowInfo());
            }
        }
        for (int i = 0; i < count; i++)
        {
            m_pixelAccess.GetUwcWindowPixelsAccess(i, out bool found, out UwcWindowPixelsAccess uwcWindowPixelsAccess);
            if (found)
            {
                uwcWindowPixelsAccess.GetPixelAtPercentDownRightTopLeft((int)m_topRight.x, m_topRight.y , out found, out Color32 topRight);
                if (found) m_fourPixelsBasicWowInfo[i].m_topRight = topRight;
                uwcWindowPixelsAccess.GetPixelAtPercentDownRightTopLeft((int) m_topRightMiddle.x, m_topRightMiddle.y, out found, out Color32 topRightMiddle);
                if (found) m_fourPixelsBasicWowInfo[i].m_topRightMiddle = topRightMiddle;
                uwcWindowPixelsAccess.GetPixelAtPercentDownRightTopLeft((int) m_downRightMiddle.x, m_downRightMiddle.y, out found, out Color32 downRightMiddle);
                if (found) m_fourPixelsBasicWowInfo[i].m_downRightMiddle = downRightMiddle;
                uwcWindowPixelsAccess.GetPixelAtPercentDownRightTopLeft((int) m_downRight.x, m_downRight.y, out found, out Color32 downRight);
                if (found) m_fourPixelsBasicWowInfo[i].m_downRight = downRight;

                uwcWindowPixelsAccess.GetPixelAtPercentDownRightTopLeft((int)m_playerPartOne.x, m_playerPartOne.y, out found, out Color32 playerPartOne);
                if (found) m_fourPixelsBasicWowInfo[i].m_playerPartOne = playerPartOne;

                uwcWindowPixelsAccess.GetPixelAtPercentDownRightTopLeft((int)m_playerPartTwo.x, m_playerPartTwo.y, out found, out Color32 playerPartTwo);
                if (found) m_fourPixelsBasicWowInfo[i].m_playerPartTwo = playerPartTwo;

                m_fourPixelsBasicWowInfo[i].m_material = uwcWindowPixelsAccess.m_material;
                m_fourPixelsBasicWowInfo[i].UpdateData();   
            }
        }
    }

    public List<FourPixelsBasicWowInfo> m_fourPixelsBasicWowInfo = new List<FourPixelsBasicWowInfo>();

    [System.Serializable]
    public class FourPixelsBasicWowInfo
    {
        public Color32 m_topRight;
        public Color32 m_topRightMiddle;
        public Color32 m_downRightMiddle;
        public Color32 m_downRight;
        public Color32 m_playerPartOne;
        public Color32 m_playerPartTwo;

        public Material m_material;

        public float m_rMapX;
        public float m_gMapY;
        public float m_bRotationAngle;
        public float m_worldPositionX = 0;
        public float m_worldPositionY = 0;
        public float m_rPlayerLevel =0;
        public float m_gPercentLife =0;
        public float m_bPercentXp = 0;
        public string m_playerIdFocus = "FFFF-FFFFFFFF";

        public void UpdateData() {


            m_rMapX = m_topRight.r / 255f * 100f;
            m_gMapY = m_topRight.g / 255f * 100f;
            m_bRotationAngle = m_topRight.b / 255f * 360f;

            m_rPlayerLevel = m_downRight.r;
            m_gPercentLife = m_downRight.g / 255f;
            m_bPercentXp = m_downRight.b / 255f;
            int m_rx;
           int m_gx;
           int m_bx;

            int m_ry;
            int m_gy;
            int m_by;

            Color32 x32 = m_topRightMiddle;
            Color32 y32 = m_downRightMiddle;
            bool isNegativeX = x32.r >= 100;
            bool isNegativeY = y32.r >= 100;
            x32.r = (byte)(x32.r % 100);
            y32.r = (byte)(y32.r % 100);

            m_bx = (int)(x32.b);
            m_by = (int)(y32.b);

            m_gx = ((int)(x32.g)) * 100;
            m_gy = ((int)(y32.g)) * 100;

            m_rx = ((int)(x32.r)) * 10000;
            m_ry = ((int)(y32.r)) * 10000;

            m_worldPositionX = (m_rx + m_gx + m_bx) * (isNegativeX ? -1 : 1);
            m_worldPositionY = (m_ry + m_gy + m_by) * (isNegativeY ? -1 : 1);

            m_playerIdFocus = $"{FF_From_255(m_playerPartOne.r)}" +
                $"{FF_From_255(m_playerPartOne.g)}" +
                $"-{FF_From_255(m_playerPartOne.b)}" +
                $"{FF_From_255(m_playerPartTwo.r)}" +
                $"{FF_From_255(m_playerPartTwo.g)}" +
                $"{FF_From_255(m_playerPartTwo.b)}";
             
   

        }

        public string FF_From_255(int value)
        {
            string hex = value.ToString("X2");
            return hex;
        }
    }
}
