using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Properties.Domain.DTOs.Property;
using Properties.Domain.Entities;
using Properties.Domain.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Properties.Domain.Repositories
{
    public class PropertyRepository : IPropertyRepository
    {
        private readonly PropertiesDbContext _context;

        public PropertyRepository(PropertiesDbContext context)
        {
            _context=context;
        }

        public bool ChangePrice(Guid propertyId, decimal price)
        {
            try
            {
                var propertyIdParameter = new SqlParameter("@propertyId", propertyId);
                var priceParameter = new SqlParameter("@price", price);

                var sqlQuery = @"UPDATE PROPERTY SET price=@price WHERE propertyId=@propertyId";
                _context.Database.ExecuteSqlRaw(sqlQuery, propertyIdParameter, priceParameter);

                return true;
            }
            catch (Exception e)
            {
                throw new Exception("Something in the request to update the property's price went wront!"+ e.Message);
            }
        }

        public PropertyResponse Create(CreatePropertyRequest property)
        {
            try
            {
                var nameParameter = new SqlParameter("@name", property.Name);
                var addressParameter = new SqlParameter("@address", property.Address);
                var priceParameter = new SqlParameter("@price", property.Price);
                var codeInternalParameter = new SqlParameter("@codeInternal", property.CodeInternal);
                var yearParameter = new SqlParameter("@year", property.Year);
                var ownerIdParameter = new SqlParameter("@ownerId", property.OwnerId);

                var sqlQuery = @"INSERT INTO PROPERTY (name, address, price, codeInternal, year, ownerId)
                                 OUTPUT INSERTED.PropertyId
                                 VALUES (@name, @address, @price, @codeInternal, @year, @ownerId)";

                
                using (SqlConnection connection = new SqlConnection(_context.Database.GetConnectionString()))
                {
                    connection.Open();
                    var command = new SqlCommand(sqlQuery, connection);
                    command.Parameters.Add(nameParameter);
                    command.Parameters.Add(addressParameter);
                    command.Parameters.Add(priceParameter);
                    command.Parameters.Add(codeInternalParameter);
                    command.Parameters.Add(yearParameter);
                    command.Parameters.Add(ownerIdParameter);
                    Guid propertyId = (Guid)command.ExecuteScalar();
                    connection.Close();

                    return new PropertyResponse
                    {
                        PropertyId = propertyId,
                        OwnerId = property.OwnerId,
                        Name= property.Name,
                        Address = property.Address,
                        Price = property.Price,
                        CodeInternal = property.CodeInternal,
                        Year = property.Year
                    };
                }
            }
            catch (Exception e)
            {
                throw new Exception("Something in the request to create a property went wront!"+ e.Message);
            }
        }

        public PropertyResponse Get(Guid propertyId)
        {
            try
            {
                var propertyIdParameter = new SqlParameter("@propertyId", propertyId);
                var sqlQuery = @"SELECT * FROM PROPERTY WHERE propertyId=@propertyId";
                var result = _context.Properties.FromSqlRaw(sqlQuery, propertyIdParameter).FirstOrDefault();

                return new PropertyResponse
                {
                    PropertyId = result.PropertyId,
                    OwnerId = result.OwnerId,
                    Name= result.Name,
                    Address = result.Address,
                    Price = result.Price,
                    CodeInternal = result.CodeInternal,
                    Year = result.Year
                };
            }
            catch (Exception e)
            {
                throw new Exception("Something in the request to obtain a property went wront!"+ e.Message);
            }
        }

        public List<PropertyResponse> GetAll()
        {
            try
            {
                var properties = _context.Properties.Include(property => property.PropertyImages).
                    Include(property => property.PropertyTraces).ToList();

                var propertiesResponse = from property in properties
                                         select new PropertyResponse
                                         {
                                             PropertyId = property.PropertyId,
                                             OwnerId = property.OwnerId,
                                             Name= property.Name,
                                             Address = property.Address,
                                             Price = property.Price,
                                             CodeInternal = property.CodeInternal,
                                             Year = property.Year
                                         };

                return propertiesResponse.ToList();
            }
            catch (Exception e)
            {
                throw new Exception("Something in the request to obtain the properties went wront! "+ e.Message);
            }
        }

        public List<PropertyResponse> GetAllIntermediatePrices(decimal priceLow, decimal priceHigh)
        {
            try
            {
                var priceLowParameter = new SqlParameter("@priceLow", priceLow);
                var priceHighParameter = new SqlParameter("@priceHigh", priceHigh);
                var sqlQuery = @"SELECT * FROM PROPERTY WHERE [price] BETWEEN @priceLow AND @priceHigh";
                var properties = _context.Properties.FromSqlRaw(sqlQuery, priceLowParameter, priceHighParameter).
                    ToList();

                var propertiesResponse = from property in properties
                                         select new PropertyResponse
                                         {
                                             PropertyId = property.PropertyId,
                                             OwnerId = property.OwnerId,
                                             Name= property.Name,
                                             Address = property.Address,
                                             Price = property.Price,
                                             CodeInternal = property.CodeInternal,
                                             Year = property.Year
                                         };

                return propertiesResponse.ToList();
            }
            catch (Exception e)
            {
                throw new Exception("Something in the request to obtain a property went wront!"+ e.Message);
            }
        }

        public List<PropertyResponse> GetAllIntermediateYears(int yearLow, int yearHigh)
        {
            try
            {
                //var propertyIdParameter = new SqlParameter("@propertyId", propertyId);
                var yearLowParameter = new SqlParameter("@yearLow", yearLow);
                var yearHighParameter = new SqlParameter("@yearHigh", yearHigh);
                var sqlQuery = @"SELECT * FROM PROPERTY WHERE [year] BETWEEN @yearLow AND @yearHigh";
                var properties = _context.Properties.FromSqlRaw(sqlQuery, yearLowParameter, yearHighParameter).
                    ToList();

                var propertiesResponse = from property in properties
                                         select new PropertyResponse
                                         {
                                             PropertyId = property.PropertyId,
                                             OwnerId = property.OwnerId,
                                             Name= property.Name,
                                             Address = property.Address,
                                             Price = property.Price,
                                             CodeInternal = property.CodeInternal,
                                             Year = property.Year
                                         };

                return propertiesResponse.ToList();
            }
            catch (Exception e)
            {
                throw new Exception("Something in the request to obtain a property went wront!"+ e.Message);
            }
        }

        public PropertyResponse Update(UpdatePropertyRequest property)
        {
            try
            {
                var nameParameter = new SqlParameter("@name", property.Name);
                var addressParameter = new SqlParameter("@address", property.Address);
                var priceParameter = new SqlParameter("@price", property.Price);
                var codeInternalParameter = new SqlParameter("@codeInternal", property.CodeInternal);
                var yearParameter = new SqlParameter("@year", property.Year);
                var ownerIdParameter = new SqlParameter("@ownerId", property.OwnerId);
                var propertyIdParameter = new SqlParameter("@propertyId", property.PropertyId);

                var sqlQuery = @"UPDATE PROPERTY SET name=@name, address=@address, price=@price,
                                 codeInternal=@codeInternal, year=@year, ownerId=@ownerId WHERE propertyId=@propertyId";
                _context.Database.ExecuteSqlRaw(sqlQuery, nameParameter, addressParameter, priceParameter,
                    codeInternalParameter, yearParameter, ownerIdParameter, propertyIdParameter);

                return new PropertyResponse
                {
                    PropertyId = property.PropertyId,
                    OwnerId = property.OwnerId,
                    Name= property.Name,
                    Address = property.Address,
                    Price = property.Price,
                    CodeInternal = property.CodeInternal,
                    Year = property.Year
                };
            }
            catch (Exception e)
            {
                throw new Exception("Something in the request to update a property went wront!"+ e.Message);
            }
        }
    }
}
