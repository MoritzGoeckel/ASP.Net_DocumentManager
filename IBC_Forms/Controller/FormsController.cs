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
            return TestData.forms;
        }

        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetForm(int id)
        {
            var form = TestData.forms.FirstOrDefault((p) => p.Id == id);
            if (form == null)
            {
                return NotFound();
            }
            return Ok(form);
        }
    }
}
