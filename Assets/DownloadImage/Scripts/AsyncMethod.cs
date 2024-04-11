using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AsyncMethod : MonoBehaviour
{
    public InputField m_UrlInput;
    public Button m_DownloadButton;
    public Image m_Image;
    public Image m_Progress;
    public Text m_ProgressText;
    private void OnEnable()
    {
        m_DownloadButton.onClick.RemoveAllListeners();
        m_DownloadButton.onClick.AddListener(OnClickDownloadButton);
    }
    void OnClickDownloadButton()
    {
        if (!string.IsNullOrEmpty(m_UrlInput.text))
        {
            m_Image.sprite = null;
            m_Progress.gameObject.SetActive(true);
            FileDownloader.GetNetworkTexture(m_UrlInput.text,
            (result) =>
            {
                m_Image.sprite = Sprite.Create(result, new Rect(0.0f, 0.0f, result.width, result.height), new Vector2(0.5f, 0.5f), 100.0f);
                m_Image.preserveAspect = true;
            },
            (error) =>
            {
                Debug.Log(error);
                m_Progress.gameObject.SetActive(false);
            },
            (process) =>
            {

                m_Progress.fillAmount = process;
                m_ProgressText.text = (process * 100) + "%";
                if (process >= 1)
                {
                    m_Progress.gameObject.SetActive(false);
                }
            });
        }
    }
}
