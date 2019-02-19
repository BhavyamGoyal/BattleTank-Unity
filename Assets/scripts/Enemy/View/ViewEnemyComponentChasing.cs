using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Enemy
{
    public class ViewEnemyComponentChasing : MonoBehaviour
    {
        Rigidbody rb;
        public ViewEnemy mainview;
        // Start is called before the first frame update
        Vector3 lastSeen;
        public NavMeshAgent navAgent;
        private void Awake()
        {
            navAgent = GetComponent<NavMeshAgent>();

        }
        private void Start()
        {
            rb = gameObject.GetComponent<Rigidbody>();
        }
      
    public void SetFollowPosition(Vector3 position)
    {
        lastSeen = position;
        if (Mathf.Abs((gameObject.transform.position - lastSeen).magnitude) < 20)
        {
            navAgent.destination = position;
            // rb.velocity = transform.forward * 10;
        }
        else
        {
            mainview.StateChange(EnemyState.Petrolling);
        }

    }
    private void OnEnable()
    {

    }

    IEnumerator ChangeToPetrolling(Vector3 curr)
    {
        yield return new WaitForSeconds(10);
        mainview.StateChange(EnemyState.Petrolling);
    }
}
}