

using HairApp.Common.Entities;
using HairApp.Web.Data;
using HairApp.Web.Data.Entities;
using HairApp.Web.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace HairApp.Web.Helpers
{
    public class ConverterHelper : IConverterHelper
    {
        private readonly DataContext _context;
        private readonly ICombosHelper _combosHelper;
        private readonly IUserHelper _userHelper;


        public ConverterHelper(DataContext context, ICombosHelper combosHelper, IUserHelper userHelper)
        {
            _context = context;
            _combosHelper = combosHelper;
            _userHelper = userHelper;

        }

        public async Task<Booking> ToBookingAsync(BookingViewModel model, bool isNew, User user)
        {
            return new Booking
            {
                Id = isNew ? 0 : model.Id,
                Date = model.DateLocal,
                EndDate = model.DateLocal.AddMinutes(model.Service.ServiceTime),
                IdService = model.IdService,
                Service = model.Service,
                Status = model.Status,
                User = user

            };
        }

        public ShopViewModel ToBookingViewModel(Booking booking)
        {
            throw new NotImplementedException();
        }

        public async Task<Shop> ToShopAsync(ShopViewModel model, bool isNew, User user)
        {
            return new Shop
            {                
                Id = isNew ? 0 : model.Id,
                Name = model.Name,
                Description = model.Description,
                IsActive = model.IsActive,
                Addrees = model.Addrees,
                Neighborhood = await _context.Neighborhoods.FindAsync(model.NeighborhoodId),
                ShopImages = model.ShopImages,  
                
            };
        }

        public  ShopViewModel ToShopViewModel(Shop shop, City city, Departament departament)
        {            

            return new ShopViewModel
            {

                Id = shop.Id,
                Name = shop.Name,
                Description = shop.Description,
                IsActive = shop.IsActive,
                Addrees = shop.Addrees,
                Cities = _combosHelper.GetComboCities(departament.Id),
                CityId = city.Id,
                Departaments = _combosHelper.GetComboDepartaments(),
                DepartamentId = departament.Id,
                NeighborhoodId = shop.Neighborhood.Id,
                Neighborhoods = _combosHelper.GetComboNeighborhoods(city.Id),
                ShopImages = shop.ShopImages,
            };
        }



        //public Category ToCategory(CategoryViewModel model, Guid imageId, bool isNew)
        //{
        //    return new Category
        //    {
        //        Id = isNew ? 0 : model.Id,
        //        ImageId = imageId,
        //        Name = model.Name
        //    };
        //}

        //public CategoryViewModel ToCategoryViewModel(Category category)
        //{
        //    return new CategoryViewModel
        //    {
        //        Id = category.Id,
        //        ImageId = category.ImageId,
        //        Name = category.Name
        //    };
        //}


        //public async Task<Product> ToProductAsync(ProductViewModel model, bool isNew)
        //{

        //    return new Product
        //    {
        //        Agendas = model.Agendas,
        //        Histories = model.Histories,
        //        Category = await _context.Categories.FindAsync(model.CategoryId),
        //        Section = await _context.Sections.FindAsync(model.SectionId),
        //        //User = await _context.Users.FindAsync(model.UserId),
        //        Description = model.Description,
        //        Id = isNew ? 0 : model.Id,
        //        IsActive = model.IsActive,
        //        Name = model.Name,
        //        ProductImages = model.ProductImages,
        //        InventoryNumber = model.InventoryNumber,
        //        Serial = model.Serial

        //    };
        //}

        //public ProductViewModel ToProductViewModel(Product product)
        //{
        //    return new ProductViewModel
        //    {
        //        Histories = product.Histories,
        //        Agendas = product.Agendas,
        //        Categories = _combosHelper.GetComboCategories(),
        //        Category = product.Category,
        //        CategoryId = product.Category.Id,
        //        Sections = _combosHelper.GetComboSections(),
        //        Section = product.Section,
        //        SectionId = product.Section.Id,
        //        //Users = _combosHelper.GetComboUsers(),
        //        //User = product.User,
        //        //UserId = product.User.Id,
        //        Description = product.Description,
        //        Id = product.Id,
        //        IsActive = product.IsActive,
        //        Name = product.Name,
        //        ProductImages = product.ProductImages,
        //        InventoryNumber = product.InventoryNumber,
        //        Serial = product.Serial
        //    };
        //}
        //public async Task<Order> ToOrderAsync(OrderViewModel model, bool isNew)
        //{
        //    return new Order
        //    {
        //        Id = isNew ? 0 : model.Id,
        //        Date = DateTime.UtcNow,
        //        User = await _context.Users.FindAsync(model.UserId),
        //        OrderStatus = OrderStatus.Pendiente,
        //        DateSent = DateTime.UtcNow,
        //        Remarks = model.Remarks,

        //    };
        //}

        //public OrderViewModel ToOrderViewModel(Order order)
        //{
        //    return new OrderViewModel
        //    {
        //        Users = _combosHelper.GetComboUsers(),
        //        User = order.User,
        //        UserId = (order.User.Id),
        //        OrderStatus = order.OrderStatus,
        //        Date = order.Date,
        //        Id = order.Id,
        //        DateSent = order.DateSent,
        //        Remarks = order.Remarks

        //    };
        //}

        //public Section ToSection(SectionViewModel model, Guid imageId, bool isNew)
        //{
        //    return new Section
        //    {
        //        Id = isNew ? 0 : model.Id,
        //        ImageId = imageId,
        //        Name = model.Name
        //    };
        //}


        //public SectionViewModel ToSectionViewModel(Section section)
        //{
        //    return new SectionViewModel
        //    {
        //        Id = section.Id,
        //        ImageId = section.ImageId,
        //        Name = section.Name
        //    };
        //}

        //public async Task<OrderDetail> ToOrderDetailAsync(OrderDetailViewModel model, bool isNew)
        //{
        //    return new OrderDetail
        //    {
        //        Id = isNew ? 0 : model.Id,
        //        Order = await _context.Orders.FindAsync(model.OrderId),
        //        Product = await _context.Products.FindAsync(model.ProductId),
        //        Quantity = model.Quantity,
        //        Remarks = model.Remarks
        //    };
        //}

        //public OrderDetailViewModel ToOrderDetailViewModel(OrderDetail orderDetail)
        //{
        //    return new OrderDetailViewModel
        //    {
        //        Id = orderDetail.Id,                
        //        ProductId = orderDetail.Product.Id,
        //        Products = _combosHelper.GetComboProducts(),                                
        //        Quantity = orderDetail.Quantity,
        //        Remarks = orderDetail.Remarks,
        //        Order = orderDetail.Order,
        //        OrderId = orderDetail.Order.Id

        //    };
        //}

        //public async Task<Maintenance> ToMaintenanceAsync(MaintenanceViewModel model, bool isNew, User user)
        //{

        //    return new Maintenance
        //    {
        //        Id = isNew ? 0 : model.Id,
        //        User = user,
        //        Status = model.Status,
        //        UrgencyType = await _context.UrgencyTypes.FindAsync(model.UrgencyTypeId),
        //        Title = model.Title,
        //        Description = model.Description,
        //        Product = await _context.Products.FindAsync(model.ProductId),
        //        Date = DateTime.UtcNow,
        //        ImageId = model.ImageId

        //    };
        //}

        //public MaintenanceViewModel ToMaintenanceViewModel(Maintenance maintenance)
        //{
        //    return new MaintenanceViewModel
        //    {
        //        Id = maintenance.Id,
        //        User = maintenance.User,
        //        Status = maintenance.Status,
        //        UrgencyTypes = _combosHelper.GetComboUrgecyTypes(maintenance.UrgencyType.Id),
        //        UrgencyType = maintenance.UrgencyType,
        //        UrgencyTypeId = maintenance.UrgencyType.Id,
        //        Title = maintenance.Title,
        //        Description = maintenance.Description,
        //        Products = _combosHelper.GetComboProducts(string.Empty),
        //        Product = maintenance.Product,
        //        ProductId = maintenance.Product.Id,
        //        Date = maintenance.Date,
        //        ImageId = maintenance.ImageId

        //    };
        //}

        //public async Task<Agenda> ToAgendaAsync(AddAgendaViewModel model, bool isNew)
        //{
        //    return new Agenda
        //    {
        //        Id = isNew ? 0 : model.Id,
        //        Date = model.Date,
        //        DateEnd = model.DateEnd,
        //        User = await _context.Users.FindAsync(model.UserId),
        //        Product = await _context.Products.FindAsync(model.ProductId),
        //        Remarks = model.Remarks,
        //        IsAvailable = model.IsAvailable,
        //        Assigned = model.Assigned
        //    };
        //}

        //public AddAgendaViewModel ToAgendaViewModel(Agenda agenda)
        //{
        //    return new AddAgendaViewModel
        //    {
        //        Id = agenda.Id,
        //        User = agenda.User,
        //        UserId = agenda.User.Id,
        //        Users = _combosHelper.GetComboUsers(),
        //        Product = agenda.Product,
        //        ProductId = agenda.Product.Id,
        //        Products = _combosHelper.GetComboProducts(string.Empty),
        //        Date = agenda.DateLocal,
        //        DateEnd = agenda.DateEndLocal,
        //        Remarks = agenda.Remarks,
        //        IsAvailable = agenda.IsAvailable,
        //        Assigned = agenda.Assigned
        //    };
        //}
        //public async Task<History> ToHistoryAsync(HistoryViewModel model, bool isNew)
        //{
        //    return new History
        //    {
        //        Date = model.Date.ToUniversalTime(),
        //        Description = model.Description,
        //        Id = isNew ? 0 : model.Id,
        //        Product = await _context.Products.FindAsync(model.ProductId),
        //        Remarks = model.Remarks,
        //        ServiceType = await _context.ServiceTypes.FindAsync(model.ServiceTypeId),
        //        MadeBy = model.MadeBy

        //    };
        //}

        //public HistoryViewModel ToHistoryViewModel(History history)
        //{
        //    return new HistoryViewModel
        //    {
        //        Date = history.DateLocal,
        //        Description = history.Description,
        //        Id = history.Id,
        //        ProductId = history.Product.Id,
        //        Remarks = history.Remarks,
        //        ServiceTypeId = history.ServiceType.Id,
        //        ServiceTypes = _combosHelper.GetComboServiceTypes(),               
        //        MadeBy = history.MadeBy
        //    };
        //}



    }

}
