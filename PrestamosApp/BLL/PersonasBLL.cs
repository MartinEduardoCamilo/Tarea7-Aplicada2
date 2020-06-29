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
    public class PersonasBLL
    {
        public static bool Guardar(Personas persona)
        {
            if (!Existe(persona.PersonaId))// si no existe se inserta
                return Insertar(persona);
            else
                return Modificar(persona);
        }

        private static bool Insertar(Personas persona)
        {
            bool paso = false;
            Contexto contexto = new Contexto();

            try
            {
                contexto.Personas.Add(persona);
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

        public static bool Modificar(Personas persona)
        {
            bool paso = false;
            Contexto contexto = new Contexto();

            try
            {
                contexto.Entry(persona).State = EntityState.Modified;
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
                var aux = contexto.Personas.Find(id);
                if (aux != null)
                {
                    contexto.Personas.Remove(aux);//remueve la informacion de la entidad relacionada
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

        public static Personas Buscar(int id)
        {
            Contexto contexto = new Contexto();
            Personas persona;

            try
            {
                persona = contexto.Personas.Find(id);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return persona;
        }

        public static List<Personas> GetList(Expression<Func<Personas, bool>> personas)
        {
            List<Personas> lista = new List<Personas>();
            Contexto db = new Contexto();

            try
            {
                lista = db.Personas.Where(personas).ToList();
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
                encontrado = contexto.Personas.Any(p => p.PersonaId == id);
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
    }
}
