using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    private CharacterController characterController;
    private bool isActive = false;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    public void SetActive(bool active)
    {
        isActive = active;
        UpdateHighlight();
    }

    private void UpdateHighlight()
    {
        var renderer = GetComponent<Renderer>();
        if (renderer)
        {
            renderer.material.color = isActive ? Color.green : Color.white;
        }
    }

    void Update()
    {
        if (!isActive) return;

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 move = new Vector3(h, 0, v);
        if (move.magnitude > 1f) move.Normalize();

        characterController.Move(move * moveSpeed * Time.deltaTime);
    }
}