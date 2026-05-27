using UnityEngine;

public class CanonDrag : MonoBehaviour
{
    [SerializeField] private Renderer _renderer;
    public void SetUpCanon(int index)
    {
        SetUpVisuals(index);
    }
    private void SetUpVisuals(int index)
    {
        _renderer.material.color = LevelRulesManager.instance.GetLevelRules().levelColors[index];
    }


}
