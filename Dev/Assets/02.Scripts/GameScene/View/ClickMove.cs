using UnityEngine;
using UnityEngine.UI;

public class ClickMove : MonoBehaviour {
    public int NextInt;
    private CameraController cameraController;
	void Start ()
    {
        this.GetComponent<Button>().onClick.RemoveAllListeners();
        this.GetComponent<Button>().onClick.AddListener(OnClick);
        cameraController = GameObject.FindGameObjectWithTag("CameraController").GetComponent<CameraController>();
	}
    public void OnClick()
    {
        cameraController.OnMoveToView(NextInt);
    }
	
}
