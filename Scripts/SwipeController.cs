using System.Collections;
using UnityEngine;

public class SwipeController : MonoBehaviour
{
    private Vector3 firstFingerPos, currentFingerPos;

    public float deltaX;

    private GameObject player;
        
    private bool _rotateReset;
    
    private void Awake()
    {
        player = GameObject.Find("Player");
    }

    private void Update()
    {
        if (!CanvasController.IsGameStarted) return;

        if (_rotateReset )
        {
            if (player == null) return;
            
            player.transform.rotation = Quaternion.Slerp(player.transform.rotation, Quaternion.Euler(0, 0, 0),
                Time.deltaTime * 10);
        }
    }

    private IEnumerator FinishRotate()
    {
        yield return new WaitForSeconds(0.2f);
        _rotateReset = false;
    }

    void LateUpdate()
    {
        if (!CanvasController.IsGameStarted) return;
        if (Input.GetMouseButtonDown(0))
        {
            firstFingerPos = Input.mousePosition;
        }

        if (Input.GetMouseButton(0))
        {
            currentFingerPos = Input.mousePosition;
            
            deltaX = currentFingerPos.x - firstFingerPos.x;
            deltaX = Mathf.Clamp(deltaX, -75, 75);
        }

        if (Input.GetMouseButtonUp(0))
        {
            deltaX = 0;
            _rotateReset = true;
            StartCoroutine(FinishRotate());
        }
    }
    

}