using Multi.Tenant.Domain.OLTP;
using Multi.Tenant.UnitOfWork.Classes;
using Multi.Tenant.UnitOfWork.Interfaces;
using System;
using System.Linq;

namespace Multi.Tenanat.App
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter Enagement id");
            int eng = Convert.ToInt32(Console.ReadLine());
            using (IUnitOfWorkFactory fac = new UnitOfWorkFactory())
            {
                IUnitOfWork uow = fac.GetUnitOfWork(eng);
                IRepository<User> user = uow.GetRepository<User>();
                //User u = new User
                //{
                //    Email = "random",
                //    FirstName = "Random",
                //    LastName = "Random",
                //    CreatedBy = "Random",
                //    CreatedDate = DateTime.Now,
                //    ModifiedBy = "Random",
                //    ModifiedDate = DateTime.Now,
                //};
                //user.Insert(u);
                // uow.Commit();
                var k = user.Query<User>().Select(x => x).ToList();

            }
        }
    }
}
