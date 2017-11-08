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
       }
}