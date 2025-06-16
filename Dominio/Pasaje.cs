using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio.Interfaces;

namespace Dominio
{
    public class Pasaje : IValidable, IComparable<Pasaje>
    {
        private int _id;
        private static int s_ultimoId = 0;
        private Vuelo _vuelo;
        private DateTime _fecha;
        private TipoEquipaje _equipaje;
        private double _precio;
        private Cliente _pasajero;
        private static double s_margenGanancias = 0.25;
        public int Id { get { return _id; } }
        public Vuelo Vuelo { get { return _vuelo; } }
        public TipoEquipaje Equipaje { get { return _equipaje; } }
        public double Precio { get { return _precio; } }
        public Cliente Pasajero { get { return _pasajero; } }
        public DateTime Fecha { get { return this._fecha; } }

        public Pasaje(
            Vuelo vuelo,
            DateTime fecha,
            TipoEquipaje equipaje,
            Cliente pasajero)
        {
            this._id = s_ultimoId++;
            this._vuelo = vuelo;
            this._fecha = fecha;
            this._equipaje = equipaje;
            this._pasajero = pasajero;
            CalcularPrecio();
        }

        public void Validar()
        {
            ValidarFecha();
            ValidarEquipaje();
            //agregue este metodo nuevo, validarequipaje %
            //y validarfecha ahora tiene 2 metodos adentro
        }

        //Todavia no debemos implementar el metodo CalcularPrecio, por eso es 0.
        public void CalcularPrecio()
        {
            this._precio = this._vuelo.CostoPorAsiento * (1 + s_margenGanancias + this._pasajero.CalcularTarifaEquipaje(this._equipaje)) 
                           + this._vuelo.TasasAeroportuarias();
        }

        private void ValidarFecha()
        {
            ValidarFechaValor();
            ValidarFechaFrecuencia();
        }
        private void ValidarFechaValor() //agregue este metodo; para lo mismo que equipaje. el profe puso lo de -1 por la precarga,
            //si no te tira excepcion de una, porque hay pasajes precargados previos a hoy
        // igual no me estaria andando e igual dijo que aunque el opina lo mismo y es lo logico, que sea a futuro, es innecesario
        // porque no esta claro. tambien si validamos que no se puede desde un año para atrás, es innecesario lo de 01/01/0001
        {
            if (this._fecha == DateTime.MinValue || this._fecha <= DateTime.Today.AddYears(-1)){
                throw new Exception("Debe introducir una fecha valida.");
            }
        }

        private void ValidarFechaFrecuencia()
        {
            //Casteamos de DayOfWeek a nuestro enum DiasSemana para obtener el day of week en español
            DiasSemana diaEsp = (DiasSemana)this._fecha.DayOfWeek;
            if (!this._vuelo.Frecuencia.Contains(diaEsp)){
                throw new Exception("La fecha del pasaje no corresponde con la frecuencia del vuelo.");
            }
        }
        private void ValidarEquipaje()
        {
            if (_equipaje == 0)
            {
                throw new Exception("Debe seleccionar un tipo de equipaje");
            }
        }

        public override string ToString()
        {
            return $"Id: {this._id}\n" +
                $"Pasajero: {this._pasajero.Nombre}\n" +
                $"Precio: {this._precio.ToString("0.00")}\n" +
                $"Fecha: {this._fecha.ToShortDateString()}\n" +
                $"Numero de vuelo: {this._vuelo.Numero}\n";
        }

        public int CompareTo(Pasaje other)
        {
            return (_fecha.CompareTo(other.Fecha));
        }
        
    }
}
