using System.IO;
using System.Linq;
using UnityEngine;
using SQLite4Unity3d;

public class DatabaseManager : MonoBehaviour
{
    public static DatabaseManager Instance { get; private set; }

    public static string DbPath => Path.Combine(Application.persistentDataPath, "game.db");
    private SQLiteConnection db;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeDatabase();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void InitializeDatabase()
    {
        bool crearTablas = !File.Exists(DbPath);

        try
        {
            db = new SQLiteConnection(DbPath, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create);

            if (crearTablas)
            {
                db.CreateTable<PlayerDB>();
                db.CreateTable<WeaponDB>();
                db.CreateTable<SpellDB>();
                db.CreateTable<PassiveDB>();
                db.CreateTable<PetDB>();
                db.CreateTable<PlayerWeaponDB>();
                db.CreateTable<PlayerSpellDB>();
                db.CreateTable<PlayerPassiveDB>();
                db.CreateTable<PlayerPetDB>();

                Debug.Log("Tablas de base de datos creadas correctamente.");
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError("Error al inicializar la base de datos: " + e.Message);
        }
    }


    #region Métodos Jugador
    public void GuardarJugador(PlayerDB player)
    {
        db.InsertOrReplace(player);
    }

    public PlayerDB CargarJugador(string nombre)
    {
        return db.Table<PlayerDB>()
                 .Where(p => p.Nombre == nombre)
                 .FirstOrDefault();
    }
    #endregion

    #region Métodos Armas, Hechizos, Pasivas, Mascotas
    public void GuardarArma(WeaponDB weapon) => db.InsertOrReplace(weapon);
    public WeaponDB CargarArma(int id) => db.Find<WeaponDB>(id);

    public void GuardarHechizo(SpellDB spell) => db.InsertOrReplace(spell);
    public SpellDB CargarHechizo(int id) => db.Find<SpellDB>(id);

    public void GuardarPasiva(PassiveDB passive) => db.InsertOrReplace(passive);
    public PassiveDB CargarPasiva(int id) => db.Find<PassiveDB>(id);

    public void GuardarMascota(PetDB pet) => db.InsertOrReplace(pet);
    public PetDB CargarMascota(int id) => db.Find<PetDB>(id);
    #endregion
}

