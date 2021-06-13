using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///   Used for any rope-based objects in the game, like headphone wire, vines, length, etc.
/// </summary>
public class Rope : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private List<RopeSegment> ropeSegments = new List<RopeSegment>();
    private float ropeSegLen = 0.25f;
    private int segmentLength = 35;

    private float lineWidth = 0.1f;

    private Vector2 forceGravity = new Vector2(0f, -1f);

    private const int SIMULATION_COUNT = 50;

    [SerializeField] private Transform ropeOrigin;
    [SerializeField] private Transform ropeEnd;

    // Start is called before the first frame update
    void Start()
    {
        this.lineRenderer = this.GetComponent<LineRenderer>();
        Vector3 ropeStartPoint = ropeOrigin.position;

        for (int i = 0; i < segmentLength; i++) {
            this.ropeSegments.Add(new RopeSegment(ropeStartPoint));
            ropeStartPoint.y -= ropeSegLen;
        }
    }

    // Update is called once per frame
    void Update()
    {
        this.DrawRope();
    }

    private void FixedUpdate()
    {
        this.Simulate();
    }

    public Vector2 RopeDirection() {
        // Provides the direction of the rope from origin to end as a unit vector
        Vector2 direction = (Vector2)ropeEnd.position - (Vector2)ropeOrigin.position;
        return direction.normalized;
    }

    private void Simulate()
    {
        // Simulation
        for (int i = 0; i < this.segmentLength; i++) 
        {
            RopeSegment firstSegment = this.ropeSegments[i];
            Vector2 velocity = firstSegment.posNow - firstSegment.posOld;
            firstSegment.posOld = firstSegment.posNow;
            firstSegment.posNow += velocity;
            firstSegment.posNow += this.forceGravity * Time.deltaTime;
            this.ropeSegments[i] = firstSegment;
        }

        // Constraints
        for (int i = 0; i < SIMULATION_COUNT; i++)
        {
            ApplyConstraint();
        }
    }

    private void ApplyConstraint() {
        // First segment follows main point
        RopeSegment firstSegment = this.ropeSegments[0];
        firstSegment.posNow = ropeOrigin.position;
        this.ropeSegments[0] = firstSegment;


        RopeSegment endSegment = this.ropeSegments[this.segmentLength - 1];
        endSegment.posNow = ropeEnd.position;
        this.ropeSegments[this.segmentLength - 1] = endSegment;

        for (int i = 0; i < this.segmentLength - 1; i++)
        {
            RopeSegment firstSeg = this.ropeSegments[i];
            RopeSegment secondSeg = this.ropeSegments[i+1];

            float dist = (firstSeg.posNow - secondSeg.posNow).magnitude;
            float error = Mathf.Abs(dist - this.ropeSegLen);
            Vector2 changeDir = Vector2.zero;

            if (dist > ropeSegLen) 
            {
                changeDir = (firstSeg.posNow - secondSeg.posNow).normalized;
            }
            else if (dist < ropeSegLen) 
            {
                changeDir = (secondSeg.posNow - firstSeg.posNow).normalized;
            }

            Vector2 changeAmount = changeDir * error;
            if (i != 0)
            {
                firstSeg.posNow -= changeAmount * 0.5f;
                this.ropeSegments[i] = firstSeg;
                secondSeg.posNow += changeAmount * 0.5f;
                this.ropeSegments[i+1] = secondSeg;
            }
            else
            {
                secondSeg.posNow += changeAmount;
                this.ropeSegments[i+1] = secondSeg;
            }
        }
    }

    private void DrawRope() 
    {
        float lineWidth = this.lineWidth;
        lineRenderer.startWidth = lineWidth;
        lineRenderer.endWidth = lineWidth;

        Vector3[] ropePositions = new Vector3[this.segmentLength];
        for (int i = 0; i < this.segmentLength; i++) {
            ropePositions[i] = this.ropeSegments[i].posNow;
        }

        lineRenderer.positionCount = ropePositions.Length;
        lineRenderer.SetPositions(ropePositions);
    }

    public void SetRopeEnd(Transform t) {
        this.ropeEnd = t;
    }

    public struct RopeSegment 
    {
        public Vector2 posNow;
        public Vector2 posOld;

        public RopeSegment(Vector2 pos) 
        {
            this.posNow = pos;
            this.posOld = pos;
        }
    }

    public void OnDrawGizmos()
    {
        if (ropeOrigin && ropeEnd) {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(ropeOrigin.position, ropeEnd.position);
        }
    }
}
