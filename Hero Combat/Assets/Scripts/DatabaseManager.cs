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

    public bool TieneJugadorGuardado()
    {
        return db.Table<PlayerDB>().Any();
    }

    public string ObtenerNombreJugadorGuardado()
    {
        var jugador = db.Table<PlayerDB>().FirstOrDefault();
        return jugador != null ? jugador.Nombre : null;
    }

    public void BorrarJugador(string jugadorID)
    {
        var jugador = CargarJugador(jugadorID);
        if (jugador != null)
            db.Delete(jugador);

    } }
    #endregion

    #region Métodos Armas, Hechizos
    #endregion