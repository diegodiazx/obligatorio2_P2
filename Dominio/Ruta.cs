using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Ruta
    {
        private int _id;
        private static int s_ultimoId = 0;
        private Aeropuerto _aeroSalida;
        private Aeropuerto _aeroLlegada;
        private double _distancia;

        public Aeropuerto Salida
        {
            get
            {
                return this._aeroSalida;
            }
        }

        public Aeropuerto Llegada
        {
            get
            {
                return this._aeroLlegada;
            }
        }

        public double Distancia
        {
            get
            {
                return this._distancia;
            }
        }

        public Ruta(
            Aeropuerto aeroSalida,
            Aeropuerto aeroLlegada,
            double distancia
            )
        {
            this._id = s_ultimoId++;
            this._aeroSalida = aeroSalida;
            this._aeroLlegada = aeroLlegada;
            this._distancia = distancia;
        }

        public void Validar()
        {
            ValidarDistancia();
        }

        public void ValidarDistancia()
        {
            if (this._distancia < 0)
            {
                throw new Exception("La distancia no puede ser negativa.");
            }
        }

        public double CostoOperacionAeropuertos()
        {
            return this._aeroSalida.CostoOperacion + this._aeroLlegada.CostoOperacion;
        }

    }
}
