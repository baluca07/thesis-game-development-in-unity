using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementalDamageSystem : MonoBehaviour
{
    private static Dictionary<ElementalDamageType, Dictionary<ElementalDamageType, float>> elementalResistances = new Dictionary<ElementalDamageType, Dictionary<ElementalDamageType, float>>()
    {

        { ElementalDamageType.Fire, new Dictionary<ElementalDamageType, float>()
            {
                { ElementalDamageType.Air, 1.5f },
                { ElementalDamageType.Water, 0.5f }
            }
        },
        { ElementalDamageType.Water, new Dictionary<ElementalDamageType, float>()
            {
                { ElementalDamageType.Fire, 1.5f },
                { ElementalDamageType.Earth, 0.5f }
            }
        },
        { ElementalDamageType.Air, new Dictionary<ElementalDamageType, float>()
            {
                { ElementalDamageType.Earth, 1.5f },
                { ElementalDamageType.Fire, 0.5f }
            }
        },
        { ElementalDamageType.Earth, new Dictionary<ElementalDamageType, float>()
            {
                { ElementalDamageType.Water, 1.5f },
                { ElementalDamageType.Air, 0.5f }
            }
        }
    };

    public static float GetElementalDamageMultiplier(ElementalDamageType attackType, ElementalDamageType targetType)
    {
        if (elementalResistances.ContainsKey(attackType) && elementalResistances[attackType].ContainsKey(targetType))
        {
            return elementalResistances[attackType][targetType];
        }
        return 1f;
    }
}
