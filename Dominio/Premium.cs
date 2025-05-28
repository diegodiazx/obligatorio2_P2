using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Premium : Cliente
    {
        private int _puntos;
        
        public Premium(
            string correo,
            string contra,
            string documento,
            string nombre,
            string nacionalidad
            ) : base(correo, contra, documento, nombre, nacionalidad)
        {
            this._puntos = 0;
        }

        public override double CalcularTarifaEquipaje(TipoEquipaje equipaje)
        {
            double tarifa = 0;
            if (equipaje == TipoEquipaje.BODEGA)
            {
                tarifa = 0.05;
            }
            return tarifa;
        }

        public override string ToString()
        {
            return base.ToString() +
                $"Puntos: {this._puntos}\n";
        }
    }
}
