using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class HandleXRGrapInteractable : XRGrabInteractable
{
    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);
        StartCoroutine(CalcGrabWhenHandMove(args.interactableObject.transform.parent));
    }

    private IEnumerator CalcGrabWhenHandMove(Transform handTransform)
    {
        while (true)
        {
            Vector3 distance = this.transform.position - handTransform.position;

            if (distance.magnitude > 0.3f)
            {
                this.enabled = false;
                this.enabled = true;
                yield break;
            }
            yield return null;
        }
    }
}
