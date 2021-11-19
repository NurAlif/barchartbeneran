using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NativeWebSocket;
using Newtonsoft.Json;

public class WSClient : MonoBehaviour
{
    WebSocket websocket;

    [SerializeField]
    private BarChart chart;

    async void Start()
    {
        websocket = new WebSocket("ws://192.168.182.5:8081");

        websocket.OnOpen += () =>
        {
            Debug.Log("Connection open!");
            SendWebSocketMessage();
        };

        websocket.OnMessage += (bytes) =>
        {
            Debug.Log("OnMessage!");
            Debug.Log(bytes);

            var message = System.Text.Encoding.UTF8.GetString(bytes);
            Debug.Log("OnMessage! " + message);

            List<SimpleDataModel> data = JsonConvert.DeserializeObject<List<SimpleDataModel>>(message);

            List<ChartDataset2> dataset = new List<ChartDataset2>();
            data.ForEach(d => {
                dataset.Add(new ChartDataset2(d.name, d.count));
            });

            chart.SetData(dataset);
        };

        await websocket.Connect();
    }

    void Update()
    {
        #if !UNITY_WEBGL || UNITY_EDITOR
                websocket.DispatchMessageQueue();
        #endif
    }

    async void SendWebSocketMessage()
    {
        if (websocket.State == WebSocketState.Open)
        {
            await websocket.SendText("YPTI-002");
        }
    }

    private async void OnApplicationQuit()
    {
        await websocket.Close();
    }
}
