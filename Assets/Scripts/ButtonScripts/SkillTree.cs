using UnityEngine;

public class SkillTree : MonoBehaviour
{
    [SerializeField] private StatsDisplay statsDisplay;
    [SerializeField] private PlayerStats playerStats;

    private SkillTreeButton buttonSelected;


    // Select a skill button and display its current state
    public void SelectButton(SkillTreeButton button)
    {
        if(buttonSelected != null)
        {
            buttonSelected.SetSelected(false);
        }

        buttonSelected = button;
        buttonSelected.SetSelected(true);

        int level = playerStats.GetSkillLevel(button.SkillType);
        int dmg = playerStats.GetSkillIntValue(button.SkillType);
        float spd = playerStats.GetSkillFloatValue(button.SkillType);
        float interval = playerStats.GetSkillFloatValue(button.SkillType);
        int crit = playerStats.GetSkillIntValue(button.SkillType);

        statsDisplay.ShowSkill(button.SkillName, button.SkillDescription, level, dmg, spd, interval, crit);
    }
}
