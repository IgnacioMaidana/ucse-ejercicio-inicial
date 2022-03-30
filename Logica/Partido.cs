using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica
{
    public class Partido 
    {
        //Crear una clase principal para registrar los datos de un partido determinado
        public List<Arbitro> Arbitros { get; set; }
        //3)
        public string Ciudad { get; private set; }
        //Si, es necesario modificar el metodo creado,
        //porque este no puede asignarle un valor a la propiedad que ahora es privada
        public DateTime InicioPartido { get; set; }
        public Equipo EquipoLocal { get; set; }
        public Equipo EquipoVisitante { get; set; }
        public List<Gol> Goles { get; set; }
        public int TiempoDeJuego {
            //5)
            get
            {
                //6) usar try catch
                int mayor = 0;
                foreach (Gol gol in Goles)
                {
                    if (gol.Jugador.Edad.HasValue)
                    {
                        if (gol.MinutoDeJuego > mayor)
                            mayor = gol.MinutoDeJuego;
                    }
                    
                    
                }
                foreach (Cambio cambio in Cambios)
                {
                    if (cambio.MinutoDeJuego > mayor)
                        mayor = cambio.MinutoDeJuego;
                }
                foreach (Tarjeta tarjeta in Tarjetas)
                {
                    if (tarjeta.MinutoDeJuego > mayor)
                        mayor = tarjeta.MinutoDeJuego;
                }

                return mayor;
            }

        }
        public List<Cambio> Cambios { get; set; }
        public List<Tarjeta> Tarjetas { get; set; }

        public void AgregarIncidencia(Gol gol)
        {
            this.Goles.Add(gol);
        }

        public void AgregarIncidencia(Cambio cambio)
        {
            this.Cambios.Add(cambio);
        }

        public void AgregarIncidencia(Tarjeta tarjeta)
        {
            Tarjeta tarjetaPrevia = ObtenerIncidenciaPrevia(tarjeta);

            if (tarjetaPrevia != null) {
                tarjeta.TarjetaAsociada = tarjetaPrevia;
            }

            this.Tarjetas.Add(tarjeta);
        }

        //Opcion para agregar incidencia
        private Tarjeta ObtenerIncidenciaPrevia(Tarjeta tarjeta)
        {
            return Tarjetas.Find(x => x.JugadorAfectado.Equipo.Nombre == tarjeta.JugadorAfectado.Equipo.Nombre &&
                                                        x.JugadorAfectado.Numero == tarjeta.JugadorAfectado.Numero &
                                                        tarjeta.Color == ColorTarjeta.Amarilla);
        }

        public List<string> ObtenerListadoIncidencias()
        {
            List<string> listadoDescripcionesIncidencias = new List<string>();
            List<Incidencia> incidencias = new List<Incidencia>();

            //Polimorfismo (por abstraccion)
            incidencias.AddRange(Goles);
            incidencias.AddRange(Tarjetas);
            incidencias.AddRange(Cambios);

            incidencias = incidencias.OrderBy(x => x.MinutoDeJuego).ToList(); //expresiones lambda de ordenamiento

            foreach (Incidencia incidencia in incidencias)
            {
                listadoDescripcionesIncidencias.Add(incidencia.ObtenerDescripcionIncidencia());
            }

            return listadoDescripcionesIncidencias;
        }

        public ResumenPartido ObtenerResultadoFinal()
        {
            ResumenPartido resumenPartido = new ResumenPartido();
            resumenPartido.EquipoLocal = this.EquipoLocal;
            resumenPartido.EquipoVisitante = this.EquipoVisitante;
            resumenPartido.GolesLocal = this.Goles.Count(x => !x.EsArcoLocal);
            resumenPartido.GolesVisitante = this.Goles.Count(x => x.EsArcoLocal);

            return resumenPartido;
        }

        //4) Destructor
        ~Partido()
        {
            this.Arbitros.Clear();
            this.Goles.Clear();
            this.Cambios.Clear();
            this.Tarjetas.Clear();
        }
    }
}
