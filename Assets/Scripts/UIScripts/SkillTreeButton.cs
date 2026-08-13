using UnityEngine;

public class SkillTreeButton : MonoBehaviour
{
    public GameObject selectedFrame;

    private SkillTree skillTree;

    void Awake()
    {
        skillTree = GetComponentInParent<SkillTree>();
    }

    public void OnClick()
    {
        skillTree.SelectButton(this);
    }

    public void SetSelected(bool selected)
    {
        selectedFrame.SetActive(selected);
    }
}
