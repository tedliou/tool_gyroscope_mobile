using TMPro;
using UnityEngine;
using UnityEngine.UI;
using WebSocketSharp;

public class GyroscopeSender : MonoBehaviour
{
    public TMP_InputField serverAddressInputField;
    public TMP_InputField serverPortInputField;
    public Button connectButton;
    public TMP_Text connectButtonText;

    public WebSocket ws;

    private void Start()
    {
        connectButton.onClick.AddListener(Connect);
    }

    private void Update()
    {
        if (ws != null && ws.IsAlive)
        {
            connectButtonText.text = "Disconnect";
            ws.Send(GetFormatGyro());
            return;
        }

        connectButtonText.text = "Connect";
    }

    private void Connect()
    {
        if (ws != null && ws.IsAlive)
        {
            ws.Close();
            ws = null;
        }
        else
        {
            ws = new WebSocket($"ws://{serverAddressInputField.text}:{serverPortInputField.text}/data");
            ws.Connect();
        }
    }

    private string GetFormatGyro()
    {
        Input.gyro.enabled = true;
        var gyro = Input.gyro.attitude;
        return $"{gyro.x},{gyro.y},{gyro.z},{gyro.w}";
    }
}
