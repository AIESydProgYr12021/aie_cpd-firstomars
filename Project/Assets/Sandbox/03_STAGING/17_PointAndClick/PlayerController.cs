using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace SandBox.Staging.PointClick
{
    public class PlayerController : MonoBehaviour
    {
        public LayerMask whatCanBeClickedOn;
        private NavMeshAgent myAgent;
        private Vector3 destination;
        
        // Start is called before the first frame update
        void Start()
        {
            myAgent = GetComponent<NavMeshAgent>();
        }

        // Update is called once per frame
        void Update()
        {
            if(Input.GetMouseButtonDown(0))
            {
                Ray myRay = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hitInfo;

                if (Physics.Raycast(myRay, out hitInfo, 100, whatCanBeClickedOn))
                {
                    myAgent.SetDestination(hitInfo.point);
                    destination = hitInfo.point;
                }
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, destination);

            ////walkpoint
            //if (!CanSeePlayer() && !playerInAttackRange)
            //{
            //    Gizmos.color = Color.blue;
            //    Gizmos.DrawLine(transform.position, walkPoint);
            //}
        }


    }

}
