using UnityEngine;
using UnityEngine.Video;

public class PlayFirstTimeVideo : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public string videoFilePath = "Assets/Image_Resource/Video/ShuRub intro.mp4";

    void Start()
    {
        bool isFirstRun = PlayerPrefs.GetInt("FirstRun", 0) == 0;

        if (isFirstRun)
        {
            PlayerPrefs.SetInt("FirstRun", 1); // 최초 실행 여부 저장

            videoPlayer = gameObject.AddComponent<VideoPlayer>();
            videoPlayer.playOnAwake = false;
            videoPlayer.url = videoFilePath;

            videoPlayer.prepareCompleted += OnVideoPrepared;
            videoPlayer.Prepare();
        }
    }

    void OnVideoPrepared(VideoPlayer vp)
    {
        vp.Play();
    }

    // 게임이 종료될 때 호출되는 함수
    void OnApplicationQuit()
    {
        // FirstRun 변수 초기화
        PlayerPrefs.DeleteKey("FirstRun");
    }
}
