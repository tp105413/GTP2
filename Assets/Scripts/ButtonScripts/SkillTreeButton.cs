using UnityEngine;

public class SkillTreeButton : MonoBehaviour
{
    [SerializeField] private SkillType skillType;
    [SerializeField] private string skillName;
    [TextArea(3, 10)] [SerializeField] private string skillDescription;
    [SerializeField] private bool isSkill;

    public SkillType SkillType => skillType;
    public string SkillName => skillName;
    public string SkillDescription => skillDescription;
    public bool IsSkill => isSkill;

    public GameObject selectedFrame;
    public GameObject lockedOverlay;

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

    // Hide the locked overlay when the skill is unlocked
    public void UnlockButton()
    {
        lockedOverlay.SetActive(false);
    }
}
