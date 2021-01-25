/* 
 * MotionParallax script
 * Author: Cédric Fleury (Univ. Paris-Sud, LRI & Inria)
 * Date: 23/05/2015
 */

using UnityEngine;
using UnityEngine.Networking;

public class VRMotionParallax : VRSystem
{

	// Define the screen position in a left-handed reference frame, with Y-up and
	// with the Z axis pointing towards the screen
	// WARNING: the screen has to be perpendicular to the Z axis.
	// The compuation of the left, right, bottom, top values can deal with no perpendicular screen,
	// so the deformation is correct. But the camera is not rotated in the correct direction.
	// This roration should be added to fix that, but it is possible overcome this issue:
	// the Transform of the VRSystemCenter can be used to deal with the rotation of the screen.


	GameObject head;


	void getHead()
	{
		head = GameObject.FindWithTag("Head");
//		if (head == null)
//		    head = GameObject.Find("WilderVRSystem");
	}



	void LateUpdate()
	{

		if (disabled || !cam)
			return;

		cam.transform.localPosition = gameObject.transform.localPosition;
		cam.transform.localRotation = gameObject.transform.localRotation;
		cam.transform.localScale = gameObject.transform.localScale;

		getHead();

		if (cam.orthographic)
		{
			float nearPlane = cam.nearClipPlane;
			float farPlane = cam.farClipPlane;
			
			// on the 2D horizontal and vertical planes
			Vector2 leftPoint = new Vector2(ldc.x , ldc.z );
			Vector2 rightPoint = new Vector2(rdc.x , rdc.z );
			Vector2 bottomPoint = new Vector2(ldc.y , ldc.z );
			Vector2 topPoint = new Vector2(luc.y , luc.z );
			
			cam.projectionMatrix = OrthographicOffCenter(leftPoint.x, rightPoint.x, bottomPoint.x, topPoint.x, nearPlane, farPlane);

		}
		
		else if (head != null)
		{
			Vector3 headPosition = head.transform.localPosition;
			cam.transform.Translate(headPosition);

			float nearPlane = cam.nearClipPlane;
			float farPlane = cam.farClipPlane;

			// Projection of the head on the 2D horizontal and vertical planes
			Vector2 headHProj = new Vector2(headPosition.x, headPosition.z);
			Vector2 headVProj = new Vector2(headPosition.y, headPosition.z);

			// Compute the position of the left, right, bottom and top border of the screen
			// on the 2D horizontal and vertical planes
			Vector2 leftPoint = new Vector2(ldc.x , ldc.z );
			Vector2 rightPoint = new Vector2(rdc.x , rdc.z );
			Vector2 bottomPoint = new Vector2(ldc.y , ldc.z );
			Vector2 topPoint = new Vector2(luc.y , luc.z );

			// Compute the vectors on the 2D horizontal plane
			Vector2 right2head = headHProj - rightPoint;
			Vector2 right2left = leftPoint - rightPoint;
			Vector2 right2left_unit = right2left.normalized;

			// Compute the vectors on the 2D vertical plane
			Vector2 top2head = headVProj - topPoint;
			Vector2 top2bottom = bottomPoint - topPoint;
			Vector2 top2bottom_unit = top2bottom.normalized;

			// Compute the orthogonal projection of the head on the screen on the horizontal plane
			// and then compute the position of the left and right according to this projection
			float right = Vector2.Dot(right2head, right2left_unit);
			float left = right - right2left.magnitude;

			// Compute the distance of the head to the screen on the horizontal plane
			float horizontalDist = Mathf.Sqrt(right2head.sqrMagnitude - Mathf.Pow(right, 2));

			// Compute the orthogonal projection of the head on the screen on the vertical plane
			// and then compute the position of the top and bottom according to this projection
			float top = Vector2.Dot(top2head, top2bottom_unit);
			float bottom = top - top2bottom.magnitude;

			// Compute the distance of the head to the screen on the vertical plane
			float verticalDist = Mathf.Sqrt(top2head.sqrMagnitude - Mathf.Pow(top, 2));

			// Adjust the position according to the near plane (the position was the position of the
			// screen corners, so modify it in order to have the position of the near plane corners)
			left = nearPlane * left / horizontalDist;
			right = nearPlane * right / horizontalDist;
			bottom = nearPlane * bottom / verticalDist;
			top = nearPlane * top / verticalDist;

			// Compute the projection matrix
			cam.projectionMatrix = PerspectiveOffCenter(left, right, bottom, top, nearPlane, farPlane);
		}
		else
		{
			Debug.Log("CAN'T FIND HEAD");
		}

	}


}
