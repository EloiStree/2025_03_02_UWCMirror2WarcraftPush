using System;
using UnityEngine;


public static  class ColorWowToInfoUtility
{
    public static  void ConvertDoubleColorToId(Color32 partOne, Color32 partTwo, out string playerId)
    {

        playerId = $"{FF_From_255(partOne.r)}" +
            $"{FF_From_255(partOne.g)}" +
            $"-{FF_From_255(partOne.b)}" +
            $"{FF_From_255(partTwo.r)}" +
            $"{FF_From_255(partTwo.g)}" +
            $"{FF_From_255(partTwo.b)}";
    }
    public static string FF_From_255(int value)
    {
        string hex = value.ToString("X2");
        return hex;
    }
    public static float From_F_To_percent01(char valueF)
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

    public static  int RgbTo24BitInt(byte r, byte g, byte b)
    {
        return (r << 16) | (g << 8) | b;
    }
    public static  bool[] Get24BitsFromColor(Color32 color)
    {


        bool[] bits = new bool[24];
        byte red = color.r;
        byte green = color.g;
        byte blue = color.b;

        for (int i = 0; i < 24; i++)
        {
            bits[i] = false; // Initialize all bits to false
            switch (i)
            {
                case 0: bits[i] = IsBitLeftToRightTrue(red, 0); break;
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

    public static  bool IsBitLeftToRightTrue(byte colorAsByte, int index)
    {
        return (colorAsByte & (1 << (7 - index))) != 0; // Check if the bit at position v is set
    }

    public static void GetFloatS100100100FromColor(Color32 x32, out float valueFound)
    {
        int rx;
        int gx;
        int bx;
        bool isNegativeX = x32.r >= 100;
        x32.r = (byte)(x32.r % 100);
        bx = (int)(x32.b);
        gx = ((int)(x32.g)) * 100;
        rx = ((int)(x32.r)) * 10000;
        valueFound = (rx + gx + bx) * (isNegativeX ? -1 : 1);

    }
}


[System.Serializable]
public class ColorWowMapCoordinate {

    public float m_mapX;
    public float m_mapY;
    public float m_rotationAngle360;
    public ColorWowMapCoordinate(float mapX, float mapY, float rotationAngle360)
    {
        m_mapX = mapX;
        m_mapY = mapY;
        m_rotationAngle360 = rotationAngle360;
    }
    public ColorWowMapCoordinate()
    {
        m_mapX = 0;
        m_mapY = 0;
        m_rotationAngle360 = 0;
    }
    public void SetWithColor(Color32 color)
    {
        m_mapX = color.r / 255f * 100f;
        m_mapY = color.g / 255f * 100f;
        m_rotationAngle360 = color.b / 255f * 360f;
    }

    public Vector2 GetMapPositionRaw()
    {
        return new Vector2(m_mapX / 100f, m_mapY / 100f);
    }
}

[System.Serializable]
public class ColorWowPlayer {

    public float percentLife;
    public int playerLevel;
    public float percentXp;

    public ColorWowPlayer(float percentLife, int playerLevel, float percentXp)
    {
        this.percentLife = percentLife;
        this.playerLevel = playerLevel;
        this.percentXp = percentXp;
    }
    public ColorWowPlayer()
    {
        percentLife = 0;
        playerLevel = 0;
        percentXp = 0;
    }
    public void SetWithColor(Color32 color)
    {
        percentLife = color.r / 255f;
        playerLevel = (int)(color.g);
        percentXp = color.b / 255f;
    }

}


[System.Serializable]
public class ColorWowXpModulo999999
{
    public int fullXpModulo999999;
    public ColorWowXpModulo999999(int fullXpModulo999999)
    {
        this.fullXpModulo999999 = fullXpModulo999999;
    }
    public ColorWowXpModulo999999()
    {
        fullXpModulo999999 = 0;
    }
    public void SetWithColor(Color32 color)
    {
        fullXpModulo999999 = color.r * 10000 +
                               color.g * 100 +
                               color.b * 1;
    }
}

[System.Serializable]
public class ColorWowGatherObjectId
{
    public int  gatherObjectId;

    public ColorWowGatherObjectId(int gatherObjectId)
    {
        this.gatherObjectId = gatherObjectId;
    }
    public ColorWowGatherObjectId()
    {
        gatherObjectId = 0;
    }
    public void SetWithColor(Color32 color)
    {
        // Reconstruct the objectId from the color
        // r, g, b are in [0,255] as bytes
        // objectId = r + (g * 256) + (b * 256 * 256)
        gatherObjectId = color.r + (color.g << 8) + (color.b << 16);
    }

}

[System.Serializable]
public class  ColorWowTo24Bits
{
    public bool [] bits24 = new bool [24];

    public void  SetWithColor(Color32 color)
    {
        bits24 = ColorWowToInfoUtility.Get24BitsFromColor(color);
    }
}

[System.Serializable]
public class ColorWowToWorldXY {

    public Vector2 m_worldPositionXY;

    public void SetWithColor(Color32 x32, Color32 y32)
    {
        ColorWowToInfoUtility.GetFloatS100100100FromColor(x32, out float x);
        ColorWowToInfoUtility.GetFloatS100100100FromColor(y32, out float y);

        m_worldPositionXY.y = y;
        m_worldPositionXY.x = x;

    }

    public  Vector3 GetWorldPosition()
    {
        return new Vector3(m_worldPositionXY.x , 0, m_worldPositionXY.y );
    }
}

[System.Serializable]
public class ColorWowDoubleColorToGUID
{
    public string m_guid;
    public void SetWithColor(Color32 color1, Color32 color2)
    {
        ColorWowToInfoUtility.ConvertDoubleColorToId(color1, color2, out m_guid);
    }
}


[System.Serializable]
public class WowColorTargetBinaryInfo
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
    public void SetWithBits(bool[] array24)
    {
        m_hasTarget = array24[0];
        m_isTargetPlayer = array24[1];
        m_isTargetEnemy = array24[2];
        m_isTargetInCombat = array24[3];
        m_isTargetCasting = array24[4];
        m_isTargetDeath = array24[5];
        m_isTargetFullLife = array24[6];
        m_isTargetWithin10Yards = array24[7];
        m_isTargetWithin30Yards = array24[8];
        m_isTargetFocusingPlayer = array24[9];
        m_isGlobalCooldownActive = array24[16];
        m_isTargetHasCorruption = array24[22];
        m_isTargetHasAgony = array24[23];

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
}

[System.Serializable]
public class ColorWowToTeamLife9 { 

    public float m_player1Life;
    public float m_teamLife1;
    public float m_teamLife2;
    public float m_teamLife3;
    public float m_teamLife4;
    public float m_petLife;

    public void SetWithColor(Color32 color)
    {
        string life = $"{ColorWowToInfoUtility.FF_From_255(color.r)}" +
                $"{ColorWowToInfoUtility.FF_From_255(color.g)}" +
                $"{ColorWowToInfoUtility.FF_From_255(color.b)}";

        m_player1Life = ColorWowToInfoUtility.From_F_To_percent01(life[0]);
        m_teamLife1= ColorWowToInfoUtility.From_F_To_percent01(life[1]);
        m_teamLife2= ColorWowToInfoUtility.From_F_To_percent01(life[2]);
        m_teamLife3= ColorWowToInfoUtility.From_F_To_percent01(life[3]);
        m_teamLife4 = ColorWowToInfoUtility.From_F_To_percent01(life[4]);
        m_petLife = ColorWowToInfoUtility.From_F_To_percent01(life[5]);
    }
}

[System.Serializable]
public class WowColorPlayerBinaryInfo
{
    public bool m_isCasting;
    public bool m_isGatheringHerbs;
    public bool m_isGatheringMining;
    public bool m_isFishing;
    public bool m_isOnGround;
    public bool m_isInCombat;
    public bool m_isMounted;
    public bool m_isFalling;
    public bool m_isFlying;
    public bool m_isSwimming;
    public bool m_isSteathing;
    public bool m_isDeath;
    public bool m_isUnderPercentBreathing98;
    public bool m_isUnderPercentBreathing20;
    public bool m_isUnderPercentFatigue98;
    public bool m_isUnderPercentFatigue20;
    public bool m_hasDiscoveredZoneLastSeconds;
    public bool m_isPetAlive;

    public void SetWithBits(bool[] array24)
    {
        m_isCasting = array24[0];
        m_isGatheringHerbs = array24[1];
        m_isGatheringMining = array24[2];
        m_isFishing = array24[3];
        m_isOnGround = array24[4];
        m_isInCombat = array24[5];
        m_isMounted = array24[6];
        m_isFlying = array24[7];
        m_isFalling = array24[8];
        m_isSwimming = array24[9];
        m_isSteathing = array24[10];
        m_isDeath = array24[11];
        m_isUnderPercentBreathing98 = array24[12];
        m_isUnderPercentBreathing20 = array24[13];
        m_isUnderPercentFatigue98 = array24[14];
        m_isUnderPercentFatigue20 = array24[15];
        m_hasDiscoveredZoneLastSeconds = array24[16];
        m_isPetAlive = array24[17];


        //IsCasting() and 1 or 0,                 --1 DONT CHANGE
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


}


[System.Serializable]
public class ColorWowTargetInfo { 

    public float m_targetLifePercent;
    public float m_targetLevel;
    public float m_targetPowerPercent;

    public void SetWithColor(Color32 color)
    {
        m_targetLifePercent = color.r / 255f;
        m_targetLevel = color.g;
        m_targetPowerPercent = color.b / 255f;
    }

}
