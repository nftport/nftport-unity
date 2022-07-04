using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NFTPort.Samples.Playground
{
    public class CamFollow : MonoBehaviour
    {
        public GameObject player;
        public float cameraHeight = 20.0f;

        void Update() {
            Vector3 pos = player.transform.position;
            pos.z += cameraHeight;
            transform.position = pos;
        }
    }

}

