using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelInit : MonoBehaviour
{
    public Vector2Int textureSpritesDimensions = new Vector2Int(8, 6);
    public int spriteSize = 32;
    public Vector2 pivot = new Vector2(0.5f, 0.5f);
    public GameObject[][] objects;

    public string text = "Hello";

    public float updateSpriteRate = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        try
        {
            var sprites = LoadSprites("Textures");
            var spriteMap = MapSpritesAndSymbols(sprites);
            var charMap = LoadMapFromText("map");
            this.objects = BuildMap(charMap, spriteMap);

            print("Success!!!");
        }
        catch (System.Exception ex)
        {
            print(ex.Message);
            print(ex.StackTrace);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public char[][] LoadMapFromText(string path) {
        var lines = Resources.Load<TextAsset>(path).text.Split('\n');
        char[][] map = new char[lines.Length][];

        for (int i = 0; i < lines.Length; i++)
        {
            map[i] = lines[i].ToCharArray();
        }

        return map;
    }

    public GameObject[][] BuildMap(char[][] charMap, Dictionary<char, Sprite> charSpriteMap) {
        var o = new GameObject[charMap.Length][];
        var size = new Vector2Int(charMap[0].Length, charMap.Length);

        for (int y = 0; y < charMap.Length; y++)
        {
            o[y] = new GameObject[charMap[y].Length];

            for (int x = 0; x < charMap[y].Length; x++)
            {
                var pos = new Vector3(x - size.x / 2, y - size.y / 2, Vector3.zero.z);
                o[y][x] = this.CreateSpriteObject($"{x}*{y}", charSpriteMap[charMap[y][x]], pos);
            }
        }

        return o;
    }

    public Sprite[] LoadSprites(string path) {
        var texture2D = Resources.Load<Texture2D>(path);

        if (texture2D == null) {
            throw new System.Exception("Texture not loaded");
        }

        var sprites = new Sprite[textureSpritesDimensions.x * textureSpritesDimensions.y];

        for (int i = 0, c = 0; i < textureSpritesDimensions.y; ++i) {
            for (int j = 0; j < textureSpritesDimensions.x; ++j, ++c)
            {
                sprites[c] = Sprite.Create(
                    texture2D,
                    new Rect(spriteSize * j, spriteSize * i, spriteSize, spriteSize),
                    pivot
                );
            }
        }

        return sprites;
    }

    public GameObject CreateSpriteObject(string name, Sprite sprite, Vector3 pos) {
        var obj = new GameObject(name);
        var rend = obj.AddComponent(typeof(SpriteRenderer)) as SpriteRenderer;

        rend.sprite = sprite;
        rend.transform.localScale = new Vector3(3f, 3f, 3f);
        rend.transform.position = pos;
        return obj;
    }

    public Dictionary<char, Sprite> MapSpritesAndSymbols(Sprite[] sprites) {
        var table = new Dictionary<char, Sprite>();

        table.Add('A', sprites[0]);
        table.Add('B', sprites[1]);
        table.Add('C', sprites[2]);
        table.Add('D', sprites[3]);

        return table;
    }
}
