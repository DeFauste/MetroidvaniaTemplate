using System;
using Assets.Scripts.GamePlay.Movement;
using UnityEngine;

namespace Assets.Scripts.GamePlay.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Player : MonoBehaviour, IMove
    {

        #region Checkers
        [SerializeField] private Collider2D _groundCheckerCollider = null;
        [SerializeField] private LayerMask ObstacleLayers;
        #endregion

        #region JUMP
        private bool _isJump = false;
        private float _timeBetweenJump = 0.3f;
        [SerializeField] private float JumpForce;
        #endregion

        #region RUN
        [SerializeField] private float SpeedRun;

        #endregion

        private Rigidbody2D _rb;
        private Vector3 _direction = Vector3.zero;
        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();

        }
        private void Update()
        {
            if (Input.GetKey(KeyCode.D))
            {
                Move(Vector2.right);
            }
            else if (Input.GetKey(KeyCode.A))
            {
                Move(Vector2.left);
            } else
            {
                Move(Vector2.zero);
            }
            if(Input.GetKeyDown(KeyCode.Space))
            {
                if(CanJump()) Jump(Vector2.up);
            }
        }
        private void FixedUpdate()
        {
            Move();
        }
        public void Dash(Vector3 direction) => throw new NotImplementedException();
        public void Jump(Vector3 direction)
        {
            float force = JumpForce;
            if (_rb.velocity.y < 0)
            {
                force -= _rb.velocity.y;
            }
            _rb.AddForce(Vector2.up * force, ForceMode2D.Impulse);
        }
        public void Move(Vector3 direction)
        {
            _direction = direction;
        }
        private void Move()
        {
            _rb.velocity = new Vector2(SpeedRun * _direction.normalized.x, _rb.velocity.y);
        }
        public void Sit() => throw new NotImplementedException();


        #region Meth Checkers

        private bool CanJump()
        {
            if(_groundCheckerCollider != null)
            {
                return _groundCheckerCollider.IsTouchingLayers(ObstacleLayers);
            }
            return false;
        }

        #endregion
    }
}
