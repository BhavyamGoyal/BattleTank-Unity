using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cameras
{
    public class MainCamera : MonoBehaviour
    {
        // Start is called before the first frame update

        public GameObject target;
        public Vector3 offSet=new Vector3(0,15,-20);
        // Update is called once per frame
        void Update()
        {
            if (target != null)

                gameObject.transform.position = target.transform.position+offSet;
        }
    }
}