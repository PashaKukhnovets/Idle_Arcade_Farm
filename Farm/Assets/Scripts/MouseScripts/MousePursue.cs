using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MousePursue : MouseSeek
{
    public float maxPrediction;
    private GameObject targetAux;
    private Agent targetAgent;
    public float targetRadius;
    public float slowRadius;
    public float timeToTarget = 0.1f;

    public event UnityAction MouseRun; 

    private GameObject[] newWheats;
    private bool isChangeWheat = false;

    public override void Awake()
    {
        base.Awake();
        targetAgent = target.GetComponent<Agent>();
        targetAux = target;
    }

    public override Steering GetSteering()
    {
        MouseRun?.Invoke();
        Steering steering = new Steering();
        Vector3 direction = target.transform.position - transform.position;
        float distance = Mathf.Abs(direction.magnitude);
        float targetSpeed;
        float speed = targetAgent.velocity.magnitude;
        float prediction;

        if (distance < targetRadius)
        {
            StartCoroutine(EatWheat());
            steering.linear = Vector3.zero;
            steering.angular = 0.0f;
            return steering;
        }

        if (distance > slowRadius)
            targetSpeed = targetAgent.maxSpeed;
        else
            targetSpeed = targetAgent.maxSpeed * distance / slowRadius;

        Vector3 desiredVelocity = direction;
        desiredVelocity.Normalize();
        desiredVelocity *= targetSpeed;
        steering.linear = desiredVelocity - targetAgent.velocity;
        steering.linear /= timeToTarget;

        if (steering.linear.magnitude > targetAgent.maxAccel)
        {
            steering.linear.Normalize();
            steering.linear *= targetAgent.maxAccel;
        }

        return base.GetSteering();
    }

    public bool CheckChangeWheat() {
        return isChangeWheat;
    }

    private IEnumerator EatWheat() {
        yield return new WaitForSeconds(2.5f);
        isChangeWheat = true;
        Destroy(target.gameObject);
    }
}
