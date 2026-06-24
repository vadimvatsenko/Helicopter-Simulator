using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class OpenTheDoor : MonoBehaviour
{
    private static readonly int Open = Animator.StringToHash("open");
    private Animator animator;
    private XRGrabInteractable grabInteractable;

    private void Start()
    {
        animator = GetComponent<Animator>();
        grabInteractable = GetComponent<XRGrabInteractable>();
    }

    public void OpenDoor()
    {
        Debug.Log("Open the Door");
        animator.SetTrigger(Open);
    }
}
