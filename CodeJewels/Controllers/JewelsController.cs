using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CodeJewels.DataLayer;
using CodeJewels.Models;

namespace CodeJewels.Controllers
{
    public class JewelsController : ApiController
    {
        private const int MIN_ACCEPTABLE_RATING = -10;

        public IEnumerable<Jewel> GetAll()
        {
            CodeJewelsContext context = new CodeJewelsContext();
            using (context)
            {
                return context.Jewels.ToList();
            }
        }

        public HttpResponseMessage PostJewel([FromBody] Jewel value)
        {
            CodeJewelsContext context = new CodeJewelsContext();
            using (context)
            {
                context.Jewels.Add(value);
                context.SaveChanges();

                return Request.CreateResponse(HttpStatusCode.Created);
            }
        }

        public IEnumerable<Jewel> GetBySourceCodeAndCategory(string sourceCode, string category)
        {
            CodeJewelsContext context = new CodeJewelsContext();
            using (context)
            {
                return context.Jewels
                    .Where(j => j.SourceCode.Contains(sourceCode) && j.Category == category)
                    .ToList();
            }
        }

        [HttpPost]
        [ActionName("vote")]
        public HttpResponseMessage Vote(int id, int voteValue)
        {
            if(voteValue != 1 && voteValue != -1)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid vote value");
            }

            CodeJewelsContext context = new CodeJewelsContext();
            using (context)
            {
                try
                {
                    var jewel = context.Jewels.Find(id);
                    jewel.Rating += voteValue;

                    if(jewel.Rating < MIN_ACCEPTABLE_RATING)
                    {
                        context.Jewels.Remove(jewel);
                    }

                    context.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.Created);
                }
                catch (Exception ex)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
                }
            }
        }
    }
}
