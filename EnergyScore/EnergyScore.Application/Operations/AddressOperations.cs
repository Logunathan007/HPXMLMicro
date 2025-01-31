using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnergyScore.Persistence.DBConnection;

namespace EnergyScore.Application.Operations
{
    public class AddressOperations
    {
        private readonly DbConnect _dbConnect;
        public AddressOperations(DbConnect dbConnect)
        {
            _dbConnect = dbConnect; 
        }

        public void AddAddress(Domain.Entityies.Address address)
        {
            _dbConnect.Address.Add(address);
            _dbConnect.SaveChanges();
        }
    }
}
