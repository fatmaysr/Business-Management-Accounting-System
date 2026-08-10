using System.Collections.Generic;
using EntityLayer.Concrete;

namespace BusinessLayer.Abstract
{
    public interface ISaleService
    {
        void Add(Sale sale);
        List<Sale> GetList();
        Sale GetById(int id);

        // Satışı uçtan uca tamamlar: Sale kaydı yazar + kasaya para girişi yapar
        Sale CompleteSale(string productName, string paymentType, string username);
    }
}