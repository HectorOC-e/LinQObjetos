using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinQObjetos
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ControlEmpresasEmpleados control = new ControlEmpresasEmpleados();

            //control.GetCeo();
            //control.GetEmpleadoSalarioMayorQue(15000);
            //control.GetEmpleadosOrdenados();
            //control.GetEmpleadosPildoras();
            try
            {
                Console.WriteLine("Ingrese el id de la empresa: ");
                int idEmpresa;
                idEmpresa = Convert.ToInt32(Console.ReadLine());

                control.GetEmpleadosEmpresa(idEmpresa);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }

    class ControlEmpresasEmpleados
    {
        public ControlEmpresasEmpleados() 
        {
            listaEmpresas = new List<Empresa>
            {
                new Empresa { Id = 1, Nombre = "Google" },
                new Empresa { Id = 2, Nombre = "Pildoras Informaticas"}
            };
            listaEmpleados = new List<Empleado> 
            {
                new Empleado { Id = 1, Nombre = "Sergey Brin", Cargo = "CEO", EmpresaId = 1, Salario = 15000},
                new Empleado { Id = 2, Nombre = "Juan Diaz", Cargo = "CEO", EmpresaId = 2, Salario = 18000},
                new Empleado { Id = 3, Nombre = "Larry Page", Cargo = "Gerente", EmpresaId = 1, Salario = 15001},
                new Empleado { Id = 4, Nombre = "Irina Shayk", Cargo = "Gerente", EmpresaId = 2, Salario = 16000}
            };  

        }

        public void GetCeo()
        {
            IEnumerable<Empleado> Ceos = from empleado in listaEmpleados where empleado.Cargo == "CEO" select empleado;

            foreach (Empleado empleado1 in Ceos)
            {
                empleado1.GetDatosEmpleado();
            }
        }

        public void GetEmpleadoSalarioMayorQue(double salario)
        {
            IEnumerable<Empleado> EmpleadosSalario = from empleado in listaEmpleados where empleado.Salario > salario select empleado;
            foreach (Empleado empleado2 in EmpleadosSalario)
            {
                empleado2.GetDatosEmpleado();
            }
        }

        public void GetEmpleadosOrdenados()
        {
            IEnumerable<Empleado> empleados = from empleado in listaEmpleados orderby empleado.Nombre descending select empleado;

            foreach (Empleado item in empleados)
            {
                item.GetDatosEmpleado();
            }

        }

        public void GetEmpleadosEmpresa(int idEmpresa)
        {
            IEnumerable<Empleado> empleadosPil = from empleado in listaEmpleados join empresa in listaEmpresas on
                                              empleado.EmpresaId equals empresa.Id
                                              where empresa.Id == idEmpresa select empleado;
            foreach (Empleado item in empleadosPil)
            {
                item.GetDatosEmpleado();
            }
        }

        public List<Empresa> listaEmpresas;
        public List<Empleado> listaEmpleados;
   
    }

    class Empresa
    {
        public int Id { get; set; }
        public string Nombre { get; set ; }

        public void GetDatosEmpresa() => Console.WriteLine("Empresa {0} con Id: {1}", Nombre, Id);
    }

    class Empleado
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Cargo { get; set; }
        public double Salario { get; set; }
        //Clave Foranea
        public int EmpresaId { get; set; }
        public void GetDatosEmpleado() => Console.WriteLine("Empleado {0} con Id: {1}\nCargo: {2}" +
            "\nSalario: {3}\nPertenece a la empresa: {4}", Nombre, Id, Cargo, Salario, EmpresaId);
    }
}
