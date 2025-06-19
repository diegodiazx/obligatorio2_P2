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

        public int Puntos { get { return _puntos; } set { _puntos = value; } }
        
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

        public void ValidarPuntos(int puntos)
        {
            if(puntos < 0)
            {
                throw new Exception("Los puntos deben ser un numero positivo.");
            }
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
