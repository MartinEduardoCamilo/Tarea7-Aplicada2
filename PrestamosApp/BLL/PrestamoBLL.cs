using Microsoft.EntityFrameworkCore;
using PrestamosApp.DAL;
using PrestamosApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace PrestamosApp.BLL
{
    public class PrestamoBLL
    {
        public static bool Guardar(Prestamos prestamo)
        {
            if (!Existe(prestamo.PrestamoId))// si no existe se inserta
                return Insertar(prestamo);
            else
                return Modificar(prestamo);
        }

        private static bool Insertar(Prestamos prestamo)
        {
            bool paso = false;
            Contexto contexto = new Contexto();

            try
            {
                contexto.Prestamos.Add(prestamo);
                //agrega el balance a la persona
                contexto.Personas.Find(prestamo.PersonaId).Balance += prestamo.Balance;
                paso = contexto.SaveChanges() > 0;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return paso;
        }

        public static bool Modificar(Prestamos prestamo)
        {
            bool paso = false;
            Contexto contexto = new Contexto();
            int PersonaAnterior = Buscar(prestamo.PrestamoId).PersonaId;
            decimal BalanceAnterior = Buscar(prestamo.PrestamoId).Balance;
            try
            {

                if (PersonaAnterior != prestamo.PersonaId)
                {
                    contexto.Personas.Find(PersonaAnterior).Balance -= BalanceAnterior;
                    contexto.Personas.Find(prestamo.PersonaId).Balance += prestamo.Balance;
                }
                else
                {
                    contexto.Personas.Find(prestamo.PersonaId).Balance -= BalanceAnterior;
                    contexto.Personas.Find(prestamo.PersonaId).Balance += prestamo.Balance;
                }
                contexto.Entry(prestamo).State = EntityState.Modified;
                paso = contexto.SaveChanges() > 0;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return paso;
        }

        public static bool Eliminar(int id)
        {
            bool paso = false;
            Contexto contexto = new Contexto();

            try
            {
                var aux = contexto.Prestamos.Find(id);
                if (aux != null)
                {
                    contexto.Personas.Find(aux.PersonaId).Balance -= aux.Balance; // se resta el balance a la persona que corresponda
                    contexto.Prestamos.Remove(aux);//remueve la informacion de la entidad relacionada
                    paso = contexto.SaveChanges() > 0;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return paso;
        }

        public static Prestamos Buscar(int id)
        {
            Contexto contexto = new Contexto();
            Prestamos prestamo;

            try
            {
                prestamo = contexto.Prestamos.Find(id);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return prestamo;
        }

        public static List<Prestamos> GetList(Expression<Func<Prestamos, bool>> prestamo)
        {
            List<Prestamos> lista = new List<Prestamos>();
            Contexto db = new Contexto();

            try
            {
                lista = db.Prestamos.Where(prestamo).ToList();
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                db.Dispose();
            }

            return lista;
        }

        public static bool Existe(int id)
        {
            Contexto contexto = new Contexto();
            bool encontrado = false;
            try
            {
                encontrado = contexto.Prestamos.Any(p => p.PrestamoId == id);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return encontrado;
        }

        public static List<Prestamos> Getprestamos()
        {
            List<Prestamos> lista = new List<Prestamos>();
            Contexto contexto = new Contexto();
            try
            {
                lista = contexto.Prestamos.ToList();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return lista;
        }
    }
}
