using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using agora_gaming_rtc;
using Photon.Pun;

public class VoiceChatManager : MonoBehaviourPunCallbacks
{
    string appId = "3891935dc4424cf385851cb7e9338fc0";

    public static VoiceChatManager instance;
    VideoSurface myView;
    VideoSurface remoteView;
    IRtcEngine rtcEngine;

    /*void Awake()
    {
        if (instance)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }*/

    void Start()
    {
        rtcEngine = IRtcEngine.GetEngine(appId);
        rtcEngine.OnJoinChannelSuccess += OnjoinChannelSuccess;
        rtcEngine.OnLeaveChannel += OnLeaveChannel;
        rtcEngine.OnError += (int error, string msg) =>
        {
            Debug.Log("Error: " + error + " " + msg);
        };
        GameObject go = GameObject.Find("MyView");
        myView = go.AddComponent<VideoSurface>();
        rtcEngine.EnableAudio();
        rtcEngine.JoinChannel("test", null, 0);
        rtcEngine.OnUserJoined = OnUserJoined;
        rtcEngine.OnUserOffline = OnUserOffline;
        rtcEngine.OnJoinChannelSuccess = OnJoinChannelSuccessHandler;
        rtcEngine.OnLeaveChannel = OnLeaveChannelHandler;
    }

    void OnjoinChannelSuccess(string channelName, uint uid, int elapsed)
    {
        Debug.Log("Join Channel Success");
    }
    void OnLeaveChannel(RtcStats stats)
    {
        Debug.Log("Leave Channel");
    }
    void OnTriggerEnter2D(Collider2D collider)
    {

        Debug.Log("Trigger Enter");
        rtcEngine.EnableAudio();
        rtcEngine.EnableVideo();
        rtcEngine.EnableVideoObserver();
        rtcEngine.JoinChannel(PhotonNetwork.CurrentRoom.Name);

            

    }
    void OnTriggerExit2D(Collider2D collider)
    {
        Debug.Log("Trigger Exit");
        rtcEngine.LeaveChannel();
        rtcEngine.DisableAudio();
        rtcEngine.DisableVideo();
        rtcEngine.DisableVideoObserver();

    }
    void OnDestroy()
    {
        rtcEngine.LeaveChannel();
        rtcEngine.DisableAudio();
        IRtcEngine.Destroy();
    }
    void OnJoinChannelSuccessHandler(string channelName, uint uid, int elapsed)
    {
        // can add other logics here, for now just print to the log
        Debug.LogFormat("Joined channel {0} successful, my uid = {1}", channelName, uid);
    }

    void OnLeaveChannelHandler(RtcStats stats)
    {
        myView.SetEnable(false);
        if (remoteView != null)
        {
            remoteView.SetEnable(false);
        }
    }

    void OnUserJoined(uint uid, int elapsed)
    {
        GameObject go = GameObject.Find("RemoteView");

        if (remoteView == null)
        {
            remoteView = go.AddComponent<VideoSurface>();
        }

        remoteView.SetForUser(uid);
        remoteView.SetEnable(true);
        remoteView.SetVideoSurfaceType(AgoraVideoSurfaceType.RawImage);
        remoteView.SetGameFps(30);
    }

    void OnUserOffline(uint uid, USER_OFFLINE_REASON reason)
    {
        remoteView.SetEnable(false);
    }
}
