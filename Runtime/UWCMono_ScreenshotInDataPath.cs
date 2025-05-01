using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Rendering;

public class UWCMono_ScreenshotInDataPath : MonoBehaviour
{
    public Texture2D m_currentTexture;
    public void PushIn(Texture2D image)
    {
        m_currentTexture = image;
    }

    [ContextMenu("Screenshot")]
    public void ScreenShot()
    {
        byte[] bytes = m_currentTexture.EncodeToPNG();
        string dataPath = Application.dataPath;
        string dataPathPlusDate = dataPath + "/Screenshots/" + System.DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss_ffff") + ".png";
        string dataPathDirectory = dataPath + "/Screenshots/";
        if (!Directory.Exists(dataPathDirectory))
        {
            Directory.CreateDirectory(dataPathDirectory);
        }
        System.IO.File.WriteAllBytes(dataPathPlusDate, bytes);
    }
}
