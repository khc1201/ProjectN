using UnityEngine;

public class ClickMove : MonoBehaviour {
    public int NextInt;
    private CameraController cameraController;
	void Start ()
    {
        cameraController = GameObject.FindGameObjectWithTag("CameraController").GetComponent<CameraController>();
	}
    public void OnClick()
    {
        cameraController.OnMoveToView(NextInt);
    }
	
}
