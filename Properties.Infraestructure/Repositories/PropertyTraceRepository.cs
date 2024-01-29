using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Properties.Domain.DTOs.PropertyTrace;
using Properties.Domain.Entities;
using Properties.Domain.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Properties.Domain.Repositories
{
    public class PropertyTraceRepository : IPropertyTraceRepository
    {
        private readonly PropertiesDbContext _context;

        public PropertyTraceRepository(PropertiesDbContext context)
        {
            _context=context;
        }

        public CreatePropertyTraceRequest Create(CreatePropertyTraceRequest propertyTrace)
        {
            try
            {
                var nameParameter = new SqlParameter("@name", propertyTrace.Name);
                var valueParameter = new SqlParameter("@value", propertyTrace.Value);
                var taxParameter = new SqlParameter("@tax", propertyTrace.Tax);
                var dateSaleParameter = new SqlParameter("@dateSale", propertyTrace.DateSale);
                var propertyIdParameter = new SqlParameter("@propertyId", propertyTrace.PropertyId);

                var sqlQuery = @"INSERT INTO PROPERTYTRACE (name, value, tax, dateSale, propertyId) 
                                 VALUES (@name, @value, @tax, @dateSale, @propertyId)";
                _context.Database.ExecuteSqlRaw(sqlQuery, nameParameter, valueParameter, taxParameter,
                    dateSaleParameter, propertyIdParameter);

                return propertyTrace;
            }
            catch (Exception e)
            {
                throw new Exception("Something in the request to create a propertytrace went wront!"+ e.Message);
            }
        }

        public PropertyTraceResponse Get(Guid propertyTraceId)
        {
            try
            {
                var propertyIdTraceParameter = new SqlParameter("@propertyTraceId", propertyTraceId);
                var sqlQuery = @"SELECT * FROM PROPERTYTRACE WHERE propertyTraceId=@propertyTraceId";
                var result = _context.PropertyTraces.FromSqlRaw(sqlQuery, propertyIdTraceParameter).FirstOrDefault();

                return new PropertyTraceResponse
                {
                    PropertyTraceId = result.PropertyTraceId,
                    PropertyId = result.PropertyId,
                    DateSale = result.DateSale,
                    Name = result.Name,
                    Value = result.Value,
                    Tax = result.Tax
                };
            }
            catch (Exception e)
            {
                throw new Exception("Something in the request to obtain a propertytrace went wront!"+ e.Message);
            }
        }

        public List<PropertyTraceResponse> GetAll()
        {
            try
            {
                var propertyTraces =  _context.PropertyTraces.ToList();

                var propertyTracesResponses = from propertyTrace in propertyTraces
                                              select new PropertyTraceResponse
                                              {
                                                  PropertyTraceId = propertyTrace.PropertyTraceId,
                                                  PropertyId = propertyTrace.PropertyId,
                                                  DateSale = propertyTrace.DateSale,
                                                  Name = propertyTrace.Name,
                                                  Value = propertyTrace.Value,
                                                  Tax = propertyTrace.Tax
                                              };

                return propertyTracesResponses.ToList();
            }
            catch (Exception e)
            {
                throw new Exception("Something in the request to obtain the propertytraces went wront! "+ e.Message);
            }
        }
    }
}
