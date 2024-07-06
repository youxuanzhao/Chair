using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.UI;
using UnityEngine;

public class InteractiveObject : MonoBehaviour {
    private bool isPickedUp = false;
    private GameObject originalParent;

    public void PickUp(GameObject newParent) {
        if (isPickedUp) return;

        isPickedUp = true;
        transform.localPosition = new Vector3(0, 0, 1); // Position the object in front of the player
        transform.localRotation = Quaternion.identity;
        GetComponent<Rigidbody>().isKinematic = true;
    }

    public void PutDown(Vector3 dropPosition) {
        if (!isPickedUp) return;

        isPickedUp = false;
        transform.localPosition = new Vector3(0,0,0);
        transform.position = dropPosition;
        GetComponent<Rigidbody>().isKinematic = false;
    }
}
