using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum ProjectileType 
{ 
    Linear,
    RightParabolic,
    UPParabolic,
    Multiply,
    Boomerang,
    Count
}


public class Week5Player : MonoBehaviour
{
    public Projectile projectile;
    public Transform target;
    public TextMeshProUGUI currentText;

    public List<Projectile> projectiles;

    // Start is called before the first frame update
    void Start()
    {
        LinearClick();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            projectile.gameObject.SetActive(true);
            projectile.Play(transform, target);
        }
    }

    public void LinearClick()
    {
        projectile = projectiles[(int)ProjectileType.Linear];
        currentText.text = $"Current : {ProjectileType.Linear}";
    }

    public void RightParabolicClick()
    {
        projectile = projectiles[(int)ProjectileType.RightParabolic];
        currentText.text = $"Current : {ProjectileType.RightParabolic}";
    }

    public void UpParabolcClick()
    {
        projectile = projectiles[(int)ProjectileType.UPParabolic];
        currentText.text = $"Current : {ProjectileType.UPParabolic}";
    }

    public void MultiplyClick()
    {
        projectile = projectiles[(int)ProjectileType.Multiply];
        currentText.text = $"Current : {ProjectileType.Multiply}";
    }

    public void BoomerangClick()
    {
        projectile = projectiles[(int)ProjectileType.Boomerang];
        currentText.text = $"Current : {ProjectileType.Boomerang}";
    }
} 
