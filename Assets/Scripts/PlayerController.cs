using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityTutorial.Manager;

namespace UnityTutorial.PlayerControl
{
    public class PlayerController : MonoBehaviour
    {
        private Rigidbody rb;
        private InputManager inputManager;
        private Animator animator;
        private bool hasAnimator;

        private int xVelhash;
        private int yVelhash;

        [SerializeField] private float moveSpeed = 2f;
        [SerializeField] private float runSpeed = 6f;
        [SerializeField] private float animationBlendSpeed;
        [SerializeField] private Transform cameraRoot;
        [SerializeField] private Transform cameraTransform;
        [SerializeField] private float topClamp = -40f;
        [SerializeField] private float bottomClamp = 70f;
        [SerializeField] private float sensitivity = 21.9f;

        private Vector2 currentVelocity;
        private float xRotation;

        private void Start()
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;

            rb = GetComponent<Rigidbody>();
            inputManager = GetComponent<InputManager>();

            hasAnimator = TryGetComponent(out animator);
            if (hasAnimator)
            {
                xVelhash = Animator.StringToHash("XVelocity");
                yVelhash = Animator.StringToHash("YVelocity");
            }
        }

        private void FixedUpdate()
        {
            Move();
            CamMove();
        }

        private void Move()
        {
            if (!hasAnimator) return;

            float targetSpeed = inputManager.Run ? runSpeed : moveSpeed;
            if (inputManager.Move == Vector2.zero) targetSpeed = 0.1f;

            currentVelocity = Vector2.Lerp(currentVelocity, inputManager.Move * targetSpeed, animationBlendSpeed * Time.deltaTime);

            Vector3 velocityDiff = new Vector3(currentVelocity.x - rb.linearVelocity.x, 0f, currentVelocity.y - rb.linearVelocity.z);
            rb.AddForce(transform.TransformVector(velocityDiff), ForceMode.VelocityChange);

            animator.SetFloat(xVelhash, currentVelocity.x);
            animator.SetFloat(yVelhash, currentVelocity.y);
        }

        private void CamMove()
        {
            if (!hasAnimator) return;

            float xMouse = inputManager.Look.x;
            float yMouse = inputManager.Look.y;

            cameraTransform.position = cameraRoot.position;

            xRotation -= yMouse * sensitivity * Time.deltaTime;
            xRotation = Mathf.Clamp(xRotation, topClamp, bottomClamp);

            cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            transform.Rotate(Vector3.up, xMouse * sensitivity * Time.deltaTime);
        }
    }
}
