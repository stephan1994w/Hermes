using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;

public class MouseOverlay : MonoBehaviour
{
    [SerializeField] private UILineRenderer line;
    [SerializeField] private RectTransform canvas;
    [SerializeField] private Color color;
    private InputManager inputManager;
    private Camera _camera;
    private Vector3 centrePoint;
    private Vector2 pos;
    private Color tempColor;

    private void Start() {
        Cursor.visible = false;
        _camera = Camera.main;
        inputManager = InputManager.Instance;
        centrePoint = new Vector3(Screen.width/2, Screen.height/2, _camera.nearClipPlane);
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, Input.mousePosition, _camera, out pos);
        tempColor = color;
        tempColor.a = 0f;
    }

    void Update()
    {
        Vector2 movePos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, Input.mousePosition, _camera, out movePos);
        transform.position = canvas.transform.TransformPoint(movePos);
        Vector2 [] points = { Vector2.zero, movePos};
        line.Points = points;



    }

     private Vector3? GetCurrentMousePosition()
     {
         Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
         Plane plane = new Plane(Vector3.forward, Vector3.zero);
 
         float rayDistance;
         if (plane.Raycast(ray, out rayDistance))
         {
            return ray.GetPoint(rayDistance);
         }
         return null;
     }

    
    // private void OnEnable() {
    //     playerControls.Enable();
    // }
    
    // private void OnDisable() {
    //     playerControls.Disable();
    // }
}
