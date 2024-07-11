using MonsterSlayer;

namespace MonsterSlayer
{
    internal class Nain : Guerrier
    {
        private int _pointsDeBouclier;
        public int Bouclier { get => _pointsDeBouclier; set => _pointsDeBouclier = value; }

        //Héritage de la classe Guerrier
        public Nain(string nom, int pv, int nbDesAttaque, int pointsDeBouclier) : base(nom, pv, nbDesAttaque) {
            pointsDeBouclier = Bouclier;
        }
    }
}