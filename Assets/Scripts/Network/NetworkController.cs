﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace Network
{
    public class NetworkController : MonoBehaviour
    {
        [SerializeField]
        GlobalSettings setting;
        [SerializeField]
        NetworkManager netManager;
        [SerializeField]
        ObjectCreator creator;

        // Use this for initialization
        void Start ()
        {
            if ( setting.playerType == PlayerType.HOST )
            {
                netManager.StartHost ();
                creator.SpawnObject (creator.RoomPrefab);
            }
            else if ( setting.playerType == PlayerType.CLIENT )
            {
                netManager.StartClient ();
            }
            else
            {
                Debug.Log ("PlayerType is not found");
            }
        }

        // Update is called once per frame
        void Update ()
        {

        }
    }
}