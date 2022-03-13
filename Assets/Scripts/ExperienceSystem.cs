using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperienceSystem : MonoBehaviour
{
    private int level;
    private int experience;
    private int experienceToNextLevel;


    public ExperienceSystem()
    {
        level = 0;
        experience = 0;
        experienceToNextLevel = 100;
    }
    public void AddExperience(int amount)
    {
        experience += amount;
        if(experience >= experienceToNextLevel){
            level += 1;
            experience -= experienceToNextLevel;
        }
    }
}
