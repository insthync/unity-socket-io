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
            client.On("hi", response =>
            {
                string text = response.GetValue<string>();
            });
            client.On("chat message", OnChatMessage);
            await client.ConnectAsync();
        }

        async void OnDestroy()
        {
            await client.DisconnectAsync();
        }

        private void OnChatMessage(SocketIOResponse response)
        {
            msg += $"Recieve: {response.GetValue<string>()}\n";
            text.text = msg;
        }

        public void EnterChat()
        {
            client.EmitAsync("chat message", input.text);
        }

        public void Clear()
        {
            text.text = string.Empty;
        }
    }
}
