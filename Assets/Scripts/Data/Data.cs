using UnityEngine.Tilemaps;
using UnityEngine;
using UnityEngine.AddressableAssets;

public interface IFillFromStr
{
    void FillFromString(string str);
}

public class Pair : IFillFromStr
{
    public int v1, v2;

    public void FillFromString(string str)
    {
        var arr = str.Split(',');


        v1 = int.Parse(arr[0]);
        v2 = int.Parse(arr[1]);
    }
}

public class Range : IFillFromStr
{
    public int min, max;
    public void FillFromString(string str)
    {
        var arr = str.Split(',');

        min = int.Parse(arr[0]);
        max = int.Parse(arr[1]);
    }
}


[System.Serializable]
public class NPC
{
    public int Id;
    public string Type;
    public SEX Sex;
    public string Name;
    public string Age;

    public enum SEX
    {
        None,
        M,
        F,
        D
    }
}

[System.Serializable]
public class Member
{
    public int Id;
    public SEX Sex;
    public string Name;
    public int Age;
    public string Birth;
    public int Height;

    public enum SEX
    {
        None,
        M,
        F
    }
}


[System.Serializable]
public class QuestCondition
{
    public int code;
    public QuestConditionType Type;

    public enum QuestConditionType
    {
        XYPosition,
        GoldHave
    }

    public float val1;
    public float val2;
}

[System.Serializable]
public class Tower
{
    int id;
    string name;
    string ToScene;
    bool isOpen;
}

[System.Serializable]
public class Item 
{
    public int id;
    public string name;
    public string description;
    public string tilePath;

    // [Header("Only gameplay")]
    TileBase tile;
    public TileBase Tile
    {
        get
        {
            if (tile == null)
            {
                tile = Addressables.LoadAssetAsync<TileBase>(tilePath).WaitForCompletion();
            }
            return tile;

        }
    }
    public ItemType type;
    public ItemActionType actionType;
    public int maxStackedItems;  // 최대 쌓을 수 있는 아이템 수
    //public Vector2Int range = new Vector2Int(5, 4);

    [Header("Only UI")]
    public bool stackable = true;


    public string imagePath;
    Sprite sprite;
    //[Header("Both")]
    public Sprite Image
    {
        get
        {
            if (sprite == null)
            {
                sprite=Addressables.LoadAssetAsync<Sprite>(imagePath).WaitForCompletion();
            }
            return sprite;

        }
    }

}

public enum ItemType
{
    Block, // 블럭
    Tool, // 도구
    Food, // 음식
    Furniture, // 가구
    Cloth, // 옷
    Resources, // 자원
    Object // 이상에 해당되지 않는 모든 물건

}

public enum ItemActionType
{
    Dig, // 땅파기
    Mine, // 광질
    Grow, // 나무 베기
    Fishing // 낚시 하기
    
}
