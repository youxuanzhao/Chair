using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor.UI;
using UnityEngine;

public class InteractiveObject : MonoBehaviour {
    private bool isPickedUp = false;
    private Transform originalParent;

    

    public void PickUp(Transform newParent) {
        if (isPickedUp) return;
        originalParent = transform.parent;
        transform.parent = newParent;
        isPickedUp = true;
        transform.localPosition = new Vector3(0, 0, 1); // Position the object in front of the player
        transform.localRotation = Quaternion.identity;
        GetComponent<Rigidbody>().isKinematic = true;
    }

    public void PutDown(Vector3 dropPosition) {
        if (!isPickedUp) return;
        transform.parent = originalParent;
        isPickedUp = false;
        transform.localPosition = new Vector3(0,0,0);
        transform.position = dropPosition;
        GetComponent<Rigidbody>().isKinematic = false;
    }
}
