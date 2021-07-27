using SocketIOClient;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SimpleChat
{
    public class ChatClient : MonoBehaviour
    {
        public InputField input;
        public Text text;
        private SocketIO client;
        private string msg;

        async void Start()
        {
            client = new SocketIO("http://localhost:3000/");
            client.On("chat message", OnChatMessage);
            await client.ConnectAsync();
        }

        async void OnDestroy()
        {
            await client.DisconnectAsync();
        }

        private void Update()
        {
            text.text = msg;
        }

        private void OnChatMessage(SocketIOResponse response)
        {
            msg += $"Recieve: { response.GetValue(0)}\n";
        }

        public void EnterChat()
        {
            client.EmitAsync("chat message", input.text);
            input.text = string.Empty;
        }

        public void Clear()
        {
            text.text = string.Empty;
        }
    }
}
