using System.Collections.Generic;
using UnityEngine;

public class SnapController : MonoBehaviour
{
    public List<Transform> snapPoints;
	public List<Draggable> draggableObjects;
	// public int[] index;

	public float snapRange = 0.5f;
    void Start()
    {
        foreach(Draggable draggable in draggableObjects){
			draggable.dragEndedCallback = OnDragEnded;
		}
    }
	//
	void Update()
	{

		// for(int i = 0; i < 3; i++)
		// if(draggableObjects[i].transform.position == snapPoints[i].transform.position){
		// 	index[i] = 1;
		// }
	}
    private void OnDragEnded(Draggable draggable){
		float closestDistance = -1;
		Transform closestSnapPoint = null;

		foreach(Transform snapPoint in snapPoints){
			float currentDistance = Vector2.Distance(draggable.transform.localPosition, snapPoint.localPosition);
			if(closestSnapPoint == null || currentDistance < closestDistance){
				closestSnapPoint = snapPoint;
				closestDistance = currentDistance;
			}
		}
		if(closestSnapPoint != null && closestDistance <= snapRange){
			draggable.transform.localPosition = closestSnapPoint.localPosition;
		}
	}

}
