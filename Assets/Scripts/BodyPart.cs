using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyPart : MonoBehaviour
{
    [SerializeField]
    private BodyType _bodyType;
    [SerializeField]
    private RobotController _robotController;
    public void ApplyDamage(float damage)
    {
        float newDamage = damage * GetMultiply();
        _robotController.ApplyDamage(newDamage);
    }
    private float GetMultiply()
    {
        switch (_bodyType)
        {
            case BodyType.Head:
                return 3;
            case BodyType.Hips:
                return 1;
            case BodyType.Arm:
                return 2;
            case BodyType.Leg:
                return 2;
            case BodyType.Spine:
                return 2;
            case BodyType.Chest:
                return 2;

        }
        return 1;

    }
}
public enum BodyType
{
     Head,
     Hips,
     Arm,
     Leg,
     Spine,
     Chest
}
