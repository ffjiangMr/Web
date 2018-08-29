using Source.HIMS.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TomNet.Data.Entity;
using TomNet.Data.Entity.Migrations;

namespace Source.Core.Data
{
    public class CreateDatabaseIfNotExistsWithSeed : CreateDatabaseIfNotExistsWithSeedBase<DefaultDbContext>
    {
        public CreateDatabaseIfNotExistsWithSeed()
        {
            SeedActions.Add(new CreateDatabaseSeedAction());
        }
    }
}
