using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallIconHolder : MonoBehaviour
{
    private static BallIconHolder Instance;

    [SerializeField] BallIconLevelSet Unthrown;
    [SerializeField] BallIconLevelSet Silver;
    [SerializeField] BallIconLevelSet Gold;
    [SerializeField] BallIconLevelSet Miss;

    private void Awake()
    {
        Instance = this;
    }

    public Sprite GetIconInternal(BallResult result, int tauntLevel)
    {
        switch (result)
        {
            case BallResult.UNTHROWN:
                return Unthrown.GetIcon(tauntLevel);
            case BallResult.SILVER:
                return Silver.GetIcon(tauntLevel);
            case BallResult.GOLD:
                return Gold.GetIcon(tauntLevel);
            case BallResult.MISS:
                return Miss.GetIcon(tauntLevel);
        }
        return null;
    }

    public static Sprite GetIcon(BallResult result, int tauntLevel) => Instance.GetIconInternal(result, tauntLevel);

    [System.Serializable]
    public class BallIconLevelSet
    {
        [SerializeField] private Sprite Taunt0;
        [SerializeField] private Sprite Taunt1;
        [SerializeField] private Sprite Taunt2;
        [SerializeField] private Sprite Taunt3;

        public Sprite GetIcon(int tauntLevel)
        {
            switch (tauntLevel)
            {
                case 0:
                    return Taunt0;
                case 1:
                    return Taunt1;
                case 2:
                    return Taunt2;
                case 3:
                    return Taunt3;
            }
            return null;
        }
    }
}

[System.Serializable]
public enum BallResult
{
    UNTHROWN,
    SILVER,
    GOLD,
    MISS
}
