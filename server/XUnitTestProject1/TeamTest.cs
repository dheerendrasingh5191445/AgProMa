using Microsoft.Extensions.Logging;
using Moq;
using MyNeo4j.Hubs;
using MyNeo4j.model;
using MyNeo4j.Service;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using System.Dynamic;
using Microsoft.AspNetCore.SignalR;

namespace XUnitTestProject1
{
    class TeamTest
    {
         public void TestForAddTeam()
        {   //Fact
            var mock = new Mock<ILogger<TeamHub>>();
             var mockClients = new Mock<IHubClients<dynamic>>();
            var mockService = new Mock<ITeamService>();
            mockService.Setup(m => m.AddTeam(It.IsAny<TeamMaster>())).Throws(new Exception());

            TeamHub hub = new TeamHub(mockService.Object, mock.Object);
            var result = hub.AddTeam(It.IsAny<TeamMaster>());

           
        }
    }
}
