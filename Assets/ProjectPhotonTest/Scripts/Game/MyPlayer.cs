using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class MyPlayer : MonoBehaviourPunCallbacks
{
    private Text t;

    private void Start()
    {
        var textGO = GameObject.Find("ChatText");
        t = textGO.GetComponent<Text>();

        if (photonView.IsMine)
            Chat.photonView = photonView;
    }

    [PunRPC]
    public void ReceiveMsg(string msg, PhotonMessageInfo info)
    {
        Debug.Log("ReceiveMsg :" + msg + "\n" + info.Sender);
        t.text += msg + ":" + info.Sender + "\n";
    }
}