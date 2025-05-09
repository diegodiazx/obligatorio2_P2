using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Aeropuerto
    {
        private string _codigo;
        private string _ciudad;
        private double _costoOperacion;
        private double _costoTasas;

        public string Codigo
        {
            get
            {
                return this._codigo;
            }
        }

        public double CostoOperacion
        {
            get
            {
                return this._costoOperacion;
            }
        }

        public Aeropuerto(
            string codigo,
            string ciudad,
            double costoOperacion,
            double costoTasas)
        {
            this._codigo = codigo;
            this._ciudad = ciudad;
            this._costoOperacion = costoOperacion;
            this._costoTasas = costoTasas;
        }

        public void Validar()
        {
            ValidarCodigo(this._codigo);
            ValidarCiudad();
            ValidarCostoOperacion();
            ValidarCostoTasa();
        }

        public static void ValidarCodigo(string codigo)
        {
            bool esMayus = true;
            foreach(char c in codigo)
            {
                if(c < 65 || c > 90) //ASCII : A = 65 , Z = 90
                {
                    esMayus = false;
                    break;
                }

            }
            if (codigo.Length != 3 || !esMayus )
            {
                throw new Exception("El codigo ingresado debe ser de 3 letras mayusculas.");
            }
        }

        private void ValidarCiudad()
        {
            if (string.IsNullOrEmpty(this._ciudad))
            {
                throw new Exception("La ciudad no puede ser vacia");
            }
        }

        private void ValidarCostoOperacion()
        {
            if(this._costoOperacion < 0)
            {
                throw new Exception("El costo de operacion no puede ser negativo.");
            }
        }
        private void ValidarCostoTasa()
        {
            if (this._costoTasas < 0)
            {
                throw new Exception("El costo de tasas no puede ser negativo.");
            }
        }
        
        public override bool Equals(object? obj)
        {
            if (obj == null || !(obj is Aeropuerto)) return false;
            Aeropuerto nuevo = (Aeropuerto)obj;
            return (this._codigo.Equals(nuevo._codigo));
        }
    }
}
