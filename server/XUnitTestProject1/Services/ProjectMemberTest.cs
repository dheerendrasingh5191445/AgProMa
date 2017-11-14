using Moq;
using MyNeo4j.model;
using MyNeo4j.Service;
using System;
using Xunit;

namespace XUnitTestProject1.Services
{
     public class ProjectMemberTest
    {
        [Fact]
        public void GetMemberDetails_should_not_return_null()
        {
            //arrange
            ProjectMember member = new ProjectMember() {id=1};
            var mockRepo = new Mock<IProjectMemberRepository>();
            mockRepo.Setup(m => m.GetMemberDetails(It.IsAny<int>())).Returns(member);
            ProjectMemberService memberService = new ProjectMemberService(mockRepo.Object);

            //act
            var result = memberService.GetMemberDetails(It.IsAny<int>());

            //assert
            Assert.NotNull(result);

        }

        [Fact]
        public void GetMemberDetails_should_return_memberDetails()
        {
            //arrange
            ProjectMember member = new ProjectMember() { id = 1 };
            var mockRepo = new Mock<IProjectMemberRepository>();
            mockRepo.Setup(m => m.GetMemberDetails(It.IsAny<int>())).Returns(member);
            ProjectMemberService memberService = new ProjectMemberService(mockRepo.Object);

            //act
            var result = memberService.GetMemberDetails(It.IsAny<int>());

            //assert
            Assert.IsType<ProjectMember>(result);
            Assert.Equal(member,result);

        }

        [Fact]
        public void ProjectMemberService_addMemberDetails_should_Throw_NullReferenceException()
        {
            //arrange

            ProjectMember member = new ProjectMember() { id = 1 };
            var mockRepo = new Mock<IProjectMemberRepository>();
            mockRepo.Setup(m => m.Add_MemberDetails(member)).Throws(new NullReferenceException());
            ProjectMemberService memberService = new ProjectMemberService(mockRepo.Object);
            //act
            var ex = Record.Exception(() => memberService.Add_MemberDetails(member));
            //assert
            Assert.IsType<NullReferenceException>(ex);

        }
        [Fact]
        public void ProjectMemberService_addMemberDetails_should_Throw_Format_Exception()
        {
            //arrange
            ProjectMember member = new ProjectMember();
            member.id = 1;
            member.MemberId = 1;
            member.ProjectId = 1;
            var mockRepo = new Mock<IProjectMemberRepository>();
            mockRepo.Setup(m => m.Add_MemberDetails(member)).Throws(new FormatException());
            ProjectMemberService memberService = new ProjectMemberService(mockRepo.Object);
            //act
            var ex = Record.Exception(() => memberService.Add_MemberDetails(member));
            //assert
            Assert.IsType<FormatException>(ex);

        }

    }
}
    