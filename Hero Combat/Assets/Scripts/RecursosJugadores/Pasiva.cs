using System;

namespace HeroCombat.Datos
{
    [Serializable]
    public abstract class Pasiva {
        public string nombre;
        public abstract string Aplicar(PlayerData self, PlayerData enemigo, Random rng);
    }

}