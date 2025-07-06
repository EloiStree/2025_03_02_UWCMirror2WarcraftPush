using System;
using System.Collections.Generic;
using System.Drawing;
using JetBrains.Annotations;
using NUnit.Framework.Internal;
using UnityEditor.Profiling.Memory.Experimental;
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
                if (found) m_fourPixelsBasicWowInfo[i].m_rightTop5_fullXpModulo = playerPartTwo;
                uwcWindowPixelsAccess.GetPixelAtPercentDownRightTopLeft(m_rightMarginPixel, m_cell6, out found, out Color32 playerGroupLife);
                if (found) m_fourPixelsBasicWowInfo[i].m_rightTop6_playerIdPart1 = playerGroupLife;
                uwcWindowPixelsAccess.GetPixelAtPercentDownRightTopLeft(m_rightMarginPixel, m_cell7, out found, out Color32 integerOutput);
                if (found) m_fourPixelsBasicWowInfo[i].m_rightTop7_playerIdPart2 = integerOutput;


                uwcWindowPixelsAccess.GetPixelAtPercentDownLeftTopRight(m_leftMarginPixel, m_cell0, out found, out Color32 leftTop0);
                if (found) m_fourPixelsBasicWowInfo[i].m_leftTop0_custom24Bits = leftTop0;
                uwcWindowPixelsAccess.GetPixelAtPercentDownLeftTopRight(m_leftMarginPixel, m_cell1, out found, out Color32 leftTop1);
                if (found) m_fourPixelsBasicWowInfo[i].m_leftTop1_custom24Bits = leftTop1;
                uwcWindowPixelsAccess.GetPixelAtPercentDownLeftTopRight(m_leftMarginPixel, m_cell2, out found, out Color32 leftTop2);
                if (found) m_fourPixelsBasicWowInfo[i].m_leftTop2_targetState24Bits = leftTop2;
                uwcWindowPixelsAccess.GetPixelAtPercentDownLeftTopRight(m_leftMarginPixel, m_cell3, out found, out Color32 leftTop3);
                if (found) m_fourPixelsBasicWowInfo[i].m_leftTop3_playerState24Bits = leftTop3;
                uwcWindowPixelsAccess.GetPixelAtPercentDownLeftTopRight(m_leftMarginPixel, m_cell4, out found, out Color32 leftTop4);
                if (found) m_fourPixelsBasicWowInfo[i].m_leftTop4_gatherObjectId = leftTop4;
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
        public Color32 m_rightTop5_fullXpModulo;
        public Color32 m_rightTop6_playerIdPart1;
        public Color32 m_rightTop7_playerIdPart2;

        public Color32 m_leftTop0_custom24Bits;
        public Color32 m_leftTop1_custom24Bits;
        public Color32 m_leftTop2_targetState24Bits;
        public Color32 m_leftTop3_playerState24Bits;
        public Color32 m_leftTop4_gatherObjectId;
        public Color32 m_leftTop5_targetLifeLevelPowerInfo;
        public Color32 m_leftTop6_targetPart1;
        public Color32 m_leftTop7_targetPart2;



        public bool [] m_leftTop0_custom24BitsArray         = new bool[24];
        public bool [] m_leftTop1_custom24BitsArray         = new bool[24];
        public bool [] m_leftTop2_targetState24BitsArray    = new bool[24];
        public bool [] m_leftTop3_playerState24BitsArray    = new bool[24];

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
        public int m_fullXpModulo999999 = 0;
        public float m_targetLifePercent;
        public float m_targetLevel;
        public float m_targetPowerPercent;
        public float m_targetIsCastingOrChanneling;
        public int m_windowHandle;

        public TargetBinaryInfo m_targetBinaryInfo = new TargetBinaryInfo();
        public PlayerBinaryInfo m_playerBinaryInfo = new PlayerBinaryInfo();

        [System.Serializable]
        public class TargetBinaryInfo
        {
            public bool m_hasTarget;
            public bool m_isTargetPlayer;
            public bool m_isTargetEnemy;
            public bool m_isTargetInCombat;
            public bool m_isTargetCasting;
            public bool m_isTargetDeath;
            public bool m_isTargetFullLife;
            public bool m_isTargetWithin10Yards;
            public bool m_isTargetWithin30Yards;
            public bool m_isGlobalCooldownActive;
            public bool m_isTargetHasCorruption;
            public bool m_isTargetHasAgony;
            public bool m_isTargetFocusingPlayer;
        }

        [System.Serializable]
        public class PlayerBinaryInfo
        {
            public bool isCasting;
            public bool isGatheringHerbs;
            public bool isGatheringMining;
            public bool isFishing;
            public bool isOnGround;
            public bool isInCombat;
            public bool isMounted;
            public bool isFalling;
            public bool isFlying;
            public bool isSwimming;
            public bool isSteathing;
            public bool isDeath;
            public bool isUnderPercentBreathing98;
            public bool isUnderPercentBreathing20;
            public bool isUnderPercentFatigue98;
            public bool isUnderPercentFatigue20;
            public bool hasDiscoveredZoneLastSeconds;
            internal bool isPetAlive;
        }

        public int m_gatherObjectId = 0;

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


            m_fullXpModulo999999 = m_rightTop5_fullXpModulo.r * 10000 +
                                   m_rightTop5_fullXpModulo.g * 100 +
                                   m_rightTop5_fullXpModulo.b *1;

            m_gatherObjectId =  RgbTo24BitInt(m_leftTop4_gatherObjectId.r,
                m_leftTop4_gatherObjectId.g,
                m_leftTop4_gatherObjectId.b);


            m_leftTop0_custom24BitsArray= Get24BitsFromColor(m_leftTop0_custom24Bits);
            m_leftTop1_custom24BitsArray = Get24BitsFromColor(m_leftTop1_custom24Bits);
            m_leftTop2_targetState24BitsArray = Get24BitsFromColor(m_leftTop2_targetState24Bits);
            m_leftTop3_playerState24BitsArray = Get24BitsFromColor(m_leftTop3_playerState24Bits);
            
            Turn24BitTargetColorToInfo(m_leftTop2_targetState24BitsArray);
            Turn24BitPlayerColorToInfo(m_leftTop3_playerState24BitsArray);

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

        private void Turn24BitTargetColorToInfo(bool[] array24)
        {
            m_targetBinaryInfo.m_hasTarget = array24[0];
            m_targetBinaryInfo.m_isTargetPlayer = array24[1];
            m_targetBinaryInfo.m_isTargetEnemy = array24[2];
            m_targetBinaryInfo.m_isTargetInCombat = array24[3];
            m_targetBinaryInfo.m_isTargetCasting = array24[4];
            m_targetBinaryInfo.m_isTargetDeath = array24[5];
            m_targetBinaryInfo.m_isTargetFullLife = array24[6];
            m_targetBinaryInfo.m_isTargetWithin10Yards = array24[7];
            m_targetBinaryInfo.m_isTargetWithin30Yards = array24[8];
            m_targetBinaryInfo.m_isTargetFocusingPlayer = array24[9]; 


            m_targetBinaryInfo.m_isGlobalCooldownActive = array24[16];
            m_targetBinaryInfo.m_isTargetHasCorruption = array24[22];
            m_targetBinaryInfo.m_isTargetHasAgony = array24[23];
            
            //    HasTarget() and 1 or 0,                   --1 DONT CHANGE
            //IsTargetPlayer() and 1 or 0,   --2 DONT CHANGE
            //IsTargetEnemy() and 1 or 0,   --3  DONT CHANGE
            //IsTargetInCombat() and 1 or 0,   --4  DONT CHANGE
            //IsTargetCasting() and 1 or 0,   --5 DONT CHANGE
            //IsTargetDeath() and 1 or 0,   --6 DONT CHANGE
            //--Green--
            //IsTargetFullLife() and 1 or 0,   --7 DONT CHANGE
            //IsTargetWithin10Yards() and 1 or 0,   --8 DONT CHANGE
            //IsTargetWithin30Yards() and 1 or 0,   --9 DONT CHANGE
            //0,   --10
            //0,   --11
            //0,   --12
            //0,   --13
            //0,   --14
            //0,   --15
            //0,   --16
            //-- Blue--
            //IsGlobalCooldownActive() and 1 or 0 ,   --17 DONT CHANGE
            //0,   --18
            //0,   --19
            //0,   --20
            //0,   --21
            //0,   --22
            //IsTargetHasCorruption() and 1 or 0,   --23 DONT CHANGE
            //IsTargetHasAgony() and 1 or 0,     --24 DONT CHANGE
        }

        private void Turn24BitPlayerColorToInfo(bool[] array24)
        {
            m_playerBinaryInfo.isCasting = array24[0];
            m_playerBinaryInfo.isGatheringHerbs = array24[1];
            m_playerBinaryInfo.isGatheringMining = array24[2];
            m_playerBinaryInfo.isFishing = array24[3];
            m_playerBinaryInfo.isOnGround = array24[4];
            m_playerBinaryInfo.isInCombat = array24[5];
            m_playerBinaryInfo.isMounted = array24[6];
            m_playerBinaryInfo.isFlying = array24[7];
            m_playerBinaryInfo.isFalling = array24[8];
            m_playerBinaryInfo.isSwimming = array24[9];
            m_playerBinaryInfo.isSteathing = array24[10];
            m_playerBinaryInfo.isDeath = array24[11];
            m_playerBinaryInfo.isUnderPercentBreathing98 = array24[12];
            m_playerBinaryInfo.isUnderPercentBreathing20 = array24[13];
            m_playerBinaryInfo.isUnderPercentFatigue98 = array24[14];
            m_playerBinaryInfo.isUnderPercentFatigue20 = array24[15];
            m_playerBinaryInfo.hasDiscoveredZoneLastSeconds = array24[16];
            m_playerBinaryInfo.isPetAlive = array24[17];


            //    IsCasting() and 1 or 0,                 --1 DONT CHANGE
            //IsGatheringHerbs() and 1 or 0,          --2 DONT CHANGE
            //IsGatheringMining() and 1 or 0,         --3 DONT CHANGE
            //IsFishing() and 1 or 0,                 --4 DONT CHANGE
            //IsOnGround() and 1 or 0,                --5 DONT CHANGE
            //IsPlayerInCombat() and 1 or 0,          --6 DONT CHANGE
            //IsPlayerMounted() and 1 or 0,           --7 DONT CHANGE
            //IsPlayerFlying() and 1 or 0,            --8 DONT CHANGE
            //IsPlayerFalling() and 1 or 0,           --9 DONT CHANGE
            //IsPlayerSwimming() and 1 or 0,          --10 DONT CHANGE
            //IsPlayerSteathing() and 1 or 0,         --11 DONT CHANGE
            //IsPlayerDeath() and 1 or 0,             --12 DONT CHANGE
            //IsUnderPercentBreathing(98) and 1 or 0,     --13 DONT CHANGE
            //IsUnderPercentBreathing(20) and 1 or 0,     --14 DONT CHANGE
            //IsUnderPercentFatigue(98) and 1 or 0,      --15 DONT CHANGE
            //IsUnderPercentFatigue(20) and 1 or 0,      --16 DONT CHANGE
            //HasDiscoveredZoneLastSeconds(1.5)  and 1 or 0, --17 DONT CHANGE
            //0 , --18
            //0 , --19
            //0 , --20
            //0 , --21
            //0 , --22
            //0 , --23
            //0-- 24
        }

        private bool[] Get24BitsFromColor(Color32 color)
        {

            //LUA CODE THAT CREATE THE COLOR RGB
            //  
            /*
             for i = 1, 24 do
        local bitValue = bitsArrayOf24MaxLenght[i] or 0
        if bitValue == 1 or bitValue == true then
            if i == 1 then red = red + 128 end
            if i == 2 then red = red + 64 end
            if i == 3 then red = red + 32 end
            if i == 4 then red = red + 16 end
            if i == 5 then red = red + 8 end
            if i == 6 then red = red + 4 end
            if i == 7 then red = red + 2 end
            if i == 8 then red = red + 1 end

            if i == 9 then green = green + 128 end
            if i == 10 then green = green + 64 end
            if i == 11 then green = green + 32 end
            if i == 12 then green = green + 16 end
            if i == 13 then green = green + 8 end
            if i == 14 then green = green + 4 end
            if i == 15 then green = green + 2 end
            if i == 16 then green = green + 1 end

            if i == 17 then blue = blue + 128 end
            if i == 18 then blue = blue + 64 end
            if i == 19 then blue = blue + 32 end
            if i == 20 then blue = blue + 16 end
            if i == 21 then blue = blue + 8 end
            if i == 22 then blue = blue + 4 end
            if i == 23 then blue = blue + 2 end
            if i == 24 then blue = blue + 1 end
        end
    end
             */

            bool[] bits = new bool[24];
            byte red = color.r;
            byte green = color.g;
            byte blue = color.b;

            for (int i = 0; i < 24; i++)
            {
                bits[i] = false; // Initialize all bits to false
                switch (i) { 
                    case 0:bits[i] = IsBitLeftToRightTrue(red, 0); break;
                    case 1: bits[i] = IsBitLeftToRightTrue(red, 1); break;
                    case 2: bits[i] = IsBitLeftToRightTrue(red, 2); break;
                    case 3: bits[i] = IsBitLeftToRightTrue(red, 3); break;
                    case 4: bits[i] = IsBitLeftToRightTrue(red, 4); break;
                    case 5: bits[i] = IsBitLeftToRightTrue(red, 5); break;
                    case 6: bits[i] = IsBitLeftToRightTrue(red, 6); break;
                    case 7: bits[i] = IsBitLeftToRightTrue(red, 7); break;
                    case 8: bits[i] = IsBitLeftToRightTrue(green, 0); break;
                    case 9: bits[i] = IsBitLeftToRightTrue(green, 1); break;
                    case 10: bits[i] = IsBitLeftToRightTrue(green, 2); break;
                    case 11: bits[i] = IsBitLeftToRightTrue(green, 3); break;
                    case 12: bits[i] = IsBitLeftToRightTrue(green, 4); break;
                    case 13: bits[i] = IsBitLeftToRightTrue(green, 5); break;
                    case 14: bits[i] = IsBitLeftToRightTrue(green, 6); break;
                    case 15: bits[i] = IsBitLeftToRightTrue(green, 7); break;
                    case 16: bits[i] = IsBitLeftToRightTrue(blue, 0); break;
                    case 17: bits[i] = IsBitLeftToRightTrue(blue, 1); break;
                    case 18: bits[i] = IsBitLeftToRightTrue(blue, 2); break;
                    case 19: bits[i] = IsBitLeftToRightTrue(blue, 3); break;
                    case 20: bits[i] = IsBitLeftToRightTrue(blue, 4); break;
                    case 21: bits[i] = IsBitLeftToRightTrue(blue, 5); break;
                    case 22: bits[i] = IsBitLeftToRightTrue(blue, 6); break;
                    case 23: bits[i] = IsBitLeftToRightTrue(blue, 7); break;


                }
            }


















































            return bits;
        }

        private bool IsBitLeftToRightTrue(byte colorAsByte, int indxex)
        {
            return (colorAsByte & (1 << (7 - indxex))) != 0; // Check if the bit at position v is set
        }


        private int RgbTo24BitInt(byte r, byte g, byte b)
        {
            return (r << 16) | (g << 8) | b;
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
