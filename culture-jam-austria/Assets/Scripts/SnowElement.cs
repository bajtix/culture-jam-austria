using UnityEngine;

public class Draggable : MonoBehaviour
{

	public delegate void DragEndedDelegate(Draggable draggableObject);
	public DragEndedDelegate dragEndedCallback;
	private Vector3 mouseDragStartPosition;
	private Vector3 snowDragStartPosition;
	private bool isDragged = false;
    private void OnMouseDown()
	{
		isDragged = true;
		mouseDragStartPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		snowDragStartPosition = transform.localPosition;
	}
	private void OnMouseDrag()
	{
		transform.localPosition = snowDragStartPosition + (Camera.main.ScreenToWorldPoint(Input.mousePosition) - mouseDragStartPosition);
	}
	private void OnMouseUp()
	{
		isDragged= false;
		dragEndedCallback(this);
	}
}
