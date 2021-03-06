﻿using Business.Facede.Models;
using Simple.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Facede.Paramedical.Command
{
    public class UpdateImageUrlCommandHandler : ICommandHandler<Student>
    {

        public void Execute(Student command)
        {
            var db = Database.Open();
            db.PARAMEDICALSTUDENTS.UpdateById(
                ID : command.Id,
                IMAGE_URL : command.ImageUrl
                );
        }
    }
}
