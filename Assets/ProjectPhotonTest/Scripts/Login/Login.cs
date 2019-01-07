using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class Login : MonoBehaviourPunCallbacks

{
    public InputField idField;
    public GameObject lobbyPanel;

    private void Awake()
    {
        idField.text = "Player " + Random.Range(1000, 10000);
    }

    public void OnLoginButtonClicked()
    {
        string playerName = idField.text;
        if (!playerName.Equals(""))
        {
            PhotonNetwork.LocalPlayer.NickName = playerName;
            PhotonNetwork.AutomaticallySyncScene = true;
            PhotonNetwork.ConnectUsingSettings();
        }
        else
        {
            Debug.LogError("Player Name is invalid.");
        }
    }

    public override void OnConnected()
    {
        Debug.Log("Connected!! - " + PhotonNetwork.LocalPlayer.NickName);
        gameObject.SetActive(false);
        lobbyPanel.SetActive(true);
    }
}