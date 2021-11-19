using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class DCSAPI : MonoBehaviour
{
    private const string URL = "https://be.ypti.dcs.stechoq.com/api/hololens/summary/production";
    private const string HOST = "be.ypti.dcs.stechoq.com";
    private const string API_KEY = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1IjoiMTAwMDA3IiwibCI6MTcsInQiOiIyMDIxLTExLTE5IDE2OjMzOjMzLjQ0MCIsInYiOiJSVTdhRnF0OW1Kb2s2dVljSm5EVUhYR1EiLCJpYXQiOjE2MzcyODU2MTMsImV4cCI6MTYzNzMxNDQxM30.giwWV_DCt0o1elxeT0fCWMuJstvF2jIZno2FZc5-_k8";

    public void Start()
    {
        Debug.Log("asdasd");
        StartCoroutine(ProcessRequest(URL));
    }

    private IEnumerator ProcessRequest(string uri)
    {
        //Debug.Log("{\"username\": \"100007\", \"password\": \"password\", \"emp\": true}");
        //using (UnityWebRequest request = UnityWebRequest.Post(uri, "{\"username\": \"100007\",\"password\": \"password\",\"emp\": true}"))
        /*
        WWWForm form = new WWWForm();
        form.AddField("username", "100007");
        form.AddField("password", "password");
        form.AddField("emp", "true");
        using (UnityWebRequest request = UnityWebRequest.Post(uri, form))
        */

        WWWForm form = new WWWForm();
        form.AddField("shiftDate", "2021-10-27 12:00:00");
        using (UnityWebRequest request = UnityWebRequest.Post(uri, form))
        {
            request.SetRequestHeader("Authorization", "Bearer "+API_KEY);
            //request.SetRequestHeader("Content-Type", "application/json");
            //request.SetRequestHeader("Host", "be.ypti.dcs.stechoq.com");
            //request.SetRequestHeader("User-Agent", "PostmanRuntime/7.28.4");
            //request.SetRequestHeader("Accept", "*/*");
            //request.SetRequestHeader("Accept-Encoding", "gzip, deflate, br");
            //request.SetRequestHeader("Connection", "keep-alive");

            //request.GetRequestHeader("Content-Type")

            yield return request.SendWebRequest();

            if (request.isNetworkError)
            {
                Debug.Log("Error: " + request.error);
            }
            else
            {
                Debug.Log("Received: " + request.downloadHandler.text);
            }
        }
    }
}