using Framework.Core.VariableTypes.Bool;
using Framework.Core.VariableTypes.Float;
using UnityEngine;

namespace Game
{
    public class PlayerController : MonoBehaviour
    {

        [SerializeField]
        private BoolVariableRef _gamePause;
        [SerializeField]
        private CharacterController _characterController;

        [SerializeField]
        private Transform _handsTransform;

        [SerializeField]
        private Transform _bodyTransform;

        [SerializeField]
        private FloatVariableRef _moveSpeedRef;
        [SerializeField]
        private FloatVariableRef _jumpHeightRef;
        [SerializeField]
        private FloatVariableRef _gravityRef;

        private float _moveSpeed => _moveSpeedRef.GetValue();

        private float _jumpForce = 0f;


        private float _gravity => _gravityRef.GetValue();

        [SerializeField]
        private float _mouseSensetivityMod = 1f;

        private Vector3 _direction = Vector3.zero;
        float _handsPitch = 0f;
        private bool _isPaused;

        void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        private void OnEnable()
        {
            _jumpForce = RecalculateJumpForce(_jumpHeightRef.GetValue());
            _jumpHeightRef.Changed += JumpChanged;
            _gamePause.Changed += OnPause;
        }

        private void OnPause(bool value)
        {
            SetPause(value);
        }

        private void OnDisable()
        {
            _jumpHeightRef.Changed -= JumpChanged;
            _gamePause.Changed -= OnPause;

        }

        private void JumpChanged(float value)
        {
            _jumpForce = RecalculateJumpForce(value);
        }



        public void SetPause(bool pause)
        {
            _isPaused = pause;
            if (_isPaused)
            {
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
        }

        private float RecalculateJumpForce(float jumpHeight)
        {
            return Mathf.Sqrt(2 * jumpHeight * _gravity);
        }

        // Update is called once per frame
        void Update()
        {
            if (_isPaused)
            {
                return;
            }
            var deltaTime = Time.deltaTime;


            var mouseX = Input.GetAxis(InputConstants.MOUSE_X) * _mouseSensetivityMod;
            var mouseY = Input.GetAxis(InputConstants.MOUSE_Y) * _mouseSensetivityMod;
            _handsPitch -= mouseY;
            _handsPitch = Mathf.Clamp(_handsPitch, -85f, 85f);

            _handsTransform.localRotation = Quaternion.Euler(_handsPitch, 0f, 0f);
            _bodyTransform.Rotate(Vector3.up * mouseX);


            if (_characterController.isGrounded)
            {
                var x = Input.GetAxis(InputConstants.HORIZONTALAl);
                var z = Input.GetAxis(InputConstants.VERTICAl);
                _direction = _bodyTransform.right * x + _bodyTransform.forward * z;
                _direction.Normalize();
                _direction *= _moveSpeed;
                if (Input.GetButton(InputConstants.JUMP))
                {
                    _direction.y += _jumpForce;
                }

            }

            _direction.y -= _gravity * deltaTime;
            _characterController.Move(_direction * deltaTime);
        }
    }
}
