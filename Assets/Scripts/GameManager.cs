using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ObjectTable
{
    public string Type { get; set; }
    public float Px { get; set; }
    public float Py { get; set; }
    public float Pz { get; set; }
    public float Qx { get; set; }
    public float Qy { get; set; }
    public float Qz { get; set; }
    public float Qw { get; set; }
}

public class PlayerTable : ObjectTable
{
    public int Gold { get; set; }
    public int Kills { get; set; }
    public int Ammo { get; set; }
    public float Hp { get; set; }
    public string WeaponType { get; set; }
    public bool Weapon { get; set; }
}

public class EnemyTable : ObjectTable
{
    public float Hp { get; set; }
}

public class SceneTable
{
    public string Name { get; set; }
    public int Id { get; set; }
}

[System.Serializable]
public class PrefabType
{
    public string type;
    public GameObject prefab;
}

public class GameManager : MonoBehaviour
{
    private int playerKills = 0;
    private GameObject mainMenu;
    private AsyncOperation async;
    private static GameManager instance;

    private bool loadSavedGame = false;
    private string savedGamePath;

    public PrefabType[] prefabByType;

    public int PlayerKills
    {
        get { return playerKills; }
        set
        {
            playerKills = value;
            EventObserver.Instance.Notify(ObservableEvents.KillsUpdate, gameObject, value);
        }
    }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        mainMenu = GameObject.Find("MainMenu");
        if (instance == null)
            instance = gameObject.GetComponent<GameManager>();
    }

    public static void SaveGame()
    {
        string dirPath = Application.dataPath + "/SavedGames";

        if (!System.IO.Directory.Exists(dirPath))
            System.IO.Directory.CreateDirectory(dirPath);
        
        string saveFilePath = dirPath + "/" + "savefile_" + 
            DateTime.Now.ToOADate() + ".save";

        using (var conn = new SQLite.SQLiteConnection(saveFilePath))
        {
            conn.CreateTable<PlayerTable>();
            conn.CreateTable<EnemyTable>();
            conn.CreateTable<ObjectTable>();
            conn.CreateTable<SceneTable>();

            List<GameObject> gameObjects = new List<GameObject>();
            gameObjects.Add(GameObject.FindGameObjectWithTag("Player"));
            gameObjects.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));
            gameObjects.AddRange(GameObject.FindGameObjectsWithTag("Coin"));
            gameObjects.AddRange(GameObject.FindGameObjectsWithTag("HealthPack"));
            gameObjects.AddRange(GameObject.FindGameObjectsWithTag("Ammo"));
            gameObjects.Add(GameObject.FindGameObjectWithTag("Pistol"));

            foreach (var item in gameObjects)
            {
                string type = item.tag;

                if (type == "Player")
                {
                    WeaponController wc = item.GetComponent<WeaponController>();
                    Inventory iv = item.GetComponent<Inventory>();
                    Transform t = item.GetComponent<Transform>();
                    Health hp = item.GetComponent<Health>();
                    PlayerTable pt = new PlayerTable();

                    if (wc.MainWeapon != null)
                    {
                        pt.Ammo = wc.MainWeapon.GetAmmo();
                        pt.WeaponType = "Pistol";
                        pt.Weapon = true;
                    }
                    else
                    {
                        pt.Weapon = false;
                    }

                    pt.Kills = instance.PlayerKills;
                    pt.Gold = iv.Gold;
                    pt.Type = type;
                    pt.Hp = hp.HP;
                    pt.Px = t.position.x;
                    pt.Py = t.position.y;
                    pt.Pz = t.position.z;
                    pt.Qx = t.rotation.x;
                    pt.Qy = t.rotation.y;
                    pt.Qz = t.rotation.z;
                    pt.Qw = t.rotation.w;

                    conn.Insert(pt);
                }

                if (type == "Enemy")
                {
                    EnemyTable et = new EnemyTable();
                    Health hp = item.GetComponent<Health>();
                    Transform t = item.GetComponent<Transform>();
                    et.Type = type;
                    et.Hp = hp.HP;
                    et.Px = t.position.x;
                    et.Py = t.position.y;
                    et.Pz = t.position.z;
                    et.Qx = t.rotation.x;
                    et.Qy = t.rotation.y;
                    et.Qz = t.rotation.z;
                    et.Qw = t.rotation.w;

                    conn.Insert(et);
                }

                if (type != "")
                {
                    Transform t = item.GetComponent<Transform>();
                    ObjectTable ot = new ObjectTable();
                    ot.Type = type;
                    ot.Px = t.position.x;
                    ot.Py = t.position.y;
                    ot.Pz = t.position.z;
                    ot.Qx = t.rotation.x;
                    ot.Qy = t.rotation.y;
                    ot.Qz = t.rotation.z;
                    ot.Qw = t.rotation.w;

                    conn.Insert(ot);
                }
            }

            SceneTable st = new SceneTable();
            var active = UnityEngine.SceneManagement.SceneManager.GetActiveScene();
            st.Id = active.buildIndex;
            st.Name = active.name;
            conn.Insert(st);
        }
    }

    private void CleanLevel()
    {
        List<GameObject> gameObjects = new List<GameObject>();
        gameObjects.Add(GameObject.FindGameObjectWithTag("Player"));
        gameObjects.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));
        gameObjects.AddRange(GameObject.FindGameObjectsWithTag("Coin"));
        gameObjects.AddRange(GameObject.FindGameObjectsWithTag("HealthPack"));
        gameObjects.AddRange(GameObject.FindGameObjectsWithTag("Ammo"));
        gameObjects.Add(GameObject.FindGameObjectWithTag("Pistol"));

        foreach (var item in gameObjects)
        {
            Destroy(item);
        }
    }

    public static void LoadSavedGame(string path)
    {
        if (instance.mainMenu != null)
            instance.mainMenu.SetActive(false);

        using (var conn = new SQLite.SQLiteConnection(path))
        {
            var sceneQuery = conn.Table<SceneTable>();
            instance.loadSavedGame = true;
            instance.savedGamePath = path;
            LoadLevel(sceneQuery.First().Id);
        }
    }

    private void LoadRestOfTheGame(string path)
    {
        CleanLevel();

        using (var conn = new SQLite.SQLiteConnection(path))
        {
            var playerQuery = conn.Table<PlayerTable>();
            InstantiatePlayer(playerQuery.First());

            var enemyQuery = conn.Table<EnemyTable>();
            foreach (var item in enemyQuery)
            {
                InstantiateEnemy(item);
            }

            var objQuery = conn.Table<ObjectTable>();
            foreach (var item in objQuery)
            {
                InstantiateObject(item);
            }
        }
    }

    private void InstantiatePlayer(PlayerTable pt)
    {
        var player = InstantiatePrefabByType(pt.Type);
        Vector3 newPos = new Vector3(pt.Px, pt.Py, pt.Pz);
        Quaternion newQuat = new Quaternion(pt.Qx, pt.Qy, pt.Qz, pt.Qw);
        if (pt.Weapon)
        {
            var pistol = InstantiatePrefabByType(pt.WeaponType);
            pistol.transform.position = newPos;
            pistol.GetComponent<Pistol>().SetAmmo(pt.Ammo);
        }
        PlayerKills = pt.Kills;
        player.GetComponent<Health>().HP = pt.Hp;
        player.GetComponent<Inventory>().Gold = pt.Gold;
        player.transform.position = newPos;
        player.transform.rotation = newQuat;
    }

    private void InstantiateEnemy(EnemyTable et)
    {
        var enemy = InstantiatePrefabByType(et.Type);
        Vector3 newPos = new Vector3(et.Px, et.Py, et.Pz);
        Quaternion newQuat = new Quaternion(et.Qx, et.Qy, et.Qz, et.Qw);
        enemy.GetComponent<Health>().HP = et.Hp;
        enemy.transform.position = newPos;
        enemy.transform.rotation = newQuat;
    }

    private void InstantiateObject(ObjectTable ot)
    {
        var obj = InstantiatePrefabByType(ot.Type);
        Vector3 newPos = new Vector3(ot.Px, ot.Py, ot.Pz);
        Quaternion newQuat = new Quaternion(ot.Qx, ot.Qy, ot.Qz, ot.Qw);
        obj.transform.position = newPos;
        obj.transform.rotation = newQuat;
    }

    private GameObject InstantiatePrefabByType(string type)
    {
        foreach (var item in prefabByType)
        {
            if (item.type == type)
            {
                return Instantiate(item.prefab);
            }
        }

        return null;
    }

    public static void LoadLevel(int index)
    {
        instance.StartCoroutine(instance._LoadLevel(index));
    }

    IEnumerator _LoadLevel(int index)
    {
        LoadingScreen.Show();
        async = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(index);
        async.allowSceneActivation = false;

        while (!async.isDone)
        {
            if (async.progress == 0.9f)
            {
                async.allowSceneActivation = true;

                if (loadSavedGame)
                    StartCoroutine(BeginLoadingTheRestOfTheGame());
                else
                    LoadingScreen.Hide();
            }

            yield return null;
        }
    }

    IEnumerator BeginLoadingTheRestOfTheGame()
    {
        yield return new WaitForSeconds(1f);
        LoadRestOfTheGame(savedGamePath);
        loadSavedGame = false;
        LoadingScreen.Hide();
    }
}
