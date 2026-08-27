using UnityEngine;

public class SkillTreeButton : MonoBehaviour
{
    [SerializeField] private SkillType skillType;
    [SerializeField] private string skillName;
    [TextArea(3, 10)] [SerializeField] private string skillDescription;

    public SkillType SkillType => skillType;
    public string SkillName => skillName;
    public string SkillDescription => skillDescription;

    public GameObject selectedFrame;

    private SkillTree skillTree;


    void Awake()
    {
        skillTree = GetComponentInParent<SkillTree>();
    }

    // Select the skill button
    public void OnClick()
    {
        if(skillTree.ButtonSelected != this)
        {
            skillTree.SelectButton(this);
        }
        else
        {
            skillTree.UpgradeSkill(skillType);
        }
    }

    // Show and hide the selected button frame
    public void SetSelected(bool selected)
    {
        selectedFrame.SetActive(selected);
    }
}
