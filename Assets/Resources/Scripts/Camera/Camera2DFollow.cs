using System;
using UnityEngine;

public class Camera2DFollow : MonoBehaviour
{
    public float damping = 1;
    public float lookAheadFactor = 3;
    public float lookAheadReturnSpeed = 0.5f;
    public float lookAheadMoveThreshold = 0.1f;
    public Vector2 lowerBounds = new Vector2(-100000, -100000);
    public Vector2 upperBounds = new Vector2(100000, 100000);

    private Transform target;
    private float m_OffsetZ;
    private Vector3 m_LastTargetPosition;
    private Vector3 m_CurrentVelocity;
    private Vector3 m_LookAheadPos;

    private float cameraWidth, cameraHeight;

    public void AttachTarget(Transform target)
    {
        this.target = target;
        m_LastTargetPosition = target.position;
        m_OffsetZ = (transform.position - target.position).z;
        transform.parent = null;
    }

    // Use this for initialization
    private void Start()
    {
        Camera mainCam = Camera.main;
        cameraHeight = 2 * Camera.main.orthographicSize;
        cameraWidth = cameraHeight * Camera.main.aspect;
    }


    // Update is called once per frame
    private void FixedUpdate()
    {
        if (target != null)
        {
            // only update lookahead pos if accelerating or changed direction
            float xMoveDelta = (target.position - m_LastTargetPosition).x;

            bool updateLookAheadTarget = Mathf.Abs(xMoveDelta) > lookAheadMoveThreshold;

            if (updateLookAheadTarget)
            {
                m_LookAheadPos = lookAheadFactor * Vector3.right * Mathf.Sign(xMoveDelta);
            }
            else
            {
                m_LookAheadPos = Vector3.MoveTowards(m_LookAheadPos, Vector3.zero, Time.deltaTime * lookAheadReturnSpeed);
            }

            Vector3 aheadTargetPos = target.position + m_LookAheadPos + Vector3.forward * m_OffsetZ;
            Vector3 newPos = Vector3.SmoothDamp(transform.position, aheadTargetPos, ref m_CurrentVelocity, damping);

            transform.position = new Vector3(Mathf.Clamp(newPos.x, lowerBounds.x + cameraWidth / 2, upperBounds.x - cameraWidth / 2), Mathf.Clamp(newPos.y, lowerBounds.y + cameraHeight / 2, upperBounds.y - cameraHeight / 2), newPos.z);

            m_LastTargetPosition = target.position;
        }
    }
}
