using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Bot : MonoBehaviour
{
    NavMeshAgent agent;
    public GameObject target;


    // Start is called before the first frame update
    void Start()
    {
        agent = this.GetComponent<NavMeshAgent>();
    }

    void Seek(Vector3 location)
    {
        agent.SetDestination(location);
    }

    void Flee(Vector3 location)
    {
        Vector3 fleeVector =location - this.transform.position;
        agent.SetDestination(this.transform.position - fleeVector);
    }

    void Pursure()
    {
        Vector3 targetDir = target.transform.position - this.transform.position;

        float relativeHeading = Vector3.Angle(this.transform.forward, this.transform.TransformVector(target.transform.forward));
        float toTarget = Vector3.Angle(this.transform.forward, this.transform.TransformVector(targetDir));

        if ((toTarget > 90 && relativeHeading < 20) || target.GetComponent<Drive>().currentSpeed < 0.01)
        {
            Seek(target.transform.position);
            return;
        }

        float lookAhead = targetDir.magnitude / (agent.speed + target.GetComponent<Drive>().currentSpeed);
        Seek(target.transform.position + target.transform.forward * lookAhead);
    }

    Vector3 wanderTarget = Vector3.zero;
    void Wander()
    {
        float wanderRadius = 10;
        float wanderDistance = 20;
        float wanderJitter = 1;

        wanderTarget +=new Vector3(Random.Range(-1.0f,1.0f) * wanderJitter,
                                                 0,
                                                   Random.Range(-1.0f,1.0f) * wanderJitter);
    }
   
    // Update is called once per frame
    void Update()
    {
        Pursure();
    }
}
