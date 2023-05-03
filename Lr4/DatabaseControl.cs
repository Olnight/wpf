using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lr4
{
    //выполнять методы для работы с БД
    public static class DatabaseControl
    //static созданный элемент не потребует для своей работы создания экземпляра (new)
    //У класса все входящие в него единицы будут статическими и экземпляр такого класса создать невозможно
    //Все элементы в классе также должны содержать в сигнатуре модификатор static
    {
        //метод получения данных
        //он статический и возвращает список телефонов
        public static List<phone> GetPhonesForView()
        {
            //директива using нужна чтобы объект созданный в скобках существовал до выполненния инструкции в скобках
            //Экземпляр DbAppContext создан в виде переменной, через нее будут производиться все манипуляции с базой данных
            using (DbAppContext ctx = new DbAppContext())
            {
                return ctx.phone.Include(p => p.CompanyEntity).ToList();
                //Include() делаем запрос в бд, чтобы получить список обьектов phone включаая связанные с ним компании
                //нужны List<T>, в DbSet<T>  не сможем робить
            }
        }

        public static List<company> GetCompanyForView()
        {
            using (DbAppContext ctx = new DbAppContext())
            {

                return ctx.company.Include(p => p.PhoneEntites).ToList();
            }
        }
        //добавление данных в таблицу, передан объект добавляемого телефона
        public static void AddPhone(phone phone)
        {
            using (DbAppContext ctx = new DbAppContext())
            {
                ctx.phone.Add(phone);
                ctx.SaveChanges();
                //SaveChanges() обязателен для сохранения изменения бд
            }
        }
        public static void DelPhone(phone phone)
        {
            using (DbAppContext ctx = new DbAppContext())
            {
                ctx.phone.Remove(phone);
                ctx.SaveChanges();
            }
        }


        //передается экземпляр класса Phone
        public static void UpdatePhone(phone phone)
        {
            using (DbAppContext ctx = new DbAppContext())
            {
                //Ищем объект phone
                phone _phone = ctx.phone.FirstOrDefault(p => p.id == phone.id);

                if (_phone == null)
                {
                    return;
                }

                _phone.title = phone.title;
                _phone.price = phone.price;
                _phone.companyid = phone.companyid;

                ctx.SaveChanges();
            }
        }
    }
}
