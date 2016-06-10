using IBC_Forms.Model;
using IBC_Forms.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.SessionState;

namespace IBC_Forms.Controller
{
    public class FormsController : ApiController, IRequiresSessionState
    {
        [AcceptVerbs("GET", "POST")]
        public IEnumerable<Form> GetAllForms()
        {
            return Database.getInstance().getForms();
        }

        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetForm(int id)
        {
            var form = Database.getInstance().getForms().FirstOrDefault((p) => p.Id == id);
            if (form == null)
            {
                return NotFound();
            }
            return Ok(form);
        }

        [AcceptVerbs("GET", "POST")]
        [Route("api/forms/setFormActive/{id}/{active}")]
        public setFormActiveAwnser setFormActive(int id, int active)
        {
            Database.getInstance().setFormVisible(id, active == 1);
            return new setFormActiveAwnser(id, active);
        }

        [Serializable]
        public class setFormActiveAwnser
        {
            public int Id;
            public int Active;
            public setFormActiveAwnser(int Id, int Active)
            {
                this.Id = Id;
                this.Active = Active;
            }
        }
    }
}
