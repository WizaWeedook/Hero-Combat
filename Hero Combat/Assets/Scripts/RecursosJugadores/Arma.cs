using System;

namespace HeroCombat.Datos
{
    public enum TipoArma
    {
        Afilada,
        Larga,
        Rapida,
        Pesada,
        Arrojadiza
    }

    [Serializable]
    public class Arma
    {
        public string nombre;
        public string rareza;
        public int dañoBase;
        public TipoArma tipo;
        public float velocidadGolpe;      // Ej: 1.0 = normal, <1.0 más rápido, >1.0 más lento
        public float chanceAgarrada;      // % de chance de ser agarrada/desarmada
        public float alcance;             // Ej: 1 = corto, 2 = medio, 3 = largo

        public Arma(string nombre, string rareza, int dañoBase, TipoArma tipo, float velocidadGolpe, float chanceAgarrada, float alcance)
        {
            this.nombre = nombre;
            this.rareza = rareza;
            this.dañoBase = dañoBase;
            this.tipo = tipo;
            this.velocidadGolpe = velocidadGolpe;
            this.chanceAgarrada = chanceAgarrada;
            this.alcance = alcance;
        }
    }
    public class BuffosArma
    {
        public float chanceBloqueo;
        public float chanceCritico;
        public float chancePunteria;
        public float chanceReflejar;
        public float chanceDesarmar;
        public float chanceDevolverGolpe;
        public float chanceVolverAtacar;
        public float chanceEsquivar;
        public float chanceEstunear;
    }
}

