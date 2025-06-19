using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Dominio.Interfaces;

namespace Dominio
{
    public abstract class Cliente : Usuario, IValidable, IComparable<Cliente>
    {
        protected string _documento;
        protected string _nombre;
        protected string _nacionalidad;

        public string Nombre { get { return this._nombre; } set { this._nombre = value; } }
        public string Documento { get { return this._documento; } set { this._documento = value; } }
        public string Nacionalidad { get { return this._nacionalidad; } set { this._nacionalidad = value; } }

        public Cliente() { }

        public Cliente(
            string correo,
            string contra,
            string documento,
            string nombre,
            string nacionalidad) : base(correo, contra)
        {
            this._documento = documento;
            this._nombre = nombre;
            this._nacionalidad = nacionalidad;
        }

        public abstract double CalcularTarifaEquipaje(TipoEquipaje equipaje);

        //Hacemos override del metodo Validar de Usuario
        public override void Validar()
        {
            base.Validar();
            ValidarDocumento();
            ValidarNombre();
            ValidarNacionalidad();
        }

        private void ValidarDocumento()
        {
            bool noEsNumero = false;

            foreach (char caracter in this._documento)
            {
                if (!char.IsDigit(caracter)) 
                {
                    noEsNumero = true;
                    break;
                }
            }

            if (noEsNumero) 
            {
                throw new Exception("Documento invalido, debe consistir solo de numeros.");
            }
        }

        private void ValidarNombre()
        {
            if(string.IsNullOrEmpty(this._nombre))
            {
                throw new Exception("El nombre no puede ser vacio.");
            }
        }

        private void ValidarNacionalidad()
        {
            bool condicion = false;
            foreach (char caracter in this._nacionalidad)
            {
                if (char.IsDigit(caracter))
                {
                    condicion = true;
                }
            }
            if(string.IsNullOrEmpty(this._nacionalidad) || condicion)
            {
                throw new Exception("No puede incluir numeros o ser vacio.");
            }
        }

        public override string ToString()
        {
            return $"Nombre: {this._nombre}\n" +
                base.ToString() +
                $"Nacionalidad: {this._nacionalidad}\n";
        }

        public int CompareTo(Cliente? other)
        {
            return this._documento.CompareTo(other._documento);
        }
    }
}
