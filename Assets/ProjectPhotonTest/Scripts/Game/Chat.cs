using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Chat : MonoBehaviour
//public class Chat : MonoBehaviourPunCallbacks
{
    public static PhotonView photonView;

    public void OnClicked()
    {
        Debug.Log("클릭!");
        string m = "메세지!!" + Random.Range(0, 100);
        photonView.RPC("ReceiveMsg", RpcTarget.AllViaServer, m);
    }

    /*
    public void OnClicked()
    {
        Debug.Log("클릭!");
        //ReceiveMsg("aptpwl!!!" + Random.Range(0, 100));
        string m = "메세지!!" + Random.Range(0, 100);
        photonView.RPC("ReceiveMsg", RpcTarget.AllViaServer, m);
    }

    [PunRPC]
    public void ReceiveMsg(string msg, PhotonMessageInfo info)
    {
        Debug.Log("ReceiveMsg :" + msg + "\n" + info.Sender);
    }
    */
}