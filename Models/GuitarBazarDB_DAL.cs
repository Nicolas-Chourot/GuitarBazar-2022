using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace GuitarBazar.Models
{
    public static class GuitarBazarDB_DAL
    {

        public static List<Guitar> FilteredGuitarList(this GuitarBazarDBEntities DB, int soldFilterChoice, int sellerId)
        {
            IQueryable<Guitar> guitarsFound = null;
            if (sellerId != 0)
                guitarsFound = DB.Guitars.OrderByDescending(g => g.AddDate).Where(g => g.SellerId == sellerId);
            else
                guitarsFound = DB.Guitars.OrderByDescending(g => g.AddDate);
            switch (soldFilterChoice)
            {
                case 1: /* Not sold */
                    return guitarsFound.Where(g => !g.Sold).ToList();
                case 2: /* Sold */
                    return guitarsFound.Where(g => g.Sold).ToList();
                default: return guitarsFound.ToList();
            }
        }
        public static Guitar Add_Guitar(this GuitarBazarDBEntities DB, Guitar guitar)
        {
            DB.Guitars.Add(guitar);
            DB.SaveChanges();
            return guitar;
        }
        public static bool Update_Guitar(this GuitarBazarDBEntities DB, Guitar guitar)
        {
            DB.Entry(guitar).State = EntityState.Modified;
            DB.SaveChanges();
            return true;
        }
        public static bool Remove_Guitar(this GuitarBazarDBEntities DB, int id)
        {
            Guitar guitarToDelete = DB.Guitars.Find(id);
            DB.Guitars.Remove(guitarToDelete);
            DB.SaveChanges();
            return true;
        }

        public static Seller Add_Seller(this GuitarBazarDBEntities DB, Seller seller)
        {
            DB.Sellers.Add(seller);
            DB.SaveChanges();
            return seller;
        }
        public static bool Update_Seller(this GuitarBazarDBEntities DB, Seller seller)
        {
            DB.Entry(seller).State = EntityState.Modified;
            DB.SaveChanges();
            return true;
        }
        public static bool Remove_Seller(this GuitarBazarDBEntities DB, int id)
        {
            Seller sellerToDelete = DB.Sellers.Find(id);
            DB.Sellers.Remove(sellerToDelete);
            DB.SaveChanges();
            return true;
        }
    }
}