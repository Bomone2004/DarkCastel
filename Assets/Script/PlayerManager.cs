using System.Collections;
using UnityEditor.Rendering.Universal;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PlayerManager : MonoBehaviour
{
    private static readonly int Speed = Animator.StringToHash("Speed");
    [SerializeField] float speed = 3.0f;
    [SerializeField] float rotateSpeed = 60.0f;
    [SerializeField] Vector3 forward = Vector3.forward;
    [SerializeField] Volume PostProcessvolume;
    [SerializeField] private GameManager gameManager;
    private Vignette vg;

    private bool inivisibilityActive = false;
    public bool InvisibilityActive { get => inivisibilityActive; set => inivisibilityActive = value; }

    //private float curSpeed;

    float _xRotate = 0.0f;

    CharacterController _mCharacterController;
    private Camera _camera;

    [SerializeField] Animator playerAnimator;

    void Start()
    {
        _camera = Camera.main;
        _mCharacterController = gameObject.GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if (Input.GetAxis("Mouse X") != 0)
        {
            transform.Rotate(0, Input.GetAxis("Mouse X") * rotateSpeed * Time.deltaTime, 0);
        }

        if (Input.GetAxis("Mouse Y") != 0)
        {
            _xRotate -= Input.GetAxis("Mouse Y") * rotateSpeed * Time.deltaTime;
            _xRotate = Mathf.Clamp(_xRotate, -60, 60);

            //Debug.Log(_xRotate);

            _camera.transform.localEulerAngles = new Vector3(_xRotate, 0, 0);
        }

        float turbo = Input.GetKey(KeyCode.LeftShift) ? 2:1;
        float curSpeed = speed * Input.GetAxis("Vertical") * turbo;
        Vector3 realForward = transform.TransformDirection(Vector3.forward);

        if (Input.GetButton("Horizontal"))
        {
            realForward += transform.TransformDirection(Vector3.right) * Input.GetAxis("Horizontal");
            //curSpeed = speed * Input.GetAxis("Horizontal") * turbo;
        }
        //else if (Input.GetButton("Vertical"))
        //{
        //    realForward += transform.TransformDirection(Vector3.left) * Input.GetAxis("Vertical");
        //    curSpeed = speed * Input.GetAxis("Vertical") * turbo;
        //}

        realForward = Vector3.ClampMagnitude(realForward, 1);

        _mCharacterController.SimpleMove(realForward * curSpeed);

        playerAnimator.SetFloat(Speed, curSpeed / speed);

        InvisibilityMode();
    }

    private void InvisibilityMode()
    {
        if (Input.GetKey(KeyCode.LeftControl))
        {
            inivisibilityActive = true;
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            gameManager.PlayerDeath();
        }
    }
}
