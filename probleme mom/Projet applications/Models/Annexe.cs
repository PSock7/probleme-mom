namespace Projet_applications
{
    public class Annexe
    {
        public int Id { get; set; }
        public string Nom { get; set; }

        public int Prix { get; set; }

        public double Volume { get; set; }
        
        
        public override string ToString()
        {
            return Nom;
        }
    }
}