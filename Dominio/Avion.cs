using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Avion
    {
        private string _fabricante;
        private string _modelo;
        private int _cantAsientos;
        private double _alcance;
        private double _costoPorKm;

        public double Alcance
        {
            get
            {
                return this._alcance;
            }
        }

        public string Modelo
        {
            get
            {
                return this._modelo;
            }
        }

        public double CostoPorKm
        {
            get
            {
                return this._costoPorKm;
            }
        }

        public int CantAsientos
        {
            get
            {
                return this._cantAsientos;
            }
        }

        public Avion(
            string fabricante,
            string modelo,
            int asientos,
            double alcance,
            double costoKm)
        {
            this._fabricante = fabricante;
            this._modelo = modelo;
            this._cantAsientos = asientos;
            this._alcance = alcance;
            this._costoPorKm = costoKm;
        }

        public void Validar()
        {
            this.ValidarFabricante();
            this.ValidarModelo();
            this.ValidarAsiento();
            this.ValidarAlcance();
            this.ValidarCostoKm();
        }

        public void ValidarFabricante()
        {
            if(string.IsNullOrEmpty(this._fabricante))
            {
                throw new Exception("El fabricante no puede ser vacio.");
            }
        }

        public void ValidarModelo()
        {
            if(string.IsNullOrEmpty(this._modelo))
            {
                throw new Exception("El modelo no puede ser vacio.");
            }
        }

        public void ValidarAsiento(){
            
            if(this._cantAsientos < 0)
            {
                throw new Exception("La cantidad de asientos no puede ser negativa.");
            }
        }

        public void ValidarAlcance()
        {
            if (this._alcance < 0)
            {
                throw new Exception("El alcance no puede ser negativa.");
            }
        }

        public void ValidarCostoKm()
        {
            if (this._costoPorKm < 0)
            {
                throw new Exception("El costo por km no puede ser negativo.");
            }
        }
    }



}
