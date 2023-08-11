using EFthenIncludeDemo.Contexts;
using EFthenIncludeDemo.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EFthenIncludeDemo
{
    internal class Program
    {
        static Repo<Ogrenci> _repo = new Repo<Ogrenci>();

        static void Main(string[] args)
        {
            var query = _repo.Query(ogrenci => ogrenci.Bolum);
            var list = query.ToList();
            foreach (var item in list)
                Console.WriteLine("Bölüm: " + item.Bolum.Adi + ", ÖgrenciDers Sayısı: " + item.OgrenciDers.Count +
                    ", Ders Sayısı: " + item.OgrenciDers.Select(ogrenciDers => ogrenciDers.Ders).Count());

            Console.WriteLine();

            query = _repo.Query(ogrenci => ogrenci.Bolum, ogrenci => ogrenci.OgrenciDers);
            list = query.ToList();
            foreach (var item in list)
                Console.WriteLine("Bölüm: " + item.Bolum.Adi + ", ÖgrenciDers Sayısı: " + item.OgrenciDers.Count +
                    ", Ders Sayısı: " + item.OgrenciDers.Select(ogrenciDers => ogrenciDers.Ders).Count());
        }
    }

    public class Repo<TEntity> where TEntity : class, new()
    {
        KursDbContext _db = new KursDbContext();

        public virtual IQueryable<TEntity> Query(params Expression<Func<TEntity, object>>[] entitiesToInclude)
        {
            var query = _db.Set<TEntity>().AsQueryable();
            foreach (var entityToInclude in entitiesToInclude)
            {
                query = query.Include(entityToInclude);
            }
            return query;
        }
    }
}