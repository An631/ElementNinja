using UnityEngine;
using System.Collections;

public class TouchLogic : MonoBehaviour {
	

	public static Touch currentTouch;
	public static Vector3 currentTouchInWorldPoint;
	public void CheckTouches () {
		
		// Look for touches on the screen
		if(Input.touchCount>0)
		{ 
			// If the screen has more than 0 touches
			for(int i=0; i<Input.touchCount; i++)
			{
				currentTouch = Input.GetTouch(i);

				float touchXPosition = currentTouch.position.x;
				float touchYPosition = currentTouch.position.y;

				// Translate the current touch coordinates to world coordinates to try hit tests and more
				 currentTouchInWorldPoint = Camera.main.ScreenToWorldPoint(new Vector3(touchXPosition,touchYPosition,0));
				currentTouchInWorldPoint.z=0;
				// Find out what phase the current touch is in
				switch(currentTouch.phase)
				{
				case TouchPhase.Began:
					OnTouchBeganAnywhere();

					break;
					
				case TouchPhase.Ended:
					OnTouchEndedAnywhere();

					break;
					
				case TouchPhase.Moved:
					OnTouchMovedAnywhere();

					break;
					
				case TouchPhase.Stationary:
					OnTouchStayedAnywhere();

					break;
				}
				
			}
			
		}// If
		
	}//Update
	

	public virtual void OnTouchBeganAnywhere()
	{
	}
	public virtual void OnTouchEndedAnywhere()
	{
	}
	public virtual void OnTouchMovedAnywhere()
	{
	}
	public virtual void OnTouchStayedAnywhere()
	{
	}
	
}
