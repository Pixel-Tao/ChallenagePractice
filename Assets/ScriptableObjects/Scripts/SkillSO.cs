using UnityEngine;

[CreateAssetMenu(fileName = "SkillSO", menuName = "SkillSO", order = 0)]
public class SkillSO : ScriptableObject
{
  public int id;
  public GameObject projectilePrefab;
  public string displayName;
  public string description;
  public float cooldown;
  public float damage;
  public string skillClassName;
}