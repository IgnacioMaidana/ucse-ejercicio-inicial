using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica
{
    public class CuerpoTecnico : Persona
    {
        //2) a)
        public bool EsTecnicoPrincipal
        {
            get { return this.EsTecnicoPrincipal; }
            set
            {
                if (value) //no entendi
                    this.EsTecnicoPrincipal = true;
                else
                    this.EsTecnicoPrincipal = false;
            } 
        }
        public Nullable<int> NumeroAyudanteCampo { get; set; }
    }

}
