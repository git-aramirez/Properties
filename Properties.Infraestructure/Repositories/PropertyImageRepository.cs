using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Properties.Domain.DTOs.PropertyImage;
using Properties.Domain.Entities;
using Properties.Domain.IRepositories;


namespace Properties.Domain.Repositories
{
    public class PropertyImageRepository : IPropertyImageRepository
    {
        private readonly PropertiesDbContext _context;
        public PropertyImageRepository(PropertiesDbContext context)
        {
            _context=context;
        }

        public CreatePropertyImageResquest Create(CreatePropertyImageResquest propertyImage)
        {
            try
            {
                var enabledParameter = new SqlParameter("@enabled", propertyImage.Enabled);
                var fileParameter = new SqlParameter("@file", propertyImage.File);
                var propertyIdParameter = new SqlParameter("@propertyId", propertyImage.PropertyId);

                var sqlQuery = @"INSERT INTO PROPERTYIMAGE (enabled, [File], propertyId) 
                                 VALUES (@enabled, @file, @propertyId)";
                _context.Database.ExecuteSqlRaw(sqlQuery, enabledParameter, fileParameter, propertyIdParameter);

                return propertyImage;
            }
            catch (Exception e)
            {
                throw new Exception("Something in the request to create a PropertyImage went wront!"+ e.Message);
            }
        }

        public PropertyImageResponse Get(Guid propertyImageId)
        {
            try
            {
                var propertyImageIdParameter = new SqlParameter("@propertyImageId", propertyImageId);
                var sqlQuery = @"SELECT * FROM PROPERTYIMAGE WHERE propertyImageId=@propertyImageId";
                var result = _context.PropertyImages.FromSqlRaw(sqlQuery, propertyImageIdParameter).FirstOrDefault();

                return new PropertyImageResponse
                {
                    PropertyImageId = result.PropertyImageId,
                    PropertyId = result.PropertyId,
                    File = result.File,
                    Enabled = result.Enabled,
                };
            }
            catch (Exception e)
            {
                throw new Exception("Something in the request to obtain a PropertyImage went wront!"+ e.Message);
            }
        }

        public List<PropertyImageResponse> GetAll()
        {
            try
            {
                var propertyImages = _context.PropertyImages.ToList();

                var propertyImagesResponse = from propertyImage in propertyImages
                                             select new PropertyImageResponse
                                             {
                                                 PropertyImageId = propertyImage.PropertyImageId,
                                                 PropertyId = propertyImage.PropertyId,
                                                 File = propertyImage.File,
                                                 Enabled = propertyImage.Enabled,
                                             };

                return propertyImagesResponse.ToList();
            }
            catch (Exception e)
            {
                throw new Exception("Something in the request to obtain the PropertyImages went wront! "+ e.Message);
            }
        }
    }
}
