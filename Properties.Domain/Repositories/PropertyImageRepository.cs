using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Properties.Domain.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Properties.Domain.Repositories
{
    public class PropertyImageRepository : IPropertyImageRepository
    {
        private readonly PropertiesDbContext _context;
        public PropertyImageRepository(PropertiesDbContext context)
        {
            _context=context;
        }

        public bool Create(Models.PropertyImage propertyImage)
        {
            try
            {
                var enabledParameter = new SqlParameter("@enabled", propertyImage.Enabled);
                var fileParameter = new SqlParameter("@file", propertyImage.File);
                var propertyIdParameter = new SqlParameter("@propertyId", propertyImage.PropertyId);

                var sqlQuery = @"INSERT INTO PROPERTYIMAGE (enabled, [File], propertyId) 
                                 VALUES (@enabled, @file, @propertyId)";
                _context.Database.ExecuteSqlRaw(sqlQuery, enabledParameter, fileParameter, propertyIdParameter);

                return true;
            }
            catch (Exception e)
            {
                throw new Exception("Something in the request to create a PropertyImage went wront!"+ e.Message);
            }
        }

        public Models.PropertyImage Get(Guid propertyImageId)
        {
            try
            {
                var propertyImageIdParameter = new SqlParameter("@propertyImageId", propertyImageId);
                var sqlQuery = @"SELECT * FROM PROPERTYIMAGE WHERE propertyImageId=@propertyImageId";
                var result = _context.PropertyImages.FromSqlRaw(sqlQuery, propertyImageIdParameter).FirstOrDefault();

                return result;
            }
            catch (Exception e)
            {
                throw new Exception("Something in the request to obtain a PropertyImage went wront!"+ e.Message);
            }
        }

        public List<Models.PropertyImage> GetAll()
        {
            try
            {
                return _context.PropertyImages.ToList();
            }
            catch (Exception e)
            {
                throw new Exception("Something in the request to obtain the PropertyImages went wront! "+ e.Message);
            }
        }
    }
}
