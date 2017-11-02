using Microsoft.AspNetCore.SignalR;
using MyNeo4j.model;
using MyNeo4j.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyNeo4j.Hubs
{

    public class EpicHub:Hub
    {
        private IEpicServices _service;

        public EpicHub(IEpicServices service)
        {
            _service = service;
        }

        public void SetConnectId(int userid)
        {
            _service.SetConnectId(userid,Context.ConnectionId);
        }

        public IEnumerable<EpicMaster> Get(int id)
        {
            List<EpicMaster> data = _service.GetAll(id);
            return data;

        }

        public void Post(EpicMaster backlog)
        {
            _service.Add(backlog);

        }

        public void put(int id,EpicMaster value)
        {
            _service.Update(id, value);
        }

        public void Delete(int id)
        {
            _service.Delete(id);
        }
    }
}
