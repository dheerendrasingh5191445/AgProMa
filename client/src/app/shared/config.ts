export const ConfigFile =
{
    ProjectMasterUrls: {
        getAllProjectOfEmployee: "http://localhost:52258/api/ProjectMaster/",
        addNewProject: "http://localhost:52258/api/ProjectMaster",
        deleteProject: "http://localhost:52258/api/ProjectMaster/",
        updateProject: "http://localhost:52258/api/ProjectMaster/",
        getProjectName: "http://localhost:52258/api/ProjectMaster/GetProjectName"
    },
    BacklogUrls: {
        connection: 'http://192.168.252.125:8030/backlog'
    },
    SprintUrls: {
        connection: 'http://localhost:52258/sprint'
    },
    KanBanUrls:{
        getTaskUrl : 'http://localhost:52258/api/TaskBacklog/GetAllTaskDetail/'
    },
    TeamUrls:{
        getTeamUrl:'http://localhost:52258/teamhub'
    },
    TaskAssignUrls:{
        connection:'http://localhost:52258/taskbacklog'
    },
    TaskAddUrls:{
        connection:'http://localhost:52258/taskhub'
    },
    ReleasePlanUrls:{
        connection:'http://localhost:52258/releaseplan',
        navigateNewRrelease:'/app-dashboard/newreleasedetail/1'
    },
    ProductBacklog:{
        connection:'http://localhost:52258/backlog'
    },
    DashboardUrls:{
        onLoggedOut:'app-signup'
    },
    DashboardRoleUrls:{
        onLoggedOut:'app-signup/:id'
    },
    EpicUrls:{
        connection:'http://localhost:52258/epichub'
    },
    FillDetailsUrls:{
        backOnPrevious:'/app-dashboard/project-screen',
        dashboardNavigation:'app-dashboard'
    },
    ProjectDetailUrls:{
        navigation:'role-dashboard'
    },
    RegisterUrls:{
        registerNavigationById:'app-register/:id',
        dashboardNavigation:'app-dashboard',
        signupNavigation:'/app-signup',
        signupNavigationById:'/app-signup/:id'
    },
    RegisterUserWithNewPasswordUrls:{
        signupNavigation:'/app-signup'
    }
}