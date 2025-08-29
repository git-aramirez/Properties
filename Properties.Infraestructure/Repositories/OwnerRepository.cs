using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Properties.Domain;
using Properties.Domain.DTOs.Owner;
using Properties.Domain.IRepositories;


namespace Properties.Infraestructure.Repositories
{
    public class OwnerRepository : IOwnerRepository
    {
        private readonly PropertiesDbContext _context;

        public OwnerRepository(PropertiesDbContext context)
        {
            _context =context;
        }

        public OwnerResponse Create(CreateOwnerRequest owner)
        {
            try
            {
                var nameParameter = new SqlParameter("@name", owner.Name);
                var addressParameter = new SqlParameter("@address", owner.Address);
                var birthdayParameter = new SqlParameter("@birthday", owner.Birthday);
                var sqlQuery = @"INSERT INTO OWNER (name, address, birthday) 
                                 OUTPUT INSERTED.OwnerId
                                 VALUES (@name, @address, @birthday)";

                using (SqlConnection connection = new SqlConnection(_context.Database.GetConnectionString()))
                {
                    connection.Open();
                    var command = new SqlCommand(sqlQuery, connection);
                    command.Parameters.Add(nameParameter);
                    command.Parameters.Add(addressParameter);
                    command.Parameters.Add(birthdayParameter);
                    Guid ownerId = (Guid)command.ExecuteScalar();
                    connection.Close();

                    return new OwnerResponse
                    {
                        OwnerId = ownerId,
                        Name = owner.Name,
                        Address = owner.Address,
                        Birthday = owner.Birthday,
                    };
                }
            }
            catch (Exception e)
            {
                throw new Exception("Something in the request to create an owner went wront!"+ e.Message);
            }
        }

        public OwnerResponse Get(Guid ownerId)
        {
            try
            {
                var idOwnerParameter = new SqlParameter("@ownerId", ownerId);
                var sqlQuery = @"SELECT * FROM OWNER WHERE ownerId=@ownerId";
                var result = _context.Owners.FromSqlRaw(sqlQuery, idOwnerParameter).FirstOrDefault();

                return new OwnerResponse {
                     OwnerId = result.OwnerId,
                     Name = result.Name,
                     Address = result.Address,
                     Birthday = result.Birthday,
                };
            }
            catch (Exception e)
            {
                throw new Exception("Something in the request to obtain an owner went wront!"+ e.Message);
            }
        }

        public List<OwnerResponse> GetAll()
        {
            try
            {
                var owners = _context.Owners.Include(owner => owner.Properties)
                    .ThenInclude(property => property.PropertyImages)
                    .Include(owner => owner.Properties)
                    .ThenInclude(property => property.PropertyTraces)
                    .ToList();

                var ownerResult = from owner in owners 
                                  select new OwnerResponse
                                  {
                                      OwnerId = owner.OwnerId,
                                      Name = owner.Name,
                                      Address = owner.Address,
                                      Birthday = owner.Birthday,
                                  };

                return ownerResult.ToList();
            }
            catch (Exception e)
            {
                throw new Exception("Something in the request to obtain the owners went wront! "+ e.Message);
            }
        }
    }
}
