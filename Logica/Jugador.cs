using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica
{
    public class Jugador : Persona
    {
        public Nullable<int> Numero { get; set; }
        public int NroCamiseta { get; set; }
        //2) b)
        public bool EsTitular { 
            get
            {
                return this.EsTitular;
            }
            set
            {
                if (this.NroCamiseta >= 1 && this.NroCamiseta <= 11)                
                    this.EsTitular = true;
                else
                    this.EsTitular = false;
            }
        }
        public Equipo Equipo { get; set; }
        public Posicion Posicion { get; set; }

        public override bool ProximaARetiro()
        {
            return Edad >= (Constantes.EdadRetiro - 2) 
                || Edad <= (Constantes.EdadRetiro + 2);
        }
    }
}
