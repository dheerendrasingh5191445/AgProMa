//modules declaration
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';


//component declaration
import { RegisterComponent } from './register/register.component';
import { SignupComponent } from './signup/signup.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { ProjectScreenComponent } from './project-screen/project-screen.component';
import { ProjectDetailComponent } from './project-screen/project-detail/project-detail.component';
import { FillDetailsComponent } from './project-screen/fill-details/fill-details.component';
import { ForgetPasswordComponent } from './forget-password/forget-password.component';
import { RegisterUserWithNewPasswordComponent } from './register-user-with-new-password/register-user-with-new-password.component';
import { InvitePeopleComponent } from "./invite-people/invite-people.component";
import { TeamsComponent } from './teams/teams.component';
import { EpicComponent} from './epic/epic.component';
import { BacklogComponent} from './backlog/backlog.component';
import { ReleasePlanComponent } from './release-plan/release-plan.component';
import { SprintComponent } from './sprint/sprint.component';
import { ChecklistComponent } from './checklist/checklist.component';
import { TaskAddComponent } from './taskadd/taskadd.component';
import { TaskAssignComponent } from './taskAssign/taskAssign.component';
import {LineGraphComponent} from './line-graph/line-graph.component'
import { KanbanBoardComponent } from "./kanban-board/kanban-board.component";
import { EfficiencyGraphComponent } from "./efficiency-graph/efficiency-graph.component";
import { LandingpageComponent } from './landingpage/landingpage.component';
import { DashboardLeaderComponent } from './dashboard-leader/dashboard-leader.component';
import { DashboardMemberComponent } from './dashboard-member/dashboard-member.component';


// paths to all the respective pages
const routes: Routes = [
    { path: "", redirectTo: 'landingpage', pathMatch: "full" },
    { path: 'landingpage', component:LandingpageComponent},
    { path: 'app-signup', component: SignupComponent },
    { path: 'app-register/:id', component: RegisterComponent }, 
    { path: 'app-forget-password', component: ForgetPasswordComponent },
    { path: 'app-register-user-with-new-password/:id', component: RegisterUserWithNewPasswordComponent },
    { path: 'dashboard-leader',component:DashboardLeaderComponent },
    { path: 'dashboard-member',component:DashboardMemberComponent },
    { path: 'app-dashboard', component: DashboardComponent,
    children:[
    { path:'',redirectTo:'project-screen', pathMatch:'full'},
    { path:'project-screen', component:ProjectScreenComponent},
    { path:'app-invite-people/:id', component: InvitePeopleComponent},
    { path:'project-detail', component:ProjectDetailComponent },
    { path:'fill-details/:id',component:FillDetailsComponent},
    { path:'app-teams/:id', component: TeamsComponent },
    { path:'epic/:id',component:EpicComponent},
    { path:'backlog/:id',component:BacklogComponent},
    { path:'releaseplan/:id',component:ReleasePlanComponent},
    { path:'app-sprint/:id',component:SprintComponent},
    { path:'app-checklist/:id',component:ChecklistComponent},
    { path:'taskadd/:id',component:TaskAddComponent},
    { path:'taskassign/:id',component:TaskAssignComponent},
    { path:'line-graph',component:LineGraphComponent},
    { path: 'kanban/:id', component : KanbanBoardComponent},
    { path: 'efficiency', component : EfficiencyGraphComponent}
    ]}
]
@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule],
})
export class AppRoutingModule { }