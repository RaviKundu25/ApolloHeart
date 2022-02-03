using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [PunRPC]
    void RPC_SendString(string viewID,string value)
    {
        if (!transform.GetComponent<PhotonView>().IsMine)
        {
            Debug.Log(viewID + " : " + value);
        }
    }
}
