//Evan Beers 2025/02/13
//Trigger spawn via button input
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;//xr input handling
using UnityEngine.InputSystem.XR;//specifically for meta quest controller layout

public class ObjectSpawner : MonoBehaviour
{

    public GameObject objectPrefab;//The spawning obj
    public Transform spawnPoint;//where it spawns
    public XRNode controllerNode = XRNode.RightHand;//using right controller
    public float spawnCooldown = 1f;// time between spawns

    private bool canSpawn = true;
    // Update is called once per frame
    void Update()
    {
        if (canSpawn && IsAButtonPressed())
        {
            StartCoroutine(SpawnObjectWithCooldown());
        }
    }

    bool IsAButtonPressed()
    {
        InputDevice device = InputDevices.GetDeviceAtXRNode(controllerNode);
        bool buttonPressed = false;
        if(device.TryGetFeatureValue(CommonUsages.primaryButton, out buttonPressed) && buttonPressed)
        {
            return true;
        }
        return false;
    }

    IEnumerator SpawnObjectWithCooldown()
    {
        canSpawn = false;
        SpawnObject();
        yield return new WaitForSeconds(spawnCooldown);
        canSpawn = true;
    }

    void SpawnObject()
    {
        if (objectPrefab != null && spawnPoint != null)
        {
            GameObject spawnedObject = Instantiate(objectPrefab, spawnPoint.position, spawnPoint.rotation);
        }
        else
        {
            Debug.Log("Spawn object function broke");
        }
    }
}
