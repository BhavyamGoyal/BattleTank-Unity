using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Enemy
{
    public class ViewEnemyComponentPetrolling : MonoBehaviour
    {
        // Start is called before the first frame update
        public Vector3 pos1, pos2, current;
        NavMeshAgent navAgent;
        Rigidbody rb;
        Coroutine delay;
        public void SetPetrollingPosition()
        {
            Vector3 position = pos1;
            position.x += 40;
            if(position.x>40){
                pos2=position;
                pos2.x=40;
            }else if(position.x<-40){
                pos2=position;
                pos2.x=-40;
            }else{
                pos2=position;
            }
        }
        private void Start()
        {
            pos1 = gameObject.transform.position;
            SetPetrollingPosition();
            current = pos2;
            rb = gameObject.GetComponent<Rigidbody>();
            navAgent = GetComponent<NavMeshAgent>();
            //gameObject.transform.LookAt(current);
            delay = StartCoroutine(ChangePetrollingDirection(current));
        }
        // Update is called once per frame
        void Update()
        {
            if (!(pos1 == pos2))
            {
                //rb.velocity = transform.forward * 4;
                if (Mathf.Abs((gameObject.transform.position - current).magnitude) < 1)
                {
                    if (current == pos1)
                    {
                        current = pos2;
                    }
                    else
                    {
                        current = pos1;
                    }
                    navAgent.destination = current;
                    //gameObject.transform.LookAt(current);
                    StopCoroutine(delay);
                    delay = StartCoroutine(ChangePetrollingDirection(current));

                }
            }
        }

        IEnumerator ChangePetrollingDirection(Vector3 curr)
        {
            yield return new WaitForSeconds(10);
            if (curr == pos1)
            {
                current = pos2;
            }
            else
            {
                current = pos1;
            }
            navAgent.destination = current;

            //Debug.Log(current);
            StartCoroutine(ChangePetrollingDirection(current));
        }
        public void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.tag == "Ground")
            {//Debug.Log("ground");


            }

        }
    }
}