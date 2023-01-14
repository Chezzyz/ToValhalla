using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class GUIDHandler : MonoBehaviour
{
    [SerializeField] private TMP_Text _GUID;
    [SerializeField] private TMP_Text _username;
    [SerializeField] private Button _copyButton;

    private void Awake()
    {
        Network.NetworkPlayerHandler.PlayerIDSetted += OnPlayerIDSetted;
        Network.NetworkPlayerHandler.PlayerNameSetted += OnPlayerNameSetted;
    }

    private void OnDestroy()
    {
        Network.NetworkPlayerHandler.PlayerIDSetted -= OnPlayerIDSetted;
        Network.NetworkPlayerHandler.PlayerNameSetted -= OnPlayerNameSetted;
    }

    private void OnPlayerNameSetted(string username)
    {
        _username.text = username;
    }

    private void OnPlayerIDSetted(string playerID)
    {
        _GUID.text = playerID;
    }

    private void OnEnable()
    {
        _copyButton.onClick.AddListener(CopyGUIDToClipboard);
    }

    private void CopyGUIDToClipboard()
    {
        GetClipboardManager().Call("setText", _GUID.text);
    }

    private AndroidJavaObject GetClipboardManager()
    {
        var unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        var activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
        var staticContext = new AndroidJavaClass("android.content.Context");
        var service = staticContext.GetStatic<AndroidJavaObject>("CLIPBOARD_SERVICE");
        return activity.Call<AndroidJavaObject>("getSystemService", service);
    }
}