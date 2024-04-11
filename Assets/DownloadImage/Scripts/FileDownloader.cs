using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

public class FileDownloader 
{
    public static async void GetNetworkTexture(string url, Action<Texture2D> _result, Action<Exception> _error = null, Action<float> progress = null)
    {
        try
        {
            UnityWebRequest www = UnityWebRequestTexture.GetTexture(url);
            var operation = www.SendWebRequest();
            while (!operation.isDone)
            {
                progress?.Invoke(operation.progress);
                await Task.Yield();
            }
            if (www.result != UnityWebRequest.Result.Success)
                Debug.LogError($"Failed: {www.error}" + url);

            var texResult = DownloadHandlerTexture.GetContent(www);
            _result.Invoke(texResult);
            progress?.Invoke(operation.progress);
        }
        catch (Exception ex)
        {
            _error?.Invoke(ex);
        }
    }
}
