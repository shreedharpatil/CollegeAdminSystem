﻿using Business.Facede.Models;
using Simple.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Facede.Paramedical.Query
{
    public class GetAllBranchesQuery : IQueryAll<IEnumerable<Branch>>
    {

        public IEnumerable<Branch> QueryAll()
        {
            var db = Database.Open();
            IEnumerable<Branch> branches = db.BRANCH.All().Where(db.BRANCH.APPLICATION == "paramedical").ToList<Branch>();
            return branches;
        }
    }
}
