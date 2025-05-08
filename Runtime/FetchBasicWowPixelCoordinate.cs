using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Events;

public class FetchBasicWowPixelCoordinate : MonoBehaviour
{


    public UWCMono_FetchObservedUwcWindowInScene m_pixelAccess;

    public float m_cell0 =15f / 16;
    public float m_cell1 =13f / 16;
    public float m_cell2 =11f / 16;
    public float m_cell3 =9f / 16;
    public float m_cell4 =7f / 16;
    public float m_cell5 =5f / 16;
    public float m_cell6 =3f / 16;
    public float m_cell7 =1f / 16;
    public int m_leftMarginPixel=8;
    public int m_rightMarginPixel=0;


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
            m_fourPixelsBasicWowInfo = new List<LeftRightCellPixelInfo>();
            for (int i = 0; i < count; i++)
            {
                m_fourPixelsBasicWowInfo.Add(new LeftRightCellPixelInfo());
            }
        }
        for (int i = 0; i < count; i++)
        {
            m_pixelAccess.GetUwcWindowPixelsAccess(i, out bool found, out UwcWindowPixelsAccess uwcWindowPixelsAccess);

            if (found && uwcWindowPixelsAccess!=null && uwcWindowPixelsAccess.m_window != null)
            {
                uwcWindowPixelsAccess.GetHWnd32(out int hwnd);
                m_fourPixelsBasicWowInfo[i].m_windowHandle = hwnd;

                uwcWindowPixelsAccess.GetPixelAtPercentDownRightTopLeft(m_rightMarginPixel, m_cell0, out found, out Color32 topRight);
                if (found) m_fourPixelsBasicWowInfo[i].m_rightTop0_mapXYAngle = topRight;
                uwcWindowPixelsAccess.GetPixelAtPercentDownRightTopLeft(m_rightMarginPixel, m_cell1, out found, out Color32 topRightMiddle);
                if (found) m_fourPixelsBasicWowInfo[i].m_rightTop1_worldPositionX = topRightMiddle;
                uwcWindowPixelsAccess.GetPixelAtPercentDownRightTopLeft(m_rightMarginPixel, m_cell2, out found, out Color32 downRightMiddle);
                if (found) m_fourPixelsBasicWowInfo[i].m_rightTop2_worldPositionY = downRightMiddle;
                uwcWindowPixelsAccess.GetPixelAtPercentDownRightTopLeft(m_rightMarginPixel, m_cell3, out found, out Color32 downRight);
                if (found) m_fourPixelsBasicWowInfo[i].m_rightTop3_lifeXp = downRight;
                uwcWindowPixelsAccess.GetPixelAtPercentDownRightTopLeft(m_rightMarginPixel, m_cell4, out found, out Color32 playerPartOne);
                if (found) m_fourPixelsBasicWowInfo[i].m_rightTop4_partyLife = playerPartOne;
                uwcWindowPixelsAccess.GetPixelAtPercentDownRightTopLeft(m_rightMarginPixel, m_cell5, out found, out Color32 playerPartTwo);
                if (found) m_fourPixelsBasicWowInfo[i].m_rightTop5_integerOut = playerPartTwo;
                uwcWindowPixelsAccess.GetPixelAtPercentDownRightTopLeft(m_rightMarginPixel, m_cell6, out found, out Color32 playerGroupLife);
                if (found) m_fourPixelsBasicWowInfo[i].m_rightTop6_playerIdPart1 = playerGroupLife;
                uwcWindowPixelsAccess.GetPixelAtPercentDownRightTopLeft(m_rightMarginPixel, m_cell7, out found, out Color32 integerOutput);
                if (found) m_fourPixelsBasicWowInfo[i].m_rightTop7_playerIdPart2 = integerOutput;


                uwcWindowPixelsAccess.GetPixelAtPercentDownLeftTopRight(m_leftMarginPixel, m_cell0, out found, out Color32 leftTop0);
                if (found) m_fourPixelsBasicWowInfo[i].m_leftTop0 = leftTop0;
                uwcWindowPixelsAccess.GetPixelAtPercentDownLeftTopRight(m_leftMarginPixel, m_cell1, out found, out Color32 leftTop1);
                if (found) m_fourPixelsBasicWowInfo[i].m_leftTop1 = leftTop1;
                uwcWindowPixelsAccess.GetPixelAtPercentDownLeftTopRight(m_leftMarginPixel, m_cell2, out found, out Color32 leftTop2);
                if (found) m_fourPixelsBasicWowInfo[i].m_leftTop2 = leftTop2;
                uwcWindowPixelsAccess.GetPixelAtPercentDownLeftTopRight(m_leftMarginPixel, m_cell3, out found, out Color32 leftTop3);
                if (found) m_fourPixelsBasicWowInfo[i].m_leftTop3 = leftTop3;
                uwcWindowPixelsAccess.GetPixelAtPercentDownLeftTopRight(m_leftMarginPixel, m_cell4, out found, out Color32 leftTop4);
                if (found) m_fourPixelsBasicWowInfo[i].m_leftTop4 = leftTop4;
                uwcWindowPixelsAccess.GetPixelAtPercentDownLeftTopRight(m_leftMarginPixel, m_cell5, out found, out Color32 leftTop5);
                if (found) m_fourPixelsBasicWowInfo[i].m_leftTop5_targetLifeLevelPowerInfo = leftTop5;
                uwcWindowPixelsAccess.GetPixelAtPercentDownLeftTopRight(m_leftMarginPixel, m_cell6, out found, out Color32 leftTop6);
                if (found) m_fourPixelsBasicWowInfo[i].m_leftTop6_targetPart1 = leftTop6;
                uwcWindowPixelsAccess.GetPixelAtPercentDownLeftTopRight(m_leftMarginPixel, m_cell7, out found, out Color32 leftTop7);
                if (found) m_fourPixelsBasicWowInfo[i].m_leftTop7_targetPart2 = leftTop7;




                m_fourPixelsBasicWowInfo[i].m_material = uwcWindowPixelsAccess.m_material;
                m_fourPixelsBasicWowInfo[i].UpdateData();
            }
        }
    }

    public List<LeftRightCellPixelInfo> m_fourPixelsBasicWowInfo = new List<LeftRightCellPixelInfo>();

    [System.Serializable]
    public class LeftRightCellPixelInfo
    {
        public Color32 m_rightTop0_mapXYAngle;
        public Color32 m_rightTop1_worldPositionX;
        public Color32 m_rightTop2_worldPositionY;
        public Color32 m_rightTop3_lifeXp;
        public Color32 m_rightTop4_partyLife;
        public Color32 m_rightTop5_integerOut;
        public Color32 m_rightTop6_playerIdPart1;
        public Color32 m_rightTop7_playerIdPart2;

        public Color32 m_leftTop0;
        public Color32 m_leftTop1;
        public Color32 m_leftTop2;
        public Color32 m_leftTop3;
        public Color32 m_leftTop4;
        public Color32 m_leftTop5_targetLifeLevelPowerInfo;
        public Color32 m_leftTop6_targetPart1;
        public Color32 m_leftTop7_targetPart2;


        public Material m_material;

        public float m_rMapX;
        public float m_gMapY;
        public float m_bRotationAngle;
        public float m_worldPositionX = 0;
        public float m_worldPositionY = 0;
        public float m_rPlayerLevel = 0;
        public float m_gPercentLife = 0;
        public float m_bPercentXp = 0;
        public string m_playerId = "FFFF-FFFFFFFF";
        public string m_playerIdFocus = "FFFF-FFFFFFFF";
        public float m_playerLife = 0;
        public float m_partyPlayerLife1;
        public float m_partyPlayerLife2;
        public float m_partyPlayerLife3;
        public float m_partyPlayerLife4;
        public float m_petLife;

        public float m_targetLifePercent;
        public float m_targetLevel;
        public float m_targetPowerPercent;
        public float m_targetIsCastingOrChanneling;
        public int m_windowHandle;

        public int m_last24bitUnsignedInteger = 0;
        public UnityEvent<int> m_onLast24bitUnsignedInteger = new UnityEvent<int>();

        public void UpdateData()
        {


            m_rMapX = m_rightTop0_mapXYAngle.r / 255f * 100f;
            m_gMapY = m_rightTop0_mapXYAngle.g / 255f * 100f;
            m_bRotationAngle = m_rightTop0_mapXYAngle.b / 255f * 360f;

            m_gPercentLife = m_rightTop3_lifeXp.r / 255f;
            m_rPlayerLevel = m_rightTop3_lifeXp.g;
            m_bPercentXp = m_rightTop3_lifeXp.b / 255f;


            int rx;
            int gx;
            int bx;
            int ry;
            int gy;
            int by;
            Color32 x32 = m_rightTop1_worldPositionX;
            Color32 y32 = m_rightTop2_worldPositionY;
            bool isNegativeX = x32.r >= 100;
            bool isNegativeY = y32.r >= 100;
            x32.r = (byte)(x32.r % 100);
            y32.r = (byte)(y32.r % 100);
            bx = (int)(x32.b);
            by = (int)(y32.b);
            gx = ((int)(x32.g)) * 100;
            gy = ((int)(y32.g)) * 100;
            rx = ((int)(x32.r)) * 10000;
            ry = ((int)(y32.r)) * 10000;
            m_worldPositionX = (rx + gx + bx) * (isNegativeX ? -1 : 1);
            m_worldPositionY = (ry + gy + by) * (isNegativeY ? -1 : 1);


            ConvertDoubleColorToId(m_rightTop6_playerIdPart1, m_rightTop7_playerIdPart2, out m_playerId);
            ConvertDoubleColorToId(m_leftTop6_targetPart1, m_leftTop7_targetPart2, out m_playerIdFocus);



            m_targetLifePercent = m_leftTop5_targetLifeLevelPowerInfo.r / 255f;
            m_targetLevel = m_leftTop5_targetLifeLevelPowerInfo.g ;
            m_targetPowerPercent = m_leftTop5_targetLifeLevelPowerInfo.b / 255f;

            // Turn color life to ffffff string rgb
            string life = $"{FF_From_255(m_rightTop4_partyLife.r)}" +
                $"{FF_From_255(m_rightTop4_partyLife.g)}" +
                $"{FF_From_255(m_rightTop4_partyLife.b)}";

            m_playerLife = From_F_To_percent01(life[0]);
            m_partyPlayerLife1 = From_F_To_percent01(life[1]);
            m_partyPlayerLife2 = From_F_To_percent01(life[2]);
            m_partyPlayerLife3 = From_F_To_percent01(life[3]);
            m_partyPlayerLife4 = From_F_To_percent01(life[4]);
            m_petLife = From_F_To_percent01(life[5]);


            int current32BitInteger = ((int)(m_rightTop7_playerIdPart2.b) << 16) +
                                      ((int)(m_rightTop7_playerIdPart2.g) << 8) +
                                      ((int)(m_rightTop7_playerIdPart2.r));
            // max value of 24 bit unsigned integer is 16777215
            bool changed = m_last24bitUnsignedInteger != current32BitInteger;

            m_last24bitUnsignedInteger = current32BitInteger;
            if (changed)
            {
                m_onLast24bitUnsignedInteger.Invoke(m_last24bitUnsignedInteger);
            }
        }

        private void ConvertDoubleColorToId(Color32 partOne, Color32 partTwo, out string playerId)
        {
           
            playerId = $"{FF_From_255(partOne.r)}" +
                $"{FF_From_255(partOne.g)}" +
                $"-{FF_From_255(partOne.b)}" +
                $"{FF_From_255(partTwo.r)}" +
                $"{FF_From_255(partTwo.g)}" +
                $"{FF_From_255(partTwo.b)}";
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
