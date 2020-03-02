using UnityEngine;
using System;
using System.Xml;
using System.Globalization;
using UnityEngine.Networking;

public struct VRSystemParameters
{
	public Vector3 ldc;
	public Vector3 luc;
	public Vector3 rdc;
	public Vector3 ldcTotal;
	public Vector3 lucTotal;
	public Vector3 rdcTotal;
	public bool disabled;
}


public class VRSystem : MonoBehaviour
{
	public Vector3 luc = new Vector3(-2.965f, 0.9875f, 0);
	public Vector3 ldc = new Vector3(-2.965f, -0.9875f, 0);
	public Vector3 rdc = new Vector3(2.965f, -0.9875f, 0);
	public Vector3 lucTotal = new Vector3(-2.965f, 0.9875f, 0);
	public Vector3 ldcTotal = new Vector3(-2.965f, -0.9875f, 0);
	public Vector3 rdcTotal = new Vector3(2.965f, -0.9875f, 0);
	public bool disabled;
	protected Camera cam;

	void Awake()
	{
	}

	// Use this for initialization
	protected virtual void Start()
	{

		string[] args = Environment.GetCommandLineArgs();

		if (args.Length >= 2 && !Application.isEditor)
		{

			Debug.Log("VRSystem config file: "+args[1]);


			VRSystemParameters parameters;
			try
			{
				string jsonText = System.IO.File.ReadAllText(args[1]);
				parameters = JsonUtility.FromJson<VRSystemParameters>(jsonText);
				ldc = parameters.ldc;
				luc = parameters.luc;
				rdc = parameters.rdc;
				ldcTotal = parameters.ldcTotal;
				lucTotal = parameters.lucTotal;
				rdcTotal = parameters.rdcTotal;
				disabled = parameters.disabled;
			}
			catch (Exception E)
			{
				Debug.Log("first parameter must be a valid json file path");
				Debug.Log(E.Message);
			}

		}
		else
			Debug.Log("Debug: using Unity editor parameters");

		Debug.Log("VRSystem parameters - ldc: "+ldc+" luc "+luc+" rdc "+rdc);
		Debug.Log("VRSystem parameters - ldcTotal: "+ldcTotal+" lucTotal "+lucTotal+" rdcTotal "+rdcTotal);

	}




	// Update is called once per frame
	void Update()
	{
		if (!cam) {
			GameObject cameraObject = this.gameObject;

			if (!cameraObject)
				Debug.Log ("Can't find main camera");
			else
				cam = cameraObject.GetComponent<Camera> ();
		}
	}

	void LateUpdate()
	{
	}

	void OnDestroy()
	{
	}



	// Draw the frustrum in the editor
	void OnDrawGizmosSelected()
	{

	}

	// Compute the projection matrix from the left, right, 
	// http://www.songho.ca/opengl/gl_projectionmatrix.html
	// http://docs.unity3d.com/ScriptReference/Camera-projectionMatrix.html
	protected Matrix4x4 OrthographicOffCenter(float left, float right, float bottom, float top, float near, float far)
	{
		float x = 2.0F  / (right - left);
		float y = 2.0F  / (top - bottom);
		float a = - (right + left) / (right - left);
		float b = - (top + bottom) / (top - bottom);
		float c = -(far + near) / (far - near);
		float d = -(2.0F) / (far - near);
		float e = 1.0F;
		Matrix4x4 m = new Matrix4x4();
		m[0, 0] = x;
		m[0, 1] = 0;
		m[0, 2] = 0;
		m[0, 3] = a;
		m[1, 0] = 0;
		m[1, 1] = y;
		m[1, 2] = 0;
		m[1, 3] = b;
		m[2, 0] = 0;
		m[2, 1] = 0;
		m[2, 2] = d;
		m[2, 3] = c;
		m[3, 0] = 0;
		m[3, 1] = 0;
		m[3, 2] = 0;
		m[3, 3] = e;
		return m;
	}
	
	protected Matrix4x4 PerspectiveOffCenter(float left, float right, float bottom, float top, float near, float far)
	{
		float x = 2.0F * near / (right - left);
		float y = 2.0F * near / (top - bottom);
		float a = (right + left) / (right - left);
		float b = (top + bottom) / (top - bottom);
		float c = -(far + near) / (far - near);
		float d = -(2.0F * far * near) / (far - near);
		float e = -1.0F;
		Matrix4x4 m = new Matrix4x4();
		m[0, 0] = x;
		m[0, 1] = 0;
		m[0, 2] = a;
		m[0, 3] = 0;
		m[1, 0] = 0;
		m[1, 1] = y;
		m[1, 2] = b;
		m[1, 3] = 0;
		m[2, 0] = 0;
		m[2, 1] = 0;
		m[2, 2] = c;
		m[2, 3] = d;
		m[3, 0] = 0;
		m[3, 1] = 0;
		m[3, 2] = e;
		m[3, 3] = 0;
		return m;
	}
}