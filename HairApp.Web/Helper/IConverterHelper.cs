
using HairApp.Common.Entities;
using HairApp.Web.Data.Entities;
using HairApp.Web.Models;
using System;
using System.Threading.Tasks;

namespace HairApp.Web.Helpers
{
    public interface IConverterHelper
    {

        Task<Shop> ToShopAsync(ShopViewModel model, bool isNew,User user);

        ShopViewModel ToShopViewModel(Shop shop, City city, Departament departament);

        //Category ToCategory(CategoryViewModel model, Guid imageId, bool isNew);

        //CategoryViewModel ToCategoryViewModel(Category category);

        //Task<Product> ToProductAsync(ProductViewModel model, bool isNew);

        //ProductViewModel ToProductViewModel(Product product);

        //Section ToSection(SectionViewModel model, Guid imageId, bool isNew);

        //SectionViewModel ToSectionViewModel(Section section);

        //Task<Order> ToOrderAsync(OrderViewModel model, bool isNew);

        //OrderViewModel ToOrderViewModel(Order order);

        //Task<OrderDetail> ToOrderDetailAsync(OrderDetailViewModel model, bool isNew);

        //OrderDetailViewModel ToOrderDetailViewModel(OrderDetail orderDetail);

        //Task<Maintenance> ToMaintenanceAsync(MaintenanceViewModel model, bool isNew, User user);

        //MaintenanceViewModel ToMaintenanceViewModel(Maintenance maintenance);

        //Task<Agenda> ToAgendaAsync(AddAgendaViewModel model, bool isNew);

        //AddAgendaViewModel ToAgendaViewModel(Agenda agenda);

        //Task<History> ToHistoryAsync(HistoryViewModel model, bool isNew);

        //HistoryViewModel ToHistoryViewModel(History history);  

    }

}
