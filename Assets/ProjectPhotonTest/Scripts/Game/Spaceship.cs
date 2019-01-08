using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class Spaceship : MonoBehaviourPun
{
    public float RotationSpeed = 90.0f;
    public float MovementSpeed = 2.0f;
    public float MaxSpeed = 0.2f;

    public GameObject BulletPrefab;

    private new Rigidbody rigidbody;
    private float rotation = 0.0f;
    private float acceleration = 0.0f;
    private float shootingTimer = 0.0f;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    public void Update()
    {
        if (!photonView.IsMine)
        {
            return;
        }
        rotation = Input.GetAxis("Horizontal");
        acceleration = Input.GetAxis("Vertical");

        if (Input.GetButton("Jump") && shootingTimer <= 0.0)
        {
            shootingTimer = 0.2f;
            photonView.RPC("Fire", RpcTarget.AllViaServer, rigidbody.position, rigidbody.rotation);
        }
        if (shootingTimer > 0.0f)
        {
            shootingTimer -= Time.deltaTime;
        }
    }

    public void FixedUpdate()
    {
        if (!photonView.IsMine)
        {
            return;
        }
        Quaternion rot = rigidbody.rotation *
        Quaternion.Euler(0, rotation * RotationSpeed * Time.fixedDeltaTime, 0);
        rigidbody.MoveRotation(rot);
        Vector3 force = (rot * Vector3.forward) * acceleration * 1000.0f * MovementSpeed * Time.fixedDeltaTime;
        rigidbody.AddForce(force);
        if (rigidbody.velocity.magnitude > (MaxSpeed * 1000.0f))
        {
            rigidbody.velocity = rigidbody.velocity.normalized * MaxSpeed * 1000.0f;
        }
        CheckExitScreen();
    }

    private void CheckExitScreen()
    {
        if (Camera.main == null)
        {
            return;
        }
        if (Mathf.Abs(rigidbody.position.x) > (Camera.main.orthographicSize * Camera.main.aspect))
        {
            rigidbody.position = new Vector3(-Mathf.Sign(rigidbody.position.x)
            * Camera.main.orthographicSize * Camera.main.aspect,
            0,
            rigidbody.position.z);
            // offset a little bit to avoid looping back & forth between the 2 edges
            rigidbody.position -= rigidbody.position.normalized * 0.1f;
        }
        if (Mathf.Abs(rigidbody.position.z) > Camera.main.orthographicSize)
        {
            rigidbody.position = new Vector3(rigidbody.position.x,
            rigidbody.position.y,
            -Mathf.Sign(rigidbody.position.z) * Camera.main.orthographicSize);
            // offset a little bit to avoid looping back & forth between the 2 edges
            rigidbody.position -= rigidbody.position.normalized * 0.1f;
        }
    }

    [PunRPC]
    public void Fire(Vector3 position, Quaternion rotation, PhotonMessageInfo info)
    {
        float lag = (float)(PhotonNetwork.Time - info.timestamp);
        GameObject bullet;
        /** Use this if you want to fire one bullet at a time **/
        bullet = Instantiate(BulletPrefab, rigidbody.position, Quaternion.identity) as GameObject;
        bullet.GetComponent<Bullet>().InitializeBullet(photonView.Owner,
        (rotation * Vector3.forward), Mathf.Abs(lag));
    }
}