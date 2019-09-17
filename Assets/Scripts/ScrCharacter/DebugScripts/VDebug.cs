using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VDebug : MonoBehaviour
{
    public bool isEnabled = true;
    Text DebugText;

    public static VDebug Instance;

    private List<string> debugs;

    public int Line;
    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
            return;
        }
        debugs = new List<string>();
        Instance = this;
    }

    private void FixedUpdate()
    {
        string visible = "";
        if (!isEnabled)
        {
            return;
        }
        foreach (string debug in debugs.ToArray())
        {
            visible += debug + "\n";
        }
        DebugText.text = visible;
    }

    public void Log(string text)
    {
        if (debugs.Count > Line)
        {
            debugs.RemoveAt(0);
        }
        debugs.Add(text);
    }
}
