﻿using Business.Facede;
using Business.Facede.Models;
using Business.Facede.Pharmacy.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace Api.Controllers
{
    public class PharmacyYearController : ApiController
    {
        private readonly IQueryAll<IEnumerable<string>> getYearsQuery;

        public PharmacyYearController()
        {
            this.getYearsQuery = new GetYearsQuery();
        }

        public HttpResponseMessage Get(int id, int year) {
           // var years = this.getYearsQuery.QueryAll();
            var pdf = new GeneratePharmacyFeeReceiptQuery();
            GetStudentFeeDetailsQuery query = new GetStudentFeeDetailsQuery();
            var fees = query.ExecuteQueryWith(id);
            if (fees == null) {
                return Request.CreateResponse(HttpStatusCode.OK, "No Fee receipt found for the requested student. Check student id and try again.");
            }
            var fee = fees.FirstOrDefault(p => p.Year == year);
            if (fee == null) {
                return Request.CreateResponse(HttpStatusCode.OK,"No Fee receipt found for the requested year. Check your inputs and try again.");
            }
            GetStudentQuery squery = new GetStudentQuery();
            var student = squery.ExecuteQueryWith(id.ToString());
            fee.Name = student.Name;
            fee.BranchName = student.BranchName;
            fee.FatherName = student.FatherName;

            var ms = pdf.ExecuteQueryWith(fee);

            HttpResponse Response = HttpContext.Current.Response;
            Response.ContentType = "pdf/application";
            Response.AddHeader("content-disposition", "attachment;filename=FeeReceipt_"+ id +".pdf");
            Response.OutputStream.Write(ms.GetBuffer(), 0, ms.GetBuffer().Length);

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        public HttpResponseMessage Get()
        {
            var years = this.getYearsQuery.QueryAll();
            return Request.CreateResponse(HttpStatusCode.OK, years);
        }
    }
}
