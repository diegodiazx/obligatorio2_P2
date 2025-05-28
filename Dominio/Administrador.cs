using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio.Interfaces;

namespace Dominio
{
    public class Administrador : Usuario, IValidable
    {
        private string _apodo;

        public Administrador(
            string correo,
            string contra,
            string apodo) : base(correo, contra)
        {
            this._apodo = apodo;
        }

        public void Validar()
        {
            base.Validar();
            ValidarApodo();
        }

        private void ValidarApodo()
        {
            if(string.IsNullOrEmpty(this._apodo))
            {
                throw new Exception("El apodo no puede ser vacio.");
            }
        }


        public override string ToString()
        {
            return base.ToString() +
                $"Apodo: {this._apodo}\n";
        }

    }


}
