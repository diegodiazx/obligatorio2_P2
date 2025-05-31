using Dominio.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Ocasional : Cliente
    {
        private bool _elegible;

        public bool Elegible
        {
            get
            {
                return this._elegible;
            }
            set
            {
                this._elegible = value;
            }
        }

        public Ocasional() { }

        public Ocasional(
            string correo,
            string contra,
            string documento,
            string nombre,
            string nacionalidad) : base(correo, contra, documento, nombre, nacionalidad)
        {
            AsignarElegibilidad();
        }

        public override double CalcularTarifaEquipaje(TipoEquipaje equipaje)
        {
            double tarifa = 0;
            if (equipaje == TipoEquipaje.CABINA)
            {
                tarifa = 0.1;
            } else if (equipaje == TipoEquipaje.BODEGA)
            {
                tarifa = 0.2;
            }
            return tarifa;
        }

        public void AsignarElegibilidad()
        {
            Random random = new Random();
            this._elegible = (random.Next(0, 2) == 1);
        }

        public override string ToString()
        {
            return base.ToString() +
                $"Elegible: {this._elegible}\n";
        }
    }
}
