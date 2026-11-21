using UnityEngine;
using UnityEngine.Events;

public class CameraFollow : MonoBehaviour
{
    // Target camera follow
    [SerializeField] protected Transform target;
    [SerializeField] protected Vector3 offset = new Vector3(0f, 3f, -10f);
    [SerializeField] protected float smooth_speed = 5f;
    [SerializeField] protected UnityEvent on_target_set;

    protected virtual void LateUpdate()
    {
        //Do nothing if no target is assgined
        if (target == null)
        }
            return;
        }
        
        //Move smoothly
        Vector3 desired_position = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, desired_postion) smooth_speed * Time.deltaTime);
    }

    public virtual void SetTarget(Transform new_target)
    {
        //Assign new target and fire event
        target = new_target;
        on_target_set?.Invoke();
    }

    protected virtual void OnDrawGizmos()
    {
        //Draw line from camera to target offset in Scene view
        if (target != null)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawLine(transform.position, target.position + offset);
        }
    }
}
