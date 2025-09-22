using System;
using System.Collections.Generic;
using UnityEngine;
using HeroCombat.Datos;

[Serializable]
public class PlayerData
{
    public string jugadorID = "JugadorTest";
    public int nivel = 1;
    public int experiencia = 0;
    public bool mejoraPendiente = false;

    // Stats base (se mantienen entre combates y subidas de nivel)
    public int vidaMaxima = 30;
    public int fuerzaBase = 2;
    public int velocidadBase = 2;
    public int inteligenciaBase = 2;
    public int destrezaBase = 2;
    public int resistenciaBase = 2;

    // Stats temporales (solo para combate)
    [NonSerialized] public int vida;
    [NonSerialized] public int fuerza;
    [NonSerialized] public int velocidad;
    [NonSerialized] public int inteligencia;
    [NonSerialized] public int destreza;
    [NonSerialized] public int resistencia;

    public List<Arma> armas = new();
    public List<Hechizo> hechizos = new();
    public List<Pasiva> pasivas = new();
    public List<Mascota> mascotas = new();

    public PlayerData()
    {
        ReiniciarStatsTemporales();
    }

    public PlayerData(string nombre)
    {
        jugadorID = nombre;
        ReiniciarStatsTemporales();
    }

    public PlayerData(PlayerDB db)
    {
        jugadorID = db.Nombre;
        nivel = db.Nivel;
        experiencia = db.Experiencia;
        vidaMaxima = db.Vida;
        fuerzaBase = db.Fuerza;
        velocidadBase = db.Velocidad;
        inteligenciaBase = db.Inteligencia;
        destrezaBase = db.Destreza;
        resistenciaBase = db.Resistencia;
        ReiniciarStatsTemporales();
    }

    // Reinicia stats temporales al inicio de cada combate
    public void ReiniciarStatsTemporales()
    {
        vida = vidaMaxima;
        fuerza = fuerzaBase;
        velocidad = velocidadBase;
        inteligencia = inteligenciaBase;
        destreza = destrezaBase;
        resistencia = resistenciaBase;
    }
    public void MejorarEstadistica(int stat){
        switch(stat)
        {
            case 0: fuerzaBase++; break;
            case 1: velocidadBase++; break;
            case 2: inteligenciaBase++; break;
            case 3: destrezaBase++; break;
            case 4: resistenciaBase++; break;
            case 5: vidaMaxima += 5; break;
        }
    }

    // Ganar experiencia y subir de nivel si es necesario
    public void GanarExperiencia(int cantidad)
    {
        experiencia += cantidad;
        int expPorNivel = 10;
        while (experiencia >= expPorNivel)
        {
            experiencia -= expPorNivel;
            SubirNivel();
        }
    }

    private void SubirNivel()
    {
        nivel++;
        mejoraPendiente = true;
        // Guardamos en DB inmediatamente
        DatabaseManager.Instance.GuardarJugador(ToPlayerDB());
    }

    // Aplica una mejora aleatoria a un stat base
    public void AplicarMejoraAleatoria()
    {
        System.Random rng = new();
        int stat = rng.Next(0, 6);
        switch (stat)
        {
            case 0: fuerzaBase++; break;
            case 1: velocidadBase++; break;
            case 2: inteligenciaBase++; break;
            case 3: destrezaBase++; break;
            case 4: resistenciaBase++; break;
            case 5: vidaMaxima += 5; break;
        }
        mejoraPendiente = false;

        // Guardar inmediatamente en DB
        DatabaseManager.Instance.GuardarJugador(ToPlayerDB());
    }

    // Convertir a DB
    public PlayerDB ToPlayerDB()
    {
        return new PlayerDB
        {
            Nombre = jugadorID,
            Nivel = nivel,
            Experiencia = experiencia,
            Vida = vidaMaxima,
            Fuerza = fuerzaBase,
            Velocidad = velocidadBase,
            Inteligencia = inteligenciaBase,
            Destreza = destrezaBase,
            Resistencia = resistenciaBase
        };
    }

    public void FromPlayerDB(PlayerDB db)
    {
        jugadorID = db.Nombre;
        nivel = db.Nivel;
        experiencia = db.Experiencia;
        vidaMaxima = db.Vida;
        fuerzaBase = db.Fuerza;
        velocidadBase = db.Velocidad;
        inteligenciaBase = db.Inteligencia;
        destrezaBase = db.Destreza;
        resistenciaBase = db.Resistencia;
        ReiniciarStatsTemporales();
    }
    // Elegir arma aleatoria
    public Arma ElegirArma(System.Random rng)
    {
        if (armas.Count == 0) return null;
        return armas[rng.Next(armas.Count)];
    }

    // Elegir hechizo aleatorio
    public Hechizo ElegirHechizo(System.Random rng)
    {
        if (hechizos.Count == 0) return null;
        return hechizos[rng.Next(hechizos.Count)];
    }

    // Ataque principal (decide entre arma o hechizo)
    public string Atacar(PlayerData objetivo, System.Random rng)
    {
        string log = "";
        bool tieneArma = armas.Count > 0;
        bool tieneHechizo = hechizos.Count > 0;
        bool usarHechizo = tieneHechizo && (!tieneArma || rng.NextDouble() < 0.3);

        log += EjecutarAccionDeAtaque(objetivo, rng, usarHechizo);

        // Posible ataque doble con arma
        if (tieneArma && !usarHechizo && rng.NextDouble() < 0.05)
        {
            log += "¡Ataque doble!\n";
            log += EjecutarAccionDeAtaque(objetivo, rng, usarHechizo: false);
        }

        return log;
    }

    private string EjecutarAccionDeAtaque(PlayerData objetivo, System.Random rng, bool usarHechizo)
    {
        string log = "";
        if (usarHechizo && hechizos.Count > 0)
        {
            var hechizo = ElegirHechizo(rng);
            int daño = inteligencia + hechizo.poderBase + rng.Next(1, 4);
            objetivo.vida -= daño;
            log += $"{jugadorID} lanza {hechizo.nombre} y hace {daño} de daño mágico.\n";
        }
        else if (armas.Count > 0)
        {
            var arma = ElegirArma(rng);
            int daño = fuerza + arma.dañoBase + rng.Next(1, 4);

            // Crítico
            float chanceCritico = arma.tipo == TipoArma.Afilada ? 0.25f : 0.1f;
            if (rng.NextDouble() < chanceCritico)
            {
                daño = (int)(daño * 1.5);
                log += "¡Golpe crítico! ";
            }

            objetivo.vida -= daño;
            log += $"{jugadorID} ataca con {arma.nombre} y hace {daño} de daño.\n";
        }
        else
        {
            int daño = fuerza + rng.Next(1, 4);
            objetivo.vida -= daño;
            log += $"{jugadorID} ataca a puño limpio y hace {daño} de daño.\n";
        }

        return log;
    }

    // Usar hechizo específico
    public int UsarHechizo(PlayerData objetivo, System.Random rng, out string log)
    {
        log = "";
        var hechizo = ElegirHechizo(rng);
        if (hechizo == null)
        {
            log = $"{jugadorID} intenta usar un hechizo, pero no tiene ninguno.\n";
            return 0;
        }

        int daño = inteligencia + hechizo.poderBase + rng.Next(1, 4);
        objetivo.vida -= daño;
        log = $"{jugadorID} lanza {hechizo.nombre} y hace {daño} de daño mágico.\n";
        return daño;
    }

    // Defender
    public bool Defender(System.Random rng, out string log)
    {
        log = "";
        bool bloquea = rng.NextDouble() < 0.2;
        if (bloquea) log = $"{jugadorID} bloquea el ataque.\n";
        return bloquea;
    }

    // Esquivar
    public bool Esquivar(System.Random rng, out string log)
    {
        log = "";
        bool esquiva = rng.NextDouble() < 0.1;
        if (esquiva) log = $"{jugadorID} esquiva el ataque.\n";
        return esquiva;
    }

    // Contraataque
    public bool Contraatacar(PlayerData objetivo, System.Random rng, out string log)
    {
        log = "";
        bool tieneContra = pasivas.Exists(p => p.nombre == "Contraataque");
        if (tieneContra && rng.NextDouble() < 0.3)
        {
            int daño = fuerza / 2 + rng.Next(1, 3);
            objetivo.vida -= daño;
            log = $"{jugadorID} contraataca y hace {daño} de daño.\n";
            return true;
        }
        return false;
    }

    // Ataque de mascotas
    public string AtacarConMascotas(PlayerData enemigo, System.Random rng)
    {
        string log = "";
        foreach (var m in mascotas)
        {
            if (rng.NextDouble() < m.chanceDeAtacar)
            {
                int daño = m.fuerza + rng.Next(1, 4);
                enemigo.vida -= daño;
                log += $"{jugadorID}'s mascota {m.nombre} ataca y hace {daño} de daño.\n";
            }
        }
        return log;
    }
        public void ReiniciarStats() 
    {
        vida = vidaMaxima;
        fuerza = fuerzaBase;
        velocidad = velocidadBase;
        inteligencia = inteligenciaBase;
        destreza = destrezaBase;
        resistencia = resistenciaBase;
    }


}
