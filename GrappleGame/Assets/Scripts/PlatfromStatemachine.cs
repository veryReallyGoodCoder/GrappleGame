using UnityEngine;

public class PlatfromStatemachine : MonoBehaviour
{
    
    public enum PlatformState
    {
        Normal,
        Frozen,
        Molten,
        Dessert
    }

    public Material normalMat, frozenMat, moltenMat, dessertMat;

    private MeshRenderer meshRenderer;
    public PlatformState currentState;

    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    private void Update()
    {
        SetState(currentState);
    }

    public void SetState(PlatformState state)
    {
        switch (currentState)
        {
            case PlatformState.Normal:
                meshRenderer.material = normalMat;
                break;

            case PlatformState.Frozen:
                meshRenderer.material = frozenMat;
                break;
            case PlatformState.Molten:
                meshRenderer.material = moltenMat;
                break;
            case PlatformState.Dessert:
                meshRenderer.material = dessertMat;
                break;
        }
    }
}
