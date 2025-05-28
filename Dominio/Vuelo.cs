using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio.Interfaces;

namespace Dominio
{
    public class Vuelo : IValidable
    {
        private string _numero;
        private Ruta _ruta;
        private Avion _avion;
        private List<DiasSemana> _frecuencia;
        private double _costoPorAsiento; 
        public double CostoPorAsiento { get { return _costoPorAsiento; } }

        public string Numero
        {
            get
            {
                return this._numero;
            }
        }
        public List<DiasSemana> Frecuencia
        {
            get 
            {
                return this._frecuencia;
            }
        }

        public Ruta Ruta
        {
            get
            {
                return this._ruta;
            }
        }

        public Vuelo(
            string numero,
            Ruta ruta,
            Avion avion,
            List<DiasSemana> diasSem
            )
        {
            this._numero = numero;
            this._ruta = ruta;
            this._avion = avion;
            this._frecuencia = diasSem;
            CalcularCostoPorAsiento();
        } 

        public void Validar()
        {
            ValidarDistanciaMenor();
            ValidarNumero();
        }

        public double TasasAeroportuarias()
        {
            return this._ruta.CostoOperacionAeropuertos();
        }

        public void CalcularCostoPorAsiento()
        {
            double costoKm = this._avion.CostoPorKm;
            double distancia = this._ruta.Distancia;
            double costoOperacion = this._ruta.CostoOperacionAeropuertos();
            int cantAsientos = this._avion.CantAsientos;
            this._costoPorAsiento = (costoKm*distancia+costoOperacion)/cantAsientos;
        }
       
        private void ValidarNumero()
        {
            if (this._numero.Length >= 3 && this._numero.Length <= 6 && 
                char.IsLetter(this._numero[0]) && char.IsLetter(this._numero[1]))
        {
                for (int i = 2; i < this._numero.Length; i++)
                {
                    if (!(char.IsDigit(this._numero[i])))
                    {
                        throw new Exception("Los ultimos caracteres deben ser numeros.");
                    }
                }
            } else
            {
                throw new Exception("El numero debe comenzar con 2 letras mayusculas y terminar con 1 a 4 numeros.");

            }
        }

        private void ValidarDistanciaMenor()
        {
            if(this._ruta.Distancia > this._avion.Alcance)
            {
                throw new Exception("La distancia no puede ser mayor al alcance del avion.");
            }
        }

        public bool AeropuertoEnRuta(Aeropuerto aeropuerto)
        {
            return ((this._ruta.Salida == aeropuerto) || (this._ruta.Llegada == aeropuerto));
        }

        public override string ToString()
        {
            string resultado = $"Numero: {this._numero}\n" +
                $"Modelo del avion: {this._avion.Modelo}\n" +
                $"Ruta: {this._ruta.Salida.Codigo} - {this._ruta.Llegada.Codigo}\n" +
                $"Frecuencia: ";

            foreach(DiasSemana dia in this._frecuencia)
            {
                resultado += dia + " ";
            };
            return resultado + "\n";
        }
        
        public override bool Equals(object? obj)
        {
            if (obj == null || !(obj is Vuelo)) return false;
            Vuelo nuevo = (Vuelo)obj;
            return (this._numero.Equals(nuevo._numero));
        }
    }
}
