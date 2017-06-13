using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Learning.Data;
using Learning.Web.Models;
using System.Web.Http.Routing;

namespace Learning.Web.Controllers
{
    public class BaseControllerController : ApiController
    {
        private ILearningRepository _repo;
        private ModelFactory _modelFactory;
        public BaseControllerController(ILearningRepository repo)
        {
            _repo = repo;
        }

        public ILearningRepository TheRepository
        {
            get
            {
                return _repo;
            }
        }

        public ModelFactory TheModelFactory
        {
            get
            {
                if (_modelFactory == null)
                {
                    _modelFactory = new ModelFactory(Request, _repo);
                }
                return _modelFactory;
            }
        }
    }
}
