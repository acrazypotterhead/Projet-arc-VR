using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
public class ArrowSpawner : MonoBehaviour
{
    public GameObject arrow;
    public GameObject notch;

    private XRGrabInteractable _bow;
    private bool _arrowHasBeenNotched = false;
    private GameObject _currentArrow;

    void Start()
    {
        _bow = GetComponentInParent<XRGrabInteractable>();
        PullInteraction.PullActionReleased += NotchIsEmpty;
    }

    private void OnDestroy()
    {
        PullInteraction.PullActionReleased -= NotchIsEmpty;
    }

    void Update()
    {
        // Spawn arrow when the bow is selected and there isn't one notched
        if (_bow.isSelected && !_arrowHasBeenNotched)
        {
            _arrowHasBeenNotched = true;
            StartCoroutine(DelayedSpawn()); // Call coroutine properly
        }

        // Destroy the arrow if the bow is deselected
        if (!_bow.isSelected && _currentArrow != null)
        {
            Destroy(_currentArrow);
            NotchIsEmpty(1f); // Reset arrow notching status
        }
    }

    // Called when the string is released, resets the state of the notch
    private void NotchIsEmpty(float pullAmount)
    {
        _arrowHasBeenNotched = false;
        _currentArrow = null; // Clear the reference to the current arrow
    }

    // Coroutine to delay the arrow spawn
    IEnumerator DelayedSpawn()
    {
        yield return new WaitForSeconds(1f); // Delay for 1 second before spawning

        // Instantiate arrow at the notch position
        _currentArrow = Instantiate(arrow, notch.transform.position, notch.transform.rotation);
        _currentArrow.transform.SetParent(notch.transform); // Attach arrow to the notch
    }
}