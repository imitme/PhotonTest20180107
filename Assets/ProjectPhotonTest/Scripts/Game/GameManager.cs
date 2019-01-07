using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public string spaceshipPrefabName = "MySpaceship";

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(2f);
        StartGame();
    }

    private void StartGame()
    {
        float angularStart = Random.Range(0f, 360f);
        float x = Mathf.Sin(angularStart * Mathf.Deg2Rad);
        float z = Mathf.Cos(angularStart * Mathf.Deg2Rad);
        float range = Random.Range(5f, 25f);
        Vector3 position = new Vector3(x, 0.0f, z) * range;
        Quaternion rotation = Quaternion.Euler(0.0f, angularStart, 0.0f);
        PhotonNetwork.Instantiate(spaceshipPrefabName, position, rotation, 0);
    }
}