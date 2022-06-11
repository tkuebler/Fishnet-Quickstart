using FishNet;
using FishNet.Object;
using FishNet.Object.Prediction;
using UnityEngine;
using UnityEngine.InputSystem;

namespace FishNetQuckstart.Advanced
{
    [RequireComponent(typeof(CharacterController))]
    [RequireComponent(typeof(PlayerInput))]
    public class PlayerInputDriver : NetworkBehaviour
    {
        #region Types.

        public struct MoveInputData
        {
            public Vector2 moveVector;
            public bool jump;
            public bool grounded;
        }

        public struct ReconcileData
        {
            public Vector3 Position;
            public Quaternion Rotation;
            public ReconcileData(Vector3 position, Quaternion rotation)
            {
                Position = position;
                Rotation = rotation;
            }
        }

        #endregion

        #region Fields

        private CharacterController _characterController;
        private Vector2 _moveInput;
        private bool _jump;
        [SerializeField] public float jumpSpeed = 6f;
        [SerializeField] public float speed = 8f;
        [SerializeField] public float gravity = -9.8f; // negative acceleration in y - remember physics?

        #endregion

        private void Start()
        {
            InstanceFinder.TimeManager.OnTick += TimeManager_OnTick; // Could also be in Awake
            _characterController = GetComponent(typeof(CharacterController)) as CharacterController;
            _jump = false;
        }
        public override void OnStartClient()
        {
            base.OnStartClient();
        }
        private void OnDestroy()
        {
            if (InstanceFinder.TimeManager != null)
                InstanceFinder.TimeManager.OnTick -= TimeManager_OnTick;
        }

        #region Movement Processing

        private void GetInputData(out MoveInputData moveData)
        {
            moveData = new MoveInputData
            {
                jump = _jump, 
                grounded = _characterController.isGrounded, 
                moveVector = _moveInput
            };
        }
        private void TimeManager_OnTick()
        {
            if (base.IsOwner)
            {
                Reconciliation(default, false);
                GetInputData(out MoveInputData md);
                Move(md, false);
            }

            if (base.IsServer)
            {
                Move(default, true);
                ReconcileData rd = new ReconcileData(transform.position, transform.rotation);
                Reconciliation(rd, true);
            }
        }

        #endregion

        #region Prediction Callbacks

        [Replicate]
        private void Move(MoveInputData md, bool asServer, bool replaying = false)
        {
            Vector3 move = new Vector3();
            if (md.grounded)
            {
                move.x = md.moveVector.x;
                move.y = gravity;
                move.z = md.moveVector.y;
                if (md.jump)
                {
                    move.y = jumpSpeed;
                }
            }
            else
            {
                move.x = md.moveVector.x;
                move.z = md.moveVector.y;
            }
            move.y += gravity * (float)base.TimeManager.TickDelta; // gravity is negative...
            _characterController.Move(move * speed * (float)base.TimeManager.TickDelta);
        }

        [Reconcile]
        private void Reconciliation(ReconcileData rd, bool asServer)
        {
            transform.position = rd.Position;
            transform.rotation = rd.Rotation;
        }

        #endregion

        #region UnityEventCallbacks

        public void OnMovement(InputAction.CallbackContext context)
        {
            if (!base.IsOwner)
                return;
            _moveInput = context.ReadValue<Vector2>();
        }
        public void OnJump(InputAction.CallbackContext context)
        {
            if (!base.IsOwner)
                return;
            if (context.started || context.performed)
            {
                _jump = true;
            }
            else if (context.canceled )
            {
                _jump = false;
            }
        }

        #endregion
    }
}
