using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio.Interfaces;

namespace Dominio
{
    public abstract class Usuario : IValidable
    {
        protected string _correo;
        protected string _contra; 

        public Usuario(
            string correo,
            string contra)
        {
            this._correo = correo;
            this._contra = contra;
        }

        public void Validar()
        {
            ValidarContra();
            ValidarCorreo();
        }

        private void ValidarCorreo()
        {
            if (this._correo.Contains(" ") || !(this._correo.Contains("@")))
            {
                throw new Exception("Email invalido, chequear que no sea vacio, que no tenga espacios y que contenta el caracter '@'");
            }
        }

        private void ValidarContra()
        {
            bool hayMayus = false;
            bool hayMin = false;
            bool hayNum = false;
            if (this._contra.Length < 8)
            {
                throw new Exception("La contraseña debe tener un minimo de 8 caracteres.");
            }
            foreach (char caracter in this._contra)
            {
                if (caracter >= 'A' && caracter <= 'Z')
                {
                    hayMayus = true;
                }
                if (caracter >= 'a' && caracter <= 'z')
                {
                    hayMin = true;
                }
                if (char.IsDigit(caracter))
                {
                    hayNum = true;
                }
            }
            if (!hayMayus)
            {
                throw new Exception("La contraseña debe tener al menos una mayuscula.");
            }
            if (!hayMin)
            {
                throw new Exception("La contraseña debe tener al menos una minuscula.");
            }
            if (!hayNum)
            {
                throw new Exception("La contraseña debe tener al menos un numero.");
            }
        }

        public override string ToString()
        {
            return $"Correo: {this._correo}\n";
        }

        public override bool Equals(object? obj)
        {
            if (obj == null || !(obj is Usuario)) return false;
            Usuario nuevo = (Usuario)obj;
            return (this._correo.Equals(nuevo._correo));
        }
    }
}
