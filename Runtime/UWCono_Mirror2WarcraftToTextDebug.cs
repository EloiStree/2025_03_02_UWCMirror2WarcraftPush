
using UnityEngine;
using UnityEngine.Events;

public class UWCono_Mirror2WarcraftToTextDebug : MonoBehaviour
{
    public FetchBasicWowPixelCoordinate m_source;

    [TextArea(1, 5)]
    public string m_text = "";

    public UnityEvent<string> m_onTextUpdate = new UnityEvent<string>();


    void Start()
    {
        InvokeRepeating(nameof(UpdateText), 0.1f, 0.1f);
    }

    void UpdateText()
    {
        m_text = "";
        foreach (var playerInfo in m_source.m_fourPixelsBasicWowInfo)
        {

            if (playerInfo == null) continue;


            string HandleAndPosition = playerInfo.m_windowHandle + "," + playerInfo.m_worldPositionX + ", " + playerInfo.m_worldPositionY;
            m_text += HandleAndPosition + "\n";
        }
        m_onTextUpdate.Invoke(m_text);
    }
}
