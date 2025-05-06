using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public abstract class Cliente : Usuario
    {
        protected string _documento;
        protected string _nombre;
        protected string _nacionalidad;

        public string Nombre
        {
            get
            {
                return this._nombre;
            }
        }

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

        public void Validar()
        {
            this.ValidarUsuario();
            this.ValidarDoc();
            this.ValidarNombre();
            this.ValidarNac();
        }

        public void ValidarDoc()
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

        public void ValidarNombre()
        {
            if(string.IsNullOrEmpty(this._nombre))
            {
                throw new Exception("El nombre no puede ser vacio.");
            }
        }

        public void ValidarNac()
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
        /*
        
        public override string ToString()
        {
            string mostrar = $"Nombre: {this._nombre}\n" +
                base.ToString() +
                $"Nacionalidad: {this._nacionalidad}\n";

            foreach(Pasaje p in this._pasajes)
            {
                mostrar += p.ToString() + "\n";
            }
            return mostrar;
        }*/

    }
}
