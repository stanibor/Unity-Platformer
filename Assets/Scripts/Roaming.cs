using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roaming : MonoBehaviour
{
    [SerializeField]
    float roamingSpeed = 1f;
    [SerializeField]
    List<Transform> roamingPoints;

    Transform currentPoint;
    int currentIndex = 0;

	// Use this for initialization
	void Start ()
    {
        currentIndex = 0;
        currentPoint = roamingPoints[0];
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        transform.position = Vector3.MoveTowards(transform.position, currentPoint.position, roamingSpeed * Time.fixedDeltaTime);

        if(transform.position == currentPoint.position)
        {
            currentIndex = (currentIndex + 1) % roamingPoints.Count;
            currentPoint = roamingPoints[currentIndex];
        }
	}

    void OnDrawGizmos()
    {
        if (roamingPoints == null)
            return;


        int j = 0;
        for (int i = 0; i < roamingPoints.Count; i++)
        {
            j = (i + 1) % roamingPoints.Count;
            Gizmos.DrawLine(roamingPoints[i].position, roamingPoints[j].position);
        }
    }
}
