using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Cameras
{
    public class MiniMap : MonoBehaviour
    {
        // Start is called before the first frame update
        public GameObject target;
        // Update is called once per frame
        void Update()
        {
            if (target != null)
                gameObject.transform.position = new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z);
        }
    }
}