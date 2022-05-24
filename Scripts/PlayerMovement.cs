using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public Vector3 _cameraOffset;
    
    private Vector3 _playerDirection;
    private CharacterController _characterController;
    
    public float playerForwardSpeed;
    public float playerHorizontalIntensity;
    public float playerVerticalSpeed;

    private SwipeController _swipeController;
    public bool _stopCameraFollow;

    private GameObject[] girls;
    private CarStacking carStacking;
    
    
    private void Awake()
    {
        playerForwardSpeed = 6.25f;
        playerHorizontalIntensity = 0.06f;
        playerVerticalSpeed = -20; // for gravity , constant value

        _characterController = GetComponent<CharacterController>();
        carStacking = FindObjectOfType<CarStacking>();
        _swipeController = FindObjectOfType<SwipeController>();
    }

    private void Start()
    {
        girls= GameObject.FindGameObjectsWithTag("Girl");
    }

    private void Update()
    {
        
        if (CanvasController.IsGameStarted)
        {
            //General Movement Formula
            _playerDirection = new Vector3(playerHorizontalIntensity * _swipeController.deltaX,
                playerVerticalSpeed, playerForwardSpeed) * Time.deltaTime;

            _characterController.Move(_playerDirection);
            
            //Character Rotation
            transform.rotation = Quaternion.Slerp(transform.rotation,
                Quaternion.LookRotation(new Vector3(_swipeController.deltaX / 5, 0, 20)), Time.deltaTime * 20);

            //Clamping => You can use your special values to clamp character movement
            float x_pos = transform.position.x;
            x_pos = Mathf.Clamp(x_pos, -2.15f, 9f);
            transform.position = new Vector3(x_pos, transform.position.y, transform.position.z);
            //Camera Controller
            if (!_stopCameraFollow)
            {
                Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position,
                    transform.position + _cameraOffset, Time.deltaTime * 5);
            }
            GirlDistanceController();
        }
    }

    private void GirlDistanceController()
    {
            foreach (GameObject girl in girls)
            {
                if (Vector3.Distance(transform.position, girl.transform.position) < 13)
                {
                    if (carStacking.GetActiveCarIndex() < 2)
                    {
                        girl.GetComponent<GirlAnimation>().Laugh();
                        ParticleSystem laugh = girl.transform.GetChild(0).GetComponent<ParticleSystem>();
                        if (!laugh.isPlaying)
                        {
                            laugh.Play();
                        }
                    }
                    else
                    {
                        girl.GetComponent<GirlAnimation>().Love();
                        ParticleSystem love = girl.transform.GetChild(1).GetComponent<ParticleSystem>();
                        if (!love.isPlaying)
                        {
                            love.Play();
                        }
                    }
                }
            }
    }
    
}
