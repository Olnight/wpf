using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lr4
{
    //класс, который отвечает за связь с базой данных
    public class DbAppContext : DbContext
    {
        //Поля phone и company олицетворяют таблицы в базе данных
        //тип данных DbSet<T>, который представляет список записей из базы данных
        public DbSet<phone> phone { get; set; }
        public DbSet<company> company { get; set; }
        //Вместо Т указывается название класса, с которым будет ассоциироваться таблица

        //отвечает за настройку подключения к бд
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(
                "Host=localhost;Username=postgres;Password=2305;Database=Telephone_bd");

        }

        //отвечает за настройку моделей (таблиц) из базы данных для их удобного использования в коде
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //один ко многим
            modelBuilder.Entity<phone>().HasOne(p => p.CompanyEntity)
                                        .WithMany(p => p.PhoneEntites);
        }

    }
}
