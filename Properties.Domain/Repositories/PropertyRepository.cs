using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Properties.Domain.IRepositories;
using Properties.Domain.Models;
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

        public bool Create(Property property)
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
                                 VALUES (@name, @address, @price, @codeInternal, @year, @ownerId)";
                _context.Database.ExecuteSqlRaw(sqlQuery, nameParameter, addressParameter, priceParameter,
                    codeInternalParameter, yearParameter, ownerIdParameter);

                return true;
            }
            catch (Exception e)
            {
                throw new Exception("Something in the request to create a property went wront!"+ e.Message);
            }
        }

        public Property Get(Guid propertyId)
        {
            try
            {
                var propertyIdParameter = new SqlParameter("@propertyId", propertyId);
                var sqlQuery = @"SELECT * FROM PROPERTY WHERE propertyId=@propertyId";
                var result = _context.Properties.FromSqlRaw(sqlQuery, propertyIdParameter).FirstOrDefault();
                
                return result;
            }
            catch (Exception e)
            {
                throw new Exception("Something in the request to obtain a property went wront!"+ e.Message);
            }
        }

        public List<Property> GetAll()
        {
            try
            {
                return _context.Properties.Include(property=>property.PropertyImages).
                    Include(property => property.PropertyTraces).ToList();
            }
            catch (Exception e)
            {
                throw new Exception("Something in the request to obtain the properties went wront! "+ e.Message);
            }
        }

        public List<Property> GetAllIntermediateYears(Guid propertyId, int yearLow, int yearHigh)
        {
            try
            {
                var propertyIdParameter = new SqlParameter("@propertyId", propertyId);
                var yearLowParameter = new SqlParameter("@yearLow", yearLow);
                var yearHighParameter = new SqlParameter("@yearHigh", yearHigh);
                var sqlQuery = @"SELECT * FROM PROPERTY WHERE propertyId=@propertyId AND [year] BETWEEN @yearLow AND @yearHigh";
                var result = _context.Properties.FromSqlRaw(sqlQuery, propertyIdParameter, yearLowParameter, yearHighParameter).
                    ToList();

                return result;
            }
            catch (Exception e)
            {
                throw new Exception("Something in the request to obtain a property went wront!"+ e.Message);
            }
        }

        public bool Update(Property property)
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

                return true;
            }
            catch (Exception e)
            {
                throw new Exception("Something in the request to update a property went wront!"+ e.Message);
            }
        }
    }
}
