﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR;
using UnityEngine.Networking;

namespace Device
{

    public class MRTransformer : NetworkBehaviour
    {
        GameObject positionParent;
        ViceTrackerDevice tracker;
        // Use this for initialization
        void Start ()
        {
            if ( GlobalSettings.Instance.cameraType != CameraType.MR )
            {
                Destroy (this);
            }
            positionParent = new GameObject ();
            this.transform.SetParent (positionParent.transform);
            tracker = GameObject.Find ("ViveTrackingManager(Clone)").GetComponent<ViceTrackerDevice> ();

        }

        [ClientCallback]
        void Update ()
        {
            if ( tracker == null )
            {
                tracker = GameObject.Find ("ViveTrackingManager(Clone)").GetComponent<ViceTrackerDevice> ();
            }
            else
            {


                if ( hasAuthority )
                {
                    Debug.Log ("trackerpos=" + tracker.pos);
                    positionParent.transform.position = ( -1 * InputTracking.GetLocalPosition (VRNode.CenterEye) + tracker.pos );
                    Debug.Log ("coerrect pos setted");
                    //rotationは自分の値を使用する
                }
            }
        }

        ///つかってないかもーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーー下
        [Command]
        void CmdSetTrackerTransform ()
        {
            Debug.Log ("call settransform");
            //ホストに存在するトラッカーのポジションを取得するsteamvr依存のを使用する
            this.transform.position = tracker.pos;

        }

        [ClientCallback]
        void OnGUI ()
        {
            Debug.Log ("ongui");
            if ( hasAuthority )
            {
                Debug.Log ("MR authority");
                transform.position = Vector3.zero;

            }
        }

    }
}