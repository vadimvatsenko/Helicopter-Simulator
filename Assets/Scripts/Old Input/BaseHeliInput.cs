using UnityEngine;

namespace Old_Input
{
    public class BaseHeliInput : MonoBehaviour
    {
        [Header("Base Input Properties")]
        protected float VerticalInput = 0f;
        protected float HorizontalInput = 0f;

        public void Update()
        {
            HandleInput();
        }

        protected virtual void HandleInput()
        {
            VerticalInput = Input.GetAxis("Vertical");
            HorizontalInput = Input.GetAxis("Horizontal");
        }
    }
}
