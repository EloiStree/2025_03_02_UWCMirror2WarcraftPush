using System.Collections.Generic;
using System.Net;
using Unity.Plastic.Newtonsoft.Json.Bson;
using UnityEngine;
using UnityEngine.Events;

public class FetchBasicWowPixelCoordinate : MonoBehaviour
{


    public SleepyExctractMono m_pixelAccess;

    public Vector2 m_topRight = new Vector2(0, 15f / 16);
    public Vector2 m_topRightMiddle = new Vector2(0, 13f / 16);
    public Vector2 m_downRightMiddle = new Vector2(0, 11f / 16);
    public Vector2 m_downRight = new Vector2(0, 9f / 16);
    public Vector2 m_playerPartOne = new Vector2(0, 7f / 16);
    public Vector2 m_playerPartTwo = new Vector2(0, 5f / 16);
    public Vector2 m_playerGroupLife = new Vector2(0, 3f / 16);
    public Vector2 m_integerOutput = new Vector2(0, 1f / 16);


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

            if (found && uwcWindowPixelsAccess!=null && uwcWindowPixelsAccess.m_window != null)
            {
                uwcWindowPixelsAccess.GetPixelAtPercentDownRightTopLeft((int)m_topRight.x, m_topRight.y, out found, out Color32 topRight);
                if (found) m_fourPixelsBasicWowInfo[i].m_topRight = topRight;
                uwcWindowPixelsAccess.GetPixelAtPercentDownRightTopLeft((int)m_topRightMiddle.x, m_topRightMiddle.y, out found, out Color32 topRightMiddle);
                if (found) m_fourPixelsBasicWowInfo[i].m_topRightMiddle = topRightMiddle;
                uwcWindowPixelsAccess.GetPixelAtPercentDownRightTopLeft((int)m_downRightMiddle.x, m_downRightMiddle.y, out found, out Color32 downRightMiddle);
                if (found) m_fourPixelsBasicWowInfo[i].m_downRightMiddle = downRightMiddle;
                uwcWindowPixelsAccess.GetPixelAtPercentDownRightTopLeft((int)m_downRight.x, m_downRight.y, out found, out Color32 downRight);
                if (found) m_fourPixelsBasicWowInfo[i].m_downRight = downRight;

                uwcWindowPixelsAccess.GetPixelAtPercentDownRightTopLeft((int)m_playerPartOne.x, m_playerPartOne.y, out found, out Color32 playerPartOne);
                if (found) m_fourPixelsBasicWowInfo[i].m_playerPartOne = playerPartOne;

                uwcWindowPixelsAccess.GetPixelAtPercentDownRightTopLeft((int)m_playerPartTwo.x, m_playerPartTwo.y, out found, out Color32 playerPartTwo);
                if (found) m_fourPixelsBasicWowInfo[i].m_playerPartTwo = playerPartTwo;

                uwcWindowPixelsAccess.GetPixelAtPercentDownRightTopLeft((int)m_playerGroupLife.x, m_playerGroupLife.y, out found, out Color32 playerGroupLife);
                if (found) m_fourPixelsBasicWowInfo[i].m_playerGroupLife = playerGroupLife;

                uwcWindowPixelsAccess.GetPixelAtPercentDownRightTopLeft((int)m_integerOutput.x, m_integerOutput.y, out found, out Color32 integerOutput);
                if (found) m_fourPixelsBasicWowInfo[i].m_integerOutput = integerOutput;

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
        public Color32 m_playerGroupLife;
        public Color32 m_integerOutput;

        public Material m_material;

        public float m_rMapX;
        public float m_gMapY;
        public float m_bRotationAngle;
        public float m_worldPositionX = 0;
        public float m_worldPositionY = 0;
        public float m_rPlayerLevel = 0;
        public float m_gPercentLife = 0;
        public float m_bPercentXp = 0;
        public string m_playerIdFocus = "FFFF-FFFFFFFF";
        public float m_playerLife = 0;
        public float m_partyPlayerLife1;
        public float m_partyPlayerLife2;
        public float m_partyPlayerLife3;
        public float m_partyPlayerLife4;
        public float m_petLife;
        public int m_last24bitUnsignedInteger = 0;
        public UnityEvent<int> m_onLast24bitUnsignedInteger = new UnityEvent<int>();

        public void UpdateData()
        {


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



            // Turn color life to ffffff string rgb
            string life = $"{FF_From_255(m_playerGroupLife.r)}" +
                $"{FF_From_255(m_playerGroupLife.g)}" +
                $"{FF_From_255(m_playerGroupLife.b)}";

            m_playerLife = From_F_To_percent01(life[0]);
            m_partyPlayerLife1 = From_F_To_percent01(life[1]);
            m_partyPlayerLife2 = From_F_To_percent01(life[2]);
            m_partyPlayerLife3 = From_F_To_percent01(life[3]);
            m_partyPlayerLife4 = From_F_To_percent01(life[4]);
            m_petLife = From_F_To_percent01(life[5]);


            int current32BitInteger = ((int)(m_integerOutput.b) << 16) +
                                      ((int)(m_integerOutput.g) << 8) +
                                      ((int)(m_integerOutput.r));
            // max value of 24 bit unsigned integer is 16777215
            bool changed = m_last24bitUnsignedInteger != current32BitInteger;

            m_last24bitUnsignedInteger = current32BitInteger;
            if (changed)
            {
                m_onLast24bitUnsignedInteger.Invoke(m_last24bitUnsignedInteger);
            }
        }

        public string FF_From_255(int value)
        {
            string hex = value.ToString("X2");
            return hex;
        }
        public float From_F_To_percent01(char valueF)
        {
            switch (valueF)
            {
                case 'F':
                case 'f':
                    return 15 / 15f;

                case 'E':
                case 'e':
                    return 14 / 15f;
                case 'D':
                case 'd':
                    return 13 / 15f;
                case 'C':
                case 'c':
                    return 12 / 15f;
                case 'B':
                case 'b':
                    return 11 / 15f;
                case 'A':
                case 'a':
                    return 10 / 15f;

                case '9': return 9 / 15f; // Corrected from 10 / 16f
                case '8': return 8 / 15f;
                case '7': return 7 / 15f;
                case '6': return 6 / 15f;
                case '5': return 5 / 15f;
                case '4': return 4 / 15f;
                case '3': return 3 / 15f; // Corrected from 4 / 16f
                case '2': return 2 / 15f; // Corrected from 3 / 15f
                case '1': return 1 / 15f; // Corrected from 2 / 15f
                case '0': return 0 / 15f; // Corrected from 1 / 15f

                default:
                    return 0f; // Added default return to ensure method always returns a value
            }

        }
    }
}
